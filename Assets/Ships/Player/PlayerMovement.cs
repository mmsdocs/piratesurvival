using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float reverseSpeed = 0.5f;
    [SerializeField] private float rotationSpeed = 1.0f;
    private bool canMove = true;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        Move();
        RotateBody();
    }

    private void Move()
    {
        if (!canMove) return;
        float direction = Input.GetAxis("Vertical");
        float currentSpeed = direction >= 0 ? speed : reverseSpeed;
        _rigidbody.velocity = direction * currentSpeed * _rigidbody.transform.up;
    }

    private void RotateBody()
    {
        float direction = Input.GetAxis("Horizontal") * -1;
        _rigidbody.angularVelocity = direction * rotationSpeed;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
        canMove = false;
    }
}
