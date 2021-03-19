package ru.mmo.global.xml.parsers;

import java.io.InputStream;

import javax.xml.parsers.DocumentBuilderFactory;

import org.apache.log4j.Logger;
import org.w3c.dom.Document;
import org.w3c.dom.Node;

import ru.mmo.global.xml.holder.AbstractHolder;
import ru.mmo.server.configs.DevelopConfig;

/**
 * @author Felixx
 */
public abstract class AbstractParser<H extends AbstractHolder>
{
	protected final Logger _log = Logger.getLogger(getClass());
	protected H _holder;

	protected static final boolean DEBUG = DevelopConfig.DEBUG;
	protected static final boolean SAVE_PROBLEM = DevelopConfig.DEVELOP_SAVE_PROBLEMS;

	protected void parseDocument(InputStream f, String name) throws Exception
	{
		DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
		factory.setValidating(false);
		factory.setIgnoringComments(true);
		Document doc = factory.newDocumentBuilder().parse(f);

		for(Node start0 = doc.getFirstChild(); start0 != null; start0 = start0.getNextSibling())
		{
			readData(start0, name);
		}

		f.close();
	}

	protected void readData(Node node, String file) throws Exception
	{
		readData(node);
	}

	protected void readData(Node node) throws Exception
	{
		// NO BODY
	}

	protected abstract void parse();

	protected H getHolder()
	{
		return _holder;
	}

	public void reload()
	{
		info("reload start...");
		_holder.clear();
		parse();
	}

	public void info(String st, Exception e)
	{
		_log.info(st, e);
	}

	public void info(String st)
	{
		_log.info(st);
	}
}