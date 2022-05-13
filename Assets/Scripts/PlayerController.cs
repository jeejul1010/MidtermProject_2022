using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float speed = 8f;
    public float jumpForce = 300f;
    public int jumpLimit = 2;
    int jumpCount = 0;
    float jumpTimer = 0;
    public float mouseSensitivity;
    public Camera fpsCamera;
    
    float xRotation = 0f;

    public float stepRate = 3f;
    float nextTimeToStep;

    public AudioSource footStepSound;
    Vector3 prevPosition;

    public GameObject gun;

    public Image img;

    public bool isMetPrincess = false;

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        float inputMouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float inputMouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= inputMouseVertical;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //-90~90����

        fpsCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(0f, inputMouseHorizontal, 0f);

        Vector3 moveDirection = new Vector3(inputHorizontal, 0f, inputVertical);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            //transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }

        jumpTimer += Time.deltaTime;

        if(CheckOnGround())
        {
            jumpTimer = 0;
            jumpCount = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && CheckJumpable())
        {
            jumpTimer = 0;
            jumpCount++;
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }

        if(Time.time >= nextTimeToStep && CheckMoving() && CheckOnGround())
        {
            nextTimeToStep = Time.time + 1f / stepRate;
            footStepSound.PlayOneShot(footStepSound.clip);
        }

        prevPosition = transform.position;

    }

    bool CheckMoving()
    {
        float distance = Vector3.Distance(transform.position, prevPosition);

        return distance > 0.05f;
    }

    bool CheckJumpable()
    {
        return jumpCount < jumpLimit && (jumpTimer == 0 || jumpTimer > 0.3f);
    }

    bool CheckOnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("Gun"))
    //     {
    //         gun.SetActive(true);
    //         other.gameObject.SetActive(false);
    //     }
    //     else if(other.CompareTag("Orange"))
    //     {
    //         img.gameObject.SetActive(true);
    //         img.color = new Color32(229, 115, 53, 255);
    //         FirePaint firePaint = GetComponent<FirePaint>();
    //         firePaint.havePaint = true;
    //         firePaint.chosenPaintIndex = 0;
    //     }
    //     else if (other.CompareTag("Pink"))
    //     {
    //         img.gameObject.SetActive(true);
    //         img.color = new Color32(238, 111, 205, 255);
    //         FirePaint firePaint = GetComponent<FirePaint>();
    //         firePaint.havePaint = true;
    //         firePaint.chosenPaintIndex = 1;
    //     }
    //     else if (other.CompareTag("Green"))
    //     {
    //         img.gameObject.SetActive(true);
    //         img.color = new Color32(82, 209, 93, 255);
    //         FirePaint firePaint = GetComponent<FirePaint>();
    //         firePaint.havePaint = true;
    //         firePaint.chosenPaintIndex = 2;
    //     }
    //     else if(other.CompareTag("Princess"))
    //     {
    //         isMetPrincess = true;
    //     }

    // }

    void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();

        if(col.gameObject.CompareTag("Gun"))
        {
            Debug.Log("I hit gun");
            gun.SetActive(true);
            col.gameObject.SetActive(false);
        }
        else if(col.gameObject.CompareTag("Orange"))
        {
            rb.isKinematic = true;
            Debug.Log("I hit orange");
            img.gameObject.SetActive(true);
            img.color = new Color32(229, 115, 53, 255);
            FirePaint firePaint = GetComponent<FirePaint>();
            firePaint.havePaint = true;
            firePaint.chosenPaintIndex = 0;
        }
        else if(col.gameObject.CompareTag("Pink"))
        {
            rb.isKinematic = true;
            Debug.Log("I hit pink");
            img.gameObject.SetActive(true);
            img.color = new Color32(238, 111, 205, 255);
            FirePaint firePaint = GetComponent<FirePaint>();
            firePaint.havePaint = true;
            firePaint.chosenPaintIndex = 1;
        }
        else if(col.gameObject.CompareTag("Green"))
        {
            rb.isKinematic = true;
            Debug.Log("I hit green");
            img.gameObject.SetActive(true);
            img.color = new Color32(82, 209, 93, 255);
            FirePaint firePaint = GetComponent<FirePaint>();
            firePaint.havePaint = true;
            firePaint.chosenPaintIndex = 2;
        }
        else if(col.gameObject.CompareTag("Princess"))
        {
            isMetPrincess = true;
        }
    }

}
