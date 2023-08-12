using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform sprite;
    public ParticleSystem dirtParticle;
    private Rigidbody2D rb;
    private UnityEngine.EventSystems.EventSystem currentEventSystem;

    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationDegree;
    [SerializeField] private float flipDuration;

    public static float Gravity;
    private float surfGravity;
    [SerializeField] private float surfVelocityLimit;

    private bool isGrounded;

    private void Awake()
    {
        currentEventSystem = UnityEngine.EventSystems.EventSystem.current;
        rb = GetComponent<Rigidbody2D>();
        Gravity = rb.gravityScale;
        surfGravity = Gravity * 0.5f;
    }

    private void FixedUpdate()
    {
        if (GameManager.IsGameRunning)
        {
            bool isThereInput = Input.GetMouseButton(0);
            if (isThereInput)
            {
                bool isOverUI = currentEventSystem.IsPointerOverGameObject();
                if (isOverUI)
                {
                    return; // UI element is clicked (Pause Button)
                }                
            }            

            if (GameManager.currentGameMode == GameManager.GameMode.Running)
            {
                if (isThereInput && isGrounded)
                {
                    Jump();
                }
            }
            else if (GameManager.currentGameMode == GameManager.GameMode.Surfing)
            {
                if (isThereInput)
                {
                    rb.gravityScale = -surfGravity;
                    if (rb.velocity.y > surfVelocityLimit)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, surfVelocityLimit);
                    }
                }
                else
                {
                    rb.gravityScale = surfGravity;
                    if (rb.velocity.y < -surfVelocityLimit)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -surfVelocityLimit);
                    }
                }
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        StartCoroutine(Flip(rotationDegree, flipDuration));
        dirtParticle.Stop();

        isGrounded = false;
    }

    private IEnumerator Flip(float degree = 180.0f, float duration = 1.0f)
    {
        Quaternion beginRot = sprite.rotation;
        Quaternion endRot = sprite.rotation * Quaternion.Euler(Vector3.forward * degree);

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            sprite.rotation = Quaternion.Slerp(beginRot, endRot, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SnapToGround();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;

            dirtParticle.Play();
            SnapToGround();
        }
    }

    private void SnapToGround()
    {
        Vector3 endRotation = sprite.rotation.eulerAngles;
        endRotation.z = Mathf.Round(endRotation.z / 180) * 180;
        sprite.rotation = Quaternion.Euler(endRotation);
    }
}
