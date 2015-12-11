using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float levelTime = 60f;

    [SerializeField]
    private byte maxLife = 3;

    [SerializeField]
    private byte maxBomb = 3;

    private float _startTime;
    private byte _curLife;
    private byte _curBomb;
    private uint _curScore;

    private List<EnemyController> _enemies;

    private static GameController _This = null;

    #region RO properties
    public static float TimePercent
    {
        get
        {
            return (Time.time - _This._startTime) / _This.levelTime;
        }
    }

    public static float CurLife
    {
        get
        {
            return _This._curLife;
        }
    }

    public static float CurBomb
    {
        get
        {
            return _This._curBomb;
        }
    }

    public static float MaxBomb
    {
        get
        {
            return _This.maxBomb;
        }
    }

    public static float MaxLife
    {
        get
        {
            return _This.maxLife;
        }
    }

    public static uint CurScore
    {
        get
        {
            return _This._curScore;
        }
    }
    #endregion

    private void Awake()
    {
        if (_This != this)
            if (_This == null)
                _This = this;
            else
                Destroy(this);
    
        _startTime = Time.time;
        _curLife = maxLife;
        _curBomb = maxBomb;
        _curScore = 0;
        _enemies = new List<EnemyController>();
	}

    public static void EnemyCtreate(EnemyController enemyController)
    {
        _This._enemies.Add(enemyController);
    }

    public static void EnemyDestroy(EnemyController enemyController)
    {
        _This._enemies.Remove(enemyController);
        _This._curScore++;

        if (enemyController.KillOwner == KillOwner.Trigger && enemyController.DieType == DieType.Default)
            _This._curLife--;

        if (enemyController.KillOwner == KillOwner.Player && enemyController.DieType == DieType.NotPlayer)
            _This._curLife = 0;

        if (_This._curLife == 0)
        {
            LoseGame();
            return;
        }

        if (TimePercent >= 1f && _This._enemies.Count == 0)
        {
            WinGame();
            return;
        }
    }
    
    public static void EnemyKillAll()
    {
        if (_This._curBomb > 0 && _This._enemies.Count > 0)
        {
            _This._curBomb--;
            _This._enemies.ForEach
            (
                x =>
                {
                    x.Kill(KillOwner.Bomb);
                }
            );
        }
    }

    public static void WinGame()
    {
        GUIGameController.ShowWinScreen();
    }

    public static void LoseGame()
    {
        GUIGameController.LoseWinScreen();
    }
}
