using UnityEngine;
using UnityEngine.EventSystems;

public class LifeCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject cardContainer;
    private CardsGenerator cardsGenerator;

    private void Start()
    {
        cardContainer = GameObject.Find("CardContainer");
        cardsGenerator = GameObject.Find("GameManager").GetComponent<CardsGenerator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerLifeManager.Instance.AddNewHeart();
        cardContainer.SetActive(false);
        Time.timeScale = 1;
        cardsGenerator.lifeCardsToGenerate = cardsGenerator.lifeCardsToGenerate - 1;
    }
}
