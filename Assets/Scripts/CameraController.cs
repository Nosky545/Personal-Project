using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // La camera suit la position du joueur
        transform.position = player.transform.position;

        // Rotation sur l'Axe Y
        transform.rotation = player.transform.rotation;
    }
}
