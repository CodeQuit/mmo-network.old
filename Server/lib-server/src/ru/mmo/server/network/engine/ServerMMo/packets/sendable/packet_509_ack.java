package ru.mmo.server.network.engine.ServerMMo.packets.sendable;

import ru.mmo.server.network.engine.ServerMMo.packets.UpdateServerToUpdateClientPacket;

public class packet_509_ack extends UpdateServerToUpdateClientPacket
{
	private int _status = 0;

	public packet_509_ack(int status)
	{
		_status = status;
	}

	@Override
	public void writeImpl()
	{
		writeH(509);
		writeC(0);
	}
}
