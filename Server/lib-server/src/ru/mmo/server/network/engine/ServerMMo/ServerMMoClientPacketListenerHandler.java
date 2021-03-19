package ru.mmo.server.network.engine.ServerMMo;

import org.apache.log4j.Logger;

import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.core.emuns.ConnectionState;
import ru.mmo.global.network.engine.core.interfaces.IPacketHandler;
import ru.mmo.global.network.utils.NetworkUtil;
import ru.mmo.server.network.clients.MMOClient;
import ru.mmo.server.network.engine.ServerMMo.packets.FromUpdateClientToUpdateServerPacket;
import ru.mmo.server.network.engine.ServerMMo.packets.receivable.packet_502_req;
import ru.mmo.server.network.engine.ServerMMo.packets.receivable.packet_504_req;
import ru.mmo.server.network.engine.ServerMMo.packets.receivable.packet_506_req;
import ru.mmo.server.network.engine.ServerMMo.packets.receivable.packet_508_req;

/**
 * @author: Felixx
 *          Слушает пакеты игроков.
 */
public class ServerMMoClientPacketListenerHandler implements IPacketHandler<MMOClient>
{
	private static Logger _log = Logger.getLogger(ServerMMoClientPacketListenerHandler.class);
	private boolean _aviableConnections = true;

	@Override
	public FromUpdateClientToUpdateServerPacket handle(NioBuffer buf, MMOClient client)
	{
		boolean SHOW_ALL_PACKET = false;
		if(SHOW_ALL_PACKET)
		{
			NioBuffer temp = buf;
			int allSize = temp.limit();
			int position = temp.position();
			int opcode = temp.getUnsignedShort();
			_log.info("Packet id: " + opcode + "; size: " + allSize + "; pos:" + position);
		}

		FromUpdateClientToUpdateServerPacket packet = null;
		ConnectionState state = client.getState();
		int opcode = buf.getUnsignedShort();
		_log.info("Packet id: " + opcode + "; size: " + buf.limit() + "; pos:" + buf.position());
		if(_aviableConnections)
		{
			/*
			 * _log.info("OPCODE: " + opcode);
			 * _log.info("ALLSIZE: " + buf.limit());
			 * _log.info("DATASIZE: " + (buf.limit() - 4));
			 * _log.info("CAPACITY: " + buf.capacity());
			 * _log.info("POSITION: " + buf.position());
			 * _log.info("INFO:\n" + NetworkUtil.printData(buf));
			 */
			// _log.info("OPCODE: " + opcode);
			switch(state)
			{
				case CONNECTED:
				{
					switch(opcode)
					{
						case 0:
						{
							_aviableConnections = false;
							break;
						}
						case 502:
						{
							packet = new packet_502_req();
							break;
						}
						case 504:
						{
							packet = new packet_504_req();
							break;
						}
						case 506:
						{
							packet = new packet_506_req();
							break;
						}
						case 508:
						{
							packet = new packet_508_req();
							break;
						}
						default:
						{
							debugOpcode(opcode, state, client, buf);
						}
					}
					break;
				}
			}
			return packet;
		}
		return null;
	}

	private void debugOpcode(int opcode, ConnectionState state, MMOClient client, NioBuffer buf)
	{
		_log.info("Unknown Opcode: " + opcode + " for state: " + state.name() + " from IP: " + client.getIP());
		_log.info(NetworkUtil.printData(buf));
	}
}