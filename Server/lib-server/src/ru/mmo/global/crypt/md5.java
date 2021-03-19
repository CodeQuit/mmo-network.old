/*package ru.mmo.global.crypt;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

/**
 * @author Felixx
 */
/*public class md5 implements Encode
{
	@Override
	public String encode(String md5Text)
	{
		return getMD5Hash(getMD5Hash(md5Text));
	}

	public static String getMD5Hash(String text)
	{
		try
		{
			MessageDigest md;
			md = MessageDigest.getInstance("MD5");
			byte[] md5hash = new byte[32];
			md.update(text.getBytes("iso-8859-1"), 0, text.length());
			md5hash = md.digest();
			return convertToHex(md5hash);
		}
		catch(NoSuchAlgorithmException e)
		{
			// _log.error("Exception while encoding password");
			throw new Error(e);
		}
		catch(UnsupportedEncodingException e)
		{
			// _log.error("Exception while encoding password");
			throw new Error(e);
		}
	}

	private static String convertToHex(byte[] data) throws NoSuchAlgorithmException, UnsupportedEncodingException
	{
		StringBuffer buf = new StringBuffer();

		for(byte value : data)
		{
			int halfbyte = (value >>> 4) & 0x0F;
			int two_halfs = 0;
			do
			{
				if((0 <= halfbyte) && (halfbyte <= 9))
				{
					buf.append((char) ('0' + halfbyte));
				}
				else
				{
					buf.append((char) ('a' + (halfbyte - 10)));
				}
				halfbyte = value & 0x0F;
			}
			while(two_halfs++ < 1);
		}
		return buf.toString();
	}
}*/