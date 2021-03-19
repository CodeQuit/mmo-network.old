package ru.mmo.server.utils;

/**
 * Author: Felixx
 */
public class FailedAuthAttempt
{
	private int _count;
	private long _lastAttempTime;

	public FailedAuthAttempt()
	{
		_count = 1;
		_lastAttempTime = System.currentTimeMillis();
	}

	public void increaseCounter()
	{
		if(System.currentTimeMillis() - _lastAttempTime < 300 * 1000)
			_count++;
		else
			_count = 1;

		_lastAttempTime = System.currentTimeMillis();
	}

	public int getCount()
	{
		return _count;
	}
}