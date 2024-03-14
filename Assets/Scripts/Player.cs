using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 2.5f;
    private bool isFacingRight = true;
    private bool isWalking = false;
    private bool isIdle = false;
    private bool playIdle = true;
    private float currentVelocity;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    private CoinManager coinManager;
    [SerializeField] private HealthScript healthSystem;
    private float ScreenWidth;
    void Start()
    {
        ScreenWidth = Screen.width;
    }
    void Update()
    {
        int i = 0;
        /*while(i < Input.touchCount)
        {
            if(Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                horizontal = -1;
            }
            if (Input.GetTouch(i).position.x >= ScreenWidth / 2)
            {
                horizontal = 1;
            }
            i++;
        }
        if(Input.touchCount == 0)
        {
            horizontal = 0;
        }*/
        horizontal = Input.GetAxisRaw("Horizontal");
       
        currentVelocity = rb.velocity.x;

        if (currentVelocity != 0 && !isWalking)
        {
            anim.Play("walk", 0, 0);
            isWalking = true;
            playIdle = true;
            isIdle = true;
        }
        if (currentVelocity == 0)
        {
            isWalking = false;
            isIdle = false;
        }
        if (!isIdle && playIdle)
        {
            anim.Play("idle", 0, 0);
            isIdle = true;
            playIdle = false;
        }
        Flip();
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;
        if (other.CompareTag("coin"))
        {
            coinManager = GameObject.Find("CoinsText").GetComponent<CoinManager>();
            coinManager.addCoin(1);
            Destroy(otherGameObject);
        }
        if (other.CompareTag("obstacle"))
        {
            GameObject healthObject = GameObject.Find("Health");
            healthSystem = healthObject.GetComponent<HealthScript>();
            healthSystem.takeDamage();
            Destroy(otherGameObject);
        }
    }
    
}
