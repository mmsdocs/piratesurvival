using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyShipController : ShipController
{
    [Header("Enemy Ship Specs")]
    public float speed = 0.0f;
    public float safeDistanceFromPlayer = 0.0f;
    public Sprite[] shipStates;

    protected Transform playerTransform;
    protected SpriteRenderer spriteRenderer;

    protected void Move()
    {
        if (s_Rigidbody != null && canMove && isDistanceSafeFromPlayer())
        {
            s_Rigidbody.velocity = s_Rigidbody.transform.up * speed;
        }
    }

    protected void LookPlayer()
    {
        if (playerTransform != null)
        {
            float angle = Utils.AngleBetween(transform.position, playerTransform.position, Vector2.up);
            s_Rigidbody.MoveRotation(angle);
        }
       
    }

    protected override void NextShipState()
    {
        if (shipStates.Length > 0 && currentShipState < shipStates.Length)
        {
            currentShipState++;
            spriteRenderer.sprite = shipStates[currentShipState];
        }
    }

    protected override void SetShipState(int state)
    {
        if (shipStates.Length > 0 && state < shipStates.Length)
        {
            currentShipState = state;
            spriteRenderer.sprite = shipStates[currentShipState];
        }
    }

    private bool isDistanceSafeFromPlayer() => (playerTransform.position - transform.position).magnitude >= safeDistanceFromPlayer;
}
