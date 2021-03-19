package ru.mmo.server.network.engine.ServerMMo.packets.sendable;

import ru.mmo.server.network.engine.ServerMMo.packets.UpdateServerToUpdateClientPacket;

public class packet_502_ack extends UpdateServerToUpdateClientPacket
{
	private long _status;

	public packet_502_ack(long status)
	{
		_status = status;
	}

	@Override
	public void writeImpl()
	{
		writeH(503);
		writeQ(_status); //
		writeS(getClient().getAccount()); //login
	}
}
