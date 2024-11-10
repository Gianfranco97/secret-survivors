using UnityEngine;

public class SpeedCard : Card
{
    protected override void OnCardClicked()
    {
        GameObject.Find("Player").GetComponent<PlayerActions>().speed *= 1.2f;
    }
}
