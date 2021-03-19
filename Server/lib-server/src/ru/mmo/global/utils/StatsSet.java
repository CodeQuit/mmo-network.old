package ru.mmo.global.utils;

import gnu.trove.list.TIntList;
import gnu.trove.list.array.TIntArrayList;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * @author mkizub, Felixx
 *         <BR>
 *         This class is used in order to have a set of couples (key,value).<BR>
 *         Methods deployed are accessors to the set (add/get value from its key) and addition of a whole set in the current one.
 */
@SuppressWarnings("serial")
public class StatsSet extends HashMap<String, Object>
{
	private boolean _onGetRemove;

	public StatsSet()
	{
		super(0);
	}

	public StatsSet(Map<String, Object> clone)
	{
		super(clone);
	}

	public Object getObject(String name)
	{
		if(containsKey(name))
		{
			if(_onGetRemove)
				return remove(name);
			else
				return get(name);
		}
		else
			return null;
	}

	/**
	 * Return the boolean associated to the key put in parameter ("name")
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @return boolean : value associated to the key
	 */
	public boolean getBool(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Boolean value required, but not specified");

		if(val instanceof Boolean)
			return (Boolean) val;

		try
		{
			return Boolean.parseBoolean((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Boolean value required, but found: " + val);
		}
	}

	/**
	 * Return the boolean associated to the key put in parameter ("name"). If the value associated to the key is null, this method returns the value of the parameter
	 * deflt.
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param deflt
	 *            : boolean designating the default value if value associated with the key is null
	 * @return boolean : value of the key
	 */
	public boolean getBool(String name, boolean deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Boolean)
			return (Boolean) val;

		try
		{
			return Boolean.parseBoolean((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Boolean value required, but found: " + val);
		}
	}

	/**
	 * Returns the int associated to the key put in parameter ("name").
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @return int : value associated to the key
	 */
	public int getInt(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Integer value required, but not specified");

		if(val instanceof Number)
			return ((Number) val).intValue();

		try
		{
			return Integer.parseInt((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	public short getShort(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Integer value required, but not specified");

		if(val instanceof Number)
			return ((Number) val).shortValue();

		try
		{
			return Short.parseShort((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	public Short getShort(String name, Short deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Number)
			return ((Number) val).shortValue();

		try
		{
			return Short.parseShort((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	public byte getByte(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Integer value required, but not specified");

		if(val instanceof Number)
			return ((Number) val).byteValue();

		try
		{
			return Byte.parseByte((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	public Byte getByte(String name, Byte deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Number)
			return ((Number) val).byteValue();

		try
		{
			return Byte.parseByte((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	public String[] getStringArray(String name, String[] deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		try
		{
			return String.valueOf(val).split(";");
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("StringArray value required, but found: " + val);
		}
	}

	public List<Integer> getIntegerList(String name)
	{
		List<Integer> array = new ArrayList<Integer>();
		Object val = getObject(name);
		if(val == null)
			return Collections.emptyList();

		try
		{
			for(String s : String.valueOf(val).split(";"))
			{
				if( !s.isEmpty())
					array.add(Integer.parseInt(s));
			}
			return array;
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("IntegerList value required, but found: " + val);
		}
	}

	public TIntList getTIntegerList(String name)
	{
		TIntList array = new TIntArrayList();
		Object val = getObject(name);
		if(val == null)
			return new TIntArrayList(0);

		try
		{
			for(String s : String.valueOf(val).split(";"))
			{
				if( !s.isEmpty())
					array.add(Integer.parseInt(s));
			}
			return array;
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("TIntList value required, but found: " + val);
		}
	}

	public List<String> getStringList(String name)
	{
		List<String> array = new ArrayList<String>();
		Object val = getObject(name);
		if(val == null || name.isEmpty())
			return Collections.emptyList();

		try
		{
			for(String s : ((String) val).split(";"))
			{
				if( !s.isEmpty())
					array.add(s);
			}
			return array;
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("StringList value required, but found: " + val);
		}
	}

	/**
	 * Returns the int associated to the key put in parameter ("name"). If the value associated to the key is null, this method returns the value of the parameter
	 * deflt.
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param deflt
	 *            : int designating the default value if value associated with the key is null
	 * @return int : value associated to the key
	 */
	public int getInt(String name, int deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Number)
			return ((Number) val).intValue();

		try
		{
			return Integer.parseInt((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	/**
	 * Returns the float associated to the key put in parameter ("name").
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @return float : value associated to the key
	 */
	public float getFloat(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Float value required, but not specified");

		if(val instanceof Number)
			return ((Number) val).floatValue();

		try
		{
			return (float) Double.parseDouble((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Float value required, but found: " + val);
		}
	}

	/**
	 * Returns the float associated to the key put in parameter ("name"). If the value associated to the key is null, this method returns the value of the parameter
	 * deflt.
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param deflt
	 *            : float designating the default value if value associated with the key is null
	 * @return float : value associated to the key
	 */
	public float getFloat(String name, float deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Number)
			return ((Number) val).floatValue();

		try
		{
			return (float) Double.parseDouble((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Float value required, but found: " + val);
		}
	}

	/**
	 * Returns the double associated to the key put in parameter ("name").
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @return double : value associated to the key
	 */
	public double getDouble(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Float value required, but not specified");

		if(val instanceof Number)
			return ((Number) val).doubleValue();

		try
		{
			return Double.parseDouble((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Float value required, but found: " + val);
		}
	}

	/**
	 * Returns the double associated to the key put in parameter ("name"). If the value associated to the key is null, this method returns the value of the parameter
	 * deflt.
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param deflt
	 *            : float designating the default value if value associated with the key is null
	 * @return double : value associated to the key
	 */
	public double getDouble(String name, double deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Number)
			return ((Number) val).doubleValue();

		try
		{
			return Double.parseDouble((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Float value required, but found: " + val);
		}
	}

	/**
	 * Returns the String associated to the key put in parameter ("name").
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @return String : value associated to the key
	 */
	public String getString(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("String value required, but not specified: " + name);

		return String.valueOf(val);
	}

	/**
	 * Returns the String associated to the key put in parameter ("name"). If the value associated to the key is null, this method returns the value of the parameter
	 * deflt.
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param deflt
	 *            : String designating the default value if value associated with the key is null
	 * @return String : value associated to the key
	 */
	public String getString(String name, String deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		return String.valueOf(val);
	}

	/**
	 * Returns the double associated to the key put in parameter ("name").
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @return double : value associated to the key
	 */
	public long getLong(String name)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Integer value required, but not specified");

		if(val instanceof Number)
			return ((Number) val).longValue();

		try
		{
			return Long.parseLong((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	/**
	 * Returns the Long associated to the key put in parameter ("name"). If the value associated to the key is null, this method returns the value of the parameter
	 * deflt.
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param deflt
	 *            : Long designating the default value if value associated with the key is null
	 * @return Long : value associated to the key
	 */
	public long getLong(String name, Long deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Number)
			return ((Number) val).longValue();

		try
		{
			return Long.parseLong((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	/**
	 * Returns the Long associated to the key put in parameter ("name"). If the value associated to the key is null, this method returns the value of the parameter
	 * deflt.
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param deflt
	 *            : Long designating the default value if value associated with the key is null
	 * @return Long : value associated to the key
	 */
	public Long getLong2(String name, Long deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(val instanceof Number)
			return ((Number) val).longValue();

		try
		{
			return Long.parseLong((String) val);
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Integer value required, but found: " + val);
		}
	}

	/**
	 * Returns an enumeration of &lt;T&gt; from the set
	 * 
	 * @param <T>
	 *            : Class of the enumeration returned
	 * @param name
	 *            : String designating the key in the set
	 * @param enumClass
	 *            : Class designating the class of the value associated with the key in the set
	 * @return Enum<T>
	 */
	@SuppressWarnings(value = {"unchecked"})
	public <T extends Enum<T>>T getEnum(String name, Class<T> enumClass)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Enum value of type " + enumClass.getName() + " required, but not specified");

		if(enumClass.isInstance(val))
			return (T) val;

		try
		{
			return Enum.valueOf(enumClass, String.valueOf(val));
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Enum value of type " + enumClass.getName() + "required, but found: " + val);
		}
	}

	@SuppressWarnings(value = {"unchecked"})
	public <T extends Enum<T>>T getEnumUpperCase(String name, Class<T> enumClass)
	{
		Object val = getObject(name);
		if(val == null)
			throw new IllegalArgumentException("Enum value of type " + enumClass.getName() + " required, but not specified");

		if(enumClass.isInstance(val))
			return (T) val;

		try
		{
			return Enum.valueOf(enumClass, String.valueOf(val).toUpperCase());
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Enum value of type " + enumClass.getName() + "required, but found: " + val);
		}
	}

	/**
	 * Returns an enumeration of &lt;T&gt; from the set. If the enumeration is empty, the method returns the value of the parameter "deflt".
	 * 
	 * @param <T>
	 *            : Class of the enumeration returned
	 * @param name
	 *            : String designating the key in the set
	 * @param enumClass
	 *            : Class designating the class of the value associated with the key in the set
	 * @param deflt
	 *            : <T> designating the value by default
	 * @return Enum<T>
	 */
	@SuppressWarnings(value = {"unchecked"})
	public <T extends Enum<T>>T getEnum(String name, Class<T> enumClass, T deflt)
	{
		Object val = getObject(name);
		if(val == null)
			return deflt;

		if(enumClass.isInstance(val))
			return (T) val;

		try
		{
			return Enum.valueOf(enumClass, String.valueOf(val));
		}
		catch(Exception e)
		{
			throw new IllegalArgumentException("Enum value of type " + enumClass.getName() + "required, but found: " + val);
		}
	}

	/**
	 * Add the String hold in param "value" for the key "name"
	 * 
	 * @param name
	 *            : String designating the key in the set
	 * @param value
	 *            : String corresponding to the value associated with the key
	 */
	public boolean set(String name, Object value)
	{
		return put(name, value) != null;
	}

	public Object unset(String name)
	{
		return remove(name);
	}

	public void cloneTo(StatsSet t)
	{
		for(Map.Entry<String, Object> entry : entrySet())
			t.put(entry.getKey(), entry.getValue());
	}

	public void setOnGetRemove(boolean onGetRemove)
	{
		_onGetRemove = onGetRemove;
	}

	@Override
	public StatsSet clone()
	{
		return new StatsSet(this);
	}
}