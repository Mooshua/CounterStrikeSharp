using System;
using System.Runtime.CompilerServices;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Util;

namespace CounterStrikeSharp.API.Modules.Cvars;

public class ConVar : NativeObject
{
    public ConVar(IntPtr handle) : base(handle)
    {
    }

    public string Name => Utilities.ReadStringUtf8(Handle);
    public string Description => Utilities.ReadStringUtf8(Handle + 32);

    /// <summary>
    /// The underlying data type of the ConVar.
    /// </summary>
    public unsafe ref ConVarType Type => ref Unsafe.AsRef<ConVarType>((void*)(Handle + 40));

    /// <summary>
    /// The ConVar flags as defined by <see cref="ConVarFlags"/>.
    /// </summary>
    public unsafe ref ConVarFlags Flags => ref Unsafe.AsRef<ConVarFlags>((void*)(Handle + 48));

    /// <summary>
    /// Used to access primitive value types, i.e. <see langword="bool"/>, <see langword="float"/>, <see langword="int"/>, etc.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the wrong primitive type or a non-primitive type is passed.</exception>
    /// <typeparam name="T">The type of value to retrieve</typeparam>
    public unsafe ref T AsPrimitive<T>()
    {
        var type = typeof(T);

        if (!Type.IsValueType())
            throw new InvalidOperationException("Cannot read non-primitive ConVar types using primitive deserializer");

        if (type != Type.Type())
            throw new InvalidOperationException($"Attempt to read ConVar of type {Type} with primitive type {type.FullName}.");

        return ref Unsafe.AsRef<T>((void*)(Handle + 64));
    }

    public void SetValue<T>(T value)
    {
        AsPrimitive<T>() = value;
    }

    /// <summary>
    /// Used to access reference value types, i.e. Vector, QAngle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T AsNative<T>() where T : NativeObject, new()
        => NativeObject.New<T>(Handle + 64);


    /// <summary>
    /// String value of the ConVar.
    /// </summary>
    /// <remarks>String is a special exception as we have to marshal the string to UTF8 on the send/receive to unmanaged code.
    /// </remarks>
    public string StringValue
    {
        get
        {
            if (Type != ConVarType.String)
            {
                throw new InvalidOperationException(
                    $"ConVar is a {Type} but you are trying to get a string value.");
            }

            return Utilities.ReadStringUtf8(Handle + 64);
        }
        set
        {
            if (Type != ConVarType.String)
            {
                throw new InvalidOperationException(
                    $"ConVar is a {Type} but you are trying to get a string value.");
            }

            NativeAPI.SetConvarStringValue(Handle, value);
        }
    }

    /// <summary>
    /// Shorthand for checking the <see cref="ConVarFlags.FCVAR_NOTIFY"/> flag.
    /// </summary>
    public bool Public
    {
        get => Flags.HasFlag(ConVarFlags.FCVAR_NOTIFY);
        set => Flags = (ConVarFlags) Bitwise.Flag( (Int64) Flags, (Int64) ConVarFlags.FCVAR_NOTIFY, value == true);
    }

    public override string ToString()
    {
        return $"ConVar [name={Name}, description={Description}, type={Type}, flags={Flags}]";
    }

    /// <summary>
    /// Finds a ConVar by name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ConVar? Find(string name)
    {
        var ptr = NativeAPI.FindConvar(name);
        if (ptr == IntPtr.Zero) return null;

        return new ConVar(ptr);
    }
}
