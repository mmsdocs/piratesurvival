using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour
{
    public float speed;
    public float lifetime = 1.0f;

    private Rigidbody2D cb_RigidBody;

    private void Start()
    {
        cb_RigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        cb_RigidBody.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (tag.Contains("Player") && collider.CompareTag("Enemy"))
        {
            LevelManager level = LevelManager.Instance;
            level.AddPoint();
        }
        else if (tag.Contains("Enemy") && collider.CompareTag("Player"))
        {
            PlayerHealth player = collider.GetComponent<PlayerHealth>();
            if (player != null) player.Damage();
        }

        Destroy(gameObject);
    }
}
