using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float smoothFollowWeight = 0.0f;

    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }
    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothFollowWeight * Time.deltaTime);
        }
    }
}
