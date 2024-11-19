using System.Collections;
using UnityEngine;


public class BossStage1 : MonoBehaviour
{
    [SerializeField] private BossAreaExplosions bossAreaExplosions;
    [SerializeField] private BossHyperLaser bossHyperLaser;
    [SerializeField] private Transform bossConatiner;
    [SerializeField] private RotateAroundTarget bossRotateAroundTarget;
    [SerializeField] private SlowFollow bossSlowFollow;
    [SerializeField] private SlowFollow bossHeadSlowFollow;
    [SerializeField] private GameObject darkShield;
    [SerializeField] private GameObject lightShield;
    private float attackTime = 15f;

    public IEnumerator ActiveBombsExplosions()
    {
        darkShield.SetActive(true);
        lightShield.SetActive(true);
        bossRotateAroundTarget.enabled = true;
        bossSlowFollow.isFollowing = true;
        bossHeadSlowFollow.isFollowing = false;
        bossHyperLaser.StopLaser();
        bossAreaExplosions.StartExplosions();

        yield return new WaitForSeconds(attackTime);
        StartCoroutine(ActiveHyperLaser());
    }

    public IEnumerator ActiveHyperLaser()
    {
        bossRotateAroundTarget.enabled = false;
        bossSlowFollow.isFollowing = false;
        bossHeadSlowFollow.isFollowing = true;

        yield return new WaitForSeconds(2f);
        bossAreaExplosions.StopExplosions();
        bossHyperLaser.StartLaser();

        if (bossHyperLaser.isDark) darkShield.SetActive(false);
        else lightShield.SetActive(false);

        yield return new WaitForSeconds(attackTime);
        StartCoroutine(ActiveBombsExplosions());
    }

    public IEnumerator StartFirstAttack()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(ActiveBombsExplosions());
    }
}
