package ru.mmo.global.network.protocols;

import ru.mmo.global.network.engine.NioSession;
import ru.mmo.global.network.engine.buffer.NioBuffer;
import ru.mmo.global.network.engine.core.Protocol;
import ru.mmo.global.network.utils.NetworkUtil;
import ru.mmo.server.configs.DevelopConfig;

/**
 * @author: Felixx
 */
public class MMoProtocol extends Protocol
{
	@Override
	public void decode(NioSession session, NioBuffer buf)
	{
		if(DevelopConfig.NETWORK_DEBUG)
		{
			_log.info("Принимаем:" + buf.limit() + " Доступно:" + (buf.limit() - 2));
			_log.info("INFO:\n" + NetworkUtil.printData(buf));
		}
	}

	@Override
	public void encode(NioSession session, NioBuffer buf)
	{
		if(DevelopConfig.NETWORK_DEBUG)
		{
			_log.info("Отправляем:" + buf.limit());
			_log.info("INFO:\n" + NetworkUtil.printData(buf));
		}
	}
}