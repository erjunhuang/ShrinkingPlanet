using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YanlzFramework;

public class GameUIManager : MonoBehaviour {
    public GameObject ScoreUI,gameOverUI;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver() {
        gameOverUI.SetActive(true);
    }

}
