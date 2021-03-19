package ru.mmo.server.network.engine;

import ru.mmo.global.network.engine.NioService;
import ru.mmo.global.network.engine.NioSession;
import ru.mmo.global.network.engine.core.interfaces.IClientFactory;
import ru.mmo.global.network.engine.core.interfaces.IPacketExecutor;
import ru.mmo.global.network.engine.packets.Packet;
import ru.mmo.server.network.clients.MMOClient;
import ru.mmo.server.network.threading.ThreadPoolManager;

/**
 * Фактори для подключения Игровых клиентов.
 * 
 * @author Felixx
 */
public class ServerMMoClientFactory implements IClientFactory<MMOClient>, IPacketExecutor
{
	@Override
	public MMOClient create(NioSession<MMOClient> session, NioService<MMOClient> service, IPacketExecutor ex)
	{
		return new MMOClient(session, service, ex);
	}

	@Override
	public void executePacket(Packet run)
	{
		ThreadPoolManager.getInstance().execute(run);
	}
}