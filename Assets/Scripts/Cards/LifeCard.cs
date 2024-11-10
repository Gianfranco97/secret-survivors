using UnityEngine;

public class LifeCard : Card
{
    protected override void OnCardClicked()
    {
        PlayerLifeManager.Instance.AddNewHeart();
        GameObject.Find("GameManager").GetComponent<CardsGenerator>().lifeCardsToGenerate--;
    }
}
