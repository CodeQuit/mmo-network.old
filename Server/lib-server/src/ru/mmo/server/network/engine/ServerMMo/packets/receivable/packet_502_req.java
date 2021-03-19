package ru.mmo.server.network.engine.ServerMMo.packets.receivable;

import ru.mmo.global.network.engine.core.emuns.ConnectionState;
import ru.mmo.server.controllers.AuthController;
import ru.mmo.server.controllers.AuthController.State;
import ru.mmo.server.model.enums.LoginAccess;
import ru.mmo.server.network.clients.MMOClient;
import ru.mmo.server.network.engine.ServerMMo.packets.FromUpdateClientToUpdateServerPacket;
import ru.mmo.server.network.engine.ServerMMo.packets.sendable.packet_502_ack;

public class packet_502_req extends FromUpdateClientToUpdateServerPacket
{

	private int login_len;
	private int password_len;
	private String login;
	private String password;

	@Override
	public void readImpl()
	{
		//if(_buf.hasRemaining())
		//{
			login_len = readC();
			password_len = readC();
			login = readS(login_len);
			password = readS(password_len);
		//}
	}

	@Override
	public void runImpl()
	{
		_log.info("Login: " + login);
		_log.info("Password: " + password);

		MMOClient client = getClient();
		AuthController lc = AuthController.getInstance();

		State status = lc.tryAuthLogin(login, password, client);

		_log.info("status: " + status);
		switch(status)
		{
			case VALID:
				client.setAccount(login);
				client.setState(ConnectionState.AUTHED);
				client.sendPacket(new packet_502_ack(LoginAccess.EVENT_ERROR_SUCCESS.get()));
				break;

			case WRONG:
				client.sendPacket(new packet_502_ack(LoginAccess.EVENT_ERROR_EVENT_LOG_IN_INVALID_ACCOUNT.get()));
				break;
			case BANNED:
				client.sendPacket(new packet_502_ack(LoginAccess.EVENT_ERROR_EVENT_LOG_IN_BLOCK_ACCOUNT.get()));
				break;
			case IP_ACCESS_DENIED:
				client.sendPacket(new packet_502_ack(LoginAccess.EVENT_ERROR_EVENT_LOG_IN_TIME_OUT2.get()));
				break;
			case NOT_PAID:
				client.sendPacket(new packet_502_ack(LoginAccess.EVENT_ERROR_LOGIN.get()));
				break;
			case IN_USE:
				client.sendPacket(new packet_502_ack(LoginAccess.EVENT_ERROR_EVENT_LOG_IN_ALEADY_LOGIN.get()));
				break;
		}
	}
}
