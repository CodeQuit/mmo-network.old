package ru.mmo.global.xml.holder;

import org.apache.log4j.Logger;

import ru.mmo.global.utils.StringUtil;

/**
 * @author Felixx
 */
public abstract class AbstractHolder
{
	protected Logger _log = Logger.getLogger(getClass());

	public void log()
	{
		_log.info(String.format("load %d %s(s) count.", size(), StringUtil.afterSpaceToUpperCase(getClass().getSimpleName().replace("Holder", "")).toLowerCase()));
	}

	public void info(String s)
	{
		_log.info(s);
	}

	public abstract int size();

	public abstract void clear();
}