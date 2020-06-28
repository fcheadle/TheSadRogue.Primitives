﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SadRogue.Primitives.SerializedTypes
{
    /// <summary>
    /// Serializable (pure-data) object representing a <see cref="Palette"/>.
    /// </summary>
    [Serializable]
    public struct PaletteSerialized
    {
        public List<ColorSerialized> Colors;

        public static implicit operator Palette(PaletteSerialized serialized) => new Palette(serialized.Colors.Select(colorSerialized => (Color)colorSerialized));

        public static implicit operator PaletteSerialized(Palette palette) =>
            new PaletteSerialized() { Colors = palette.Select(color => (ColorSerialized)color).ToList() };
    }


}