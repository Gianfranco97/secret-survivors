using UnityEngine;

public class OrbitalCard : Card
{
    protected override void OnCardClicked()
    {
        GameObject.Find("OrbitalWeapon").GetComponent<ObitalWeapon>().AddNewOrbital();
    }
}
