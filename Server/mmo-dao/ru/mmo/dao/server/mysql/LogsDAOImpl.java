package ru.mmo.dao.server.mysql;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import org.apache.log4j.Logger;

import ru.mmo.global.dao.annotations.DAO;
import ru.mmo.global.dbc.DatabaseFactory;
import ru.mmo.global.dbc.DatabaseUtils;
import ru.mmo.server.dao.LogsDAO;
import ru.mmo.server.model.Log;

@DAO(database = "MySQL")
public class LogsDAOImpl implements LogsDAO
{
	private static final Logger _log = Logger.getLogger(LogsDAOImpl.class);
	
	@Override
	public void insert(Log logs) 
	{
		Connection con = null;
		PreparedStatement statement = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.prepareStatement("INSERT INTO logs(id, date, login_name, text) VALUES (?,?,?,?)");
			statement.setString(3, logs.getAccount());
			statement.setString(4, logs.getText());
			statement.execute();
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCS(con, statement);
		}
	}
}
