﻿using System;
using System.Collections.Generic;
using App;
using App.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MazeBuilder.Walls {

    public class OuterWalls : MonoBehaviour {
        [Tooltip("Maze Walls prefab")]
        public GameObject [] DefaultBiomeWalls;
        public GameObject [] LavaBiomeWalls;
        private MazeSizeGenerator _mazeSize;

		private void GenerateWall(int size, Quaternion rotationQuaternion,
		    Func<float, Vector3> getPosition, Func<Vector3, float> increment) {
		    var rootObjForWalls = new GameObject {name = "WallsGroup"};
		    for (float i = 0; i < size;) {
				var randomWall = DefaultBiomeWalls[Random.Range(0, DefaultBiomeWalls.Length)];
				var wall = AppManager.Instance.InstantiateSOC(randomWall, getPosition(i), rotationQuaternion);
				var render = wall.GetComponent<Renderer>();
			    wall.transform.parent = rootObjForWalls.transform;
			    i += increment(render.bounds.size) - Random.Range(3, 20); // to have no gaps and create uniq wall
			}

		}

        private void Start() {
            AppManager.Instance.EventHub.Subscribe("MazeCreated", SetUp, this);
        }

        private void SetUp(object sender, EventArguments eventArguments) {
            _mazeSize = AppManager.Instance.MazeSize;

            GenerateWall(_mazeSize.X * Constants.Maze.TILE_SIZE + 25, Quaternion.Euler(0, 90, 0),
                getPosition: index => new Vector3(-25, 0, index), increment: bounds => bounds.z); // Left

            GenerateWall(_mazeSize.Y * Constants.Maze.TILE_SIZE + 25, Quaternion.identity,
                getPosition: index => new Vector3(index, 0, -25), increment: bounds => bounds.x); // Bottom

            GenerateWall(_mazeSize.X * Constants.Maze.TILE_SIZE + 25, Quaternion.Euler(0, 270, 0),
                getPosition: index => new Vector3(_mazeSize.X * 8 + 25, 0, index), increment: bounds => bounds.z); // Right

            GenerateWall(_mazeSize.Y * Constants.Maze.TILE_SIZE + 25, Quaternion.Euler(0, 180, 0),
                getPosition: index => new Vector3(index, 0, _mazeSize.Y * 8 + 25), increment: bounds => bounds.x); // Top

        }

    }
}