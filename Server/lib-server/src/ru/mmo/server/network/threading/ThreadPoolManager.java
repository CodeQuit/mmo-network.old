package ru.mmo.server.network.threading;

import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

import ru.mmo.global.threading.IThreadExecute;
import ru.mmo.global.threading.PriorityThreadFactory;
import ru.mmo.server.configs.DevelopConfig;

public class ThreadPoolManager implements IThreadExecute
{
	private ScheduledThreadPoolExecutor _poolExecute;

	private static ThreadPoolManager _instance;

	public static ThreadPoolManager getInstance()
	{
		if(_instance == null)
			_instance = new ThreadPoolManager();
		return _instance;
	}

	ThreadPoolManager()
	{
		_poolExecute = new ScheduledThreadPoolExecutor(DevelopConfig.POOL_SIZE, new PriorityThreadFactory("General Packet Pool", Thread.NORM_PRIORITY + 2));
	}

	public void shutdown()
	{
		try
		{
			_poolExecute.awaitTermination(1, TimeUnit.SECONDS);
			_poolExecute.shutdown();

			System.out.println("All ThreadPools are now stoped.");
		}
		catch(InterruptedException e)
		{
			e.printStackTrace();
		}
	}

	@Override
	public void execute(Runnable r)
	{
		_poolExecute.execute(r);
	}

	@Override
	public ScheduledFuture schedule(Runnable rub, long t1)
	{
		return _poolExecute.schedule(rub, t1, TimeUnit.MILLISECONDS);
	}
}