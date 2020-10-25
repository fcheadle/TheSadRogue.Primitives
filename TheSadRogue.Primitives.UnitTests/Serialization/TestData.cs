﻿using System;
using System.Collections.Generic;
using System.Linq;
using SadRogue.Primitives.SerializedTypes;

namespace SadRogue.Primitives.UnitTests.Serialization
{
    public static class TestData
    {
        #region Original Data
        /// <summary>
        /// List of expressive versions of types.  The assumptions here are:
        ///     1. All types in this list are serializable via generic data contract serialization
        ///     2. All types in this list serialize to JSON objects (JObject) when using Newtonsoft.Json
        /// </summary>
        private static readonly IEnumerable<object> _expressiveTypes = new object[]
        {
            // AreaSerialized
            new AreaSerialized
            {
                Positions = new List<PointSerialized>
                {
                    new PointSerialized { X = 1, Y = 2 },
                    new PointSerialized { X = 3, Y = 4 },
                    new PointSerialized { X = 5, Y = 6 }
                }
            },
            // BoundedRectangleSerialized
            new BoundedRectangleSerialized
            {
                Area = new RectangleSerialized {X = 1, Y = 4, Width = 10, Height = 14},
                Bounds = new RectangleSerialized {X = -10, Y = -1, Width = 100, Height = 101 }
            },
            // ColorSerialized
            new ColorSerialized  { R = 120, G = 121, B = 122, A = 150 },
            // GradientStopSerialized
            new GradientStopSerialized
            {
                Color = new ColorSerialized { R = 100, G = 101, B = 102, A = 103 },
                Stop = 0.5f
            },
            // GradientSerialized
            new GradientSerialized
            {
                Stops = new List<GradientStopSerialized>
                {
                    new GradientStopSerialized
                    {
                        Color = new ColorSerialized { R = 100, G = 101, B = 102, A = 103 },
                        Stop = 0.5f
                    },
                    new GradientStopSerialized
                    {
                        Color = new ColorSerialized {R = 200, G = 201, B = 202, A = 203 },
                        Stop = 1.0f
                    }
                }
            },
            // PaletteSerialized
            new PaletteSerialized
            {
                Colors = new List<ColorSerialized>
                {
                    new ColorSerialized { R = 100, G = 101, B = 102, A = 103 },
                    new ColorSerialized {R = 200, G = 201, B = 202, A = 203 }
                }
            },
            // PointSerialized
            new PointSerialized { X = 10, Y = 20 },
            // PolarCoordinateSerialized
            new PolarCoordinateSerialized { Radius = 5.0, Theta = 3 * Math.PI / 2.0 },
            // RectangleSerialized
            new RectangleSerialized { X = 10, Y = 20, Width = 100, Height = 200 }
        };

        /// <summary>
        /// Any expressive types (ones that implicitly convert for the sake of serialization), that don't serialize
        /// to JSON Object (generally instead serialize to primitive types like integers).
        /// </summary>
        private static readonly IEnumerable<object> _expressivePrimitiveTypes = new object[]
        {
            // AdjacencyRule.Types
            AdjacencyRule.Types.Cardinals, AdjacencyRule.Types.Diagonals,
            // Direction.Types
            Direction.Types.Down, Direction.Types.Right,
            // Distance.Types
            Distance.Types.Chebyshev, Distance.Types.Euclidean,
            // Radius.Types
            Radius.Types.Square, Radius.Types.Circle,
        };

        /// <summary>
        /// List of all non-expressive types that serialize to JSON objects (JObject)
        /// </summary>
        private static readonly object[] _nonExpressiveJsonObjects =
        {
            // AdjacencyRules
            AdjacencyRule.Cardinals, AdjacencyRule.EightWay,
            // BoundedRectangle
            new BoundedRectangle(new Rectangle(1, 4, 10, 14), new Rectangle(-10, -9, 100, 101)),
            // Colors
            new Color(.5f, .6f, .7f), new Color(120, 121, 122, 100), Color.AliceBlue,
            // Directions
            Direction.Down, Direction.UpRight,
            // Distances
            Distance.Chebyshev, Distance.Manhattan,
            // GradientStop
            new GradientStop(new Color(100, 101, 102, 103), .5f),
            // Points
            new Point(-1, -5), new Point(4, 9),
            // Polar Coordinates
            new PolarCoordinate(10.0, 0.25), new PolarCoordinate(5.0, 3 * Math.PI / 2.0),
            // Radii
            Radius.Circle, Radius.Diamond,
            // Rectangles
            new Rectangle(1, 2, 3, 4), new Rectangle(-10, -4, 56, 68),
        };

