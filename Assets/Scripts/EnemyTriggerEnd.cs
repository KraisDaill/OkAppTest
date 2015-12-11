using UnityEngine;
using System.Collections;

public class EnemyTriggerEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            other.GetComponent<EnemyController>().Kill(KillOwner.Trigger);
    }
}
