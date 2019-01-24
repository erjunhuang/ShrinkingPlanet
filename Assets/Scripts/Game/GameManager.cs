using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public PlayerController currentPlayer;
    public FauxGravityAttractor currentFauxGravityAttractor;
    void Awake ()
	{
	}

	public void GameOver ()
	{
        Managers.gameUIManager.GameOver();
	}
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
