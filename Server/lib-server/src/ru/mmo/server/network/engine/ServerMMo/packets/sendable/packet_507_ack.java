package ru.mmo.server.network.engine.ServerMMo.packets.sendable;

import java.util.Date;

import ru.mmo.server.network.engine.ServerMMo.packets.UpdateServerToUpdateClientPacket;

public class packet_507_ack extends UpdateServerToUpdateClientPacket
{
	Date date = new Date();

	public packet_507_ack()
	{

	}

	@SuppressWarnings("deprecation")
	@Override
	public void writeImpl()
	{
		writeH(507);
		writeS("Тестовый сервер Point Blank", 255);
		writeC(date.getDate());
		writeC(date.getMonth());
		writeH(date.getYear());
	}
}
