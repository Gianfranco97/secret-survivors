using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int time = 5;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(ReduceTime), 0, 1f);
    }

    private string FormatSeconds(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes}m {seconds}s";
    }

    private void ReduceTime()
    {
        if (time > 0)
        {
            time -= 1;
            text.text = FormatSeconds(time);
        }
    }
}
