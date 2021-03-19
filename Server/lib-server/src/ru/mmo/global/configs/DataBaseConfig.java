package ru.mmo.global.configs;

import java.io.IOException;

import org.apache.log4j.Logger;

import ru.mmo.global.utils.ExitCode;

/**
 * @author Felixx
 */
public class DataBaseConfig
{
	private static final Logger _log = Logger.getLogger(DataBaseConfig.class);

	private static final String FileName = "./configs/database.ini";

	/** Драйвер баз данных. */
	public static String DATABASE_DRIVER;
	/** Путь к базе данных. */
	public static String DATABASE_URL;
	/** Максимально разрешонное количество подключений к базе данных. */
	public static int DATABASE_MAX_CONNECTIONS;

	/** Имя пользователя для базы данных. */
	public static String DATABASE_USER;
	/** Пароль для пользователя базы данных. */
	public static String DATABASE_PASSWORD;

	/** Дебаг подключений */
	public static boolean DATABASE_DEBUG_CONNECTIONS;

	public static void load()
	{
		try
		{
			Config conf = new Config(FileName);

			DATABASE_DRIVER = conf.getStringValue("database", "Driver", "com.mysql.jdbc.Driver");
			DATABASE_URL = conf.getStringValue("database", "Url", "jdbc:mysql://localhost:3306/aion_authserver?useUnicode=true&characterEncoding=UTF-8&autoReconnect=true");
			DATABASE_MAX_CONNECTIONS = conf.getIntegerValue("database", "MaxConnections", 50);
			DATABASE_DEBUG_CONNECTIONS = conf.getBooleanValue("database", "Debug", false);

			DATABASE_USER = conf.getStringValue("access", "User", "root");
			DATABASE_PASSWORD = conf.getStringValue("access", "Password", "");

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