﻿using MazeBuilder.Utility;
using UnityEngine;

namespace MazeBuilder.BiomeGenerators {
    public class SafehouseGenerator : AbstractBiomeGenerator {
        #region BiomeWalls
        [Header("Biome Walls")]
        public GameObject FlatWall;
        #endregion

        #region BiomeFloor
        [Header("Biome Floor")]
        public GameObject Floor;
        #endregion

        #region BiomeFloor

        [Header("Biome Lighting Objetcs")]
        public GameObject NightParticles;

        #endregion

        private readonly CollectionRandom _biomeFloors = new CollectionRandom();

        private void Awake() {
            base.Awake();
            _biomeFloors.Add(Floor, "earthFloors", typeof(GameObject), 1.0f);
        }

        public override void CreateWall(Biome biome, Coordinate coordinate, Maze maze) {
            var go = Instantiate(FlatWall, GetDefaultPositionVector(coordinate, true), Quaternion.identity);
        }
        public override void CreateFloor(Biome biome, Coordinate coordinate, Maze maze) {
            var go = (bool) ChancesToSpawnFloors.GetRandom(typeof(bool));
            if (go) {
                Instantiate((GameObject) _biomeFloors.GetRandom(typeof(GameObject)),
                    GetDefaultPositionVector(coordinate, false), Quaternion.identity);
            }

        }
    }
}