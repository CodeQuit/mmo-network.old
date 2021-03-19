package ru.mmo.global.network.engine;

import java.io.IOException;
import java.nio.ByteOrder;
import java.nio.channels.SelectionKey;
import java.nio.channels.ServerSocketChannel;
import java.nio.channels.SocketChannel;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.core.CloseType;

/**
 * Author: Felixx
 */
public class NioProcessor<T extends NioClient>
{
	private Logger _log = Logger.getLogger(NioProcessor.class);
	private NioService<T> _service;

	private static final int HEADER_SIZE = 0;

	public NioProcessor(NioService<T> service)
	{
		_service = service;
	}

	public void accept(SelectionKey key)
	{
		SocketChannel sc;
		try
		{
			while((sc = ((ServerSocketChannel) key.channel()).accept()) != null)
			{
				if(_service.getAcceptFilter().accept(sc))
				{
					sc.configureBlocking(false);
					NioSession<T> nioSession = new NioSession<T>(sc, sc.socket(), _service);

					_service.fireSessionCreate(nioSession);
				}
				else
				{
					sc.socket().close();
				}
			}
		}
		catch(Exception e)
		{
			if(e instanceof IOException)
			{
				return;
			}

			_service.fireCatchException(e);
		}
	}

	public void read(SelectionKey key)
	{
		NioSession nioSession = (NioSession) key.attachment();

		if(nioSession.getSocket().isClosed())
		{
			return;
		}
		try
		{
			int readBytes = 0;

			try
			{
				readBytes = nioSession.read(nioSession.READ_BUFFER);
			}
			finally
			{
				nioSession.READ_BUFFER.flip();
			}

			if(readBytes == -1)
			{
				_service.close(nioSession, CloseType.NORMAL);
			}
			if(readBytes == -2)
			{
				_service.close(nioSession, CloseType.FORCE);
			}

			if(readBytes == 0)
			{
				return;
			}

			while(nioSession.READ_BUFFER.remaining() > HEADER_SIZE + 2 && nioSession.READ_BUFFER.remaining() >= (nioSession.READ_BUFFER.getUnsignedShort(nioSession.READ_BUFFER.position())))
			{
				int size = (nioSession.READ_BUFFER.getUnsignedShort()) - HEADER_SIZE + 2;

				NioBuffer OUT = NioBuffer.allocate(size);
				OUT.order(ByteOrder.LITTLE_ENDIAN);
				OUT.put(nioSession.READ_BUFFER.array(), nioSession.READ_BUFFER.position(), size);
				OUT.flip();
				_log.info("cur pos: " + nioSession.READ_BUFFER.position());
				_log.info("req pos: " + nioSession.READ_BUFFER.position() + size);

				nioSession.READ_BUFFER.position(nioSession.READ_BUFFER.position() + size);

				_service.fireReceiveBuffer(nioSession, OUT);
			}

			if(nioSession.READ_BUFFER.hasRemaining())
			{
				nioSession.READ_BUFFER.compact();
			}
			else
			{
				nioSession.READ_BUFFER.clear();
			}
		}
		catch(Exception e)
		{
			if(e instanceof IOException)
			{
				_service.close(nioSession, CloseType.FORCE);
			}
			_service.fireCatchException(e);
		}
	}

	public void write(SelectionKey key)
	{
		NioSession nioSession = (NioSession) key.attachment();
		NioBuffer buf = nioSession.WRITE_BUFFER;
		int writeBytes = 0;

		try
		{
			if(buf.hasRemaining())
			{
				writeBytes = nioSession.write(buf);

				if(writeBytes == 0)
				{
					return;
				}

				if(buf.hasRemaining())
				{
					return;
				}
			}

			while(true)
			{
				buf.clear();

				NioBuffer buf2 = nioSession.getNextMessage();

				if(buf2 == null)
				{
					buf.limit(0);
					if(nioSession.isWait())
					{
						nioSession.setIsWait(false);
						_service.close(nioSession, CloseType.NORMAL);
					}
					break;
				}

				buf.put(buf2);
				buf.flip();

				writeBytes = nioSession.write(buf);

				if(writeBytes == 0)
				{
					return;
				}

				if(buf.hasRemaining())
				{
					return;
				}
			}

			key.interestOps(key.interestOps() & ~SelectionKey.OP_WRITE);
		}
		catch(Exception e)
		{
			if(e instanceof IOException)
			{
				_service.close(nioSession, CloseType.FORCE);
			}
			_service.fireCatchException(e);
		}
	}
}