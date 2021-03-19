package ru.mmo.server.network.engine.ServerMMo.packets.receivable;

import ru.mmo.server.network.engine.ServerMMo.packets.FromUpdateClientToUpdateServerPacket;
import ru.mmo.server.network.engine.ServerMMo.packets.sendable.packet_507_ack;

public class packet_506_req extends FromUpdateClientToUpdateServerPacket
{
	int i = 0;

	@Override
	public void readImpl()
	{
		readC();
		i = readC();
	}

	@Override
	public void runImpl()
	{
		sendPacket(new packet_507_ack());
	}
}
