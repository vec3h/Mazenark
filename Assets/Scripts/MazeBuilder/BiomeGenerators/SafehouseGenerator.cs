﻿using App;
using App.EventSystem;
using MazeBuilder.Utility;
using UnityEngine;

namespace MazeBuilder.BiomeGenerators {
    public class SafehouseGenerator : AbstractBiomeGenerator {

        #region BiomeSafehouse
        [Header("Safehouse")]
        public GameObject Safehouse;
        #endregion

        private new void Awake() {
            base.Awake();
        }


        [System.Obsolete("Safehouse spawned by App.InGameServerSpawner")]
        private void PlaceSafeHouse() {
            Instantiate(Safehouse,
                Utils.GetDefaultPositionVector(new Coordinate(AppManager.Instance.MazeInstance.Height / 2,
                AppManager.Instance.MazeInstance.Width / 2), 0.1f), Quaternion.identity);
        }

        public override void CreateWall(Biome biome, Coordinate coordinate, Maze maze) {
            AppManager.Instance.InstantiateSOC(FlatWall, Utils.GetDefaultPositionVector(coordinate), Quaternion.identity);
        }
    }
}