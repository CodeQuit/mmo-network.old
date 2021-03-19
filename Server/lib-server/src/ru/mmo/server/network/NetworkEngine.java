package ru.mmo.server.network;

import java.net.InetAddress;
import java.net.InetSocketAddress;

import ru.mmo.global.network.engine.NioAcceptor;
import ru.mmo.global.network.engine.core.HelperAdapter;
import ru.mmo.global.network.protocols.MMoProtocol;
import ru.mmo.global.utils.ExitCode;
import ru.mmo.server.configs.NetworkConfig;
import ru.mmo.server.network.clients.MMOClient;
import ru.mmo.server.network.engine.ServerMMoClientFactory;
import ru.mmo.server.network.engine.ServerMMo.ServerMMoClientPacketListenerHandler;

public class NetworkEngine
{
	private static NetworkEngine _instance;

	private NioAcceptor<MMOClient> _authClientListener;

	private static final String update = "Открыт порт для подключения клиентов MMo-Network: ";

	public static NetworkEngine getInstance()
	{
		if(_instance == null)
			_instance = new NetworkEngine();
		return _instance;
	}

	NetworkEngine()
	{

		try
		{
			InetSocketAddress address;
			if(NetworkConfig.SERVER_LISTENER_HOST.equals("*"))
			{
				address = new InetSocketAddress(NetworkConfig.SERVER_LISTENER_PORT);
			}
			else
			{
				address = new InetSocketAddress(InetAddress.getByName(NetworkConfig.SERVER_LISTENER_HOST), NetworkConfig.SERVER_LISTENER_PORT);
			}

			ServerMMoClientFactory gcl = new ServerMMoClientFactory();
			ServerMMoClientPacketListenerHandler gp = new ServerMMoClientPacketListenerHandler();
			HelperAdapter<MMOClient> adapter = new HelperAdapter<MMOClient>(gcl, gcl, gp);
			_authClientListener = new NioAcceptor<MMOClient>(adapter, address, new MMoProtocol(), 1);
			_authClientListener.bind(update);
		}
		catch(Exception e)
		{
			e.printStackTrace();
			System.exit(ExitCode.CODE_ERROR.getId());
		}
	}
}