using UnityEngine;
using UnityEngine.EventSystems;

public class RateShostsCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject cardContainer;
    private PlayerWeapon playerWeapon;

    private void Start()
    {
        cardContainer = GameObject.Find("CardContainer");
        playerWeapon = GameObject.Find("PlayerHalo").GetComponent<PlayerWeapon>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(playerWeapon.timeBetweenShots);
        playerWeapon.timeBetweenShots = playerWeapon.timeBetweenShots * 0.85f;
        cardContainer.SetActive(false);
        Time.timeScale = 1;
    }
}
