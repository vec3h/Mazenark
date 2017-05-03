﻿using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using Lobby;
using UnityEngine;
using UnityEngine.Networking;

namespace App {
    public class NetworkHttpManager : MonoBehaviour {
        public static NetworkHttpManager Instance;
        private int _instanceId;

        private void Start() {
            if (Instance == null) {
                Instance = this;
            }
            _instanceId = FindObjectOfType<LobbyManager>().InstanceId;
        }

        public UnityWebRequest GetRequest(string url, Action<string> callback, Action<string> error) {
            var request = UnityWebRequest.Get(url);
            StartCoroutine(WaitForRequest(request, callback, error));
            return request;
        }

        public void RoomJoinedOrLeaved(bool joined) {
            var url = joined ? NetworkConstants.RoomPlayerJoined : NetworkConstants.RoomPlayerLeft;
            var request = UnityWebRequest.Post(url, "");
            UploadHandler customUploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(
                JsonUtility.ToJson(new Room {room = _instanceId})));
            customUploadHandler.contentType = "application/json";
            request.uploadHandler = customUploadHandler;
            StartCoroutine(WaitForRequest(request, null, null));
        }

        public void GameStartedOrEnded(bool started) {
            var url = started ? NetworkConstants.RoomGameStarted : NetworkConstants.RoomGameEnded;
            var request = UnityWebRequest.Post(url, "");
            UploadHandler customUploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(
                JsonUtility.ToJson(new Room {room = _instanceId})));
            customUploadHandler.contentType = "application/json";
            request.uploadHandler = customUploadHandler;
            StartCoroutine(WaitForRequest(request, null, null));
        }

        private IEnumerator WaitForRequest(UnityWebRequest www, Action<string> callback,  Action<string> error) {
            yield return www.Send();
            if (www.error != null && error != null) {
                error(JsonUtility.ToJson(new Error {error = "Network error, try again latter"}));
                yield break;
            }
            if(www.responseCode == 400 && error != null) {
                error(www.downloadHandler.text);
            } else {
                if (callback != null) {
                    callback(www.downloadHandler.text);
                }
            }
        }
    }
}