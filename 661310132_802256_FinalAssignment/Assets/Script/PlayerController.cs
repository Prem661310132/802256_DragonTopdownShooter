using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthBar playerHealthBar;
    public int maxHealth = 20;
    private int currentHealth;
    public float speed = 7.5f;
    public float bulletSpeed = 12f;
    private bool isImmortal = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        playerHealthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }


    void Update()
    {
        float moveX = Input.GetAxis("JoyHorizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("JoyVertical") * speed * Time.deltaTime;
        transform.Translate(moveX, moveY, 0);

        // ยิงกระสุนตามทิศทางลูกศรที่กด
        if (Input.GetButtonDown("Y_Button"))
        {
            Shoot(Vector2.up);
        }
        else if (Input.GetButtonDown("A_Button"))
        {
            Shoot(Vector2.down);
        }
        else if (Input.GetButtonDown("X_Button"))
        {
            Shoot(Vector2.left);
        }
        else if (Input.GetButtonDown("B_Button"))
        {
            Shoot(Vector2.right);
        }
        else if (Input.GetButtonDown("R2_Button"))
        {
            if (isImmortal == false)
            {
                isImmortal = true;
            }
            else if (isImmortal == true)
            {
                isImmortal = false;
            }
        }

    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Destroy(bullet, 2f);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerHealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    System.Collections.IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f); 
        spriteRenderer.color = originalColor; 
    }


    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player Died!");
    }
}