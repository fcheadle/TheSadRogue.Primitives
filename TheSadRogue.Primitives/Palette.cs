﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SadRogue.Primitives
{
    /// <summary>
    /// A palette of colors.
    /// </summary>
    [DataContract]
    public class Palette : IEnumerable<Color>
    {
        [DataMember]
        private readonly Color[] _colors;

        /// <summary>
        /// How many colors the palette has.
        /// </summary>
        public int Length => _colors.Length;

        /// <summary>
        /// Gets or sets a color in the palette by index.
        /// </summary>
        /// <param name="index">Index of the color.</param>
        /// <returns>A color.</returns>
        public Color this[int index]
        {
            get => _colors[index];
            set => _colors[index] = value;
        }

        /// <summary>
        /// Creates a new palette with the specified amount of colors.
        /// </summary>
        /// <param name="colors">The number of colors.</param>
        public Palette(int colors)
        {
            _colors = new Color[colors];

            for (int i = 0; i < colors; i++)
            {
                _colors[i] = new Color();
            }
        }

        /// <summary>
        /// Creates a new palette of colors from a list of existing colors.
        /// </summary>
        /// <param name="colors">The list of colors this palette is made from.</param>
        public Palette(IEnumerable<Color> colors) => _colors = new List<Color>(colors).ToArray();

        /// <summary>
        /// Shifts the entire palette once to the left.
        /// </summary>
        public void ShiftLeft()
        {
            Color lostColor = _colors[0];
            Array.Copy(_colors, 1, _colors, 0, _colors.Length - 1);
            _colors[^1] = lostColor;
        }

        /// <summary>
        /// Shifts the entire palette once to the right.
        /// </summary>
        public void ShiftRight()
        {
            Color lostColor = _colors[^1];
            Array.Copy(_colors, 0, _colors, 1, _colors.Length - 1);
            _colors[0] = lostColor;
        }

        /// <summary>
        /// Shifts a range of colors in the palette once to the left.
        /// </summary>
        /// <param name="startingIndex">The starting index in the palette.</param>
        /// <param name="count">The amount of colors to shift from the starting index.</param>
        public void ShiftLeft(int startingIndex, int count)
        {
            Color lostColor = _colors[startingIndex];
            Array.Copy(_colors, startingIndex + 1, _colors, startingIndex, count - 1);
            _colors[startingIndex + count - 1] = lostColor;
        }

        /// <summary>
        /// Shifts a range of colors in the palette once to the right.
        /// </summary>
        /// <param name="startingIndex">The starting index in the palette.</param>
        /// <param name="count">The amount of colors to shift from the starting index.</param>
        public void ShiftRight(int startingIndex, int count)
        {
            Color lostColor = _colors[startingIndex + count - 1];
            Array.Copy(_colors, startingIndex, _colors, startingIndex + 1, count - 1);
            _colors[startingIndex] = lostColor;
        }

        /// <summary>
        /// Gets the closest color in the palette to the provided color.
        /// </summary>
        /// <param name="color">The color to find.</param>
        /// <returns>The closest matching color.</returns>
        public Color GetNearest(Color color) => _colors[GetNearestIndex(color)];

        /// <summary>
        /// Gets the index of the closest color in the palette to the provided color.
        /// </summary>
        /// <param name="color">The color to find.</param>
        /// <returns>The palette index of the closest color.</returns>
        public int GetNearestIndex(Color color)
        {
            int lowestDistanceIndex = -1;
            int lowestDistance = int.MaxValue;
            int currentDistance;
            for (int i = 0; i < _colors.Length; i++)
            {
                currentDistance = Math.Abs(_colors[i].R - color.R) + Math.Abs(_colors[i].G - color.G) + Math.Abs(_colors[i].B - color.B);

                if (currentDistance < lowestDistance)
                {
                    lowestDistance = currentDistance;
                    lowestDistanceIndex = i;
                }
            }
            return lowestDistanceIndex;
        }

        /// <summary>
        /// Gets the list of colors in the palette.
        /// </summary>
        /// <returns>The colors in the palette.</returns>
        public IEnumerator<Color> GetEnumerator() => ((IEnumerable<Color>)_colors).GetEnumerator();

        /// <summary>
        /// Gets the list of colors in the palette.
        /// </summary>
        /// <returns>The colors in the palette.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _colors.GetEnumerator();

        /// <summary>
        /// True if the given object is a Palette and its colors are identical and in the same order.
        /// </summary>
        /// <param name="obj"/>
        /// <returns>True if the given object is a Palette and its colors are identical and in the same order; false otherwise.</returns>
        public override bool Equals(object? obj) => obj is Palette palette && this == palette;

        /// <summary>
        /// Returns hash code based on the colors.
        /// </summary>
        /// <returns>Hash code for the object based on the colors.</returns>
        public override int GetHashCode() => _colors.GetHashCode();

        /// <summary>
        /// Returns true if the two palettes hold identical colors in the same order.
        /// </summary>
        /// <param name="p1"/>
        /// <param name="p2"/>
        /// <returns>True if the two palettes hold identical colors in the same order; false otherwise.</returns>
        public static bool operator==(Palette p1, Palette p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            if (p1 is null || p2 is null)
                return false;

            if (p1.Length != p2.Length)
                return false;

            for (int i = 0; i < p1.Length; i++)
                if (p1[i] != p2[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Returns true if the two palettes have different colors or the same colors in a different order.
        /// </summary>
        /// <param name="p1"/>
        /// <param name="p2"/>
        /// <returns>True if the two palettes have different colors or the same colors in a different order; false otherwise.</returns>
        public static bool operator !=(Palette p1, Palette p2) => !(p1 == p2);
    }
}
