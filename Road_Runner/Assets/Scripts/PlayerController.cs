using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    private Animator playerAnim;
    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 10.0f;
    public bool gameOver = false;

    // Reference to the GameManager
    public GameManager gameManager;

    // Player's score
    private int score = 0;

    // UI text element to show the score
    public TextMeshProUGUI scoreText;

    // Audio clips for jump and crash
    public AudioClip jumpsound;
    public AudioClip crashsound;
    public AudioClip coinsound;
    public AudioClip gameoversound;
    private AudioSource playerAudio;

    // Particle systems for jumping and crashing
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        gameManager = FindObjectOfType<GameManager>();
        UpdateScoreText(); // Initialize the score text at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;

        // Bound player movement within the defined range
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        // Player jump logic
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpsound, 1.0f);
            dirtParticle.Stop();
        }
    }

    // Handle collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("gameObject"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashsound, 1.0f);
            playerAudio.PlayOneShot(gameoversound, 1.0f);
            dirtParticle.Stop();
            explosionParticle.Play();

            // Calling the GameOver method from GameManager
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }
    }

    // Method to increase score when collecting a coin
    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
        playerAudio.PlayOneShot(coinsound, 1.0f); // Update the score UI text
    }

    // Method to update the score UI text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Get the current score (useful for other scripts like GameManager)
    public int GetScore()
    {
        return score;
    }
}
