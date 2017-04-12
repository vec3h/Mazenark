﻿using System.Collections.Generic;
using App;
using App.EventSystem;
using MazeBuilder.BiomeGenerators.PlacementRules;
using MazeBuilder.Utility;
using UnityEngine;

namespace MazeBuilder.BiomeGenerators {
    public class EarthGenerator : AbstractBiomeGenerator {

		// should be an array...
        [Header("Biome Placing Rules")]
		[SerializeField]
		private PlacementRule _outerEdges;
		[SerializeField]
		private PlacementRule _innerEdges;
		[SerializeField]
		private PlacementRule _straightWalls;

        [Header("Biome floors")]
		[SerializeField]
		private GameObject _floor2;


        private new void Awake() {
            base.Awake();
		}

        protected override void OnNight(object sender, EventArguments args) {
            EnableParticles();
        }

        protected override void OnDay(object sender, EventArguments args) {
            DisableParticles();
        }

        private void EnableParticles() {
            foreach (var particles in ParticleList) {
                particles.Play();
            }

        }
        private void DisableParticles() {
            foreach (var particles in ParticleList) {
                particles.Stop();
            }
        }

        protected override void StartPostPlacement(object sender, EventArguments e) {
            PlaceLightingObjects();
        }

        public override void CreateWall(Biome biome, Coordinate coordinate, Maze maze) {
            GameObject parent = new GameObject();

            foreach (Edge edge in Edge.Edges) {
				var edgeMeshTemplate = _outerEdges.GetMeshForPlacement(maze, coordinate, edge);
				if (edgeMeshTemplate == null)
					edgeMeshTemplate = _innerEdges.GetMeshForPlacement(maze, coordinate, edge);
				if (edgeMeshTemplate == null)
					edgeMeshTemplate = _straightWalls.GetMeshForPlacement(maze, coordinate, edge);

				if (edgeMeshTemplate != null) {
					var edgeMesh = AppManager.Instance.InstantiateSOC(edgeMeshTemplate, Utils.GetDefaultPositionVector(coordinate), edge.Rotation);
					edgeMesh.name = string.Format(edge.Name);
					edgeMesh.transform.parent = parent.transform;
				}
            }

            parent.name = string.Format("Cube at {0}:{1}", coordinate.X, coordinate.Y);
        }

        public override void CreateFloor(Biome biome, Coordinate coordinate, Maze maze) {
            if (FloorEnviromentSpawnChance >= Random.Range(1, 100)) {
                AppManager.Instance.InstantiateSOC((GameObject) BiomeFloorsEnviroment.GetRandom(typeof(GameObject)),
                    Utils.GetDefaultPositionVector(coordinate, 0.2f), Quaternion.identity);
            }

            AppManager.Instance.InstantiateSOC(_floor2, Utils.GetDefaultPositionVector(coordinate), Edge.UpRight.Rotation);
		}

        private void PlaceLightingObjects() {
            ParticleList = PlaceLightingParticles(Biome.Earth, NightParticles);
            PlaceTorches(Biome.Earth);
        }

//        private void PlaceTorches() {
//            var mazeTiles = AppManager.Instance.MazeInstance.Maze;
//
//            for (var i = 0; i < mazeTiles.Width; i++) {
//                for (var j = 1; j < mazeTiles.Height - 1; j++) {
//                    var wall = mazeTiles[i, j];
//                    if (wall.Type == Tile.Variant.Wall && mazeTiles[i, j + 1].Type != Tile.Variant.Wall
//                                                       && mazeTiles[i, j - 1].Type != Tile.Variant.Wall) {
//                        PlaceTorch(wall);
//                    }
//                }
//            }
//        }

    }

}
