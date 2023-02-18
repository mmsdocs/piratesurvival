using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour
{
    public float speed;
    public float lifetime = 1.0f;

    private Rigidbody2D cb_RigidBody;
    private float startTime = 0.0f;

    private void Start()
    {
        cb_RigidBody = GetComponent<Rigidbody2D>();
        startTime = Time.timeSinceLevelLoad;
    }

    private void FixedUpdate()
    {
        cb_RigidBody.velocity = transform.up * speed;
        if (Time.timeSinceLevelLoad - startTime >= lifetime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            PlayerShipController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShipController>();
            player.IncrementScore();
        }

        Destroy(gameObject);
    }
}
