using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIGameController : MonoBehaviour
{
    [SerializeField]
    private Text lifeLabel;

    [SerializeField]
    private Text bombLabel;

    [SerializeField]
    private Text timeLabel;

    [SerializeField]
    private Material progressBar;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private GameObject pauseScreen;

    private static GUIGameController _This = null;

    void Start ()
    {
        if (_This != this)
            if (_This == null)
                _This = this;
            else
                Destroy(this);
	}
	
	void Update ()
    {
        lifeLabel.text = "Life: " + GameController.CurLife + "/" + GameController.MaxLife;
        bombLabel.text = "Bomb\n" + GameController.CurBomb + "/" + GameController.MaxBomb;
        timeLabel.text = GameController.TimePercent.ToString("0.0%");
        progressBar.SetFloat("_Percent", GameController.TimePercent);
    }

    //use in gui
    public void ShowPause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    //use in gui
    public void HidePause()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    //use in gui
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
    }

    //use in gui
    public void MainMenu()
    {
        Application.LoadLevel(0);
        Time.timeScale = 1;
    }

    //use in gui
    public void BombActive()
    {
        GameController.EnemyKillAll();
    }

    public static void ShowWinScreen()
    {
        Time.timeScale = 0;
        _This.winScreen.SetActive(true);
    }

    public static void LoseWinScreen()
    {
        Time.timeScale = 0;
        _This.loseScreen.SetActive(true);
    }
    
}
