using System.Drawing;
using System.Linq;

namespace CounterStrikeSharp.API.Modules.Cvars;

public static class ConVarFlagExtensions
{
	public static ConVarType[] ValueType = new[]
	{
		ConVarType.Bool,
		ConVarType.Int16, ConVarType.UInt16,
		ConVarType.Int32, ConVarType.UInt32,
		ConVarType.Int64, ConVarType.UInt64,
		ConVarType.Float32, ConVarType.Float64,
	};

	/// <summary>
	/// Get a managed type corresponding to this ConVarType value.
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static Type Type(this ConVarType type)
	{
		return type switch
		{
			ConVarType.Invalid => throw new InvalidOperationException(),
			ConVarType.Bool => typeof(Boolean),
			ConVarType.Int16 => typeof(Int16),
			ConVarType.UInt16 => typeof(UInt16),
			ConVarType.Int32 => typeof(Int32),
			ConVarType.UInt32 => typeof(UInt32),
			ConVarType.Int64 => typeof(Int64),
			ConVarType.UInt64 => typeof(UInt64),
			ConVarType.Float32 => typeof(float),
			ConVarType.Float64 => typeof(double),
			ConVarType.String => typeof(string),

			//	No handlers (yet!) for these types:
			//	ConVarType.Color => ,
			//	ConVarType.Vector2 => ,
			//	ConVarType.Vector3 => ,
			//	ConVarType.Vector4 => ,
			//	ConVarType.Qangle => ,
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}

	/// <summary>
	/// Determine whether or not the provided <see cref="ConVarType"/>
	/// is a "value" type, or has a corresponding native type (a la <see cref="Type"/> extension)
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static bool IsValueType(this ConVarType type)
	{
		return ValueType.Contains(type);
	}
}
