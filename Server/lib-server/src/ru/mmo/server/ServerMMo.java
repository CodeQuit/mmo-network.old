package ru.mmo.server;

import org.apache.log4j.Logger;

import ru.mmo.global.configs.DataBaseConfig;
import ru.mmo.global.dao.DAOManager;
import ru.mmo.global.dbc.DatabaseFactory;
import ru.mmo.global.info.PrintInfo;
import ru.mmo.global.logging.LoggingService;
import ru.mmo.server.configs.DevelopConfig;
import ru.mmo.server.configs.NetworkConfig;
import ru.mmo.server.configs.ServerConfig;
import ru.mmo.server.controllers.AuthController;
import ru.mmo.server.network.NetworkEngine;

/**
 * @author Felixx
 */
public class ServerMMo
{
	private static final Logger _log = Logger.getLogger(ServerMMo.class);

	public static void main(String... args) throws Exception
	{
		PrintInfo.getInstance();

		LoggingService.load();

		PrintInfo.printLines("Loadig configs", "Begin");
		DevelopConfig.load();
		ServerConfig.load();
		NetworkConfig.load();
		DataBaseConfig.load();
		PrintInfo.printLines("Loadig configs", "End");
		PrintInfo.printEmptyLine();

		PrintInfo.printLines("Init DataBase", "Begin");
		DatabaseFactory.getInstance();
		DAOManager.getInstance().load("server");
		PrintInfo.printLines("Init DataBase", "End");

		AuthController.load();

		PrintInfo.printLines("Init Nerwork engine", "Begin");
		NetworkEngine.getInstance();
		PrintInfo.printLines("Init Nerwork engine", "End");
		PrintInfo.printEmptyLine();

		PrintInfo.printLibInfo(_log);
		PrintInfo.printEmptyLine();
		PrintInfo.getInstance().info();
	}
}