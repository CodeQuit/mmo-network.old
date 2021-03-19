package ru.mmo.global.threading;

import org.apache.log4j.Logger;

/**
 * @author Felixx
 */
public abstract class RunnableTask implements Runnable
{
	protected static final Logger _log = Logger.getLogger(RunnableTask.class);

	public abstract void runImpl() throws Exception;

	@Override
	public final void run()
	{
		try
		{
			runImpl();
		}
		catch(Exception e)
		{
			_log.info("Exception: " + e, e);
		}
	}
}