using UnityEngine;

public class LuckCard : Card
{
    protected override void OnCardClicked()
    {
        GameObject.Find("Player").GetComponent<PlayerActions>().luck += 1.5f;
    }
}
