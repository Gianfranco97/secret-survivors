using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BossAreaExplosions : MonoBehaviour
{
    [SerializeField] private List<GameObject> lightExplosions;
    [SerializeField] private List<GameObject> darkExplosions;
    private int nextLightExplosionIndex = 0;
    private int nextDarkExplosionIndex = 0;
    [SerializeField] private SpriteRenderer bossArena;
    public float arenaCreationTime = 1f;
    private float arenaRadius;
    private Coroutine explosionsCorutine;

    private IEnumerator GetWordLimits()
    {
        yield return new WaitForSeconds(arenaCreationTime);
        arenaRadius = bossArena.bounds.extents.x;
    }

    private void Start()
    {
        StartCoroutine(GetWordLimits());
    }

    private Vector3 GetRamdonPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * arenaRadius;

        return new Vector3(randomPoint.x, randomPoint.y, 0);
    }

    private IEnumerator ShakeCameraWithExplosion()
    {
        yield return new WaitForSeconds(2f);
        SFXManager.instance.PlaySound("Boom");
        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.5f, 1f));
    }

    private IEnumerator DisableExplisionObject(GameObject explosion)
    {
        yield return new WaitForSeconds(2.4f);
        explosion.SetActive(false);
    }

    private void MakeExplosion(bool isDark)
    {
        bool usePlayerPosition = Random.Range(0, 3) == 0;
        List<GameObject> explosions = isDark ? darkExplosions : lightExplosions;
        int Index = isDark ? nextDarkExplosionIndex : nextLightExplosionIndex;

        if (usePlayerPosition)
        {
            explosions[Index].transform.position = GameObject.Find("Player").transform.position;
        }
        else
        {
            explosions[Index].transform.localPosition = GetRamdonPosition();
        }

        explosions[Index].SetActive(true);
        StartCoroutine(ShakeCameraWithExplosion());
        StartCoroutine(DisableExplisionObject(explosions[Index]));

        if (Index == explosions.Count - 1)
            if (isDark) nextDarkExplosionIndex = 0; 
            else nextLightExplosionIndex = 0;
        else
            if (isDark) nextDarkExplosionIndex++; 
            else nextLightExplosionIndex++;
    }

    private IEnumerator MakeExplosions()
    {
        while (true)
        {
            MakeExplosion(true);
            yield return new WaitForSeconds(1.2f);
            MakeExplosion(false);
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void StartExplosions()
    {
        Debug.Log("StartExplosions");
        explosionsCorutine = StartCoroutine(MakeExplosions());
    }

    public void StopExplosions()
    {
        StopCoroutine(explosionsCorutine);
    }
}
