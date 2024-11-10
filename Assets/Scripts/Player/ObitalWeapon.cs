using System.Collections.Generic;
using UnityEngine;

public class ObitalWeapon : MonoBehaviour
{
    [SerializeField] private GameObject orbitalPrefab;
    [SerializeField] private float orbitalRadius = 10f;
    private int orbitalCount = 0;
    private IkarugaColor playerIkarugaColor;
    private List<GameObject> orbitals = new List<GameObject>();

    private void Start()
    {
        CreateOrbitals();
        playerIkarugaColor = GameObject.Find("Player").GetComponent<IkarugaColor>();
    }

    private void CreateOrbitals()
    {
        for (int i = 0; i < orbitalCount; i++)
        {
            if (i < orbitals.Count)
            {
                RepositionOrbital(i);
            }
            else
            {
                GameObject orbital = Instantiate(orbitalPrefab, transform.position, Quaternion.identity);
                orbital.transform.parent = transform;
                orbitals.Add(orbital);
                RepositionOrbital(i);
            }
        }
    }

    private void RepositionOrbital(int index)
    {
        float angle = index * Mathf.PI * 2 / orbitalCount;
        float xPosition = Mathf.Cos(angle) * orbitalRadius;
        float yPosition = Mathf.Sin(angle) * orbitalRadius;
        orbitals[index].transform.localPosition = new Vector3(xPosition, yPosition, 0);
    }

    public void AddNewOrbital()
    {
        orbitalCount++;
        GameObject newOrbital = Instantiate(orbitalPrefab, transform.position, Quaternion.identity);
        IkarugaColor orbitalIkaruga = newOrbital.GetComponent<IkarugaColor>();

        newOrbital.transform.parent = transform;
        orbitalIkaruga.isDark = playerIkarugaColor.isDark;
        orbitalIkaruga.MatchColor();

        orbitals.Add(newOrbital);
        CreateOrbitals();
    }

    public void ChangeObitalsColor()
    {
        foreach (var orbital in orbitals)
        {
            orbital.GetComponent<IkarugaColor>().SwitchColor();
        }
    }
}