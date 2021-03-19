package ru.mmo.global.threading;

import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

/**
 * @author Felixx
 */
public class ParallelExecutor
{
	private ThreadPoolExecutor executor;

	public ParallelExecutor(String name, int prio, int cores)
	{
		executor = new ThreadPoolExecutor(cores, cores, 1L, TimeUnit.SECONDS, new LinkedBlockingQueue<Runnable>(), new PriorityThreadFactory(name, prio));
	}

	public ParallelExecutor(String name, int prio)
	{
		this(name, prio, Runtime.getRuntime().availableProcessors());
	}

	public ParallelExecutor(String name)
	{
		this(name, 5);
	}

	public ParallelExecutor(int prio)
	{
		this("Paralel Executor", prio);
	}

	public ParallelExecutor()
	{
		this("Paralel Executor", 5);
	}

	public boolean waitForFinishAndDestroy(long timeout, TimeUnit unit) throws InterruptedException
	{
		executor.shutdown();
		if(executor.awaitTermination(timeout, unit))
		{
			executor = null;
			return true;
		}
		return false;
	}

	public boolean waitForFinishAndDestroy() throws InterruptedException
	{
		return waitForFinishAndDestroy(1L, TimeUnit.DAYS);
	}

	public void execute(Runnable r)
	{
		executor.execute(r);
	}
}