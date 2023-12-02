using System.Runtime.InteropServices;

using CounterStrikeSharp.API.Engine.Entities;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Utils;

namespace CounterStrikeSharp.API;

public abstract class NativeEntity : NativeObject
{
    public new IntPtr Handle => EntitySystem.GetEntityByHandle(EntityHandle) ?? IntPtr.Zero;

    private CEntityHandle? _handle;

    public CEntityHandle EntityHandle => _handle ?? new CEntityHandle(EntitySystem.GetRawHandleFromEntityPointer(Handle));

    public NativeEntity(IntPtr pointer) : base(pointer)
    {
        _handle = new(EntitySystem.GetRawHandleFromEntityPointer(pointer));
    }

    public NativeEntity(uint rawHandle) : base(EntitySystem.GetEntityByHandle(rawHandle) ?? IntPtr.Zero)
    {
        _handle = new(rawHandle);
    }

    protected NativeEntity()
    {
    }
}
