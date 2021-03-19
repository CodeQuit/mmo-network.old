package ru.mmo.global.utils;

import java.util.List;

public class Rnd
{
	private static MTRandom _rnd = new MTRandom();

	public static float get() // get random number from 0 to 1
	{
		return _rnd.nextFloat();
	}

	public static int get(int n)
	{
		return (int) Math.floor(_rnd.nextDouble() * n);
	}

	public static byte get(byte n)
	{
		return (byte) Math.floor(_rnd.nextDouble() * n);
	}

	public static long get(long n)
	{
		return (long) Math.floor(_rnd.nextDouble() * n);
	}

	public static long get(long min, long max)
	{
		return min + (long) Math.floor(_rnd.nextDouble() * (max - min + 1));
	}

	public static int get(int min, int max) // get random number from min to max (not max-1 !)
	{
		return min + (int) Math.floor(_rnd.nextDouble() * (max - min + 1));
	}

	public static byte get(byte min, byte max) // get random number from min to max (not max-1 !)
	{
		return (byte) (min + Math.floor(_rnd.nextDouble() * (max - min + 1)));
	}

	public static int get(int[] array)
	{
		if(ArrayUtils.isEmpty(array))
		{
			return 0;
		}

		if(array.length == 1)
		{
			return array[0];
		}

		return array[get(array.length)];
	}

	public static <T>T get(T[] array)
	{
		if(ArrayUtils.isEmpty(array))
		{
			return null;
		}

		if(array.length == 1)
		{
			return array[0];
		}

		return array[get(array.length)];
	}

	public static <T>T get(List<T> collection)
	{
		if(collection.isEmpty())
		{
			return null;
		}

		if(collection.size() == 1)
		{
			return collection.get(0);
		}

		return collection.get(get(collection.size()));
	}

	public static int nextInt()
	{
		return _rnd.nextInt();
	}

	public static int nextInt(int n)
	{
		return (int) Math.floor(_rnd.nextDouble() * n);
	}

	public static double nextDouble()
	{
		return _rnd.nextDouble();
	}

	public static double nextGaussian()
	{
		return _rnd.nextGaussian();
	}

	public static boolean nextBoolean()
	{
		return _rnd.nextBoolean();
	}

	/**
	 * Рандомайзер для подсчета шансов.<br>
	 * Рекомендуется к использованию вместо Rnd.get()
	 * 
	 * @param chance
	 *            от 0 до 100
	 * @return true в случае успешного выпадания.
	 *         <li>Если chance <= 0, вернет false
	 *         <li>Если chance >= 100, вернет true
	 */
	public static boolean chance(int chance)
	{
		return chance >= 1 && (chance > 99 || _rnd.nextInt(99) + 1 <= chance);
	}

	/**
	 * Рандомайзер для подсчета шансов.<br>
	 * Рекомендуется к использованию вместо Rnd.get() если нужны очень маленькие шансы
	 * 
	 * @param chance
	 *            от 0 до 100
	 * @return true в случае успешного выпадания.
	 *         <li>Если chance <= 0, вернет false
	 *         <li>Если chance >= 100, вернет true
	 */
	public static boolean chance(double chance)
	{
		return _rnd.nextDouble() <= chance / 100.;
	}
}