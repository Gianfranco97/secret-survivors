using UnityEngine;

public class RateShotsCard : Card
{
    protected override void OnCardClicked()
    {
        GameObject.Find("PlayerHalo").GetComponent<PlayerWeapon>().timeBetweenShots *= 0.7f;
    }
}
