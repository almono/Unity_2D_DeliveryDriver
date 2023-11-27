using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    public float moveSpeed = 10f, turnSpeed = 300f, slowSpeed = 5f, fastSpeed = 18f;
    public bool hasPackage = false;
    public int packageScore = 0;

    private SpriteRenderer playerSprite;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float turnAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -turnAmount);
        transform.Translate(0, moveAmount, 0);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        moveSpeed = slowSpeed; // on any type of collision
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Package") && !hasPackage)
        {
            playerSprite.color = new Color32(0, 255, 34, 255);
            hasPackage = true;
            Destroy(other.gameObject, 0.2f);
        } else if (other.CompareTag("Customer") && hasPackage)
        {
            playerSprite.color = new Color32(255, 255, 255, 255);
            hasPackage = false;
            packageScore += 1;

            UpdateScore();
            StartCoroutine(PackageSpawner.instance.SpawnPackage());
        } else if(other.CompareTag("SpeedBoost"))
        {
            moveSpeed = fastSpeed;
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + packageScore;
    }
}
