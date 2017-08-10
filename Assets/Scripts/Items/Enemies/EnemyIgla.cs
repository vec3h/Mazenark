﻿using CharacterControllers;
using GameEnv.GameEffects;
using UnityEngine;

namespace Items.Enemies {
    public class EnemyIgla : Weapon {
        
        protected override void Start() {}

        void OnCollisionEnter(Collision other) {
            var go = other.gameObject;
            if (go.CompareTag("Player")) {
                go.GetComponent<ServerCharacterController>().TakeDamage(100, 3.5f);   
            }
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ContactPoint contact = other.contacts[0];
            gameObject.transform.position = contact.point;

            Destroy(gameObject, 1.5f);
            Invoke(nameof(BeginDisolve), 1.0f);
        }                

        private void BeginDisolve() {
            GetComponent<Disolve>().BeginDisolve();
        }

        public override void Fire() {            
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 50;
        }
    }
}