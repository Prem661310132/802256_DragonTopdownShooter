using System;
using UnityEngine;
using System.Collections;
public class DragonController : MonoBehaviour
{
    public GameObject fireballPatternD1;
    public GameObject fireballPatternD2;
    public GameObject fireballPatternT1;
    public GameObject fireballPatternT2;
    public GameObject fireballPatternT3;
    public Transform firePoint;
    public Transform T3firePoints;
    public Vector3 firePointOffset;
    public float patternIntervalPhase1 = 1.5f;
    public float patternIntervalPhase2 = 0.5f;
    public float moveSpeed = 3f;
    public HealthBar bossHealthBar;
    public int maxHealth = 100;
    private int currentHealth;
    private int PatternD = 1;
    private int PatternT = 1;
    public bool isPhase2 = false;
    public int damage = 1;
    private bool isImmortal = false; 
    private bool isAttacking = true;

    private float patternTimer;
    private Animator animator;

    void Start()
    {
        patternTimer = patternIntervalPhase1;
        currentHealth = maxHealth;
        bossHealthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        firePoint.position = transform.position + firePointOffset;

        if (!isPhase2 && currentHealth <= maxHealth / 2)
        {
            EnterPhase2();
        }

        patternTimer -= Time.deltaTime;
        if (patternTimer <= 0&& isAttacking)
        {
            if (!isPhase2)
            {
                if (PatternD == 1)
                {
                    ShootPatternD1();
                }
                else if (PatternD == 2)
                {
                    ShootPatternD2();
                }
            }
            else

            {
                 if (PatternT == 1 || PatternT == 3 || PatternT == 5)
                 {
                     ShootPatternT1();
                 }
                 else if (PatternT == 2 || PatternT == 4 || PatternT == 6)
                 {
                     ShootPatternT2();
                 }
                 else if (PatternT == 7)
                 {
                     ShootPatternT3();
                 }
            }

                if (isPhase2 == true)
                {
                    patternTimer = patternIntervalPhase2;

                }
                else if (!isPhase2)
                {
                    patternTimer = patternIntervalPhase1;
                }
            
        }
        
        if (!isPhase2)
        {
            MoveInNPattern();
        }
        else
        {
            MoveInSPattern();
        }
    }

    void EnterPhase2()
    {
        isPhase2 = true;
        animator.SetTrigger("Transform"); 
        
        transform.localScale = new Vector3(2, 2, 1);
        bossHealthBar.ChangeToPhase2Color();
        StartCoroutine(Phase2Immortality());
    }

    void ShootPatternD1()
    {
        Instantiate(fireballPatternD1, firePoint.position, firePoint.rotation);
        ++PatternD;
    }

    void ShootPatternD2()
    {
        Instantiate(fireballPatternD2, firePoint.position, firePoint.rotation);
        --PatternD;
    }

    void ShootPatternT1()
    {
        Instantiate(fireballPatternT1, firePoint.position, firePoint.rotation);
        ++PatternT;
    }

    void ShootPatternT2()
    {
        Instantiate(fireballPatternT2, firePoint.position, firePoint.rotation);
        ++PatternT;
    }

    void ShootPatternT3()
    {
        Instantiate(fireballPatternT3, T3firePoints.position, T3firePoints.rotation);
        PatternT -= 6;
    }

    void MoveInNPattern()
    {
        float x = Mathf.PingPong(Time.time * moveSpeed, 12) - 6;
        float y = Mathf.Sin(Time.time * moveSpeed) * 1.1f + 2.5f;
        transform.position = new Vector3(x, y, 0);
    }

    void MoveInSPattern()
    {
        float x = Mathf.Sin(Time.time * moveSpeed) * 7;
        float y = Mathf.PingPong(Time.time * moveSpeed, 4.5f) ;
        transform.position = new Vector3(x, y, 0);
    }

    public void TakeDamage(int damage)
    {
        if (isImmortal) return;
        currentHealth -= damage;
        bossHealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


    IEnumerator Phase2Immortality()
    {
        isImmortal = true;
        isAttacking = false;

        yield return new WaitForSeconds(7f); 

        isImmortal = false;
        isAttacking = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
         
        }
    }
}