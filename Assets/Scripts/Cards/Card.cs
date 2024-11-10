using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, IPointerClickHandler
{
    protected GameObject cardContainer;

    protected virtual void Start()
    {
        cardContainer = GameObject.Find("CardContainer");
    }

    private void HideAndResetCard()
    {
        Time.timeScale = 1;
        cardContainer.SetActive(false);
        SFXManager.instance.PlaySound("CardSelected");
    }

    protected abstract void OnCardClicked();

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCardClicked();
        HideAndResetCard();
    }
}