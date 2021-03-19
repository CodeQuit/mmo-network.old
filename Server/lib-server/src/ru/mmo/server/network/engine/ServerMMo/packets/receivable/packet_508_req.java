package ru.mmo.server.network.engine.ServerMMo.packets.receivable;

import ru.mmo.server.network.engine.ServerMMo.packets.FromUpdateClientToUpdateServerPacket;
import ru.mmo.server.network.engine.ServerMMo.packets.sendable.packet_509_ack;

public class packet_508_req extends FromUpdateClientToUpdateServerPacket
{

	private int gameid;

	public packet_508_req()
	{

	}

	@Override
	public void readImpl()
	{
		gameid = readC();
	}

	@Override
	public void runImpl()
	{
		sendPacket(new packet_509_ack(0));
	}
}
