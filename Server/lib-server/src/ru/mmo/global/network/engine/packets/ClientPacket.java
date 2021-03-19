package ru.mmo.global.network.engine.packets;

import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.server.configs.DevelopConfig;

/**
 * Author: Felixx
 */
public abstract class ClientPacket<T extends NioClient> extends Packet<T>
{
	protected final int readD()
	{
		try
		{
			return _buf.getInt();
		}
		catch(Exception e)
		{
			_log.info("Missing D for: " + this + "; [Message]: " + e);
		}
		return 0;
	}

	protected final int readC()
	{
		try
		{
			return _buf.getUnsigned();
		}
		catch(Exception e)
		{
			_log.info("Missing C for: " + this + "; [Message]: " + e);
		}
		return 0;
	}

	protected final int readH()
	{
		try
		{
			return _buf.getUnsignedShort();
		}
		catch(Exception e)
		{
			_log.info("Missing H for: " + this + "; [Message]: " + e);
		}
		return 0;
	}

	protected final double readF()
	{
		try
		{
			return _buf.getDouble();
		}
		catch(Exception e)
		{
			_log.info("Missing F for: " + this + "; [Message]: " + e);
		}
		return 0;
	}

	protected final long readQ()
	{
		try
		{
			return _buf.getLong();
		}
		catch(Exception e)
		{
			_log.info("Missing Q for: " + this + "; [Message]: " + e);
		}
		return 0;
	}

	protected final String readS()
	{
		String res = "";
		Charset charset = Charset.forName(DevelopConfig.DEVELOP_CODEPAGE); // windows-1251
		CharsetDecoder decoder = charset.newDecoder();
		try
		{
			res = _buf.getString(decoder);

			int idx = res.indexOf((char) 0x00);
			if(idx != -1)
			{
				res = res.substring(0, idx);
			}
			return res;
		}
		catch(Exception e)
		{
			_log.info("Missing S for: " + this + "; [Message]: " + e);
			return null;
		}
	}

	protected String readS(int size)
	{
		String res = "";
		Charset charset = Charset.forName(DevelopConfig.DEVELOP_CODEPAGE);
		CharsetDecoder decoder = charset.newDecoder();
		try
		{
			res = _buf.getString(size, decoder);

			int idx = res.indexOf((char) 0x00);
			if(idx != -1)
			{
				res = res.substring(0, idx);
			}
			return res;
		}
		catch(Exception e)
		{
			_log.info("Missing S for: " + this + "; [Message]: " + e);
			return null;
		}
	}

	protected final byte[] readB(byte[] b)
	{
		try
		{
			_buf.get(b);
		}
		catch(Exception e)
		{
			_log.info("Missing byte[] for: " + this + "; [Message]: " + e);
		}
		return b;
	}

	protected final byte[] readB(int length)
	{
		byte[] result = new byte[length];
		try
		{
			_buf.get(result);
		}
		catch(Exception e)
		{
			_log.info("Missing byte[] for: " + this + "; [Message]: " + e);
		}
		return result;
	}

	public final boolean readBoolD()
	{
		boolean b = false;
		try
		{
			b = readD() == 1;
		}
		catch(Exception e)
		{
			_log.info("Missing Bool for: " + this + "; [Message]: " + e);
		}

		return b;
	}

	public final boolean readBoolC()
	{
		boolean b = false;
		try
		{
			b = (_buf.getUnsigned()) == 1;
		}
		catch(Exception e)
		{
			_log.info("Missing Bool for: " + this + "; [Message]: " + e);
		}

		return b;
	}

	public abstract void readImpl();

	public abstract void runImpl();

	public final boolean read()
	{
		try
		{
			readImpl();
			return true;
		}
		catch(Exception e)
		{
			e.printStackTrace();
			return false;
		}
	}

	@Override
	public void run()
	{
		try
		{
			runImpl();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}

	@Override
	public String toString()
	{
		return "[S] " + getPacketName();
	}
}
