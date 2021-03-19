package ru.mmo.global.utils;

/**
 * @author Felixx
 */
public enum ExitCode
{
	/**
	 * Указывает, что сервер успешно завершил свою работу.<br>
	 */
	CODE_NORMAL(0),

	/**
	 * Указывает, что сервер завершился с ошибкой.<br>
	 */
	CODE_ERROR(1),

	/**
	 * Указывает, что сервер успешно завершил свою работу и должен быть перезапущен.<br>
	 */
	CODE_RESTART(2);

	private int _id;

	ExitCode(int id)
	{
		_id = id;
	}

	public int getId()
	{
		return _id;
	}
}
