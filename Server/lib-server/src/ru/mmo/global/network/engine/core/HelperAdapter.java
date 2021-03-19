package ru.mmo.global.network.engine.core;

import java.io.IOException;
import java.nio.ByteOrder;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.global.network.engine.NioService;
import ru.mmo.global.network.engine.NioSession;
import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.core.interfaces.IClientFactory;
import ru.mmo.global.network.engine.core.interfaces.IPacketExecutor;
import ru.mmo.global.network.engine.core.interfaces.IPacketHandler;
import ru.mmo.global.network.engine.packets.ClientPacket;
import ru.mmo.server.configs.DevelopConfig;

/**
 * Author: Felixx
 */
@SuppressWarnings("unchecked")
public class HelperAdapter<T extends NioClient>
{
	protected final static Logger _log = Logger.getLogger(HelperAdapter.class);

	protected final IClientFactory<T> _clientFactory;
	protected final IPacketExecutor _packetExecutor;
	protected final IPacketHandler<T> _handler;
	private NioService<T> _service;

	public HelperAdapter(IClientFactory<T> clientFactory, IPacketExecutor packetExecutor, IPacketHandler<T> packethandler)
	{
		_clientFactory = clientFactory;
		_packetExecutor = packetExecutor;
		_handler = packethandler;
	}

	public void sessionCreate(NioSession nioSession)
	{
		T client = (T) _clientFactory.create(nioSession, _service, _packetExecutor);
		nioSession.setClient(client);
		client.onStart();
	}

	public void sessionClose(NioSession session, CloseType type)
	{
		T client = (T) session.getClient();

		if(client != null && type == CloseType.NORMAL)
		{
			client.onEnd();
		}

		if(client != null && type == CloseType.FORCE)
		{
			client.onForceEnd(); // /gg
		}
	}

	public void catchException(NioSession session, Throwable cause)
	{
		if(DevelopConfig.NETWORK_DEBUG && !(cause instanceof IOException))
		{
			cause.printStackTrace();
		}

		if(session == null || session.getClient() == null)
		{
			return;
		}

		T client = (T) session.getClient();

		if(client != null)
		{
			client.catchException(cause);
		}
	}

	/**
	 * Метод вызывает при получке даных.
	 * 
	 * @param nioSession
	 * @param buffer
	 */
	public synchronized void receive(NioSession nioSession, NioBuffer buffer)
	{
		if(nioSession == null || nioSession.getClient() == null)
		{
			return;
		}

		T client = (T) nioSession.getClient();

		buffer.order(ByteOrder.LITTLE_ENDIAN);

		if(buffer.position() != 0)
		{
			return;
		}

		try
		{
			ClientPacket<T> packet = _handler.handle(buffer, client);

			if(packet != null)
			{
				packet.init(client, buffer);

				if(DevelopConfig.NETWORK_DEBUG)
				{
					_log.info("receive packet " + packet);
				}

				if(packet.read())
				{
					packet.clear();

					_packetExecutor.executePacket(packet);
				}
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}

	public void setService(NioService<T> service)
	{
		_service = service;
	}
}