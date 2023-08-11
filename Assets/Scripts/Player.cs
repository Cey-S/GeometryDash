using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform sprite;
    public ParticleSystem dirtParticle;
    private Rigidbody2D rb;

    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationDegree;
    [SerializeField] private float flipDuration;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                StartCoroutine(Flip(rotationDegree, flipDuration));
                dirtParticle.Stop();

                isGrounded = false;
            }
        }        
    }

    private IEnumerator Flip(float degree = 180.0f, float duration = 1.0f)
    {
        Quaternion beginRot = sprite.rotation;
        Quaternion endRot = sprite.rotation * Quaternion.Euler(Vector3.forward * degree);

        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
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
