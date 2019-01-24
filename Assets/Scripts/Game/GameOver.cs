using UnityEngine;
using YanlzFramework;

public class GameOver : MonoBehaviour {

	void Update ()
	{
		if (Input.GetButtonDown("Jump"))
		{
			Managers.Game.Restart();
		}

		GetComponent<RectTransform>().localScale = Vector3.one * Planet.Size;
	}

	public void Menu ()
	{
        //SceneManager.LoadScene("Menu");
        SceneManager.Instance.ChangeSceneDirect(EnumSceneType.HallScene);
	}

}
