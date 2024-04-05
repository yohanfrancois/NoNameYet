using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float slideDuration = 1.0f;
    public float slidingSpeed = 5.0f;
    public float slidingCooldown = 10f;
    public TextMeshProUGUI cooldownText;


    private SpriteRenderer spriteRenderer;
    private float horizontal;
    private Animator anim;
    private bool isSliding = false;
    private bool slideReady = true;
    private float slideTimer;
    private float slideCooldownTimer = 0;
    private bool isDead = false;
    private BombSpawner bombSpawner;
    private PauseManager pauseManager;

    public bool IsDead => isDead;


    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bombSpawner = GetComponent<BombSpawner>();
        pauseManager = FindObjectOfType<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.position.x > 9)
        {
            transform.position = new Vector3(9f, transform.position.y, 0f);
            horizontal = 0f;
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(-9f, transform.position.y, 0f);
            horizontal = 0f;
        }
        else 
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        if (!isDead && !pauseManager.IsPaused) { 
            anim.SetFloat("Speed", Mathf.Abs(horizontal));
            spriteRenderer.flipX = horizontal < 0f ? true : false;


            Vector3 movement = new Vector3(horizontal, 0f, 0f);
            transform.Translate(movement * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && !isSliding && horizontal != 0 && slideReady)
            {
                StartSlide();
            }
            if (isSliding)
            {
                Slide();
            }

            ApplyCooldown();
        }
    }

    void StartSlide()
    {
        isSliding = true;
        slideReady = false;
        slideCooldownTimer = slidingCooldown;
        slideTimer = 0f;
        anim.SetBool("isSliding", true);
    }

    void Slide()    
    {
        slideTimer += Time.deltaTime;

        // Move the player horizontally during the slide
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalInput * slidingSpeed * Time.deltaTime, 0f, 0f));

        // End the slide after the specified duration
        if (slideTimer >= slideDuration)
        {
            isSliding = false;
            anim.SetBool("isSliding", false);
        }
    }

    void ApplyCooldown()
    {
        if (!slideReady)
        {
            slideCooldownTimer -= Time.deltaTime;
            cooldownText.text = Math.Round(slideCooldownTimer, 1) + "s";
            if (slideCooldownTimer < 0f)
            {
                slideReady = true;
                cooldownText.text = "Ready !";
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb") || collision.gameObject.CompareTag("Explosion"))
        {
            Debug.Log("Collision");
            anim.SetBool("isDead", true);
            isDead = true;
            bombSpawner.CancelInvoke("SpawnBomb");
        }
    }
}
