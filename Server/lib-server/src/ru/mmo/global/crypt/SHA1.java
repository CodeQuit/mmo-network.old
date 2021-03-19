package ru.mmo.global.crypt;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

/**
 * @author Felixx
 */
public class SHA1 implements Encode
{
	public String encode(String password)
	{
		try
		{
			MessageDigest messageDiegest = MessageDigest.getInstance("SHA-1");
			messageDiegest.update(password.getBytes("UTF-8"));
			return Base64.encodeToString(messageDiegest.digest(), false);
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
}