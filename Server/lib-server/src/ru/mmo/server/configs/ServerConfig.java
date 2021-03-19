package ru.mmo.server.configs;

import java.io.IOException;

import org.apache.log4j.Logger;

import ru.mmo.global.configs.Config;
import ru.mmo.global.utils.ExitCode;

/**
 * @author Felixx, DarkSkeleton
 */
public class ServerConfig
{
	private static final Logger _log = Logger.getLogger(ServerConfig.class);

	private static final String FileName = "./configs/updateserver.ini";
	public static String SERVER_ADDR;

	public static void load()
	{
		try
		{
			Config conf = new Config(FileName);
			SERVER_ADDR = conf.getStringValue("updater", "fileserver", "localhost");
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