package ru.mmo.global.crypt;

public class BitRotate
{
	public static byte[] decrypt(byte[] data, int shift)
	{
		byte lastElement = data[data.length - 1];
		for(int i = data.length - 1; i > 0; i--)
		{
			data[i] = (byte) (((data[i - 1] & 0xFF) << (8 - shift)) | ((data[i] & 0xFF) >> shift));
		}
		data[0] = (byte) ((lastElement << (8 - shift)) | ((data[0] & 0xFF) >> shift));
		return data;
	}
}
