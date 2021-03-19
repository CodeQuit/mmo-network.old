package ru.mmo.global.network.engine.core.interfaces;

import java.util.EventListener;

import ru.mmo.global.network.engine.NioService;

/**
 * @author: Felixx
 */
public interface NioServiceListener extends EventListener
{
	/**
	 * вызывается когда сервис стартовал, забинден хост либо приконектилось к хосту
	 * 
	 * @param service
	 */
	public void serviceActivated(NioService service);

	/**
	 * аналогично выше ток наоборот
	 * 
	 * @param service
	 */
	public void serviceDeactivated(NioService service);

	/**
	 * устанавливает в листенер режим шатдауна
	 * 
	 * @param val
	 */
	public void serviceShutdown(boolean val);

}
