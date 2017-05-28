﻿using MazeBuilder;
using UnityEngine;

namespace Enemies {
    public class EarthBiomeEnemySpawner : AbstractEnemySpawner {        

        private new void Start() {
            base.Start();
            SetUpEmptyTiles(Biome.Earth);
            SpawnEnemies();
            CreateEnemyBehaivor();
            SpawnBosses();
        }
    }
}