package ru.mmo.global.network.engine.core.interfaces;

import java.nio.channels.SocketChannel;

/**
 * Author: Felixx
 */
public interface IAcceptFilter
{
	public boolean accept(SocketChannel ch);
}
