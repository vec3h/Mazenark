﻿using UnityEngine;
using UnityEngine.Networking;
using Weapons;


namespace Controls.Bosses {    
    public class BossShieldedControl : BasicBossControl {
        private Vector3 _lookDirection;
		
        protected override void Update() {
            if (!IsAlive || !isServer) return;

            if (CheckPlayersNear(out TargetPosition)) {
                
                _lookDirection = TargetPosition - transform.position;
                _lookDirection.Normalize();
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookDirection), 0.1f);                    

                AttackTimePassed += Time.deltaTime;

                if (AttackTimePassed > TimeForAttack) {

                    SetAnimation("Attack", true);
                    Fire(TargetPosition);

                    AttackTimePassed = 0f;
                }                
            }
            
            SetAnimation("Attack", false);
        }
        
        protected override void Fire(Vector3 direction) {
            var pos = transform.position;
            pos.y += 3.5f;
            direction.y += 2f;
            var activeItem = Instantiate(Weapon, pos, Quaternion.identity);
            var weapon = activeItem.GetComponent<Weapon>();            
            activeItem.transform.LookAt(direction);
            weapon.Fire();
            NetworkServer.Spawn(activeItem);
            Destroy(weapon, 10.0f);            
        }
    }
}
