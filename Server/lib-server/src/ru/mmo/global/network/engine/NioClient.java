package ru.mmo.global.network.engine;

import java.nio.ByteOrder;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.core.CloseType;
import ru.mmo.global.network.engine.core.emuns.ConnectionState;
import ru.mmo.global.network.engine.core.interfaces.IPacketExecutor;
import ru.mmo.global.network.engine.packets.ServerPacket;
import ru.mmo.server.configs.DevelopConfig;

/**
 * Author: Felixx
 */
@SuppressWarnings({"unchecked", "rawtypes"})
public abstract class NioClient<T extends NioClient>
{
	protected final Logger _log = Logger.getLogger(getClass());

	private ConnectionState _state;
	protected final NioSession<T> _session;
	private final NioService _service;
	protected final IPacketExecutor _packetExecutor;

	public NioClient(NioSession<T> session, NioService service, IPacketExecutor executor)
	{
		_state = ConnectionState.CONNECTED;
		_session = session;
		_service = service;
		_packetExecutor = executor;
	}

	public abstract void onStart();

	public abstract void onEnd();

	public abstract void onForceEnd();

	public abstract void catchException(Throwable cases);

	public String getIPString()
	{
		try
		{
			return _session.getAddress().getHostAddress();
		}
		catch(Exception e)
		{
			return "NOT CONNECTED";
		}
	}

	public synchronized void sendPacket(ServerPacket<T> packet)
	{
		if(packet == null)
		{
			return;
		}

		if(DevelopConfig.NETWORK_DEBUG)
		{
			_log.info("send packet " + packet.toString());
		}

		NioBuffer buff = NioBuffer.allocate(1);
		buff.setAutoExpand(true);
		buff.order(ByteOrder.LITTLE_ENDIAN);

		packet.init((T) this, buff);

		_packetExecutor.executePacket(packet);
	}

	public void writeFromPacket(ServerPacket<T> pa)
	{
		NioBuffer buf = pa.getBuffer();

		pa.write();
		short size = (short) (buf.limit() - 4);
		buf.putShort(0, size);

		if(buf != null)
		{
			buf.flip();
			_session.put(buf);
		}
	}

	public void close(ServerPacket<T> packet)
	{
		sendPacket(packet);
		close(true);
	}

	/**
	 * Параметр isWait значит, что конект не закроется пока WRITE_BUFFER ен будет пуст,
	 * тоисть не отправлены все пакеты которые есть в очериди
	 * 
	 * @param isWait
	 */
	public void close(boolean isWait)
	{
		if(isWait)
		{
			getSession().setIsWait(true);
		}
		else
		{
			_service.close(_session, CloseType.NORMAL);
		}
	}

	public NioSession<T> getSession()
	{
		return _session;
	}

	public String getIP()
	{
		try
		{
			return _session.getAddress().getHostAddress();
		}
		catch(Exception e)
		{
			return "NONE";
		}
	}

	public ConnectionState getState()
	{
		return _state;
	}

	public void setState(ConnectionState s)
	{
		_state = s;
	}

	public int getCryptKey()
	{
		return 0;
	}

	public int getId()
	{
		return 0;
	}
}