        /// <summary>
        /// Dictionary of object types to an unordered but complete list of fields that each object type should have
        /// serialized in its JSON object form.  All objects in SerializableValuesJsonObjects should have an entry here.
        /// </summary>
        public static readonly Dictionary<Type, string[]> TypeSerializedFields = new Dictionary<Type, string[]>
        {
            { typeof(AdjacencyRule), new[] { "Type" } },
            { typeof(AreaSerialized), new[] { "Positions" } },
            { typeof(BoundedRectangle), new[] { "_area", "_boundingBox" } },
            { typeof(BoundedRectangleSerialized), new[] { "Area", "Bounds" } },
            { typeof(Color), new[] { "_packedValue" } },
            { typeof(ColorSerialized), new[] { "R", "G", "B", "A" } },
            { typeof(Direction), new[] { "Type" } },
            { typeof(Distance), new[] { "Type" } },
            { typeof(GradientStop), new[] { "Color", "Stop" } },
            { typeof(GradientStopSerialized), new[] { "Color", "Stop" } },
            { typeof(GradientSerialized), new[] { "Stops" } },
            { typeof(PaletteSerialized), new[] { "Colors" } },
            { typeof(Point), new[] { "X", "Y" } },
            { typeof(PointSerialized), new[] { "X", "Y" } },
            { typeof(PolarCoordinate), new[] { "Radius", "Theta" } },
            { typeof(PolarCoordinateSerialized), new[] { "Radius", "Theta" } },
            { typeof(Radius), new[] { "Type" } },
            { typeof(Rectangle), new[] { "X", "Y", "Width", "Height" } },
            { typeof(RectangleSerialized), new[] { "X", "Y", "Width", "Height" } }
        };

        /// <summary>
        /// Objects that are JSON serializable but should NOT serialize to JSON objects (instead, for example,
        /// JSON array)
        /// </summary>
        public static readonly IEnumerable<object> SerializableValuesNonJsonObjects = new object[]
        {
            // Area
            new Area((1, 2), (3, 4), (5, 6)),
            // Gradient
            new Gradient(new Color(100, 101, 102, 103), new Color(200, 201, 202, 203)),
            // Palette
            new Palette(new[] { new Color(100, 101, 102, 103), new Color(150, 151, 152, 149) }),
        };


        /// <summary>
        /// Dictionary of non-expressive types to their expressive type
        /// </summary>
        public static readonly Dictionary<Type, Type> RegularToExpressiveTypes = new Dictionary<Type, Type>
        {
            [typeof(AdjacencyRule)] = typeof(AdjacencyRule.Types),
            [typeof(Area)] = typeof(AreaSerialized),
            [typeof(BoundedRectangle)] = typeof(BoundedRectangleSerialized),
            [typeof(Color)] = typeof(ColorSerialized),
            [typeof(Direction)] = typeof(Direction.Types),
            [typeof(Distance)] = typeof(Distance.Types),
            [typeof(GradientStop)] = typeof(GradientStopSerialized),
            [typeof(Gradient)] = typeof(GradientSerialized),
            [typeof(Palette)] = typeof(PaletteSerialized),
            [typeof(Point)] = typeof(PointSerialized),
            [typeof(PolarCoordinate)] = typeof(PolarCoordinateSerialized),
            [typeof(Radius)] = typeof(Radius.Types),
            [typeof(Rectangle)] = typeof(RectangleSerialized)
        };


        /// <summary>
        /// List of non-serializable objects that do have serializable equivalents (expressive types).
        /// </summary>
        private static readonly object[] _nonSerializableValuesWithExpressiveTypes = { };
        #endregion

        #region Combinatory Data

        /// <summary>
        /// All types that should serialize with binary serializers.
        /// </summary>
        public static IEnumerable<object> BinarySerializableTypes => _expressiveTypes.Concat(_expressivePrimitiveTypes);

        /// <summary>
        /// All objects that should serialize to JSON objects.  All should have entries in TypeSerializedFields
        /// </summary>
        public static IEnumerable<object> SerializableValuesJsonObjects
            => _expressiveTypes.Concat(_nonExpressiveJsonObjects);

        /// <summary>
        /// Objects that should have expressive versions of types.  Each item must have an entry in
        /// RegularToExpressiveTypes
        /// </summary>
        public static IEnumerable<object> AllNonExpressiveTypes
            => _nonExpressiveJsonObjects.Concat(SerializableValuesNonJsonObjects).Concat(_nonSerializableValuesWithExpressiveTypes);

        /// <summary>
        /// All JSON objects for which we can test serialization equality
        /// </summary>
        public static IEnumerable<object> AllSerializableObjects =>
            SerializableValuesJsonObjects.Concat(SerializableValuesNonJsonObjects);
        #endregion
    }
}