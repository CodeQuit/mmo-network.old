package ru.mmo.global.network.engine;

import java.io.IOException;
import java.net.InetAddress;
import java.net.Socket;
import java.nio.ByteOrder;
import java.nio.channels.ReadableByteChannel;
import java.nio.channels.SelectionKey;
import java.nio.channels.SocketChannel;
import java.nio.channels.WritableByteChannel;
import java.util.ArrayDeque;
import java.util.Deque;

import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.core.CloseType;

/**
 * Author: Felixx
 */
public class NioSession<T extends NioClient>
{
	protected NioService<T> _service;
	protected final SelectionKey _key;
	protected final SocketChannel _channel;
	protected final Socket _socket;
	protected final ReadableByteChannel _readByteChannel;
	protected final WritableByteChannel _writeByteChannel;
	protected boolean _pendingClose;

	protected CloseType _closeType;
	protected boolean _isClosing;
	protected boolean _isWait;

	public NioBuffer READ_BUFFER;
	public NioBuffer WRITE_BUFFER;

	protected final Deque<NioBuffer> _sendQueue = new ArrayDeque<NioBuffer>();

	protected T _client;

	public NioSession(SocketChannel channel, Socket socket, NioService<T> service) throws Exception
	{
		_channel = channel;
		_socket = socket;
		_readByteChannel = socket.getChannel();
		_writeByteChannel = socket.getChannel();
		_service = service;

		_key = _channel.register(_service.getSelector(), SelectionKey.OP_READ);
		_key.attach(this);

		READ_BUFFER = NioBuffer.allocate(1024 * 128);
		READ_BUFFER.order(ByteOrder.LITTLE_ENDIAN);

		WRITE_BUFFER = NioBuffer.allocate(1);
		WRITE_BUFFER.setAutoExpand(true);
		WRITE_BUFFER.setAutoShrink(true);
		WRITE_BUFFER.flip();
		WRITE_BUFFER.order(ByteOrder.LITTLE_ENDIAN);
	}

	public int read(final NioBuffer buf) throws Exception
	{
		return _readByteChannel.read(buf.buf());
	}

	public int write(final NioBuffer buf) throws Exception
	{
		return _writeByteChannel.write(buf.buf());
	}

	public CloseType getCloseType()
	{
		return _closeType;
	}

	public void setCloseType(CloseType s)
	{
		_closeType = s;
	}

	public boolean isClosing()
	{
		return _isClosing;
	}

	public void setClosing(boolean s)
	{
		_isClosing = s;
	}

	public final void put(NioBuffer buff)
	{
		_service.getProtocol().encode(this, buff);

		if( !buff.hasRemaining())
		{
			buff.flip();
		}

		if(buff.hasRemaining())
		{
			synchronized(_sendQueue)
			{
				if( !isWriteDisabled())
				{
					_sendQueue.addLast(buff);
				}
			}

			enableWriteInterest();
		}
	}

	protected void enableWriteInterest()
	{
		if(_key.isValid())
		{
			_key.interestOps(_key.interestOps() | SelectionKey.OP_WRITE);
			_key.selector().wakeup();
		}
	}

	public NioBuffer getNextMessage()
	{
		return _sendQueue.pollFirst();
	}

	protected final boolean isWriteDisabled()
	{
		return _pendingClose || _isClosing;
	}

	public void clear() throws IOException
	{
		_socket.close();
		_channel.close();
		_readByteChannel.close();
		_writeByteChannel.close();
	}

	public SelectionKey getSelectionKey()
	{
		return _key;
	}

	public Socket getSocket()
	{
		return _socket;
	}

	public void setClient(T client)
	{
		_client = client;
	}

	public T getClient()
	{
		return _client;
	}

	public InetAddress getAddress()
	{
		return _socket.getInetAddress();
	}

	public boolean isWait()
	{
		return _isWait;
	}

	public void setIsWait(boolean b)
	{
		_isWait = b;
	}
}