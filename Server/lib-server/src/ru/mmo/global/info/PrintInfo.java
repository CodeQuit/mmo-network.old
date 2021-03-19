package ru.mmo.global.info;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.jar.Attributes;
import java.util.jar.JarInputStream;
import java.util.jar.Manifest;

import org.apache.log4j.Logger;

/**
 * @author: Felixx
 */
public class PrintInfo
{
	private static PrintInfo _instance;
	private static final Logger _log = Logger.getLogger(PrintInfo.class);
	private long _startTime;

	public static PrintInfo getInstance()
	{
		if(_instance == null)
			_instance = new PrintInfo();
		return _instance;
	}

	private PrintInfo()
	{
		_startTime = System.currentTimeMillis();
	}

	public void info()
	{
		System.gc();
		long seconds = (System.currentTimeMillis() - _startTime) / 1000;

		printLines("Memory", "Info");
		print("Загружено за %d секунд(%.3f минут)", seconds, (seconds / 60F));
		print("Использовано %s, максимум %s.", MemoryInfo.getMemoryUsedMb(), MemoryInfo.getMemoryMaxMb());
		print("Использовано %.2f%% процентов выделеной памяти.", MemoryInfo.getMemoryUsagePercent());
		printLines(null, null);
	}

	public void info2()
	{
		printLines("Memory", "Info");
		print("Использовано %s, максимум %s.", MemoryInfo.getMemoryUsedMb(), MemoryInfo.getMemoryMaxMb());
		print("Использовано %.2f%% процентов выделеной памяти.", MemoryInfo.getMemoryUsagePercent());
		printLines(null, null);
	}

	public static void print(String g, Object... a)
	{
		_log.info(String.format(g, a));
	}

	public static void printEmptyLine()
	{
		System.out.println(new StringBuilder(80));
	}

	public static void printLines(String name, String type)
	{
		StringBuilder st = new StringBuilder(80);
		if(name != null)
		{
			st.append("---[").append(name).append(']');
		}
		String end = type == null ? "" : "[" + type + "]---";

		final int count = 79 - (end.length());

		while(st.length() != count)
		{
			st.append('-');
		}

		st.append(end);

		System.out.println(st);
	}

	public static void printLibInfo(Logger log)
	{
		printLines("Library", "Info");

		for(File f : new File("./libs/").listFiles())
		{
			if(f.getName().endsWith(".jar"))
			{
				String version = null;
				String revision = null;
				try
				{
					JarInputStream jar = new JarInputStream(new FileInputStream(f));
					Manifest man = jar.getManifest();

					if(man != null)
					{
						version = man.getMainAttributes().getValue("Implementation-Version");
						revision = man.getMainAttributes().getValue("Implementation-Revision");

						if(version == null || revision == null)
						{
							if(man.getEntries() != null)
							{
								for(Attributes a : man.getEntries().values())
								{
									if(version == null)
									{
										version = a.getValue("Implementation-Version");
									}
									if(revision == null)
									{
										revision = a.getValue("Implementation-Revision");
									}
								}
							}
						}
					}
				}
				catch(IOException e)
				{}

				if(version != null)
				{
					log.info("version (" + f.getName() + "): " + version);
				}
				else if(revision != null)
				{
					log.info("revision (" + f.getName() + "): " + revision);
				}
				else
				{
					log.info("version (" + f.getName() + "): none");
				}
			}
		}
		printLines(null, null);
	}
}