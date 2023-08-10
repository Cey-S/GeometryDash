using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform sprite;
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

                isGrounded = false;
            }
        }        
    }

    private IEnumerator Flip(float degree = 180.0f, float duration = 1.0f)
    {
        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            sprite.Rotate(Vector3.back * degree * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 endRotation = sprite.rotation.eulerAngles;
        endRotation.z = Mathf.Round(endRotation.z / 180) * 180;
        sprite.rotation = Quaternion.Euler(endRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
}
