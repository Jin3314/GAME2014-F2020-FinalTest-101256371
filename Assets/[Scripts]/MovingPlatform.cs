/*
 MovingPlatform.cs, YeongjinLim, 101256371,last Modified in 2021-12-14, script for moving and shrinking platform.
 Revision History;
 01 - Added floating feature
 02 - Added shrinking feature
 03 - Added SFXs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//main class of moving(shrinking) platform.
public class MovingPlatform : MonoBehaviour
{
    //variables for moving(shrinking) platform.
    public bool isUpDown;
    public float movingSpeed; 
    public float amplitude;  
    Vector2 startPosition;
    Vector3 defaultScale = new Vector3(1, 1, 1);
    //variables for sfx.
    public AudioClip audioShrink;
    public AudioClip audioShrinkBack;
    AudioSource audioSource;

    

    Rigidbody2D rb;

 
    //Start 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        this.audioSource = GetComponent<AudioSource>();
    }

    //FixedUpdate for floating.
    private void FixedUpdate()
    {
        if (isUpDown)
        {
            rb.transform.position = new Vector2(startPosition.x, startPosition.y + amplitude * Mathf.Sin(Time.time * movingSpeed));
        }
        else
        {
            rb.transform.position = new Vector2(startPosition.x + amplitude * Mathf.Sin(Time.time * movingSpeed), startPosition.y);
        }

    }

    //If player stays on platform, platform will be shirnk.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && collision.gameObject.GetComponent<PlayerBehaviour>().isGrounded)
        {
            PlaySound("SHRINK");
            defaultScale = transform.localScale;
            Vector3 scale = defaultScale;
            scale.x = scale.x * 0.9f;
            scale.y = scale.y * 0.9f;
            transform.localScale = scale;
        }
    }

    //If player exits platform, platform will be shirnk back to original size.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(transform.localScale.x < 0.4) // if platform's x size is smaller than 0.4, it will be shrink back
            {
                StartCoroutine(ShrinkBack());
            }
            

        }
    }

    //coroutine for shrinkBack
    IEnumerator ShrinkBack()
    {
        yield return new WaitForSeconds(2);

        PlaySound("SHRINKBACK");
        transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(2);

    }

    //simple sfx function.
    void PlaySound(string action)
    {
        switch (action)
        {
            case "SHRINK":
                audioSource.clip = audioShrink;
                break;
            case "SHRINKBACK":
                audioSource.clip = audioShrinkBack;
                break;
        }
        audioSource.Play();
    }
}