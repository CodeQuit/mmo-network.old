package ru.mmo.global.xml.parsers;

import java.io.File;
import java.io.FileInputStream;

import ru.mmo.global.xml.holder.AbstractHolder;

/**
 * @author Felixx
 */
public abstract class AbstractFileParser<H extends AbstractHolder> extends AbstractParser<H>
{
	private String _file;

	protected AbstractFileParser(String file, H holder)
	{
		_file = file;
		_holder = holder;

		parse();
	}

	@Override
	protected final void parse()
	{
		try
		{
			File file = new File(_file);

			if( !file.exists())
			{
				_log.info("file " + file.getAbsolutePath() + " not exists");
				return;
			}

			parseDocument(new FileInputStream(file), file.getName());
		}
		catch(Exception e)
		{
			_log.info("Exception: " + e, e);
		}

		if(_holder != null)
		{
			_holder.log();
		}
	}
}
