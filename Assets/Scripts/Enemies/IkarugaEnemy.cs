using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkarugaEnemy : MonoBehaviour
{
    public bool isDark = false;
    private SpriteRenderer sprite;

    private void Start()
    {
        isDark = Random.Range(0, 10) >= 5;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = isDark ? Color.red : Color.green;
    }
}
