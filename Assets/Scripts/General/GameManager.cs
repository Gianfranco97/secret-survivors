using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class GameDataRecord
{
    public int totalTries = 0;
    public int totalWins = 0;
    public EnemiesDefeatCounterData enemyBasicDefeatRecord;
    public EnemiesDefeatCounterData enemyFastDefeatRecord;
    public EnemiesDefeatCounterData enemyFireDefeatRecord;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameDataUI;
    [SerializeField] private MusicManager musicManager;
    public GameDataRecord gameDataRecord = new GameDataRecord();
    private EnemiesCounter enemiesCounter;
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        enemiesCounter = GetComponent<EnemiesCounter>();
        saveFilePath = Application.persistentDataPath + "/gameData.dat";
        LoadData();
        gameDataRecord.totalTries++;
        musicManager.PlayGameMusic();
    }

    private void FinishGame()
    {
        SaveData();
        gameDataUI.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        Time.timeScale = 0;
        FinishGame();
        enemiesCounter.UpdateCounterTextsGameOver();
        gameOverMenu.SetActive(true);

        SFXManager.instance.PlaySound("GameOver");
        musicManager.StopMusic();
    }

    public void ShowWin()
    {
        gameDataRecord.totalWins++;
        FinishGame();
        // TODO: Play sound
        // TODO: Change music
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    private void RestarTime()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        RestarTime();
    }

    public void ShowMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        RestarTime();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        gameDataRecord.enemyBasicDefeatRecord.dark = Mathf.Max(enemiesCounter.basic.dark, gameDataRecord.enemyBasicDefeatRecord.dark);
        gameDataRecord.enemyBasicDefeatRecord.light = Mathf.Max(enemiesCounter.basic.light, gameDataRecord.enemyBasicDefeatRecord.light);
        gameDataRecord.enemyFastDefeatRecord.dark = Mathf.Max(enemiesCounter.fast.dark, gameDataRecord.enemyFastDefeatRecord.dark);
        gameDataRecord.enemyFastDefeatRecord.light = Mathf.Max(enemiesCounter.fast.light, gameDataRecord.enemyFastDefeatRecord.light);
        gameDataRecord.enemyFireDefeatRecord.dark = Mathf.Max(enemiesCounter.fire.dark, gameDataRecord.enemyFireDefeatRecord.dark);
        gameDataRecord.enemyFireDefeatRecord.light = Mathf.Max(enemiesCounter.fire.light, gameDataRecord.enemyFireDefeatRecord.light);

        bf.Serialize(file, gameDataRecord);
        file.Close();

        Debug.Log("Data saved in binary format at " + saveFilePath);
    }

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);

            gameDataRecord = (GameDataRecord)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }
}
