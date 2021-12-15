using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool isUpDown;
    public float movingSpeed;
    public float amplitude;
    Vector2 startPosition;
    Vector3 defaultScale = new Vector3(1, 1, 1);

    Rigidbody2D rb;

    public float changespeed = 1;
    Vector3 temp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        //defaultScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        /* if (isUpDown)
        {
            rb.transform.position = new Vector2(startPosition.x, startPosition.y + amplitude * Mathf.Sin(Time.time * movingSpeed));
        }
        else
        {
            rb.transform.position = new Vector2(startPosition.x + amplitude * Mathf.Sin(Time.time * movingSpeed), startPosition.y);
        }*/

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && collision.gameObject.GetComponent<PlayerBehaviour>().isGrounded)
        {
           // collision.collider.transform.SetParent(transform);
            defaultScale = transform.localScale;
            Vector3 scale = defaultScale;
            scale.x = scale.x * 0.9f;
            scale.y = scale.y * 0.9f;
            transform.localScale = scale;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //collision.collider.transform.SetParent(null);
            if(transform.localScale.x < 0.4)
            {
                StartCoroutine(Shrink());
            }
            

        }
    }

    IEnumerator Shrink()
    {
        yield return new WaitForSeconds(2);

        transform.localScale = new Vector3(1, 1, 1);
    }
}