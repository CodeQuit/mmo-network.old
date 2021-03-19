package ru.mmo.global.network.engine;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.nio.channels.SelectionKey;
import java.nio.channels.Selector;
import java.nio.channels.ServerSocketChannel;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.core.HelperAdapter;
import ru.mmo.global.network.engine.core.Protocol;
import ru.mmo.global.network.engine.core.interfaces.IAcceptFilter;

/**
 * Author: Felixx
 */
public class NioAcceptor<T extends NioClient> extends NioService<T>
{
	private ServerSocketChannel _serverSocket;

	public NioAcceptor(HelperAdapter<T> handler, InetSocketAddress address, Protocol protocol, IAcceptFilter acceptFilter, int size)
	{
		super(handler, address, protocol, acceptFilter, size);
	}

	public NioAcceptor(HelperAdapter<T> handler, InetSocketAddress address, Protocol protocol, int size)
	{
		super(handler, address, protocol, size);
	}

	/**
	 * Биндит хост по определенном адресе
	 */
	public void bind(String msg)
	{
		try
		{
			_selector = Selector.open();

			_serverSocket = ServerSocketChannel.open();
			_serverSocket.configureBlocking(false);
			_serverSocket.socket().bind(_address);
			_serverSocket.register(_selector, SelectionKey.OP_ACCEPT);
			_serverSocket.socket().setReceiveBufferSize(1024 * 128);

			Logger.getLogger(getClass()).info(msg + _address);

			fireServiceActivated();
		}
		catch(IOException e)
		{
			Logger.getLogger(getClass()).info("can't bind address " + _address);

			fireServiceDeactivated();
			fireCatchException(e);
		}

		fireServiceShutdown(false); // это не зависит от условий
	}

	@Override
	public void shutdown()
	{
		try
		{
			fireServiceShutdown(true);

			if(_acceptor != null)
			{
				_acceptor.shutdown();
			}
			if(_serverSocket != null)
			{
				_serverSocket.close();
			}
		}
		catch(IOException e)
		{
			//
		}
	}
}