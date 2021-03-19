package ru.mmo.server.configs;

import java.io.IOException;

import org.apache.log4j.Logger;

import ru.mmo.global.configs.Config;
import ru.mmo.global.utils.ExitCode;

/**
 * @author Felixx
 */
public class DevelopConfig
{
	private static final Logger _log = Logger.getLogger(DevelopConfig.class);

	private static final String FileName = "./configs/develop.ini";

	/** Включить дебаг. */
	public static boolean DEBUG;
	/** Включить дебаг сети. */
	public static boolean NETWORK_DEBUG;
	/** Сохранять проблемы в файл. */
	public static boolean DEVELOP_SAVE_PROBLEMS;

	/** Кодировка текста в пакетах */
	public static String DEVELOP_CODEPAGE;

	/** Размер пула */
	public static int POOL_SIZE;

	public static void load()
	{
		try
		{
			Config conf = new Config(FileName);

			DEBUG = conf.getBooleanValue("develop", "Debug", false);
			NETWORK_DEBUG = conf.getBooleanValue("develop", "NetworkDebug", true);
			DEVELOP_SAVE_PROBLEMS = conf.getBooleanValue("develop", "SaveProblems", false);

			DEVELOP_CODEPAGE = conf.getStringValue("develop", "DEVELOP_CODEPAGE", "windows-1251");

			POOL_SIZE = conf.getIntegerValue("thread", "PoolSize", 1);
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