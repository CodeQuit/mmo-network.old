package ru.mmo.server.network.engine.ServerMMo.packets.sendable;

import ru.mmo.server.network.engine.ServerMMo.packets.UpdateServerToUpdateClientPacket;

public class INIT extends UpdateServerToUpdateClientPacket
{

	@Override
	public void writeImpl()
	{
		writeH(500);
		writeD(_client.getId()); // ID Клиента
		writeH(_client.getCryptKey()); // Ключ шифрования
		writeH(_client.hashCode());
	}
}
