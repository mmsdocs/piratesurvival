using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyShipController : ShipController
{
    private NavMeshAgent _agent;

    [Header("Enemy Ship Specs")]
    public float speed = 0.0f;
    public float safeDistanceFromPlayer = 0.0f;
    public Sprite[] shipStates;

    protected Transform playerTransform;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = speed;
    }

    protected void Move()
    {
        if (s_Rigidbody == null) return;
        if (!canMove) return;
        if (isDistanceSafeFromPlayer())
        {
            _agent.isStopped = false;
            if (playerTransform != null) _agent.SetDestination(playerTransform.position);
        }
        else _agent.isStopped = true;
    }

    protected void LookPlayer()
    {
        if (_agent == null) return;
        
        float angle = Utils.AngleBetween(transform.position, _agent.steeringTarget, Vector2.up);
        s_Rigidbody.MoveRotation(angle);
       
    }

    protected override void NextShipState()
    {
        if (shipStates.Length < 0) return;
     
        currentShipState++;
        if (currentShipState < shipStates.Length) 
            spriteRenderer.sprite = shipStates[currentShipState];
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
