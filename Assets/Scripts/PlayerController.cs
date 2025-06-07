using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //public GameObject plusFiveTxt;

    [Header("Movement")]
    public float playerspeed = 5f;
    public Rigidbody2D rb;
    bool isPlayerGrounded;
    public bool isAlive;


    [Header("Settings for Jump")]
    public float playerJumpForce = 5f;
    public Transform groundCheck; 
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;


    [Header("Particles")]
    public ParticleSystem explotion;
    public ParticleSystem coinCollectEffect;
    public ParticleSystem redCoinCollectEffect;

    [Header("Red Canvas")]
    public GameObject redCanvas;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        OnPlayerMove();
    }

    void OnPlayerMove()
    {
        if (isAlive) 
        {
            playerspeed += Time.deltaTime * 0.1f;
            rb.linearVelocity = new Vector2(playerspeed, rb.linearVelocity.y);
        }

        isPlayerGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down , checkRadius, groundLayer);
        if ((Input.GetKeyDown(KeyCode.W)) && isPlayerGrounded && isAlive)
        {
            AudioManager.Instance.PlayJump();
            print("Jumped");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerJumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") && isAlive)
        {
            AudioManager.Instance.PlayDie();
            isAlive = false;
            ParticleSystem effect = Instantiate(explotion, transform.position, Quaternion.identity);
            effect.Play();
            StartCoroutine(ShowGameOverAfterDelay(1f));
            GameManager.instance.DisplayScore();
        }
    }

    IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIManager.instance.OnGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            PopUp();
            AudioManager.Instance.PlayCoinCollect();
            ParticleSystem effect = Instantiate(coinCollectEffect, transform.position, Quaternion.identity);
            effect.Play();

            Destroy(effect.gameObject, effect.main.duration);

            Destroy(collision.gameObject);

            GameManager.instance.UpdateScore();
        }

        if (collision.CompareTag("RedCoin"))
        {
            PopUp();
            AudioManager.Instance.PlayRedCoinCollect();
            ParticleSystem effect = Instantiate(redCoinCollectEffect, transform.position, Quaternion.identity);
            effect.Play();
            ShowCanvasTemporarily(0.2f);
            Destroy(effect.gameObject, effect.main.duration);

            Destroy(collision.gameObject);

            GameManager.instance.DecreaseScore();
        }

        if (collision.CompareTag("PremiumCoin"))
        {
            PopUp();
            AudioManager.Instance.PlayCoinCollect();
            ParticleSystem effect = Instantiate(coinCollectEffect, transform.position, Quaternion.identity);
            effect.Play();

            Destroy(effect.gameObject, effect.main.duration);
            Destroy(collision.gameObject);

            GameManager.instance.IncreasePlusFiveScore();
        }
    }

    public void ShowCanvasTemporarily(float duration)
    {
        StartCoroutine(EnableCanvasTemporarily(duration));
    }

    private IEnumerator EnableCanvasTemporarily(float duration)
    {
        redCanvas.SetActive(true); 
        yield return new WaitForSeconds(duration); 
        redCanvas.SetActive(false); 
    }

    void PopUp()
    {
        Vector3 originalScale = (GameManager.instance.score).transform.localScale;
        Vector3 targetScale = originalScale * 1.15f;

        LeanTween.scale((GameManager.instance.score).gameObject, targetScale, 0.1f).setEaseOutBounce().setOnComplete(() =>
        {
            LeanTween.scale((GameManager.instance.score).gameObject, originalScale, 0.2f).setEaseInOutQuad();
        });
    }
}
