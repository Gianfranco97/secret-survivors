using UnityEngine;

public class IncreaseBaseDamageCard : Card
{
    protected override void OnCardClicked()
    {
        GameObject.Find("Player").GetComponent<PlayerActions>().IncreaseBaseDamage(1.5f);
    }
}
