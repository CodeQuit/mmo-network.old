package ru.mmo.global.network.engine.core;

import java.io.IOException;
import java.nio.channels.SelectionKey;
import java.util.Iterator;
import java.util.Set;

import ru.mmo.global.network.engine.NioService;
import ru.mmo.global.network.engine.NioSession;

/**
 * Author: Felixx
 */
public class Acceptor extends Thread
{
	private NioService _service;
	private boolean _shutdown = false;

	public Acceptor(NioService service)
	{
		_service = service;
	}

	@Override
	public void run()
	{
		while( !_shutdown)
		{
			int size = 0;

			try
			{
				size = _service.getSelector().select();
			}
			catch(IOException e)
			{
				_service.fireCatchException(e);
			}

			if(size > 0)
			{
				Set<SelectionKey> keys = _service.getSelector().selectedKeys();
				Iterator<SelectionKey> i = keys.iterator();
				while(i.hasNext())
				{
					SelectionKey key = i.next();
					i.remove();

					if( !key.isValid())
					{
						continue;
					}

					switch(key.readyOps())
					{
						case SelectionKey.OP_CONNECT:
							break;
						case SelectionKey.OP_ACCEPT:
							_service.getProcessor().accept(key);
							break;
						case SelectionKey.OP_READ:
							_service.getProcessor().read(key);
							break;
						case SelectionKey.OP_WRITE:
							_service.getProcessor().write(key);
							break;
						case SelectionKey.OP_READ | SelectionKey.OP_WRITE:
							_service.getProcessor().read(key);
							if(key.isValid())
							{
								_service.getProcessor().write(key);
							}
							break;
					}
				}
			}

			pendingClose();

			try
			{
				Thread.sleep(10);
			}
			catch(InterruptedException e)
			{
				_service.fireCatchException(e);
			}
		}
	}

	public void pendingClose()
	{
		synchronized(_service.pendingClose())
		{
			while( !_service.pendingClose().isEmpty())
			{
				NioSession session = (NioSession) _service.pendingClose().pollFirst();

				_service.fireSessionClose(session, session.getCloseType());
				closeSessionImpl(session);
			}
		}

	}

	public void closeSessionImpl(NioSession session)
	{
		try
		{

			session.clear();
		}
		catch(IOException e)
		{
			// IGNORED
		}
		finally
		{
			session.getSelectionKey().attach(null);
			session.getSelectionKey().cancel();
			_service.getSessions().remove(session);
		}
	}

	public void shutdown()
	{
		_shutdown = true;
	}
}
