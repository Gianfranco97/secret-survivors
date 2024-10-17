using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameDataRecord
{
    public EnemiesDefeatCounter enemyBasicDefeatRecord;
    public EnemiesDefeatCounter enemyFastDefeatRecord;
    public EnemiesDefeatCounter enemyFireDefeatRecord;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameDataUI;
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
    }

    public void ShowGameOverMenu()
    {
        SaveData();
        enemiesCounter.UpdateCounterTextsGameOver();
        gameOverMenu.SetActive(true);
        gameDataUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        gameDataRecord.enemyBasicDefeatRecord = enemiesCounter.basic;
        gameDataRecord.enemyFastDefeatRecord = enemiesCounter.fast;
        gameDataRecord.enemyFireDefeatRecord = enemiesCounter.fire;

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
