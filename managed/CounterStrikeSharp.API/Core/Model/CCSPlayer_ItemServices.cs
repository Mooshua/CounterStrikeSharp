﻿/*
 *  This file is part of CounterStrikeSharp.
 *  CounterStrikeSharp is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  CounterStrikeSharp is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with CounterStrikeSharp.  If not, see <https://www.gnu.org/licenses/>. *
 */

using CounterStrikeSharp.API.Modules.Memory;

namespace CounterStrikeSharp.API.Core.Model;

public partial class CCSPlayer_ItemServices
{
    /// <summary>
    /// Drops the active player weapon on the ground.
    /// </summary>
    public void DropActivePlayerWeapon(CBasePlayerWeapon activeWeapon)
    {
        VirtualFunction.CreateVoid<nint, nint>(Handle, GameData.GetOffset("CCSPlayer_ItemServices_DropActivePlayerWeapon"))(Handle, activeWeapon.Handle);
    }

    /// <summary>
    /// Removes every weapon from the player.
    /// </summary>
    public void RemoveWeapons()
    {
        VirtualFunction.CreateVoid<nint>(Handle, GameData.GetOffset("CCSPlayer_ItemServices_RemoveWeapons"))(Handle);
    }
}
