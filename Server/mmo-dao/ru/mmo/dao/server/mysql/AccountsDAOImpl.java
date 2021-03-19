package ru.mmo.dao.server.mysql;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.apache.log4j.Logger;

import ru.mmo.global.dao.annotations.DAO;
import ru.mmo.global.dbc.DatabaseFactory;
import ru.mmo.global.dbc.DatabaseUtils;
import ru.mmo.server.dao.AccountsDAO;
import ru.mmo.server.model.Account;

/**
 * @author: Felixx
 */
@DAO(database = "MySQL")
public class AccountsDAOImpl implements AccountsDAO
{
	private static final Logger _log = Logger.getLogger(AccountsDAOImpl.class);

	@Override
	public Account select(String lo)
	{
		Connection con = null;
		PreparedStatement statement = null;
		ResultSet rset = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.prepareStatement("SELECT password, money FROM users WHERE username=? LIMIT 1");
			statement.setString(1, lo);
			rset = statement.executeQuery();
			if(rset.next())
			{
				Account account = new Account();
				account.setLogin(lo);
				// account.setAccessLevel(rset.getInt("access_level"));
				// account.setAllowIPs(rset.getString("AllowIPs"));
				account.setPassword(rset.getString("password"));
				// account.setBanExpires(rset.getLong("banExpires"));
				account.setPay(rset.getLong("money"));
				// account.setLastServer(rset.getInt("lastServer"));
				return account;
			}
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCSR(con, statement, rset);
		}

		return null;
	}

	@Override
	public void insert(Account acc)
	{
		Connection con = null;
		PreparedStatement statement = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.prepareStatement("INSERT INTO users(username, password, money) VALUES (?,?,?)");
			statement.setString(1, acc.getLogin());
			statement.setString(2, acc.getPassword());
			statement.setLong(3, acc.isPay());
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

	@Override
	public int getMoney(String acc)
	{
		Connection con = null;
		PreparedStatement statement = null;
		ResultSet rset = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.prepareStatement("SELECT money FROM users WHERE username=? LIMIT 1");
			statement.setString(1, acc);
			rset = statement.executeQuery();
			if(rset.next())
			{
				return rset.getInt("money");
			}
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCSR(con, statement, rset);
		}

		return 0;
	}

	@Override
	public void updateMoney(String acc, int money)
	{
		Connection con = null;
		PreparedStatement stmt = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			stmt = con.prepareStatement("UPDATE users SET money = ? WHERE username = ? LIMIT 1");
			stmt.setLong(1, money);
			stmt.setString(2, acc);
			stmt.execute();
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCS(con, stmt);
		}
	}

	@Override
	public void updateLastActiveIP(Account acc)
	{
		Connection con = null;
		PreparedStatement stmt = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			stmt = con.prepareStatement("UPDATE users SET lastactive = ?, lastIP = ?  WHERE username = ? LIMIT 1");
			stmt.setLong(1, acc.getLastActive());
			stmt.setString(2, acc.getLastIP());
			stmt.setString(3, acc.getLogin());
			stmt.execute();
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCS(con, stmt);
		}
	}

	@Override
	public void updateLastServer(String account, int server)
	{
		Connection con = null;
		PreparedStatement stmt = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			stmt = con.prepareStatement("UPDATE accounts SET lastserver = ?  WHERE username LIKE ? LIMIT 1");
			stmt.setInt(1, server);
			stmt.setString(2, account);
			stmt.execute();
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCS(con, stmt);
		}
	}

	@Override
	public int getLastServer(String account)
	{
		int serverId = 0;
		Connection con = null;
		PreparedStatement stmt = null;
		ResultSet rset = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			stmt = con.prepareStatement("SELECT lastServer FROM accounts WHERE login LIKE ? LIMIT 1");
			stmt.setString(1, account);
			rset = stmt.executeQuery();
			if(rset.next())
			{
				serverId = rset.getInt("lastserver");
			}
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCS(con, stmt);
		}
		return serverId;
	}

	@Override
	public void updatePlayersInAccount(String login, String player, int server, byte type)
	{
		Connection con = null;
		PreparedStatement stmt = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();

			stmt = con.prepareStatement(type > 0 ? "DELETE FROM accounts_info WHERE login LIKE ? AND player LIKE ? AND server = ?" : "INSERT INTO accounts_info VALUES(?,?,?)");
			stmt.setString(1, login);
			stmt.setString(2, player);
			stmt.setInt(3, server);
			stmt.execute();
		}
		catch(SQLException e)
		{
			_log.info("SQLException: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeDatabaseCS(con, stmt);
		}
	}
}