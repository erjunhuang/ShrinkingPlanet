using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour {
    private static GameManager _gameManager;
    public static GameManager Game
    {
        get { return _gameManager; }
    }

    private static MeteorSpawner _meteorSpawner;
    public static MeteorSpawner meteorSpawner
    {
        get { return _meteorSpawner; }
    }

    private static PlayerController _playerController;
    public static PlayerController playerController
    {
        get { return _playerController; }
    }

    private static GameUIManager _gameUIManager;
    public static GameUIManager gameUIManager
    {
        get { return _gameUIManager; }
    }
    private static AudioManager _audioManager;
    public static AudioManager audioManager
    {
        get { return _audioManager; }
    }
     
    // Use this for initialization
    void Awake () {

        _gameManager = GetComponent<GameManager>();
        _meteorSpawner = GetComponent<MeteorSpawner>();
        _playerController = GetComponent<PlayerController>();
        _gameUIManager = GetComponent<GameUIManager>();
        _audioManager = GetComponent<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
