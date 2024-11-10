using UnityEngine;

public class IkarugaEnemy : IkarugaColor
{
    private void Start()
    {
        isDark = Random.Range(0, 2) == 0;
        MatchColor();
    }
}
