using CounterStrikeSharp.API.Modules.Utils;

namespace CounterStrikeSharp.API.Core.Model;

public partial class CPlayerPawnComponent
{
    public PointerTo<CBasePlayerPawn> Pawn => new PointerTo<CBasePlayerPawn>(this.Handle + 0x30);
}
