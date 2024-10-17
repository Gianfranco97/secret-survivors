using UnityEngine;
using UnityEngine.EventSystems;

public class AddFirePointCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject cardContainer;
    private CardsGenerator cardsGenerator;
    private PlayerWeapon playerWeapon;

    private void Start()
    {
        cardContainer = GameObject.Find("CardContainer");
        cardsGenerator = GameObject.Find("GameManager").GetComponent<CardsGenerator>();
        playerWeapon = GameObject.Find("PlayerHalo").GetComponent<PlayerWeapon>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 1;
        cardContainer.SetActive(false);
        cardsGenerator.addFirePointCardsToGenerate = cardsGenerator.addFirePointCardsToGenerate - 1;
        playerWeapon.ActiveFirePoint();
    }
}
