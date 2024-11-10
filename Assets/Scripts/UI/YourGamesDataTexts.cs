using TMPro;
using UnityEngine;

public class YourGamesDataTexts : MonoBehaviour
{
    public TextMeshProUGUI totalTriesText;
    public TextMeshProUGUI totalWinsText;

    private void Start()
    {
        totalTriesText.text = GameManager.Instance.gameDataRecord.totalTries.ToString();
        totalWinsText.text = GameManager.Instance.gameDataRecord.totalWins.ToString();
    }
}
