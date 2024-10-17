using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 5;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(ReduceTime), 0, 1f);
    }

    private void ReduceTime()
    {
        if (time > 0)
        {
            time -= 1;
            text.text = "Time: " + time.ToString("F0") + "s";
        }
    }
}
