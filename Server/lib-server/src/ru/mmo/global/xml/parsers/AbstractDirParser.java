package ru.mmo.global.xml.parsers;

import java.io.File;
import java.io.FileInputStream;

import ru.mmo.global.xml.holder.AbstractHolder;

/**
 * @author Felixx
 */
public abstract class AbstractDirParser<H extends AbstractHolder> extends AbstractParser<H>
{
	private String _root;
	private String _ignoringFile;

	protected AbstractDirParser(String root, String ignoringFile, H holder)
	{
		_root = root;
		_ignoringFile = ignoringFile;
		_holder = holder;
		parse();
	}

	@Override
	protected final void parse()
	{
		parse(_root);

		if(_holder != null)
		{
			_holder.log();
		}
	}

	protected void parse(String root)
	{
		File file = null;
		try
		{
			File dir = new File(root);

			if( !dir.exists())
			{
				_log.info("Dir " + dir.getAbsolutePath() + " not exists");
				return;
			}

			File[] files = dir.listFiles();

			for(File f : files)
			{
				if( !f.isHidden())
				{
					if(f.isDirectory())
					{
						parse(f.getAbsolutePath());
					}
					else if(f.getName().endsWith(".xml") && !f.getName().equals(_ignoringFile)) // затичка для шаблона
					{
						try
						{
							file = f;

							parseDocument(new FileInputStream(file), file.getName());
						}
						catch(Exception e)
						{
							_log.info("Exception: " + e + " in file: " + f.getName(), e);
						}
					}
				}
			}
		}
		catch(Exception e)
		{
			_log.info("Exception: " + e, e);
		}
	}
}
