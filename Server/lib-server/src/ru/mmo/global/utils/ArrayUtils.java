package ru.mmo.global.utils;

import java.lang.reflect.Array;
import java.util.Collection;

/**
 * @author: Felixx
 */
public class ArrayUtils
{
	public static final Object[] EMPTY_OBJECTS = new Object[0];

	@SuppressWarnings("unchecked")
	public static <T>T[] addToArray(final T[] source, final T ins)
	{
		int length = source.length; // размер предыдущего масива и точка для записи новой записи

		T[] description = (T[]) Array.newInstance(source.getClass().getComponentType(), length + 1);
		System.arraycopy(source, 0, description, 0, length);
		description[length] = ins;

		return description;
	}

	/**
	 * Возвращает содержется ли это значение (int) в данном массиве.
	 * 
	 * @param array
	 *            - Сам массив.
	 * @param val
	 *            - Значение для поиска.
	 * @return boolean
	 */
	public static boolean isInArray(final int[] array, final int val)
	{
		if(array != null)
		{
			for(int $ : array)
			{
				if($ == val)
					return true;
			}
		}

		return false;
	}

	/**
	 * Возвращает содержется ли это значение (Object) в данном массиве.
	 * 
	 * @param array
	 *            - Сам массив.
	 * @param val
	 *            - Значение для поиска.
	 * @return boolean
	 */
	public static <T>boolean isInArray(final T[] array, final T val)
	{
		if(array != null)
		{
			for(T $ : array)
			{
				if($.equals(val))
					return true;
			}
		}

		return false;
	}

	/**
	 * Возвращает индекс, на котором расположено значение.
	 * 
	 * @param array
	 *            - Сам массив.
	 * @param val
	 *            - Значение для определения индекса.
	 * @return Положение значения в массиве.
	 */
	public static int indexOf(final int[] array, int val)
	{
		if(array != null)
		{
			for(int i = 0; i < array.length; i++)
			{
				if(array[i] == val)
				{
					return i;
				}
			}
		}

		return -1;
	}

	/**
	 * Пустой ли массив?
	 * 
	 * @param array
	 * @return
	 */
	public static boolean isEmpty(final int[] array)
	{
		return array == null || array.length == 0;
	}

	/**
	 * Пустой ли массив?
	 * 
	 * @param array
	 * @return
	 */
	public static <T>boolean isEmpty(final T[] array)
	{
		return array == null || array.length == 0;
	}

	/**
	 * Приравнивает всем значениям массива к null
	 * 
	 * @param array
	 */
	public static <T>void free(final T[] array)
	{
		if(isEmpty(array))
		{
			return;
		}

		for(int i = 0; i < array.length; i++)
		{
			array[i] = null;
		}
	}

	/**
	 * Переводит коллекцию в int массив.
	 * 
	 * @param t
	 * @return
	 */
	public static int[] intArray(Collection<? extends Number> t)
	{
		int[] array = new int[t.size()];
		int i = 0;
		for(Number $ : t)
		{
			array[i++] = $.intValue();
		}

		return array;
	}
}