package ru.mmo.server.model;

import org.apache.log4j.Logger;

import ru.mmo.server.network.clients.MMOClient;
import ru.mmo.server.network.engine.ServerMMo.packets.UpdateServerToUpdateClientPacket;

/**
 * TODO: Нужно ли все это?
 * 
 * @author DarkSkeleton
 *
 */
public class MMoPlayer 
{
	private static final Logger _log = Logger.getLogger(MMoPlayer.class);
	private String _accountName;
	private MMOClient _connection;
	private boolean _isConnected = true;
	
	public MMoPlayer(long objectId, String accountName)
	{
		//super();
		_accountName = accountName;
	}
	
	public MMOClient getClient()
	{
		return _connection;
	}
	
	public void close()
	{
		if(_connection != null)
			_connection.close(false);
	}
	
	public synchronized void sendPacket(final Iterable<UpdateServerToUpdateClientPacket> packets)
	{
		if(_isConnected)
		{
			try
			{
				if(_connection != null)
				{
					for(UpdateServerToUpdateClientPacket packet : packets)
						_connection.sendPacket(packet);
				}
			}
			catch(final Exception e)
			{
				_log.info("Problem with send packet " + e, e);
			}
		}
	}

	public void sendPacket(UpdateServerToUpdateClientPacket... packets)
	{
		if(_isConnected)
		{
			try
			{
				if(_connection != null)
				{
					for(UpdateServerToUpdateClientPacket packet : packets)
					{
						if(packet == null)
						{
							_log.info("PACKET = NULL");
							continue;
						}
						_connection.sendPacket(packet);
					}
				}
				else
				{
					_log.info("CONNECTION = NULL");
				}
			}
			catch(final Exception e)
			{
				_log.info("Problem with send packet " + e, e);
			}
		}
	}
	
}
