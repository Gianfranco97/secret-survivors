using TMPro;
using UnityEngine;

[System.Serializable]
public class EnemiesDefeatCounterData
{
    public int dark = 0;
    public int light = 0;
    public int total => dark + light;
}

[System.Serializable]
public class EnemiesDefeatCounterDataInGame : EnemiesDefeatCounterData
{
    public bool isDarkNewRecord = false;
    public bool isLightNewRecord = false;
}

public enum EnemyType
{
    Basic,
    Fast,
    Fire
}

[System.Serializable]
public class TextCounter
{
    public TextMeshProUGUI recordDarkCounterText;
    public TextMeshProUGUI lastGameDarkCounterText;
    public TextMeshProUGUI recordLightCounterText;
    public TextMeshProUGUI lastGameLightCounterText;
}

[System.Serializable]
public class TextCounters
{
    public TextCounter basic = new TextCounter();
    public TextCounter fast = new TextCounter();
    public TextCounter fire = new TextCounter();
}

[System.Serializable]
public class EnemiesDefeatCounterInGame
{
    public TextMeshProUGUI textComponent;
    public Animator newRecordAnimator;
    public bool isNewRecordShowedInGame = false;
}

public class EnemiesCounter : MonoBehaviour
{
    public EnemiesDefeatCounterDataInGame basic = new EnemiesDefeatCounterDataInGame();
    public EnemiesDefeatCounterDataInGame fast = new EnemiesDefeatCounterDataInGame();
    public EnemiesDefeatCounterDataInGame fire = new EnemiesDefeatCounterDataInGame();
    public EnemiesDefeatCounterInGame basicCounter;
    public EnemiesDefeatCounterInGame fastCounter;
    public EnemiesDefeatCounterInGame fireCounter;
    [SerializeField] private TextCounters gameOver;
    [SerializeField] private GameManager gameManager;

    private EnemiesDefeatCounterData GetEnemyCounter(EnemyType enemyType, bool isRecord)
    {
        switch (enemyType)
        {
            case EnemyType.Basic:
                return isRecord ? gameManager.gameDataRecord.enemyBasicDefeatRecord : basic;
            case EnemyType.Fast:
                return isRecord ? gameManager.gameDataRecord.enemyFastDefeatRecord : fast;
            case EnemyType.Fire:
                return isRecord ? gameManager.gameDataRecord.enemyFireDefeatRecord : fire;
            default:
                return null;
        }
    }

    private EnemiesDefeatCounterInGame GetEnemiesDefeatCounterInGame(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Basic:
                return basicCounter;
            case EnemyType.Fast:
                return fastCounter;
            case EnemyType.Fire:
                return fireCounter;
            default:
                return null;
        }
    }

    private void UpdateCounterText(EnemyType enemyType)
    {
        EnemiesDefeatCounterData enemyCounterRecord = GetEnemyCounter(enemyType, true);
        EnemiesDefeatCounterData enemyCounterCurrentGame = GetEnemyCounter(enemyType, false);
        EnemiesDefeatCounterInGame enemiesDefeatCounterInGame = GetEnemiesDefeatCounterInGame(enemyType);

        enemiesDefeatCounterInGame.textComponent.text = enemyCounterCurrentGame.total.ToString();

        if (enemyCounterCurrentGame.total > enemyCounterRecord.total && !enemiesDefeatCounterInGame.isNewRecordShowedInGame)
        {
            enemiesDefeatCounterInGame.newRecordAnimator.SetTrigger("ShowNewRecord");
            enemiesDefeatCounterInGame.textComponent.color = Color.green;
            enemiesDefeatCounterInGame.isNewRecordShowedInGame = true;
        }
    }

    private void ShowGlow(TextMeshProUGUI textComponent)
    {
        var textWidth = textComponent.preferredWidth;
        GameObject glow = textComponent.transform.Find("Glow").gameObject;
        glow.transform.localPosition = new Vector3(textWidth + 5, 11, 0);
        glow.SetActive(true);
    }

    private void FormatEnemyCounter(EnemyType enemyType, TextCounter enemyTextCounter)
    {
        EnemiesDefeatCounterDataInGame enemyCounterLastGame = GetEnemyCounter(enemyType, false) as EnemiesDefeatCounterDataInGame;
        EnemiesDefeatCounterData enemyCounterRecord = GetEnemyCounter(enemyType, true);

        enemyTextCounter.lastGameLightCounterText.text = enemyCounterLastGame.light.ToString();
        enemyTextCounter.recordLightCounterText.text = enemyCounterRecord.light.ToString();
        if (enemyCounterLastGame.isLightNewRecord)
        {
            enemyTextCounter.recordLightCounterText.color = Color.green;
            enemyTextCounter.lastGameLightCounterText.color = Color.green;
            ShowGlow(enemyTextCounter.recordLightCounterText);
        }

        enemyTextCounter.lastGameDarkCounterText.text = enemyCounterLastGame.dark.ToString();
        enemyTextCounter.recordDarkCounterText.text = enemyCounterRecord.dark.ToString();
        if (enemyCounterLastGame.isDarkNewRecord)
        {
            enemyTextCounter.recordDarkCounterText.color = Color.green;
            enemyTextCounter.lastGameDarkCounterText.color = Color.green;
            ShowGlow(enemyTextCounter.recordDarkCounterText);
        }
    }

    private void FormatCounterTexts(TextCounters counterComponet)
    {
        FormatEnemyCounter(EnemyType.Basic, counterComponet.basic);
        FormatEnemyCounter(EnemyType.Fast, counterComponet.fast);
        FormatEnemyCounter(EnemyType.Fire, counterComponet.fire);
    }

    public void UpdateCounterTextsGameOver()
    {
        FormatCounterTexts(gameOver);
    }

    public void AddDefeatEnemy(EnemyType enemyType, bool isDark)
    {
        EnemiesDefeatCounterDataInGame enemyCounter = GetEnemyCounter(enemyType, false) as EnemiesDefeatCounterDataInGame;

        if (isDark)
        {
            enemyCounter.dark += 1;
            enemyCounter.isDarkNewRecord = enemyCounter.dark > GetEnemyCounter(enemyType, true).dark;
        }
        else
        {
            enemyCounter.light += 1;
            enemyCounter.isLightNewRecord = enemyCounter.light > GetEnemyCounter(enemyType, true).light;
        }

        UpdateCounterText(enemyType);
    }
}
