package ru.mmo.global.dbc;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import org.apache.log4j.Logger;

import ru.mmo.global.configs.DataBaseConfig;

/**
 * @author Felixx
 */
public class DatabaseUtils
{
	private static final Logger _log = Logger.getLogger(DatabaseUtils.class);

	/**
	 * Закрыть коннект
	 * 
	 * @param conn
	 *            - коннект к базе данных
	 */
	public static void closeConnection(Connection conn)
	{
		if(conn != null)
		{
			try
			{
				if(DataBaseConfig.DATABASE_DEBUG_CONNECTIONS)
				{
					StringBuilder sb = new StringBuilder();
					StackTraceElement[] elems = Thread.currentThread().getStackTrace();
					for(int i = 0; i < elems.length; i++)
					{
						sb.append(elems[i].toString() + "\n");
					}
					if(conn instanceof ConnectionWrapper)
					{
						ConnectionWrapper cw = (ConnectionWrapper) conn;
						_log.info("connection " + cw.getId() + " close: \n" + sb.toString());
					}
					else
					{
						_log.info("connection " + conn.hashCode() + " close: \n" + sb.toString());
					}
				}
				conn.close();
			}
			catch(SQLException e)
			{}
		}
	}

	/**
	 * Закрыть Statement
	 * 
	 * @param stmt
	 *            - Statement
	 */
	public static void closeStatement(Statement stmt)
	{
		if(stmt != null)
		{
			try
			{
				stmt.close();
			}
			catch(SQLException e)
			{
				//
			}
		}
	}

	/**
	 * Закрыть ResultSet
	 * 
	 * @param rs
	 *            - ResultSet
	 */
	public static void closeResultSet(ResultSet rs)
	{
		if(rs != null)
		{
			try
			{
				rs.close();
			}
			catch(SQLException e)
			{}
		}
	}

	/**
	 * Закрыть коннект, Statement и ResultSet
	 * 
	 * @param conn
	 *            - Connection
	 * @param stmt
	 *            - Statement
	 * @param rs
	 *            - ResultSet
	 */
	public static void closeDatabaseCSR(Connection conn, Statement stmt, ResultSet rs)
	{
		closeResultSet(rs);
		closeStatement(stmt);
		closeConnection(conn);
	}

	/**
	 * закрыть коннект, Statement
	 * 
	 * @param conn
	 *            - Connection
	 * @param stmt
	 *            - Statement
	 */
	public static void closeDatabaseCS(Connection conn, Statement stmt)
	{
		closeStatement(stmt);
		closeConnection(conn);
	}

	/**
	 * закрыть Statement и ResultSet
	 * 
	 * @param stmt
	 *            - Statement
	 * @param rs
	 *            - ResultSet
	 */
	public static void closeDatabaseSR(Statement stmt, ResultSet rs)
	{
		closeResultSet(rs);
		closeStatement(stmt);
	}
}