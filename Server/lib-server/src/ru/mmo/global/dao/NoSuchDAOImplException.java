package ru.mmo.global.dao;

import java.util.NoSuchElementException;

/**
 * @author Felixx
 */
@SuppressWarnings("serial")
public class NoSuchDAOImplException extends NoSuchElementException
{
	public NoSuchDAOImplException(String t)
	{
		super(t);
	}
}
