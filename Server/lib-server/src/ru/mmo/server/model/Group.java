package ru.mmo.server.model;

/**
 * @author: DarkSkeleton
 */
public class Group
{
	private long user_id;
	private long group_id;

	public long getUserId()
	{
		return user_id;
	}

	public void setUserId(long id)
	{
		user_id = id;
	}

	public long getGroupId()
	{
		return group_id;
	}

	public void setGroupId(long id)
	{
		group_id = id;
	}
}