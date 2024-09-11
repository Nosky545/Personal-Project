using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public float enemySpeed;
    public bool isAlive;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasPower)
        {
            Flee();
        }

        else
        {
            Attack();
        }
    }

    void Attack()
    {
        if (isAlive)
        {
            transform.Translate(enemySpeed * Time.deltaTime * Vector3.forward);
            transform.LookAt(player.transform.position);
        }
    }

    void Flee()
    {
        if (isAlive)
        {
            transform.Translate(enemySpeed * Time.deltaTime * Vector3.forward);
            transform.LookAt(-player.transform.position);
        }
    }
}
