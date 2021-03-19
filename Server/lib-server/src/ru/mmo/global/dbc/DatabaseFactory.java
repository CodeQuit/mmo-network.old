package ru.mmo.global.dbc;

import java.sql.Connection;
import java.sql.SQLException;

import org.apache.commons.dbcp.BasicDataSource;
import org.apache.log4j.Logger;

import ru.mmo.global.configs.DataBaseConfig;

/**
 * @author Felixx
 */
public class DatabaseFactory
{
	private static DatabaseFactory _instance;
	private BasicDataSource _dataSource;
	private static final Logger _log = Logger.getLogger(DatabaseFactory.class);

	public static DatabaseFactory getInstance() throws SQLException
	{
		if(_instance == null)
		{
			_instance = new DatabaseFactory();
		}
		return _instance;
	}

	private DatabaseFactory()
	{
		init();
		_log.info("Соеденение с базой данных успешно");
	}

	private void init()
	{
		_dataSource = new BasicDataSource();
		_dataSource.setDriverClassName(DataBaseConfig.DATABASE_DRIVER);
		_dataSource.setUsername(DataBaseConfig.DATABASE_USER);
		_dataSource.setPassword(DataBaseConfig.DATABASE_PASSWORD);
		_dataSource.setUrl(DataBaseConfig.DATABASE_URL);
		// _dataSource.setInitialSize(1);
		// _dataSource.setMaxActive(DatabaseProperties.MAX_CONNECTIONS);
		// _dataSource.setMaxIdle(DatabaseProperties.MAX_CONNECTIONS);
		// _dataSource.setMinIdle(1);
		// _dataSource.setMaxWait(-1);

		// _dataSource.setDefaultAutoCommit(false);
		_dataSource.setValidationQuery("SELECT 1");
		// _dataSource.setTestOnBorrow(false);
		// _dataSource.setTestWhileIdle(true);

		// _dataSource.setRemoveAbandoned(true);
		// _dataSource.setRemoveAbandonedTimeout(60);

		// _dataSource.setTimeBetweenEvictionRunsMillis(DatabaseProperties.IDLE_TEST_PERIOD * 1000);
		// _dataSource.setNumTestsPerEvictionRun(DatabaseProperties.MAX_CONNECTIONS);
		// _dataSource.setMinEvictableIdleTimeMillis(DatabaseProperties.IDLE_TIMEOUT * 1000);
	}

	public Connection newConnection() throws SQLException
	{
		if(DataBaseConfig.DATABASE_DEBUG_CONNECTIONS)
		{
			StringBuilder sb = new StringBuilder();
			StackTraceElement[] elems = Thread.currentThread().getStackTrace();
			for(int i = 0; i < elems.length; i++)
			{
				sb.append(elems[i].toString() + "\n");
			}
			ConnectionWrapper con = new ConnectionWrapper(_dataSource.getConnection());
			_log.info("connection " + con.getId() + " open: \n" + sb.toString());
			return con;
		}
		return _dataSource.getConnection();
	}

	public void shutdown()
	{
		try
		{
			_dataSource.close();
		}
		catch(Exception e)
		{}
	}
}