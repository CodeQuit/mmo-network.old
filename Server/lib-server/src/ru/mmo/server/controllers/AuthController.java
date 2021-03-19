package ru.mmo.server.controllers;

import java.util.HashMap;
import java.util.Map;

import org.apache.log4j.Logger;

import ru.mmo.global.dao.DAOManager;
import ru.mmo.global.network.NetList;
import ru.mmo.server.dao.AccountsDAO;
import ru.mmo.server.dao.GroupsDAO;
import ru.mmo.server.dao.LogsDAO;
import ru.mmo.server.model.Account;
import ru.mmo.server.model.Group;
import ru.mmo.server.model.Log;
import ru.mmo.server.network.clients.MMOClient;
import ru.mmo.server.utils.AccountUtils;
import ru.mmo.server.utils.FailedAuthAttempt;

public class AuthController
{
	private static AccountsDAO _dao = DAOManager.getDAOImpl(AccountsDAO.class);
	private static GroupsDAO _gro_dao = DAOManager.getDAOImpl(GroupsDAO.class);
	private static LogsDAO _log_dao = DAOManager.getDAOImpl(LogsDAO.class);
	private static Logger _log = Logger.getLogger(AuthController.class);

	private static AuthController _instance;

	private final Map<String, MMOClient> _authServerClients = new HashMap<String, MMOClient>();
	//
	private final Map<String, FailedAuthAttempt> _hackList = new HashMap<String, FailedAuthAttempt>();
	@SuppressWarnings("unused")
	private final Map<String, FailedAuthAttempt> _useIn = new HashMap<String, FailedAuthAttempt>();

	public static enum State
	{
		VALID, //
		WRONG, //
		NOT_PAID, //
		BANNED, //
		IN_USE, //
		IP_ACCESS_DENIED
	}

	public static void load() throws Exception
	{
		if(_instance == null)
		{
			_instance = new AuthController();
		}
		else
		{
			throw new IllegalStateException("AuthController can only be loaded a single time.");
		}
	}

	public static AuthController getInstance()
	{
		return _instance;
	}

	private AuthController() throws Exception
	{

	}

	public boolean isAccountInLoginServer(String a)
	{
		return _authServerClients.containsKey(a);
	}

	public MMOClient removeAuthedLoginClient(String account)
	{
		if(isAccountInLoginServer(account))
			return _authServerClients.remove(account);
		return null;
	}

	public MMOClient getAuthedClient(String account)
	{
		return _authServerClients.get(account);
	}

	public State tryAuthLogin(String account, String password, MMOClient client)
	{
		String ip = client.getIPString();
		State ret = loginValid(account, password, client);

		if(ret != State.VALID)
		{
			FailedAuthAttempt failedAttempt = _hackList.get(ip);
			int failedCount;

			if(failedAttempt == null)
			{
				_hackList.put(ip, new FailedAuthAttempt());
				failedCount = 1;
			}
			else
			{
				failedAttempt.increaseCounter();
				failedCount = failedAttempt.getCount();
			}

			if(failedCount >= 10)
			{
				_log.info("ban ip " + ip);

				// IPManager.getInstance().addToListIp(ip, "AUTO BAN SYSTEM", 10, false);

				_hackList.remove(ip);
			}

			return ret;
		}
		else
		{
			_hackList.remove(ip);

			MMOClient anotherClient = _authServerClients.get(account);
			if(anotherClient == null)
			{
				_authServerClients.put(account, client);
				return State.VALID;
			}
			else if(anotherClient != null)
			{
				_authServerClients.remove(account);
				anotherClient.close(true);
				return State.IN_USE;
			}
		}
		return State.WRONG;
	}

	public State loginValid(String user, String password, MMOClient client)
	{
		String ip = client.getIPString();

		// TODO LOG NLogClient.log(EEvent.TRY_AUTH, ip, user == null ? "-" : user, password == null ? "-" : password);

		Account account = _dao.select(user);

		if(account != null)
		{
			if(account.getAllowIPs() != null && !account.getAllowIPs().isEmpty() && !account.getAllowIPs().equals("*"))
			{
				NetList allowedList = new NetList();
				allowedList.LoadFromString(account.getAllowIPs(), ",");
				if( !allowedList.isIpInNets(client.getIPString()))
				{
					return State.IP_ACCESS_DENIED;
				}
			}

			if(account.getPassword().equals(""))
			{
				return State.WRONG;
			}

			boolean banned = false;
			long banTime = account.getBanExpires();

			if(banTime == -1)
			{
				banned = true;
			}
			else if(banTime > 0)
			{
				if(banTime < System.currentTimeMillis() / 1000)
				{
					account.setBanExpires(0);
				}
				else
				{
					banned = true;
				}
			}

			if(banned)
			{
				return State.BANNED;
			}
		}
		

		if(account != null)
		{
			account.setLastActive(System.currentTimeMillis() / 1000);
			account.setLastIP(ip);
			_dao.updateLastActiveIP(account);
			
			//TODO: переделать а то както не очень смотрится
			//Group group = _gro_dao.select(account.getId());
			//client.setGroupId(group.getUserId());
			Log log = new Log();
			log.setAccount(account.getLogin());
			log.setText("Вход успешно выполнен пользователем с версией UNKNOWN");
			_log_dao.insert(log);


			return State.VALID;
		}

		return State.WRONG;
	}

	// public ScrambledKeyPair getScrambledRSAKeyPair()
	// {
	// return _keyPairs[Rnd.nextInt(10)];
	// }
}