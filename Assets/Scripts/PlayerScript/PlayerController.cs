using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Componentes
    Rigidbody playerRb;

    // Variables
    public float playerSpeed;
    public float jumpForce;

    public float smoothCrouch;

    // Codiciones
    public bool onGround;
    public bool jump;
    public bool crouch;
    public bool crouching;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJumping();
        Crounch();
    }

    private void FixedUpdate()
    {
        PlayerMovimiento();
    }

    void PlayerMovimiento()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3 (moveX, 0f, moveZ);

        transform.Translate(movimiento * playerSpeed * Time.fixedDeltaTime);
    }

    void PlayerJumping()
    {
        jump = Input.GetKeyDown(KeyCode.Space);

        if (jump && onGround && crouching == false)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    public void Crounch()
    {
        if (crouching == false)
        {
            crouch = Input.GetKey(KeyCode.RightControl);

            float targetLocalScaleY = crouch ? 0.65f : 1f;
            float newScaleY = Mathf.Lerp(transform.localScale.y, targetLocalScaleY, Time.deltaTime * smoothCrouch);

            transform.localScale = new Vector3(1, newScaleY, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Under"))
        {
            crouching = true;

            float newScaleY = Mathf.Lerp(transform.localScale.y, 0.65f, Time.deltaTime * smoothCrouch);

            transform.localScale = new Vector3(1, newScaleY, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Under"))
        {
            crouching = false;

            float newScaleY = Mathf.Lerp(transform.localScale.y, 1f, Time.deltaTime * smoothCrouch);

            transform.localScale = new Vector3(1, newScaleY, 1);
        }

    }




}
