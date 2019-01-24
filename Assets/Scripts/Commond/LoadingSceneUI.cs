using UnityEngine.SceneManagement;
using UnityEngine;
using YanlzFramework;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class LoadingSceneUI : MonoBehaviour
{
    public Text text;
    public delegate void LoadingEvent();
    private void Start()
    {
        text = GetComponent<Text>();
    }
    public void Load(EnumSceneType sceneType) {
        YanlzFramework.SceneManager.Instance.ChangeSceneDirect(sceneType,(progress) =>{
            text.text = "100/"+ Mathf.FloorToInt(progress * 100);
        });
    }
}
