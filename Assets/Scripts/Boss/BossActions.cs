using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BossStage
{
    None,
    Stage1,
    Stage2
}

public class BossActions : MonoBehaviour
{
    private SpriteRenderer background;
    [SerializeField] private GameObject environment;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject bossArena;
    [SerializeField] private GameObject bossDataStg1;
    [SerializeField] private GameObject bossDataStg2;
    [SerializeField] private GameObject enemiesGenerator;
    [SerializeField] private GameObject worldToDestroy;
    [SerializeField] private GameObject bossEntity;
    [SerializeField] private GameObject bossHeadCombined;
    [SerializeField] private GameObject bossHead1;
    [SerializeField] private GameObject bossHead2;
    [SerializeField] private GameObject bossHead3;
    [SerializeField] private GameObject bossHead4;
    [SerializeField] private float bossEntryDelay = 0.5f;
    [SerializeField] private float bossStartRotationDelay = 0.3f;
    [SerializeField] private float bossStartFollowDelay = 0.3f;
    [SerializeField] private Animator mainCameraAnimator;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider bossHeadCombinedLife;
    [SerializeField] private Slider bossHead1Life;
    [SerializeField] private Slider bossHead2Life;
    [SerializeField] private Slider bossHead3Life;
    [SerializeField] private Slider bossHead4Life;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private BossStage1 bossStage1;
    [SerializeField] private GameObject BossExposions;
    [SerializeField] private GameObject BossLaser;
    [SerializeField] private SlowFollow bossSlowFollow;
    [SerializeField] private RotateAroundTarget bossRotateAroundTarget;
    public BossStage bossStage = BossStage.None;

    private void Start()
    {
        background = environment.GetComponent<SpriteRenderer>();
    }
    
    private IEnumerator FishishBattle()
    {
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
        if (timer.time == 0 && bossStage == BossStage.None)
        {
            StartCoroutine(StartStg1());
            return;
        }

        if (bossHeadCombinedLife.value == 0 && bossStage == BossStage.Stage1)
        {
            StartCoroutine(StartStg2());
            return;
        }

        if (bossHead1Life.value == 0 && bossHead2Life.value == 0 && bossHead3Life.value == 0 && bossHead4Life.value == 0 && bossStage == BossStage.Stage2)
        {
            StartCoroutine(FishishBattle());
        }
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

    public IEnumerator StartStg1()
    {
        bossStage = BossStage.Stage1;
        StartCoroutine(StartBossMusic());
        var playerPosition = GameObject.Find("Player").transform.position;

        background.color = new Color32(14, 14, 14, 255);
        level.color = new Color32(255, 255, 255, 255);
        time.color = new Color32(255, 255, 255, 255);
        enemiesGenerator.SetActive(false);
        enemiesGenerator.GetComponent<EnemiesGenerator>().KillAllToEnemies();
        bossArena.transform.position = playerPosition;
        bossEntity.transform.position = new Vector3(playerPosition.x, playerPosition.y + 200, playerPosition.z);
        Destroy(GameObject.Find("ItemsContainer"));
        Destroy(worldToDestroy);
        bossArena.SetActive(true);
        bossDataStg1.SetActive(true);
        StartCoroutine(bossStage1.StartFirstAttack());
        bossHeadCombined.SetActive(true);

        mainCameraAnimator.SetTrigger("ZoomOut");
        yield return new WaitForSeconds(bossEntryDelay);
        StartCoroutine(StartBossFollow());
        StartCoroutine(StartBossRotation());
    }

    public IEnumerator StartStg2()
    {
        BossExposions.SetActive(false);
        BossLaser.SetActive(false);
        yield return new WaitForSeconds(1f);
        var playerPosition = GameObject.Find("Player").transform;
        bossSlowFollow.target = playerPosition;
        bossSlowFollow.yOffset = 0;
        bossSlowFollow.isFollowing = true;
        bossRotateAroundTarget.target = playerPosition;
        bossRotateAroundTarget.enabled = true;
        bossStage = BossStage.Stage2;
        bossDataStg1.SetActive(false);
        bossDataStg2.SetActive(true);
        bossHead1.SetActive(true);
        bossHead2.SetActive(true);
        bossHead3.SetActive(true);
        bossHead4.SetActive(true);
    }
}
