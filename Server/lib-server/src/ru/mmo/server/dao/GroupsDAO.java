package ru.mmo.server.dao;

import ru.mmo.server.model.Group;

/**
 * @author: Felixx, DarkSkeleton
 */
public interface GroupsDAO
{
	Group select(long id);
}