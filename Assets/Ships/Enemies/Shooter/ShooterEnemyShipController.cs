using UnityEngine;

public class ShooterEnemyShipController : EnemyShipController
{
    [Header("Enemy Shooter Specs")]
    public float shootDistance = 0.0f;
    public Transform cannon;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;

        s_Rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        LookPlayer();

        if (playerTransform != null)
        {
            float distanceFromPlayer = Vector3.Distance(s_Rigidbody.transform.position, playerTransform.position); ;
            if (distanceFromPlayer <= shootDistance && CanShoot()) Shoot(cannon);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCannonBall"))
        {
            Damage();
        }
    }
}
