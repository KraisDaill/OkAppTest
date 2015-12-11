using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour
{
    //use in gui
    public void StartGame ()
    {
        Application.LoadLevel(1);
	}

    //use in gui
    public void Exit ()
    {
        Application.Quit();
	}
}
