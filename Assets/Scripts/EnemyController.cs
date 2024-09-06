using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public float enemySpeed = 1f;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasPower)
        {
            Vector3 lookDirection = (transform.position - player.transform.position).normalized;
            transform.Translate(enemySpeed * Time.deltaTime * lookDirection);
        }

        else
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(enemySpeed * Time.deltaTime * lookDirection);
        }
    }
}
