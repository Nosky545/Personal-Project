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

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -40, 40);

        transform.localRotation = Quaternion.Euler(0, mouseX, 0);

        pickupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
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
            Destroy(gameObject);
        }
    }

    IEnumerator PickupCountdownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPower = false;
        pickupIndicator.SetActive(false);
    }
}
