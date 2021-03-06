﻿using System.Diagnostics;

namespace MazeBuilder.Utility {
	public class Room {
	    public Room(Coordinate topLeft, Coordinate bottomRight) {
			Init(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);
		}

		public Room(int topLeftXCoordinate, int topLeftYCoordinate, int bottomRightXCoordinate, int bottomRightYCoordinate) {
			Init(topLeftXCoordinate, topLeftYCoordinate, bottomRightXCoordinate, bottomRightYCoordinate);
		}

		public Room(Coordinate center, int halfWidth, int halfHeight) {
			Init(center.X - halfWidth, center.Y - halfHeight, center.X + halfWidth, center.Y + halfHeight);
		}

		private void Init(int topLeftXCoordinate, int topLeftYCoordinate, int bottomRightXCoordinate, int bottomRightYCoordinate) {
			var lesserX = topLeftXCoordinate < bottomRightXCoordinate ? topLeftXCoordinate : bottomRightXCoordinate;
			var greaterX = topLeftXCoordinate < bottomRightXCoordinate ? bottomRightXCoordinate : topLeftXCoordinate;
			var lesserY = topLeftYCoordinate < bottomRightYCoordinate ? topLeftYCoordinate : bottomRightYCoordinate;
			var greaterY = topLeftYCoordinate < bottomRightYCoordinate ? bottomRightYCoordinate : topLeftYCoordinate;

			TopLeftCorner = new Coordinate(lesserX, lesserY);
			BottomRightCorner = new Coordinate(greaterX, greaterY);
		}

		public Coordinate TopLeftCorner { get; private set; }

	    public Coordinate TopRightCorner {
			get {
				return new Coordinate(BottomRightCorner.X, TopLeftCorner.Y);
			}
		}

		public Coordinate BottomRightCorner { get; private set; }

	    public Coordinate BottomLeftCorner {
			get {
				return new Coordinate(TopLeftCorner.X, BottomRightCorner.Y);
			}
		}

		// Will return top-left coordinate of four center squares if room has even sides.
		public Coordinate Center {
			get {
				return new Coordinate((TopLeftCorner.X + BottomRightCorner.X) / 2, (TopLeftCorner.Y + BottomRightCorner.Y) / 2);
			}
		}

		public int Width {
			get {
				return BottomRightCorner.X - TopLeftCorner.X;
			}
		}

		public int Height {
			get {
				return BottomRightCorner.Y - TopLeftCorner.Y;
			}
		}

		private bool IsCoordinateLiesWithin(int x, int y) {
			return IsCoordinateLiesWithin(new Coordinate(x, y));
		}

		private bool IsCoordinateLiesWithin(Coordinate point) {
			return TopLeftCorner.X <= point.X && TopLeftCorner.Y <= point.Y && BottomRightCorner.X >= point.X && BottomRightCorner.Y >= point.Y;
		}

		public bool IntersectsRoomAndOneTileMargin(Room anotherRoom) {
			bool doIntersect = false;
			// ±1 because of Minkovsky. Just provides beforementioned margin of 1 tile.
			int x = TopLeftCorner.X - 1;
			int y = TopLeftCorner.Y - 1;
			int xRight = BottomRightCorner.X + 1;
			int yBottom = BottomRightCorner.Y + 1;

			// if one of the points above lies within another room, they intersect each other.
			doIntersect = IsCoordinateLiesWithin(x, y) || IsCoordinateLiesWithin(x, yBottom)
						  || IsCoordinateLiesWithin(xRight, y) || IsCoordinateLiesWithin(xRight, yBottom);

			return doIntersect;
		}
	}
}
