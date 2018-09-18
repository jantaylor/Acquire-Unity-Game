using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    // Manager Prefabs to instantiate
    public GameObject gameManager;
    public GameObject soundManager;


    void Awake() {
        
        // Instantiate each manager if they are null
        if (GameManager.Instance == null)
            Instantiate(gameManager);

        if (SoundManager.Instance == null)
            Instantiate(soundManager);
    }
}
