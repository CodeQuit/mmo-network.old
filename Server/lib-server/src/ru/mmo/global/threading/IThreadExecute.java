package ru.mmo.global.threading;

import java.util.concurrent.ScheduledFuture;

/**
 * @author Felixx
 */
public interface IThreadExecute
{
	void execute(Runnable r);

	ScheduledFuture schedule(Runnable rub, long t1);
}