package ru.mmo.server.model;

public class Log 
{
	private String _text;
	private String _acc;
	
	public String getText()
	{
		return _text;
	}
	
	public void setText(String t)
	{
		_text = t;
	}
	
	public String getAccount()
	{
		return _acc;
	}
	
	public void setAccount(String acc)
	{
		_acc = acc; 
	}
}
