using System.Numerics;

namespace CounterStrikeSharp.API.Util;

public static class Bitwise
{
	/// <summary>
	/// Set all bits in <paramref name="enable"/> in <paramref name="original"/>
	/// and return the result.
	/// </summary>
	/// <param name="original"></param>
	/// <param name="enable"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Set<T>(T original, T enable)
		where T : IBitwiseOperators<T, T, T>
	{
		return original | enable;
	}

	/// <summary>
	/// Toggle all bits in <paramref name="toggle"/> within integer <paramref name="toggle"/>
	/// and return the result.
	/// </summary>
	/// <param name="original"></param>
	/// <param name="toggle"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Toggle<T>(T original, T toggle)
		where T : IBitwiseOperators<T, T, T>
	{
		//	Use an XOR to flip bits

		//	1 0 -> 1
		//	1 1 -> 0
		//	0 1 -> 1
		//	0 0 -> 0
		return original ^ toggle;
	}

	/// <summary>
	/// Clear all bits in <paramref name="enable"/> in <paramref name="original"/>
	/// and return the result.
	/// </summary>
	/// <param name="original"></param>
	/// <param name="enable"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Clear<T>(T original, T enable)
		where T : IBitwiseOperators<T, T, T>
	{
		//	Invert "enable" and then perform a bitwise AND
		return original & (~enable);
	}

	/// <summary>
	/// Sets or clears the bits in <paramref name="flags"/> depending
	/// on whether or not <paramref name="value"/> is true.
	/// True = Set, False = Clear.
	/// </summary>
	/// <param name="original"></param>
	/// <param name="index"></param>
	/// <param name="value"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Flag<T>(T original, T flags, bool value)
		where T : IBitwiseOperators<T, T, T>
	{
		if (value)
			return Set(original, flags);

		return Clear(original, flags);
	}
}
