using UnityEngine;

public class AddFirePointCard : Card
{
    protected override void OnCardClicked()
    {
        GameObject.Find("GameManager").GetComponent<CardsGenerator>().addFirePointCardsToGenerate--;
        GameObject.Find("PlayerHalo").GetComponent<PlayerWeapon>().ActiveFirePoint();
    }
}
