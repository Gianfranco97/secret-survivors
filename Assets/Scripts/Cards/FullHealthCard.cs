using UnityEngine;
using UnityEngine.EventSystems;

public class FullHealthCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject cardContainer;

    private void Start()
    {
        cardContainer = GameObject.Find("CardContainer");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerLifeManager.Instance.FullHelath();
        cardContainer.SetActive(false);
        Time.timeScale = 1;
    }
}
