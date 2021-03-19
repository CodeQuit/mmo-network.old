package ru.mmo.global.network.engine.core.interfaces;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.packets.ClientPacket;

/**
 * Author: Felixx
 */
public interface IPacketHandler<T extends NioClient>
{
	public ClientPacket<T> handle(NioBuffer buf, T client);
}
