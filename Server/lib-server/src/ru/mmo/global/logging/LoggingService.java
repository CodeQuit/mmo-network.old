package ru.mmo.global.logging;

import java.io.File;
import java.lang.reflect.Field;
import java.net.URL;
import java.util.logging.Handler;
import java.util.logging.LogManager;
import java.util.logging.Logger;

import org.apache.log4j.Hierarchy;
import org.apache.log4j.xml.DOMConfigurator;

import ru.mmo.global.logging.log4j.JuliToLog4JHandler;
import ru.mmo.global.logging.log4j.ThrowableAsMessageAwareFactory;

public class LoggingService
{
	public static void load()
	{
		File logDir = new File("./log/");

		if( !logDir.exists())
		{
			logDir.mkdir();
		}

		try
		{
			URL url = LoggingService.class.getResource("log4j.xml");
			DOMConfigurator.configure(url);
		}
		catch(Exception e)
		{
			return;
		}

		overrideDefaultLoggerFactory();

		Logger logger = LogManager.getLogManager().getLogger("");
		for(Handler h : logger.getHandlers())
		{
			logger.removeHandler(h);
		}

		logger.addHandler(new JuliToLog4JHandler());
	}

	private static void overrideDefaultLoggerFactory()
	{
		// Hack here, we have to overwrite default logger factory
		Hierarchy lr = (Hierarchy) org.apache.log4j.LogManager.getLoggerRepository();
		try
		{
			Field field = lr.getClass().getDeclaredField("defaultFactory");
			field.setAccessible(true);
			String cn = System.getProperty("log4j.loggerfactory", ThrowableAsMessageAwareFactory.class.getName());
			Class<?> c = Class.forName(cn);
			field.set(lr, c.newInstance());
			field.setAccessible(false);
		}
		catch(NoSuchFieldException e)
		{
			// never thrown
			e.printStackTrace();
		}
		catch(IllegalAccessException e)
		{
			// never thrown
			e.printStackTrace();
		}
		catch(ClassNotFoundException e)
		{
			e.printStackTrace();
		}
		catch(InstantiationException e)
		{
			e.printStackTrace();
		}
	}
}
