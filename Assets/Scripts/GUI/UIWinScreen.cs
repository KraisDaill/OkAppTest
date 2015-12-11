using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIWinScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stars;

    [SerializeField]
    private Text scoreLabel;

    private void OnEnable ()
    {
        var i = 0;
        for (; i < GameController.CurLife && i < stars.Length; i++)
            stars[i].SetActive(true);
        for (; i < stars.Length; i++)
            stars[i].SetActive(false);

        scoreLabel.text = "Score: " + GameController.CurScore * 33 + GameController.CurScore * 3 + GameController.CurScore;
    }

}
