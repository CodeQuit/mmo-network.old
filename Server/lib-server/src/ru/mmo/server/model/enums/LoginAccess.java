package ru.mmo.server.model.enums;

public enum LoginAccess
{
	/** Успешная авторизация. */
	EVENT_ERROR_SUCCESS(0x00000001),

	/** Ваш адрес электронной почты не подтвержден. */
	EVENT_ERROR_LOGIN(0x80000100),

	/** Данный логин в процессе подключения. */
	EVENT_ERROR_EVENT_LOG_IN_ALEADY_LOGIN(0x80000101),

	/** Неверный логин/пароль */
	EVENT_ERROR_EVENT_LOG_IN_INVALID_ACCOUNT(0x80000102),

	/** Сервер перегружен, соединение сброшено. Повторите попытку позже */
	EVENT_ERROR_EVENT_LOG_IN_TIME_OUT1(0x80000105),

	/** Ваш аккаунт заблокирован из за нарушения правил */
	EVENT_ERROR_EVENT_LOG_IN_BLOCK_ACCOUNT(0x80000107),

	/** Cервер переполнен */
	EVENT_ERROR_EVENT_LOG_IN_MAXUSER(0x8000010E),

	EVENT_ERROR_EVENT_LOG_IN_TIME_OUT2(0x8000010F);

	private int _value;

	LoginAccess(int code)
	{
		_value = code;
	}

	public int get()
	{
		return _value;
	}
}