using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float aimCooldown = 0.5f;
    private float currentAimCooldown = 0f;

    [Header("Front weapon Specs")]
    [SerializeField] private Transform frontWeapon;
    [SerializeField] private Transform frontWeaponBulletExit;
    [SerializeField] private GameObject frontAimLine;

    [Header("Side weapon Specs")]
    [SerializeField] private Transform[] sideWeapons;
    [SerializeField] private GameObject sideAimLine;

    [Header("Cannon Specs")]
    [SerializeField] private float cannonBallShootCooldown = 0f;
    [SerializeField] private Vector3 cannonBallExplosionScale = new Vector3(0.25f, 0.25f, 1f);
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private AudioSource cannonSfx;
    [SerializeField] private GameObject explosion;
    private bool canShoot = true;

    private void Update()
    {
        RotateFrontalCannon();
        Shoot();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.up * 4.0f);
    }

    private void RotateFrontalCannon()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float result = Vector2.Dot(transform.up, (mousePosition - (Vector2) transform.position).normalized);
        if (result > 0.75f)
        {
            Vector2 direction = mousePosition - (Vector2) frontWeapon.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction.normalized);
            angle = Mathf.Clamp(angle, -67.5f, 67.5f);
            frontWeapon.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private void Shoot()
    {
        if (!canShoot) return;

        if (Input.GetButton("Front Weapon"))
        {
            currentAimCooldown += Time.deltaTime;
            if (currentAimCooldown >= aimCooldown) frontAimLine.SetActive(true);
        }
        else if (Input.GetButton("Side Weapon"))
        {
            currentAimCooldown += Time.deltaTime;
            if (currentAimCooldown >= aimCooldown) sideAimLine.SetActive(true);
        }
        if (Input.GetButtonUp("Front Weapon"))
        {
            Shoot(frontWeaponBulletExit);
            Cooldown();
        }
        else if (Input.GetButtonUp("Side Weapon"))
        {
            foreach (Transform weapon in sideWeapons) Shoot(weapon);
            Cooldown();
        }
    }

    private void Shoot(Transform fromWeapon)
    {
        canShoot = false;

        GameObject explosionInstance = Instantiate(explosion, fromWeapon);
        explosionInstance.transform.localScale = cannonBallExplosionScale;

        Animator explosionAnimator = explosionInstance.GetComponent<Animator>();
        explosionAnimator.SetTrigger("OnExplode");

        GameObject cannonBallInstance = Instantiate(cannonBall, fromWeapon.position, fromWeapon.rotation);
        cannonBallInstance.tag = gameObject.tag + "CannonBall";
        cannonBallInstance.layer = gameObject.layer;

        cannonSfx.Play();
    }

    private void Cooldown()
    {
        currentAimCooldown = 0f;
        sideAimLine.SetActive(false);
        frontAimLine.SetActive(false);
        Invoke(nameof(ShootCooldown), cannonBallShootCooldown);
    }

    private void ShootCooldown() => canShoot = true;
}
