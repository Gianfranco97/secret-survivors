using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject cardContainer;
    private CircleCollider2D objectMagnetCollider;

    private void Start()
    {
        cardContainer = GameObject.Find("CardContainer");
        objectMagnetCollider = GameObject.Find("ObjectMagnet").GetComponent<CircleCollider2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        objectMagnetCollider.radius = objectMagnetCollider.radius * 1.5f;
        cardContainer.SetActive(false);
        Time.timeScale = 1;
    }
}


