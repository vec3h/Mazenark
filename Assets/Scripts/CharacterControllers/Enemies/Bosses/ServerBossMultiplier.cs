﻿using Controls.Bosses;
using Loot;
using UnityEngine;
using UnityEngine.Networking;


namespace CharacterControllers.Enemies.Bosses {
    public class ServerBossMultiplier : ServerCharacterController {
        [SerializeField] [Range(1, 5)] private int _countOfMultiply = 3;
        private int Multiplied { get; set; }

        private void Start() {
            if (!isServer) return;
            IsNpc = true;            
        }

        public override void TakeDamage(int amount, float timeOfDeath = 2f, string whoCasted = "Enemy") {
            if (!isServer) return;
            Multiplied++;
            if (Multiplied == _countOfMultiply) {
                var pos = transform.position;
                pos.y = 1.5f;
                FindObjectOfType<LootManager>().CreateLoot(pos, 100f);                              
            } else {
                for (var i = 0; i < 2; i++) {
                    var newBoss = Instantiate(gameObject);
                    newBoss.GetComponent<ServerBossMultiplier>().Multiplied = Multiplied;
                    newBoss.GetComponent<BossMultiplierControl>().SetSpawnRoom(
                        gameObject.GetComponent<BossMultiplierControl>().GetSpawnRoom());

                    newBoss.transform.localScale = new Vector3(gameObject.transform.localScale.x / 1.5f,
                        gameObject.transform.localScale.y / 1.5f, gameObject.transform.localScale.z / 1.5f);

                    newBoss.transform.position = new Vector3(gameObject.transform.position.x - i * 5,
                        gameObject.transform.position.y, gameObject.transform.position.z - i * 5);
                                        
                    NetworkServer.Spawn(newBoss);
                }
            }
            gameObject.GetComponent<BossMultiplierControl>().Die(timeOfDeath);            
            Destroy(gameObject, timeOfDeath);                   
        }
    }

}

