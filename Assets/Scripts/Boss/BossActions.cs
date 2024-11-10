using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject worldToDestroy;
    [SerializeField] private GameObject bossEntity;
    [SerializeField] private GameObject bossEntry;
    [SerializeField] private GameObject bossHead1;
    [SerializeField] private GameObject bossHead2;
    [SerializeField] private GameObject bossHead3;
    [SerializeField] private GameObject bossHead4;
    [SerializeField] private float bossEntryDelay = 1.5f;
    [SerializeField] private float bossStartRotationDelay = 0.3f;
    [SerializeField] private float bossStartFollowDelay = 0.3f;
    [SerializeField] private Animator mainCameraAnimator;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider bossTotalHealt;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip bossMusic;
    private bool isBossFigthStarted = false;
    private bool isBossFigthEnded = false;

    private void Start()
    {
        background = environment.GetComponent<SpriteRenderer>();
    }

    private IEnumerator FishishBattle()
    {
        Debug.Log("Finish Battle");
        Time.timeScale = 0f;
        var playerPosition = GameObject.Find("Player").transform.position;
        Camera.main.transform.position = new Vector3(playerPosition.x, playerPosition.y, Camera.main.transform.position.z);
        mainCameraAnimator.SetTrigger("ZoomIn");
        SFXManager.instance.PlaySound("BossIntro");
        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(3f, 10f));
        Camera.main.gameObject.GetComponent<CameraFade>().TriggerFade();
        yield return new WaitForSecondsRealtime(2f);
        SceneInfo.SceneOrigin = SceneOrigin.Game;
        gameManager.ShowWin();
    }

    private void Update()
    {
        if (timer.time == 0 && !isBossFigthStarted)
        {
            StartCoroutine(StartBossFight());
        }

        if (bossTotalHealt.value == 0 && !isBossFigthEnded)
        {
            isBossFigthEnded = true;
            StartCoroutine(FishishBattle());
        }
    }

    private void ZoomOutCamerea()
    {
        mainCameraAnimator.SetTrigger("ZoomOut");
    }

    private IEnumerator StartBossRotation()
    {
        yield return new WaitForSeconds(bossStartRotationDelay);
        bossEntity.GetComponent<RotateAroundTarget>().enabled = true;
    }

    private IEnumerator StartBossFollow()
    {
        yield return new WaitForSeconds(bossStartFollowDelay);
        bossEntity.GetComponent<SlowFollow>().isFollowing = true;
    }

    private IEnumerator StartBossMusic()
    {
        music.Stop();
        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(1f, 4f));
        SFXManager.instance.PlaySound("BossIntro");
        yield return new WaitForSeconds(bossEntryDelay);
        music.clip = bossMusic;
        music.Play();
    }

    public IEnumerator StartBossFight()
    {
        // TODO: Play SFX
        StartCoroutine(StartBossMusic());
        var playerPosition = GameObject.Find("Player").transform.position;

        isBossFigthStarted = true;
        background.color = new Color32(14, 14, 14, 255);
        level.color = new Color32(255, 255, 255, 255);
        time.color = new Color32(255, 255, 255, 255);
        enemiesGenerator.SetActive(false);
        enemiesGenerator.GetComponent<EnemiesGenerator>().KillAllToEnemies();
        bossArena.transform.position = playerPosition;
        bossEntity.transform.position = playerPosition;
        Destroy(GameObject.Find("ItemsContainer"));
        Destroy(worldToDestroy);
        bossArena.SetActive(true);
        bossData.SetActive(true);
        bossEntry.SetActive(true);

        yield return new WaitForSeconds(bossEntryDelay);
        ZoomOutCamerea();
        StartCoroutine(StartBossFollow());
        StartCoroutine(StartBossRotation());
        bossEntry.SetActive(false);
        bossHead1.SetActive(true);
        bossHead2.SetActive(true);
        bossHead3.SetActive(true);
        bossHead4.SetActive(true);
    }
}
