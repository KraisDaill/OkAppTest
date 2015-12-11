using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float spawnTime = 1f;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Transform[] enemies;

    private float _curSpawnTime;
    
	private void Start ()
    {
        _curSpawnTime = spawnTime;
	}
	
	private void Update ()
    {
	    if (_curSpawnTime < 0)
        {
            _curSpawnTime = spawnTime - (spawnTime / 1.5f * GameController.TimePercent);
            Spawn();
        }
        _curSpawnTime -= Time.deltaTime;

        if (GameController.TimePercent >= 1)
            Destroy(this);
	}

    private void Spawn()
    {
        var enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var enemyRigidBody = enemy.GetComponent<Rigidbody>();
        var enemyController = enemy.GetComponent<EnemyController>();

        enemy.parent = transform;
        enemy.position = spawnPoint.position;
        enemy.rotation = spawnPoint.rotation;

        switch (enemyController.MoveType)
        {
            case MoveType.Forward:
                enemyRigidBody.velocity = enemy.forward * (speed + speed * GameController.TimePercent);
                break;
            case MoveType.Diagonal:
                enemy.rotation *= Quaternion.Euler(0, Random.Range(0, 2) > 0 ? -45 : 45, 0);
                enemyRigidBody.velocity = enemy.forward * (speed + speed * GameController.TimePercent);
                break;
        }

        GameController.EnemyCtreate(enemyController);
    }
}
