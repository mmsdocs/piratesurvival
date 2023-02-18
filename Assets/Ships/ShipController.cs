using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Ship Common Specs")]
    public int health = 3;
    public int maxHealth = 3;
    public HealthBarController healthBar;
    public float cannonBallShootCooldown = 0.0f;
    public Vector3 cannonBallExplosionScale = new Vector3(0.25f, 0.25f, 1.0f);
    public GameObject cannonBall;
    public AudioSource cannonSfx;

    [Header("Animations Specs")]
    public GameObject explosion;

    protected Rigidbody2D s_Rigidbody;
    protected bool canMove = true;
    protected int currentShipState = 0;

    private float shootInstant = 0.0f;

    protected virtual void NextShipState() => currentShipState++;

    protected virtual void SetShipState(int state) => currentShipState = state;

    protected void Explode()
    {
        GameObject explosionInstance = Instantiate(explosion, transform);
        Animator explosionAnimator = explosionInstance.GetComponent<Animator>();

        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        s_Rigidbody.velocity = Vector3.zero;
        canMove = false;
        
        explosionAnimator.SetTrigger("OnExplode");
        if (transform.parent.CompareTag("ShipController")) Destroy(transform.parent.gameObject, 0.34f);
    }

    protected void DecreaseHealth(int value = 1)
    {
        health = Mathf.Clamp(health - value, 0, maxHealth);
        healthBar.SetValue(health);
    }

    protected void Damage()
    {
        DecreaseHealth();
        NextShipState();
        if (health == 0)
        {
            Explode();
        }
    }

    protected bool CanShoot()
    {
        return Time.realtimeSinceStartup - shootInstant >= cannonBallShootCooldown;
    }

    protected void Shoot(Transform fromWeapon)
    {
        GameObject explosionInstance = Instantiate(explosion, fromWeapon);
        explosionInstance.transform.localScale = cannonBallExplosionScale;

        Animator explosionAnimator = explosionInstance.GetComponent<Animator>();
        explosionAnimator.SetTrigger("OnExplode");

        GameObject cannonBallInstance = Instantiate(cannonBall, fromWeapon.position, fromWeapon.rotation);
        cannonBallInstance.tag = gameObject.tag + "CannonBall";
        cannonBallInstance.layer = gameObject.layer;
        shootInstant = Time.realtimeSinceStartup;

        cannonSfx.Play();
    }
}
