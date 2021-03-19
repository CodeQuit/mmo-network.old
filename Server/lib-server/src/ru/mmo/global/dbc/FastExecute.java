package ru.mmo.global.dbc;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Statement;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

@Deprecated
public abstract class FastExecute
{
	private static Logger _log = Logger.getLogger(FastExecute.class);

	/**
	 * Выполняет простой sql запросов, где ненужен контроль параметров<BR>
	 * ВНИМАНИЕ: В данном методе передаваемые параметры не проходят проверку на предмет SQL-инъекции!
	 * 
	 * @param query
	 *            Строка SQL запроса
	 * @return false в случае ошибки выполнения запроса либо true в случае успеха
	 */
	public static boolean set(String query)
	{
		Connection con = null;
		Statement statement = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.createStatement();
			statement.executeUpdate(query);
		}
		catch(Exception e)
		{
			_log.warn("Could not execute update '" + query + "': " + e, e);
			return false;
		}
		finally
		{
			DatabaseUtils.closeDatabaseCS(con, statement);
		}
		return true;
	}

	public static Object get(String query)
	{
		Object ret = null;
		Connection con = null;
		Statement statement = null;
		ResultSet rset = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.createStatement();
			rset = statement.executeQuery(query + " LIMIT 1");
			ResultSetMetaData md = rset.getMetaData();

			if(rset.next())
			{
				if(md.getColumnCount() > 1)
				{
					ConcurrentHashMap<String, Object> tmp = new ConcurrentHashMap<String, Object>();
					for(int i = md.getColumnCount(); i > 0; i--)
					{
						tmp.put(md.getColumnName(i), rset.getObject(i));
					}
					ret = tmp;
				}
				else
				{
					ret = rset.getObject(1);
				}
			}

		}
		catch(Exception e)
		{
			_log.warn("Could not execute query '" + query + "': " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCSR(con, statement, rset);
		}
		return ret;
	}

	public static int simple_get_int(String ret_field, String table, String where)
	{
		String query = "SELECT " + ret_field + " FROM `" + table + "` WHERE " + where + " LIMIT 1;";

		int res = 0;
		Connection con = null;
		PreparedStatement statement = null;
		ResultSet rset = null;

		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.prepareStatement(query);
			rset = statement.executeQuery();

			if(rset.next())
			{
				res = rset.getInt(1);
			}
		}
		catch(Exception e)
		{
			_log.warn("mSGI: Error in query '" + query + "':" + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCSR(con, statement, rset);
		}

		return res;
	}
}
