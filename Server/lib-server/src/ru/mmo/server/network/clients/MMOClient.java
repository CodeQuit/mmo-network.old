package ru.mmo.server.network.clients;

import ru.mmo.global.network.engine.NioClient;
import ru.mmo.global.network.engine.NioService;
import ru.mmo.global.network.engine.NioSession;
import ru.mmo.global.network.engine.core.interfaces.IPacketExecutor;
import ru.mmo.server.model.MMoPlayer;
import ru.mmo.server.network.engine.ServerMMo.packets.sendable.INIT;

/**
 * Клиент mmo.
 * 
 * @author Felixx
 */
public class MMOClient extends NioClient<MMOClient>
{
	private String account;
	private long group_id;
	private MMoPlayer _player;

	public MMOClient(NioSession<MMOClient> session, NioService service, IPacketExecutor executor)
	{
		super(session, service, executor);

		String ip = getIPString();
		// if(IPManager.getInstance().checkIP(ip))
		// {
		// close(true);
		// _log.info("Drop connection from banned IP: " + ip);
		// }
	}

	public MMoPlayer getMMoPlayer()
	{
		return _player;
	}
	
	@Override
	public int getCryptKey()
	{
		return 8080;
	}

	@Override
	public int getId()
	{
		return hashCode() / 4096; // 5404;
	}

	public long getGroupID()
	{
		return group_id;
	}
	
	public void setGroupId(long id)
	{
		group_id = id;
	}
	
	public void setAccount(String user)
	{
		account = user;
	}

	public String getAccount()
	{
		return account;
	}

	@Override
	public void onStart()
	{
		_log.info("onStart: " + getIP());
		sendPacket(new INIT());
	}

	@Override
	public void onEnd()
	{
		_log.info("onEnd: " + getIP());
	}

	@Override
	public void onForceEnd()
	{
		_log.info("onForceEnd: " + getIP());
	}

	@Override
	public void catchException(Throwable cases)
	{
		_log.info("Exception: " + cases);
	}
}