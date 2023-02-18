using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI levelTime;

    public void SetScoreValue(int value) { if (value > 0) score.text = value.ToString(); }

    public void SetLevelTimeValue(int value, bool isCritical = false)
    {
        if (isCritical) levelTime.color = Color.red;
        if (value > 0) levelTime.text = value.ToString();
    }
}
