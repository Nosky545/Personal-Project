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

    private Animator animator;

    private Vector3 previousPosition;

    private GameManager gameManager;

    public float timeRemaining;

    public AudioClip deathSound;
    public AudioClip enemyDeathSound;
    public AudioClip powerupSound;

    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<AudioSource>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            Move();
            Look();
        }

        if (hasPower)
        {
            timeRemaining -= Time.deltaTime;
            gameManager.powerupSlider.value = timeRemaining;
        }

        previousPosition.x = transform.position.x;
        previousPosition.z = transform.position.z;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            hasPower = true;
            Destroy(other.gameObject);
            StartCoroutine(PickupCountdownRoutine());
            gameManager.powerupUI.SetActive(true);
            timeRemaining = 10;
            animator.SetBool("isGrowing", true);
            playerAudio.PlayOneShot(powerupSound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPower == true)
        {
            Animator enemyAnim = collision.gameObject.GetComponentInChildren<Animator>();
            enemyAnim.SetBool("isDying", true);

            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.isAlive = false;

            GameObject dead = collision.gameObject;
            StartCoroutine(DeathCountdownRoutine(dead));

            playerAudio.PlayOneShot(enemyDeathSound);
        }

        else if (collision.gameObject.CompareTag("Enemy") && hasPower == false)
        {
            playerAudio.PlayOneShot(deathSound);
            gameManager.GameOver();
        }
    }

    IEnumerator PickupCountdownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPower = false;
        gameManager.powerupUI.SetActive(false);
        animator.SetBool("isGrowing", false);
    }

    IEnumerator DeathCountdownRoutine(GameObject dead)
    {
        yield return new WaitForSeconds(3);
        Destroy(dead);
    }
}
