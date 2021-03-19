package ru.mmo.dao.server.mysql;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.apache.log4j.Logger;

import ru.mmo.global.dao.annotations.DAO;
import ru.mmo.global.dbc.DatabaseFactory;
import ru.mmo.global.dbc.DatabaseUtils;
import ru.mmo.server.dao.GroupsDAO;
import ru.mmo.server.model.Group;

@DAO(database = "MySQL")
public class GroupsDAOImpl implements GroupsDAO
{
	private static final Logger _log = Logger.getLogger(AccountsDAOImpl.class);

	@Override
	public Group select(long objectID)
	{
		Connection con = null;
		PreparedStatement statement = null;
		ResultSet rset = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			statement = con.prepareStatement("SELECT group_id FROM users_groups WHERE user_id=? LIMIT 1");
			statement.setLong(1, objectID);
			rset = statement.executeQuery();
			if(rset.next())
			{
				Group group = new Group();
				group.setUserId(rset.getLong("user_id"));
				group.setGroupId(rset.getLong("group_id"));
				return group;
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

}
