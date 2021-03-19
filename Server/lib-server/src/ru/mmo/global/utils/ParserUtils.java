package ru.mmo.global.utils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import org.w3c.dom.Node;

/**
 * @author Felixx
 */
public class ParserUtils
{
	public static List<Node> nodes(Node parent, String name)
	{
		List<Node> nodeList = new ArrayList<Node>(4);
		for(Node second = parent.getFirstChild(); second != null; second = second.getNextSibling())
		{
			if(second.getNodeName().equalsIgnoreCase(name))
				nodeList.add(second);
		}

		return nodeList.isEmpty() ? Collections.<Node> emptyList() : nodeList;
	}

	public static short shortValue(Node node)
	{
		return Short.parseShort(node.getNodeValue());
	}

	public static long longValue(Node node)
	{
		return Long.parseLong(node.getNodeValue());
	}

	public static long longValue(Node node, long def)
	{
		return node == null ? def : Long.parseLong(node.getNodeValue());
	}

	public static boolean boolValue(Node node)
	{
		return Boolean.parseBoolean(node.getNodeValue());
	}

	public static boolean boolValue(Node node, boolean def)
	{
		return node == null ? def : Boolean.parseBoolean(node.getNodeValue());
	}

	public static boolean boolValue(String node)
	{
		return Boolean.parseBoolean(node);
	}

	public static boolean boolValue(String t, boolean def)
	{
		return t == null ? def : Boolean.parseBoolean(t);
	}

	public static float floatValue(Node node, float def)
	{
		return node == null ? def : floatValue(node);
	}

	public static float floatValue(Node node)
	{
		return Float.parseFloat(node.getNodeValue());
	}

	public static double doubleValue(Node node, double def)
	{
		return node == null ? def : doubleValue(node);
	}

	public static double doubleValue(Node node)
	{
		return Double.parseDouble(node.getNodeValue());
	}

	public static byte byteValue(Node node)
	{
		return Byte.parseByte(node.getNodeValue());
	}

	public static byte byteValue(Node node, byte def)
	{
		return node == null ? def : byteValue(node);
	}

	public static byte byteValue(String node)
	{
		return Byte.parseByte(node);
	}

	public static String stringValue(Node node)
	{
		return String.valueOf(node.getNodeValue());
	}

	public static String stringValue(Node node, String def)
	{
		return node == null ? def : stringValue(node);
	}

	public static String stringValue(String t)
	{
		return t;
	}

	public static String stringValue(String t, String def)
	{
		return t == null ? def : stringValue(t);
	}

	public static int intValue(Node node)
	{
		return Integer.parseInt(node.getNodeValue());
	}

	public static int intValue(String node)
	{
		return Integer.parseInt(node);
	}

	public static int intValue(String s, int defaultf)
	{
		return s == null ? defaultf : intValue(s);
	}

	public static int intValue(Node node, int defaultf)
	{
		return node == null ? defaultf : intValue(node);
	}

	public static <T extends Enum<T>>T enumValue(Node node, Class<T> clazz)
	{
		return T.valueOf(clazz, node.getNodeValue());
	}

	public static <T extends Enum<T>>T enumValue(Node node, Class<T> clazz, T def)
	{
		return node == null ? def : enumValue(node, clazz);
	}

	public static <T extends Enum<T>>T enumValue(String t, Class<T> clazz)
	{
		return T.valueOf(clazz, t);
	}

	public static <T extends Enum<T>>T enumValue(String t, Class<T> clazz, T def)
	{
		return t == null ? def : enumValue(t, clazz);
	}
}