/*
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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace CounterStrikeSharp.API
{
    public abstract class NativeObject
    {

        /// <summary>
        /// The underlying pointer object that this native wraps
        /// </summary>
        [NotNull]
        public IntPtr Handle { get; init; }

        [Pure]
        public NativeObject(IntPtr pointer)
        {
            Handle = pointer;
        }

        /// <summary>
        /// Construct an instance of this type without an initial handle
        /// </summary>
        [Pure]
        public NativeObject()
        {

        }

        /// <summary>
        /// Returns a new instance of the specified type using the pointer from the passed in object.
        /// </summary>
        /// <remarks>
        /// Useful for creating a new instance of a class that inherits from NativeObject.
        /// e.g. <code>var weaponServices = playerWeaponServices.As&lt;CCSPlayer_WeaponServices&gt;();</code>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T As<T>()
            where T : NativeObject, new()
        {
            return new T() { Handle = this.Handle };
        }

        /// <summary>
        /// Create a new NativeObject handle for the specified pointer
        /// </summary>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T New<T>(IntPtr handle)
            where T : NativeObject, new()
            => new T() { Handle = handle };

        /// <summary>
        /// Convert this native pointer into a managed pointer of type <typeparamref name="T"/>.
        /// Note that this conversion does not do any safety checking!
        /// </summary>
        /// <param name="fieldIndex">the byte-index of the field to read. Defaults to zero.</param>
        /// <typeparam name="T">The primitive type to interpret the handle as</typeparam>
        /// <returns>A managed pointer to (this + fieldIndex)</returns>
        public unsafe ref T AsPointer<T>(int fieldIndex = 0)
            where T : unmanaged
        {
            return ref Unsafe.AsRef<T>( IntPtr.Add( Handle, fieldIndex).ToPointer() );
        }
    }
}
