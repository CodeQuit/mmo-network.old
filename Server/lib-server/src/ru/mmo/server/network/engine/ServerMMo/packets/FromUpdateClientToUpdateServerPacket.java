package ru.mmo.server.network.engine.ServerMMo.packets;

import ru.mmo.global.network.engine.packets.ClientPacket;
import ru.mmo.server.network.clients.MMOClient;

/**
 * @author: Felixx
 */
public abstract class FromUpdateClientToUpdateServerPacket extends ClientPacket<MMOClient>
{
	@Override
	public String toString()
	{
		return "[UC -> US] " + getPacketName();
	}
}