package ru.mmo.global.network.engine;

import java.net.InetSocketAddress;
import java.nio.channels.Selector;
import java.nio.channels.SocketChannel;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.List;
import java.util.NoSuchElementException;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.core.Acceptor;
import ru.mmo.global.network.engine.core.CloseType;
import ru.mmo.global.network.engine.core.HelperAdapter;
import ru.mmo.global.network.engine.core.Protocol;
import ru.mmo.global.network.engine.core.interfaces.IAcceptFilter;
import ru.mmo.global.network.engine.core.interfaces.NioServiceListener;

/**
 * @author: Felixx
 */
public abstract class NioService<T extends NioClient>
{
	protected final Logger _log = Logger.getLogger(getClass());
	protected volatile Selector _selector;

	protected final List<NioServiceListener> _listeners;
	protected final HelperAdapter<T> _handler;

	protected final List<NioProcessor<T>> _processorPool = new ArrayList<NioProcessor<T>>();
	private int _currentProcessor = 0;

	protected IAcceptFilter _acceptFilter;
	protected Acceptor _acceptor;
	protected InetSocketAddress _address; // address to bind or connect
	protected final Protocol _protocol;

	protected List<NioSession> _sessionList = new ArrayList<NioSession>();
	private final ArrayDeque<NioSession> _pendingClose;

	protected NioService(HelperAdapter<T> handler, InetSocketAddress address, Protocol protocol, IAcceptFilter acceptFilter, int size)
	{
		_listeners = new ArrayList<NioServiceListener>();
		_pendingClose = new ArrayDeque<NioSession>();
		_handler = handler;
		_handler.setService(this);

		if(acceptFilter == null)
		{
			_acceptFilter = new IAcceptFilter()
			{

				@Override
				public boolean accept(SocketChannel ch)
				{
					return true;
				}
			};
		}
		else
		{
			_acceptFilter = acceptFilter;
		}

		if(protocol == null)
		{
			_protocol = new Protocol();
		}
		else
		{
			_protocol = protocol;
		}

		_address = address;

		for(int i = 0; i <= size; i++)
		{
			_processorPool.add(new NioProcessor<T>(this));
		}

	}

	protected NioService(HelperAdapter<T> handler, InetSocketAddress address, Protocol protocol, int size)
	{
		this(handler, address, protocol, null, size);
	}

	public void init()
	{
		_acceptor = new Acceptor(this);
		_acceptor.start();
	}

	public void fireCatchException(Throwable throwable)
	{
		if(_handler == null)
		{
			throw new NullPointerException("handler");
		}

		_handler.catchException(null, throwable);
	}

	public void fireSessionCreate(NioSession nioSession)
	{
		if(_handler == null)
		{
			throw new NullPointerException("handler");
		}

		_handler.sessionCreate(nioSession);
		_sessionList.add(nioSession);
	}

	public void fireSessionClose(NioSession nioSession, CloseType type)
	{
		if(_handler == null)
		{
			throw new NullPointerException("handler");
		}

		_handler.sessionClose(nioSession, type);
		_sessionList.remove(nioSession);
	}

	public void fireReceiveBuffer(NioSession nioSession, NioBuffer buffer)
	{
		if(_handler == null)
		{
			throw new NullPointerException("handler");
		}

		_protocol.decode(nioSession, buffer);

		if(buffer.hasRemaining())
		{
			_handler.receive(nioSession, buffer);
		}
	}

	public void fireCatchException(NioSession nioSession, Throwable throwable)
	{
		if(_handler == null)
		{
			throw new NullPointerException("handler");
		}

		_handler.catchException(nioSession, throwable);
	}

	public void fireServiceActivated()
	{
		for(NioServiceListener listener : _listeners)
		{
			if(listener == null)
			{
				throw new NullPointerException();
			}
			listener.serviceActivated(this);
		}

		init();
	}

	public void fireServiceDeactivated()
	{
		for(NioServiceListener listener : _listeners)
		{
			if(listener == null)
			{
				throw new NullPointerException("listener");
			}
			listener.serviceDeactivated(this);
		}
	}

	public void fireServiceShutdown(boolean vl)
	{
		for(NioServiceListener listener : _listeners)
		{
			if(listener == null)
			{
				throw new NullPointerException("listener");
			}
			listener.serviceShutdown(vl);
		}
	}

	public void addListener(NioServiceListener listener)
	{
		if(listener == null)
		{
			throw new NullPointerException("listener");
		}

		_listeners.add(listener);
	}

	public void removeListener(NioServiceListener listener)
	{
		if( !_listeners.contains(listener))
		{
			throw new NoSuchElementException("listener");
		}

		_listeners.remove(listener);
	}

	public Selector getSelector()
	{
		return _selector;
	}

	public NioProcessor<T> getProcessor()
	{
		if(_currentProcessor == (_processorPool.size() - 1))
		{
			_currentProcessor = 0;
		}
		else
		{
			_currentProcessor++;
		}

		return _processorPool.get(_currentProcessor);
	}

	public Protocol getProtocol()
	{
		return _protocol;
	}

	public List<NioSession> getSessions()
	{
		return _sessionList;
	}

	public ArrayDeque<NioSession> pendingClose()
	{
		return _pendingClose;
	}

	public IAcceptFilter getAcceptFilter()
	{
		return _acceptFilter;
	}

	public void close(NioSession session, CloseType type)
	{
		if(session.isClosing())
		{
			return;
		}

		session.setCloseType(type);
		session.setClosing(true);

		synchronized(_pendingClose)
		{
			_pendingClose.add(session);
		}
	}

	public abstract void shutdown();
}