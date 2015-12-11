using UnityEngine;
using System.Collections;

public enum MoveType
{
    Forward = 0,
    Diagonal = 1,
}

public enum DieType
{
    Default = 0,
    NotPlayer = 1,
}

public enum KillOwner
{
    Player = 0,
    Trigger = 1,
    Bomb = 2,
}

public class EnemyController : MonoBehaviour
{
    private const float SLOW_SPEED = 5f;

    [SerializeField]
    private MoveType moveType;

    [SerializeField]
    private DieType dieType;

    private KillOwner _killOwner;

    public MoveType MoveType
    {
        get
        {
            return moveType;
        }
    }

    public DieType DieType
    {
        get
        {
            return dieType;
        }
    }

    public KillOwner KillOwner
    {
        get
        {
            return _killOwner;
        }
    }

    private void OnMouseDown ()
    {
        Kill(KillOwner.Player);
	}
	
    public void Kill(KillOwner killOwner)
    {
        _killOwner = killOwner;
        GetComponent<Animator>().SetBool("IsDie", true);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().velocity /= SLOW_SPEED;
    }

    //use in animator
	private void Die ()
    {
        GameController.EnemyDestroy(this);
        DestroyObject(gameObject);
	}

}
