package ru.mmo.server.utils;

import ru.mmo.global.crypt.Encode;
import ru.mmo.global.crypt.SHA1;

/**
 * Author: Felixx
 */
public class AccountUtils
{
	private static Encode _crypt;

	public static String encodePassword(String st)
	{
		//if(_crypt == null)
		//{
			// AuthServerConfig.USE_MD5
		//_crypt = new SHA1();
		//}
//
		return _crypt.encode(st);
	}

	public static boolean check(String normal)
	{
		return encodePassword(normal).equals(normal);
	}
}