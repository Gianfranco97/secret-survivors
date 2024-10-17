using TMPro;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    private SpriteRenderer background;
    [SerializeField] private GameObject environment;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject bossArena;
    [SerializeField] private GameObject bossData;
    [SerializeField] private GameObject enemiesGenerator;
    private bool isBossFigthStarted = false;

    private void Start()
    {
        background = environment.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (timer.time == 0 && !isBossFigthStarted)
        {
            StartBossFigth();
        }
    }

    public void StartBossFigth()
    {
        isBossFigthStarted = true;
        background.color = new Color32(14, 14, 14, 255);
        level.color = new Color32(255, 255, 255, 255);
        time.color = new Color32(255, 255, 255, 255);
        enemiesGenerator.SetActive(false);
        enemiesGenerator.GetComponent<EnemiesGenerator>().KillAllToEnemies();
        bossArena.transform.position = GameObject.Find("Player").transform.position;
        bossArena.SetActive(true);
        bossData.SetActive(true);
    }
}
