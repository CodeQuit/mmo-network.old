package ru.mmo.global.network.engine.core.interfaces;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.global.network.engine.NioService;
import ru.mmo.global.network.engine.NioSession;

/**
 * Author: Felixx
 */
public interface IClientFactory<T extends NioClient>
{
	public T create(NioSession<T> session, NioService<T> service, IPacketExecutor ex);
}
