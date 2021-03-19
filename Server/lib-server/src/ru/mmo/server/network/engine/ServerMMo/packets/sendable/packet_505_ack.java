package ru.mmo.server.network.engine.ServerMMo.packets.sendable;

import ru.mmo.server.network.engine.ServerMMo.packets.UpdateServerToUpdateClientPacket;

public class packet_505_ack extends UpdateServerToUpdateClientPacket
{

	@Override
	public void writeImpl()
	{
		writeH(505);
		writeD(1);

		for(int i = 0; i < 1; i++)
		{
			writeC(0);
			writeC(10);
			writeC(11);
			writeS("PointBlank", 10);
			writeS("Point Blank", 11);
			writeC(1);
			writeC(0);
		}
	}
}
