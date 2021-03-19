package ru.mmo.global.network.engine.core;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.NioSession;
import ru.mmo.global.network.engine.buffer.NioBuffer;

/**
 * Author: Felixx
 */
public class Protocol
{
	protected final Logger _log = Logger.getLogger(getClass());

	/**
	 * Simple implemention of decode
	 * 
	 * @param session
	 * @param buf
	 * @return
	 */
	public void decode(NioSession session, NioBuffer buf)
	{}

	/**
	 * Simple implementiom of encode
	 * 
	 * @param session
	 * @param buf
	 * @return
	 */
	public void encode(NioSession session, NioBuffer buf)
	{}
}