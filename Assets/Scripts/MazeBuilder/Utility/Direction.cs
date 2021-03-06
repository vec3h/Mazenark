﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeBuilder.Utility {
	public class Direction {
		private static List<Direction> directions = new List<Direction>();
		public static List<Direction> Directions { get { return directions; } }

		public int DeltaX { get; private set; }
		public int DeltaY { get; private set; }
		public Direction Opposite { get; private set; }
		public string Name { get; private set; }

		private Direction(int dx, int dy, string name) {

			DeltaX = dx;
			DeltaY = dy;
			Name = name;

            // directions.Add(this);
		}

		public Coordinate Shift(Coordinate toBeShifted) {
			return new Coordinate(toBeShifted.X + DeltaX, toBeShifted.Y + DeltaY);
		}

		public override bool Equals(object obj) {
			// If parameter is null return false:
			Direction other = obj as Direction;

			if (other == null)
				return false;

			// Return true if the fields match:
			return (DeltaX == other.DeltaX) && (DeltaY == other.DeltaY);
		}

		public override int GetHashCode() {
			int hash = 37;

			hash = hash * 13 + DeltaX.GetHashCode();
			hash = hash * 13 + DeltaY.GetHashCode();

			return hash;
		}

		public static Direction Up = new Direction(0, -1, "Up");
		public static Direction Left = new Direction(-1, 0, "Left");
		public static Direction Bottom = new Direction(0, +1, "Bottom");
		public static Direction Right = new Direction(+1, 0, "Right");
		public static Direction Nowhere = new Direction(0, 0, "Nowhere");		// May be buggy! Maybe it should not be added into Directions list...

		static Direction() {
			Up.Opposite = Bottom;
			Left.Opposite = Right;
			Bottom.Opposite = Up;
			Right.Opposite = Left;
			Nowhere.Opposite = Nowhere;

		    directions.Add(Up);
		    directions.Add(Left);
		    directions.Add(Right);
		    directions.Add(Bottom);

		}
	}
}
