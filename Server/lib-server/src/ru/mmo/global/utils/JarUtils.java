package ru.mmo.global.utils;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.jar.JarEntry;
import java.util.jar.JarInputStream;

import org.apache.log4j.Logger;

/**
 * @author Felixx
 */
public class JarUtils
{
	private static final Logger _log = Logger.getLogger(JarUtils.class);

	public static List<Class<?>> listClasses(String jar, String packageA)
	{
		List<Class<?>> list = new ArrayList<Class<?>>();
		try
		{
			JarInputStream jarFile = new JarInputStream(new FileInputStream(jar));
			JarEntry jarEntry;

			while((jarEntry = jarFile.getNextJarEntry()) != null)
			{
				String name = jarEntry.getName().replaceAll("/", "\\.");
				if(name.startsWith(packageA) && name.endsWith(".class"))
				{
					if( !name.contains("$"))
					{
						try
						{
							list.add(Class.forName(name.replace(".class", "")));
						}
						catch(ClassNotFoundException e)
						{
							_log.info("ClassNotFoundException: " + e, e);
						}
					}
				}
			}
		}
		catch(IOException e)
		{
			_log.info("IOException: " + e, e);
		}
		return list;
	}
}