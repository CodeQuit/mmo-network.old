package ru.mmo.global.network.engine.packets;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.global.network.engine.buffer.NioBuffer;

/**
 * Author: Felixx
 */
@SuppressWarnings("rawtypes")
public abstract class Packet<T extends NioClient> implements Runnable
{
	protected final Logger _log = Logger.getLogger(getClass());

	protected NioBuffer _buf;
	protected T _client;

	public void clear()
	{
		_buf = null;
	}

	public void init(T client, NioBuffer buffer)
	{
		_buf = buffer;
		_client = client;
	}

	public T getClient()
	{
		return _client;
	}

	public NioBuffer getBuffer()
	{
		return _buf;
	}

	public String getPacketName()
	{
		return getClass().getSimpleName();
	}

	@SuppressWarnings("unchecked")
	public void sendPacket(ServerPacket<T> packet)
	{
		getClient().sendPacket(packet);
	}

	public boolean debug()
	{
		return false;
	}
}