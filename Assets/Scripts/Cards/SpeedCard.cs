using UnityEngine;
using UnityEngine.EventSystems;

public class SpeedCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject cardContainer;
    private PlayerActions playerActions;

    private void Start()
    {
        cardContainer = GameObject.Find("CardContainer");
        playerActions = GameObject.Find("Player").GetComponent<PlayerActions>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        playerActions.speed = playerActions.speed * 1.2f;
        cardContainer.SetActive(false);
        Time.timeScale = 1;
    }
}
