package ru.mmo.global.configs;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Properties;
import java.util.regex.Pattern;

/**
 * @author Felixx
 */
public final class Config
{
	private static Properties iniProperty = new Properties();

	private static String DEFAULT_CHARSET = "UTF-8";
	private static Pattern BOOLEAN_REGEX = Pattern.compile("^true|yes|on|t|y|1$", Pattern.CASE_INSENSITIVE);

	public Config(String fname, String charset) throws IOException
	{
		parse(fname, charset);
	}

	public Config(String fname) throws IOException
	{
		parse(fname, null);
	}

	private void parse(String fname, String charset) throws IOException
	{
		BufferedReader br = new BufferedReader(new InputStreamReader(new FileInputStream(fname), charset == null ? DEFAULT_CHARSET : charset));

		try
		{
			String section = "";

			String line;

			while((line = br.readLine()) != null)
			{
				if(line.startsWith(";"))
				{
					continue;
				}

				if(line.startsWith("["))
				{
					section = line.substring(1, line.lastIndexOf("]")).trim();
					continue;
				}

				addProperty(section, line);
			}
		}
		finally
		{
			br.close();
		}
	}

	private void addProperty(String section, String line)
	{
		int equalIndex = line.indexOf("=");

		if(equalIndex > 0)
		{
			String name = section + '.' + line.substring(0, equalIndex).trim();
			String value = line.substring(equalIndex + 1).trim();

			iniProperty.put(name, value);
		}
	}

	public String getStringValue(String section, String var, String def)
	{
		return iniProperty.getProperty(section + '.' + var, def);
	}

	private String getStringValue(String section, String var)
	{
		return iniProperty.getProperty(section + '.' + var);
	}

	public byte getByteValue(String section, String var, byte def)
	{
		String sval = getStringValue(section, var);
		return sval == null ? def : Byte.decode(sval).byteValue();
	}

	public short getShortValue(String section, String var, short def)
	{
		String sval = getStringValue(section, var);
		return sval == null ? def : Short.decode(sval).shortValue();
	}

	public int getIntegerValue(String section, String var, int def)
	{
		String sval = getStringValue(section, var);
		return sval == null ? def : Integer.decode(sval).intValue();
	}

	public long getLongValue(String section, String var, long def)
	{
		String sval = getStringValue(section, var);
		return sval == null ? def : Long.decode(sval).longValue();
	}

	public float getFloatValue(String section, String var, float def)
	{
		String sval = getStringValue(section, var);
		return sval == null ? def : Float.parseFloat(sval);
	}

	public double getDoubleValue(String section, String var, double def)
	{
		String sval = getStringValue(section, var);
		return sval == null ? def : Double.parseDouble(sval);
	}

	public boolean getBooleanValue(String section, String var, boolean def)
	{
		String sval = getStringValue(section, var);
		return sval == null ? def : BOOLEAN_REGEX.matcher(sval).matches();
	}

}