using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float mouseX;
    public float mouseY;

    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody playerRb;

    public bool hasPower;

    public GameObject pickupIndicator;

    private Animator animator;

    private Vector3 previousPosition;

    private GameManager gameManager;

    public float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (hasPower)
        {
            timeRemaining -= Time.deltaTime;
            gameManager.powerupSlider.value = timeRemaining;
        }

        previousPosition.x = transform.position.x;
        previousPosition.z = transform.position.z;

        pickupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        if (transform.position.x != previousPosition.x || transform.position.z != previousPosition.z)
        {
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void Look()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -40, 40);

        transform.localRotation = Quaternion.Euler(0, mouseX, 0);
    }

    void Jump()
    {
        if (transform.position.y < 1)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            hasPower = true;
            Destroy(other.gameObject);
            StartCoroutine(PickupCountdownRoutine());
            pickupIndicator.SetActive(true);
            gameManager.powerupUI.SetActive(true);
            timeRemaining = 10;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPower == true)
        {
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("Enemy") && hasPower == false)
        {
            gameManager.GameOver();
        }
    }

    IEnumerator PickupCountdownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPower = false;
        pickupIndicator.SetActive(false);
        gameManager.powerupUI.SetActive(false);
    }
}
