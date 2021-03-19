package ru.mmo.global.info;

import java.lang.management.ManagementFactory;
import java.lang.management.MemoryUsage;

/**
 * @author: Felixx
 */
public class MemoryInfo
{
	public static double getMemoryUsagePercent()
	{
		MemoryUsage heapMemoryUsage = ManagementFactory.getMemoryMXBean().getHeapMemoryUsage();
		return 100.0F * (float) heapMemoryUsage.getUsed() / (float) heapMemoryUsage.getMax();
	}

	public static double getMemoryFreePercent()
	{
		return 100.0D - getMemoryUsagePercent();
	}

	public static String getMemoryMaxMb()
	{
		return getMemoryMax() / 1048576L + " Мб";
	}

	public static String getMemoryUsedMb()
	{
		return getMemoryUsed() / 1048576L + " Мб";
	}

	public static long getMemoryMax()
	{
		return ManagementFactory.getMemoryMXBean().getHeapMemoryUsage().getMax();
	}

	public static long getMemoryUsed()
	{
		return ManagementFactory.getMemoryMXBean().getHeapMemoryUsage().getUsed();
	}
}