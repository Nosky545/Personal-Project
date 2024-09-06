using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float speed = 1f;
    public float rotateSpeed = 30f;
    public float sizeSpeed = 1f;

    public Vector3 topLimit = new Vector3(0, 5, 0);
    public Vector3 bottomLimit = new Vector3(0, 0, 0);

    public float maxSize = 2f;
    public float minSize = 1f;

    private Renderer objRenderer;
    
    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }
    
    void Update()
    {
        // Rotation
        Rotation();
    
        // Translation
        GoingUpAndDown();
        
        // Scaling
        SizingUpAndDown();

        // Coloring
        Coloring();
    
    }

    void Rotation()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    void GoingUpAndDown()
    {
        transform.position = Vector3.Lerp(bottomLimit, topLimit, Mathf.PingPong(Time.time * speed, 1));
    }

    void SizingUpAndDown()
    {
        float scale = Mathf.Lerp(minSize, maxSize, Mathf.PingPong(Time.time * speed, 1));
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Coloring()
    {
        float r = Mathf.PingPong(Time.time * speed, 1);
        float g = Mathf.PingPong(Time.time * speed + 0.5f, 1);
        float b = Mathf.PingPong(Time.time * speed + 1, 1);

        objRenderer.material.color = new Color(r, g, b);
    }
}
