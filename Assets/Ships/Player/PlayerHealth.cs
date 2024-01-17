using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private Transform shipStates;
    [SerializeField] private GameObject explosion;
    [SerializeField] private int amount = 10;
    private int currentShipState = 0;

    private int totalAmount = 10;

    private void Start()
    {
        totalAmount = amount;
        healthBar.SetValue(totalAmount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) Damage();
    }

    private void OnDestroy()
    {
        if (amount == 0) LevelManager.Instance.GameOver();
    }

    public void Damage(int damageAmount = 1)
    {
        amount -= damageAmount;
        healthBar.SetValue(amount);
        if (amount >= 0) NextShipState();
        if (amount <= 0) Explode();
    }

    private void NextShipState()
    {
        if (shipStates.childCount > 0 && currentShipState < shipStates.childCount)
        {
            shipStates.GetChild(currentShipState).gameObject.SetActive(false);
            currentShipState++;
            shipStates.GetChild(currentShipState).gameObject.SetActive(true);
        }
    }

    private void Explode()
    {
        GameObject explosionInstance = Instantiate(explosion, transform);
        Animator explosionAnimator = explosionInstance.GetComponent<Animator>();

        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        PlayerMovement player = GetComponent<PlayerMovement>();
        player.Stop();

        explosionAnimator.SetTrigger("OnExplode");
        float animationLength = explosionAnimator.runtimeAnimatorController.animationClips[0].length;
        if (transform.parent.CompareTag("ShipController")) Destroy(transform.parent.gameObject, animationLength);
    }
}
