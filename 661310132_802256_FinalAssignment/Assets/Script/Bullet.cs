using UnityEngine;

public class Bullet: MonoBehaviour
{
    public float speed = 0f;
    public float lifetime = 2f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            DragonController dragon = collision.GetComponent<DragonController>();
            if (dragon != null)
            {
                dragon.TakeDamage(damage);
            }
            Destroy(gameObject);

        }
    }
}