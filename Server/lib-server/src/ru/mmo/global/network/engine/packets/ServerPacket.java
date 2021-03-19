package ru.mmo.global.network.engine.packets;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.server.configs.DevelopConfig;

/**
 * Author: Felixx
 */
@SuppressWarnings({"unchecked", "rawtypes"})
public abstract class ServerPacket<T extends NioClient> extends Packet<T>
{
	protected final void writeC(Enum en)
	{
		writeC(en.ordinal());
	}

	protected final void writeD(Enum en)
	{
		writeD(en.ordinal());
	}

	protected final void writeD()
	{
		writeD(0x00);
	}

	protected final void writeH()
	{
		writeH(0x00);
	}

	protected final void writeH(Enum value)
	{
		writeH(value.ordinal());
	}

	protected final void writeC()
	{
		writeC(0x00);
	}

	protected final void writeF()
	{
		writeF(0x00);
	}

	protected final void writeF(float value)
	{
		writeF((double) value);
	}

	protected final void writeQ()
	{
		writeQ(0x00);
	}

	protected final void writeS()
	{
		writeChar('\000');
	}

	protected final void writeS(CharSequence name)
	{
		if(name == null)
		{
			name = "";
		}

		int length = name.length();

		for(int i = 0; i < length; i++)
		{
			writeChar(name.charAt(i));
		}

		writeChar('\000');
	}

	protected final void writeS(String name, int count)
	{
		try
		{
			if(name != null)
			{
				writeB(name.getBytes(DevelopConfig.DEVELOP_CODEPAGE));
				writeB(new byte[count - name.length()]);
			}
		}
		catch(Exception e)
		{
			_log.info("Missing S for: " + this + "; [Message]: " + e);
		}
	}

	protected final void writeBoolC(boolean t)
	{
		writeC(t ? 1 : 0);
	}

	protected final void writeBoolH(boolean t)
	{
		writeH(t ? 1 : 0);
	}

	protected final void writeBoolD(boolean t)
	{
		writeD(t ? 1 : 0);
	}

	protected final void writeB(byte[] data)
	{
		writeB(data, 0, data.length);
	}

	protected final void writeC(int value)
	{
		_buf.put((byte) value);
	}

	protected final void writeH(int value)
	{
		_buf.putShort((short) value);
	}

	protected final void writeChar(char c)
	{
		_buf.putChar(c);
	}

	protected final void writeD(int value)
	{
		_buf.putInt(value);
	}

	protected final void writeQ(long value)
	{
		_buf.putLong(value);
	}

	protected final void writeF(double value)
	{
		_buf.putDouble(value);
	}

	protected final void writeB(byte[] data, int of, int l)
	{
		_buf.put(data, of, l);
	}

	@Override
	public void init(T client, NioBuffer buffer)
	{
		_buf = buffer;
		_client = client;

		_buf.putShort((short) 0);
	}

	public abstract void writeImpl();

	public abstract void runImpl();

	public final void write()
	{
		try
		{
			writeImpl();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}

	@Override
	public final void run()
	{
		boolean error = false;

		try
		{
			runImpl();
		}
		catch(Exception e)
		{
			e.printStackTrace();
			error = true;
		}

		if( !error)
		{
			if(getClient() != null)
			{
				getClient().writeFromPacket(this);
			}
		}
	}
}