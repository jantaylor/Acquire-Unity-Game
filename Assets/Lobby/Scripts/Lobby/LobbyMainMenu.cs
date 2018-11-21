using UnityEngine;
using UnityEngine.UI;

namespace Prototype.NetworkLobby {
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour {
        public LobbyManager lobbyManager;

        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public InputField ipInput;
        public InputField matchNameInput;

        public void OnEnable() {
            lobbyManager.topPanel.ToggleVisibility(true);

            // Get Local IP isntead of 127.0.0.1
            ipInput.text = CommonFunctions.LocalIPAddress();

            ipInput.onEndEdit.RemoveAllListeners();
            ipInput.onEndEdit.AddListener(onEndEditIP);

            matchNameInput.onEndEdit.RemoveAllListeners();
            matchNameInput.onEndEdit.AddListener(onEndEditGameName);
        }

        public void OnClickHost() {
            lobbyManager.StartHost();
            lobbyManager.SetServerInfo("Hosting", lobbyManager.networkAddress);
        }

        public void OnClickJoin() {
            lobbyManager.ChangeTo(lobbyPanel);

            lobbyManager.networkAddress = ipInput.text;
            lobbyManager.StartClient();

            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }

        public void OnClickDedicated() {
            lobbyManager.ChangeTo(null);
            lobbyManager.StartServer();

            lobbyManager.backDelegate = lobbyManager.StopServerClbk;

            lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);
        }

        public void OnClickCreateMatchmakingGame() {
            lobbyManager.StartMatchMaker();
            lobbyManager.matchMaker.CreateMatch(
                matchNameInput.text.Length >= 1 ? matchNameInput.text : Constants.DefaultRoom,
                (uint)lobbyManager.maxPlayers,
                true,
                "", "", "", 0, 0,
                lobbyManager.OnMatchCreate);

            lobbyManager.backDelegate = lobbyManager.StopHost;
            lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Matchmaker Host", lobbyManager.matchHost, lobbyManager.roomInfo.text);
        }

        public void OnClickOpenServerList() {
            lobbyManager.StartMatchMaker();
            lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
            lobbyManager.ChangeTo(lobbyServerList);
        }

        void onEndEditIP(string text) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                OnClickJoin();
            }
        }

        void onEndEditGameName(string text) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                OnClickCreateMatchmakingGame();
            }
        }

    }
}
