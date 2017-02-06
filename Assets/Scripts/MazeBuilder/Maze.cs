﻿using System;
using System.Collections.Generic;

public class Maze {
	private Tile[,] tiles;
    private List<Coordinate> importantPlaces = new List<Coordinate>();  // Should have at least one path leading to them.

	public Maze (int width = 10, int height = 10) {
        if (width < 5)
            width = 5;
        if (height < 5)
            height = 5;

        tiles = new Tile[width, height];

        for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                tiles[i, j] = new Tile();

    }

	public Tile[,] Tiles {
		get {
			return tiles;
		}
	}

    public List<Coordinate> ImportantPlaces{
        get{
            return importantPlaces;
        }
    }

    public class Coordinate{
        public Coordinate(int x, int y){
            X = x;
            Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }
    }
}




