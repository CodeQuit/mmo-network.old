package ru.mmo.server.dao;

import ru.mmo.server.model.Account;

/**
 * @author: Felixx, DarkSkeleton
 */
public interface AccountsDAO
{
	Account select(String account);

	void insert(Account acc);

	int getMoney(String acc);

	void updateMoney(String acc, int money);

	void updateLastActiveIP(Account acc);

	void updateLastServer(String acc, int server);

	void updatePlayersInAccount(String login, String player, int server, byte type);

	int getLastServer(String account);
}