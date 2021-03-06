﻿using UnityEngine;
using UnityEngine.UI;

namespace Prototype.NetworkLobby {
    public class LobbyTopPanel : MonoBehaviour {
        public bool isInGame = false;
        public bool isInLobby = true;

        protected bool isDisplayed = true;
        protected Image panelImage;

        void Start() {
            panelImage = GetComponent<Image>();
        }


        void Update() {
            if (!isInGame) {
                return;
            }

            if (!isInLobby) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                ToggleVisibility(!isDisplayed);
            }

        }

        public void ToggleVisibility(bool visible) {
            isDisplayed = visible;
            foreach (Transform t in transform) {
                t.gameObject.SetActive(isDisplayed);
            }

            if (panelImage != null) {
                panelImage.enabled = isDisplayed;
            }
        }
    }
}