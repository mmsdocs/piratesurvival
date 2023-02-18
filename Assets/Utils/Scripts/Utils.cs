using UnityEngine;

public class Utils
{
    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    public static float AngleBetween(Vector3 origin, Vector3 target, Vector2 worldUp)
    {
        Vector2 direction = target - origin;
        return Vector2.SignedAngle(worldUp, direction);
    }
}
