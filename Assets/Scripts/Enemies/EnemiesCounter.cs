using TMPro;
using UnityEngine;

[System.Serializable]
public class EnemiesDefeatCounter
{
    public int dark = 0;
    public int light = 0;
}

public enum EnemyType
{
    Basic,
    Fast,
    Fire
}

public class EnemiesCounter : MonoBehaviour
{
    public EnemiesDefeatCounter basic = new EnemiesDefeatCounter();
    public EnemiesDefeatCounter fast = new EnemiesDefeatCounter();
    public EnemiesDefeatCounter fire = new EnemiesDefeatCounter();
    [SerializeField] private TextMeshProUGUI basicCounterText;
    [SerializeField] protected TextMeshProUGUI fastCounterText;
    [SerializeField] public TextMeshProUGUI fireCounterText;
    [SerializeField] private TextMeshProUGUI gameOverBasicDarkCounterText;
    [SerializeField] private TextMeshProUGUI gameOverBasicLightCounterText;
    [SerializeField] protected TextMeshProUGUI gameOverFastDarkCounterText;
    [SerializeField] protected TextMeshProUGUI gameOverFastLightCounterText;
    [SerializeField] public TextMeshProUGUI gameOverFireDarkCounterText;
    [SerializeField] public TextMeshProUGUI gameOverFireLightCounterText;

    public void AddDefeatEnemy(EnemyType enemyType, bool isDark)
    {
        switch (enemyType)
        {
            case EnemyType.Basic:
                if (isDark) basic.dark = basic.dark + 1;
                else basic.light = basic.light + 1;
                break;
            case EnemyType.Fast:
                if (isDark) fast.dark = fast.dark + 1;
                else fast.light = fast.light + 1;
                break;
            case EnemyType.Fire:
                if (isDark) fire.dark = fire.dark + 1;
                else fire.light = fire.light + 1;
                break;
        }
    }

    public void UpdateCounterTexts()
    {
        basicCounterText.text = (basic.light + basic.dark).ToString();
        fastCounterText.text = (fast.dark + fast.light).ToString();
        fireCounterText.text = (fire.dark + fire.light).ToString();
    }

    public void UpdateCounterTextsGameOver()
    {
        gameOverBasicDarkCounterText.text = basic.dark.ToString();
        gameOverBasicLightCounterText.text = basic.light.ToString();
        gameOverFastDarkCounterText.text = fast.dark.ToString();
        gameOverFastLightCounterText.text = fast.light.ToString();
        gameOverFireDarkCounterText.text = fire.dark.ToString();
        gameOverFireLightCounterText.text = fire.light.ToString();  
    }
}
