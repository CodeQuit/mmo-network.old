package ru.mmo.global.dao;

import java.lang.reflect.Method;
import java.lang.reflect.Modifier;
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.log4j.Logger;

import ru.mmo.global.dao.annotations.DAO;
import ru.mmo.global.dbc.DatabaseFactory;
import ru.mmo.global.dbc.DatabaseUtils;
import ru.mmo.global.utils.JarUtils;

/**
 * @author Felixx
 */
public class DAOManager
{
	private static final Logger _log = Logger.getLogger(DAOManager.class);
	private static DAOManager _instance;

	private Map<Class<?>, Object> _daoList = new HashMap<Class<?>, Object>();
	private String DATABASE_NAME;

	public static DAOManager getInstance()
	{
		if(_instance == null)
		{
			_instance = new DAOManager();
		}
		return _instance;
	}

	private DAOManager()
	{
		initDatabase();
	}

	public void load(String packageName)
	{
		List<Class<?>> classes = JarUtils.listClasses("./libs/mmo-dao.jar", "ru.mmo.dao." + packageName);
		for(Class<?> clazz : classes)
		{
			if( !clazz.isEnum() && !Modifier.isAbstract(clazz.getModifiers()))
			{
				addClass(clazz);
			}
		}

		for(Object object : _daoList.values())
		{
			Method[] methods = object.getClass().getDeclaredMethods();
			for(Method m : methods)
			{
				if(m.getName().equals("init") && m.getParameterTypes().length == 0)
				{
					try
					{
						m.invoke(object);
					}
					catch(Exception e)
					{
						_log.info("Fail to call init. Exception: " + e, e);
					}
				}
			}
		}
		_log.info("Загружено " + _daoList.size() + " dao(s) impl.");
	}

	private void initDatabase()
	{
		Connection con = null;
		try
		{
			con = DatabaseFactory.getInstance().newConnection();
			DatabaseMetaData data = con.getMetaData();
			DATABASE_NAME = data.getDatabaseProductName();
		}
		catch(SQLException e)
		{
			_log.info("Exception: " + e, e);
		}
		finally
		{
			DatabaseUtils.closeConnection(con);
		}
	}

	protected void addClass(Class<?> a)
	{
		if( !a.isAnnotationPresent(DAO.class))
		{
			return;
		}

		Class<?> implInterface = null;
		Class<?>[] interfaces = a.getInterfaces();
		if(interfaces.length == 0 || interfaces.length > 1)
		{
			return;
		}

		implInterface = interfaces[0];

		try
		{
			DAO annotation = a.getAnnotation(DAO.class);
			Object dao = a.newInstance();
			if(annotation.database().equalsIgnoreCase(DATABASE_NAME))
			{
				_daoList.put(implInterface, dao);
			}
		}
		catch(Exception e)
		{
			_log.info("Exception: " + e, e);
		}
	}

	public static <T>T getDAOImpl(Class<T> clazz)
	{
		return getInstance().getDAOImpl0(clazz);
	}

	@SuppressWarnings("unchecked")
	private <T>T getDAOImpl0(Class<T> clazz)
	{
		T t = (T) _daoList.get(clazz);
		if(t == null)
		{
			throw new NoSuchDAOImplException("Not find dao impl: " + clazz.getName());
		}
		return t;
	}
}