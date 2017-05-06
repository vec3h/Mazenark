﻿using App.EventSystem;
using Lobby;
using UnityEngine;

using UnityEngine.UI;

namespace Ui {
    public class GameEndUi : MonoBehaviour {
        public Canvas CanvasObject;

        private void Start () {
            App.AppManager.Instance.EventHub.Subscribe("maze:levelCompleted", OnLevelComplete, this);
            App.AppManager.Instance.EventHub.Subscribe("PlayerDied", OnPlayerDead, this);
        }

        private void OnDestroy() {
            App.AppManager.Instance.EventHub.UnsubscribeFromAll(this);
        }

        private void OnPlayerDead(object sender, EventArguments args) {
            Debug.Log(GetComponent<LobbyPlayer>().playerName);
            CanvasObject.enabled = true;
            Text t = transform.GetChild(0).GetChild(0).GetComponent<Text>();
            t.text = "You Died";
            t.color = Color.red;
        }

        private void OnLevelComplete(object sender, EventArguments args) {
//            if (args.Message == )
            CanvasObject.enabled = true;
            Text t = transform.GetChild(0).GetChild(0).GetComponent<Text>();
            t.text = "Success! Take this award";
            t.color = Color.green;
        }

        private void TurnOffCanvas() {
            CanvasObject.enabled = false;
        }
    }
}
