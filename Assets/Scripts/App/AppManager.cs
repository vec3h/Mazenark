﻿using App.Eventhub;
using MazeBuilder.Utility;
using UnityEngine;

namespace App {
    public class AppManager : MonoBehaviour {
        public static AppManager Instance { get; private set; }
        public MazeSizeGenerator MazeSize { get; private set; }
        public MazeBuilder.MazeBuilder MazeInstance { get; set; }
        public Publisher EventHub { get; private set; }
        [HideInInspector] public bool IsSinglePlayer = false;

        private Camera _cam;

        private void Start() {
            if (Instance == null) {
                Instance = this;
                
                EventHub = new Publisher();
                MazeSize = new MazeSizeGenerator();
                
                DontDestroyOnLoad(gameObject);
            } else {
                DestroyImmediate(gameObject);
            }
        }

        public void Init() { // used from LobbyManager to remove all eventhandlers registered in previous game and maze instance
            EventHub.ClearHandlers();
            MazeInstance = null;
        }

        public void TurnOffAndSetupMainCamera() { // Here gameover camera will be set
            Camera.main.transform.position = Utils.TransformToWorldCoordinate(new Coordinate(
                MazeInstance.Height / 2 - 1, MazeInstance.Width / 2 - 2), 10.5f);
            _cam = Camera.main;
            _cam.enabled = false;
        }

        public void TurnOnMainCamera() {
            if (_cam) {
                _cam.enabled = true;
            }
        }
    }
}
