package ru.mmo.global.network.utils;

import java.net.InetAddress;
import java.net.UnknownHostException;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.buffer.NioBuffer;

/**
 * @author Felixx
 */
public class NetworkUtil
{
	/**
	 * check if IP address match pattern
	 * *
	 * 
	 * @param pattern
	 *            *.*.*.* , 192.168.1.0-255 , *
	 * @param address
	 *            -
	 *            192.168.1.1<BR>
	 *            <code>address = 10.2.88.12 pattern = *.*.*.* result: true<BR>
	 *            address = 10.2.88.12 pattern = * result: true<BR>
	 *            address = 10.2.88.12 pattern = 10.2.88.12-13 result: true<BR>
	 *            address = 10.2.88.12 pattern = 10.2.88.13-125 result: false<BR></code>
	 * @return true if address match pattern
	 * @author KID, -Nemesiss-
	 */
	static final byte[] HEX_CHAR_TABLE = {(byte) '0', (byte) '1', (byte) '2', (byte) '3', (byte) '4', (byte) '5', (byte) '6', (byte) '7', (byte) '8', (byte) '9', (byte) 'a', (byte) 'b', (byte) 'c', (byte) 'd', (byte) 'e', (byte) 'f'};

	private static Logger _log = Logger.getLogger(NetworkUtil.class);

	public static boolean checkIPMatching(String pattern, String address)
	{
		if(pattern.equals("*.*.*.*") || pattern.equals("*"))
		{
			return true;
		}

		String[] mask = pattern.split("\\.");
		String[] ip_address = address.split("\\.");
		for(int i = 0; i < mask.length; i++)
		{
			if(mask[i].equals("*") || mask[i].equals(ip_address[i]))
			{
				// none
			}
			else if(mask[i].contains("-"))
			{
				byte min = Byte.parseByte(mask[i].split("-")[0]);
				byte max = Byte.parseByte(mask[i].split("-")[1]);
				byte ip = Byte.parseByte(ip_address[i]);
				if(ip < min || ip > max)
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		return true;
	}

	public static String printData(byte[] data, int len)
	{
		final StringBuilder result = new StringBuilder(len * 4);

		int counter = 0;

		for(int i = 0; i < len; i++)
		{
			if(counter % 16 == 0)
			{
				result.append(fillHex(i, 4) + ": ");
			}

			result.append(fillHex(data[i] & 0xff, 2) + " ");
			counter++;
			if(counter == 16)
			{
				result.append("   ");

				int charpoint = i - 15;
				for(int a = 0; a < 16; a++)
				{
					int t1 = 0xFF & data[charpoint++];
					if(t1 > 0x1f && t1 < 0x80)
					{
						result.append((char) t1);
					}
					else
					{
						result.append('.');
					}
				}

				result.append("\n");
				counter = 0;
			}
		}

		int rest = data.length % 16;
		if(rest > 0)
		{
			for(int i = 0; i < 17 - rest; i++)
			{
				result.append("   ");
			}

			int charpoint = data.length - rest;
			for(int a = 0; a < rest; a++)
			{
				int t1 = 0xFF & data[charpoint++];
				if(t1 > 0x1f && t1 < 0x80)
				{
					result.append((char) t1);
				}
				else
				{
					result.append('.');
				}
			}

			result.append("\n");
		}

		return result.toString();
	}

	public static String fillHex(int data, int digits)
	{
		String number = Integer.toHexString(data);

		for(int i = number.length(); i < digits; i++)
		{
			number = "0" + number;
		}

		return number;
	}

	public static String printData(byte[] raw)
	{
		return printData(raw, raw.length);
	}

	public static String printData(java.nio.ByteBuffer buf)
	{
		byte[] data = new byte[buf.remaining()];
		buf.get(data);
		String hex = printData(data, data.length);
		buf.position(buf.position() - data.length);
		return hex;
	}

	public static String printData(NioBuffer buf)
	{
		byte[] data = new byte[buf.remaining()];
		buf.get(data);
		String hex = printData(data, data.length);
		buf.position(buf.position() - data.length);
		return hex;
	}

	public static String getHex(byte b)
	{
		String result = "";
		int index = 0;
		int v = b & 0xFF;
		byte[] hex = new byte[2];
		hex[index++] = HEX_CHAR_TABLE[v >>> 4];
		hex[index++] = HEX_CHAR_TABLE[v & 0xF];
		try
		{
			result = new String(hex, "ASCII");
		}
		catch(Exception e)
		{}

		return result;
	}

	public static String getHex(byte[] raw)
	{
		String result = "";
		int index = 0;
		byte[] hex = new byte[2 * raw.length];

		for(byte b : raw)
		{
			int v = b & 0xFF;
			hex[index++] = HEX_CHAR_TABLE[v >>> 4];
			hex[index++] = HEX_CHAR_TABLE[v & 0xF];
		}
		try
		{
			result = new String(hex, "ASCII");
		}
		catch(Exception e)
		{}

		return result;
	}

	/** Переводит строку с IP или домена в массив байт айпишника */
	public static byte[] parseIpToBytes(String address)
	{
		InetAddress inetAddr = null;
		try
		{
			inetAddr = InetAddress.getByName(address);
		}
		catch(UnknownHostException e)
		{
			e.printStackTrace();
		}
		return inetAddr.getAddress();
	}
}