﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SadRogue.Primitives.SerializedTypes
{
    /// <summary>
    /// Serializable (pure-data) object representing an <see cref="Area"/>.
    /// </summary>
    [Serializable]
    public struct AreaSerialized
    {
        /// <summary>
        /// Positions in the area.
        /// </summary>
        public List<PointSerialized> Positions;

        /// <summary>
        /// Converts <see cref="AreaSerialized"/> to <see cref="Area"/>.
        /// </summary>
        /// <param name="serialized"/>
        /// <returns/>
        public static implicit operator Area(AreaSerialized serialized)
            => new Area(serialized.Positions.Select(pos => (Point)pos));

        /// <summary>
        /// Converts <see cref="Area"/> to <see cref="AreaSerialized"/>.
        /// </summary>
        /// <param name="area"/>
        /// <returns/>
        public static implicit operator AreaSerialized(Area area)
            => new AreaSerialized() { Positions = area.Positions.Select(p => (PointSerialized)p).ToList() };
    }
}
