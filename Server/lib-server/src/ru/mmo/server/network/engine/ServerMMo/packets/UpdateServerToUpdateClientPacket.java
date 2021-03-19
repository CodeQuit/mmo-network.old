package ru.mmo.server.network.engine.ServerMMo.packets;

import ru.mmo.global.network.engine.packets.ServerPacket;
import ru.mmo.server.network.clients.MMOClient;

/**
 * @author: Felixx
 */
public abstract class UpdateServerToUpdateClientPacket extends ServerPacket<MMOClient>
{
	@Override
	public void runImpl()
	{}

	@Override
	public String toString()
	{
		return "[US -> UC] " + getClass().getSimpleName();
	}
}