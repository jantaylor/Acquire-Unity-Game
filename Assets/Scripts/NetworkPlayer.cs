using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    #region Class Attributes

    [SyncVar(hook = "OnTurnChange")]
    public bool isTurn = false;

    [SyncVar]
    public bool ready = false;

    public PlayerController controller;

    #endregion

    #region Class Functions

    public override void OnStartClient() {
        DontDestroyOnLoad(this);

        base.OnStartClient();
        Debug.Log("Client Network Player start.");
        StartPlayer();
    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        // TODO: SETUP
        //controller.SetupLocalPlayer(playerName, id, color);
    }

    [Server]
    public void StartPlayer() {
        ready = true;
    }

    public void StartGame() {
        TurnStart();
    }

    [Server]
    public void TurnStart() {
        isTurn = true;
        RpcTurnStart();
    }

    [ClientRpc]
    private void RpcTurnStart() {
        controller.TurnStart();
    }

    [Server]
    public void TurnEnd() {
        isTurn = false;
        RpcTurnEnd();
    }

    [ClientRpc]
    private void RpcTurnEnd() {
        controller.TurnEnd();
    }


    public void OnTurnChange(bool turn) {
        if (isLocalPlayer) {
            // TODO: play turn sound 
            Debug.Log("Your turn");
            UpdateHud();
        }
    }

    public void UpdateHud() {
        Debug.Log("updating hud.");
    }

    #endregion
}