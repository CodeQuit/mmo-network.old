package ru.mmo.server.model;

/**
 * @author: Felixx, DarkSkeleton
 */
public class Account
{
	private long _id;
	private String _login;
	private String _password;
	private long _lastActive;
	private String _allowIPs;
	private long _banExpires;
	private int _lastServer;
	private int _accessLevel;
	private String _lastIP;
	private long _pay;

	public long getId()
	{
		return _id;
	}
	
	public void setId(long id)
	{
		_id = id;
	}
	
	public String getLogin()
	{
		return _login;
	}

	public void setLogin(String login)
	{
		_login = login;
	}

	public String getPassword()
	{
		return _password;
	}

	public void setPassword(String password)
	{
		_password = password;
	}

	public long getLastActive()
	{
		return _lastActive;
	}

	public void setLastActive(long lastActive)
	{
		_lastActive = lastActive;
	}

	public String getAllowIPs()
	{
		return _allowIPs;
	}

	public void setAllowIPs(String allowIPs)
	{
		_allowIPs = allowIPs;
	}

	public long getBanExpires()
	{
		return _banExpires;
	}

	public void setBanExpires(long banExpires)
	{
		_banExpires = banExpires;
	}

	public int getLastServer()
	{
		return _lastServer;
	}

	public void setLastServer(int lastServer)
	{
		_lastServer = lastServer;
	}

	public String getLastIP()
	{
		return _lastIP;
	}

	public void setLastIP(String lastIP)
	{
		_lastIP = lastIP;
	}

	public long isPay()
	{
		return _pay;
	}

	public void setPay(long pay)
	{
		_pay = pay;
	}

	public int getAccessLevel()
	{
		return _accessLevel;
	}

	public void setAccessLevel(int accessLevel)
	{
		_accessLevel = accessLevel;
	}
}