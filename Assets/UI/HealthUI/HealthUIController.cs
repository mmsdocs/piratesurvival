using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    public Vector3 positionOffset = Vector3.up;
    public Transform targetTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3(targetTransform.position.x + positionOffset.x, targetTransform.position.y + positionOffset.y, transform.position.z);
    }
}
