package ru.mmo.server.network.engine.ServerMMo.packets.receivable;

import ru.mmo.server.network.engine.ServerMMo.packets.FromUpdateClientToUpdateServerPacket;
import ru.mmo.server.network.engine.ServerMMo.packets.sendable.packet_505_ack;

public class packet_504_req extends FromUpdateClientToUpdateServerPacket
{

	@Override
	public void readImpl()
	{
		// просто запрос...
	}

	@Override
	public void runImpl()
	{
		sendPacket(new packet_505_ack());
	}

}
