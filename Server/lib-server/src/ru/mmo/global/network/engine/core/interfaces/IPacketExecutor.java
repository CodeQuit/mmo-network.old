package ru.mmo.global.network.engine.core.interfaces;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.global.network.engine.packets.Packet;

/**
 * Author: Felixx
 */
public interface IPacketExecutor<T extends NioClient<?>>
{
	public void executePacket(Packet<T> run);
}
