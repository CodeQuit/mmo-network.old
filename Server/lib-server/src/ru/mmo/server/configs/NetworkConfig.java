package ru.mmo.server.configs;

import java.io.IOException;

import org.apache.log4j.Logger;

import ru.mmo.global.configs.Config;
import ru.mmo.global.utils.ExitCode;

/**
 * @author Felixx
 */
public class NetworkConfig
{
	private static final Logger _log = Logger.getLogger(NetworkConfig.class);

	private static final String FileName = "./configs/network.ini";

	/** Хост для приема подключений клиента обновлений. */
	public static String SERVER_LISTENER_HOST;
	/** Порт приема подключения клиента обновлений. */
	public static int SERVER_LISTENER_PORT;

	public static void load()
	{
		try
		{
			Config conf = new Config(FileName);

			SERVER_LISTENER_HOST = conf.getStringValue("listener", "ServerHost", "127.0.0.1");
			SERVER_LISTENER_PORT = conf.getIntegerValue("listener", "ServerPort", 11000);
		}
		catch(IOException e)
		{
			_log.error("Не возможно загрузить файлы конфигурации.", e);
			System.exit(ExitCode.CODE_ERROR.getId());
		}
		catch(Exception e)
		{
			_log.error("Ошибка доступа к файлу или директории.", e);
			System.exit(ExitCode.CODE_ERROR.getId());
		}
		finally
		{
			_log.info(FileName + " Успешно загружен.");
		}
	}
}