package ru.mmo.global.network.engine;

import java.net.InetSocketAddress;
import java.nio.channels.SelectionKey;
import java.nio.channels.Selector;
import java.nio.channels.SocketChannel;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.core.HelperAdapter;
import ru.mmo.global.network.engine.core.Protocol;

/**
 * Author: Felixx
 */
public class NioConnector<T extends NioClient> extends NioService<T>
{
	private NioSession<T> _session;

	public NioConnector(HelperAdapter<T> handler, InetSocketAddress address, Protocol protocol, int size)
	{
		super(handler, address, protocol, size);
	}

	public void connect()
	{
		try
		{
			if(_session != null)
			{
				_session.clear();
				_session = null;
			}

			_selector = Selector.open();
			SocketChannel channel = SocketChannel.open(_address);
			channel.configureBlocking(false);
			channel.register(_selector, SelectionKey.OP_CONNECT);

			_session = new NioSession<T>(channel, channel.socket(), this);

			Logger.getLogger(getClass()).info("Подключение с сервером авторитизации успешно: " + _address);

			fireServiceActivated();
			fireSessionCreate(_session);
		}
		catch(Throwable e)
		{
			Logger.getLogger(getClass()).info("Не могу подключиться к сереру авторитизиции по адресу: " + _address);

			fireServiceDeactivated();
			fireCatchException(e);
		}

		fireServiceShutdown(false); // это не зависит от условий
	}

	@Override
	public void shutdown()
	{
		fireServiceShutdown(true);
	}

	public NioSession getSession()
	{
		return _session;
	}
}