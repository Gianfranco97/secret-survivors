using UnityEngine;

public class MagnetCard : Card
{
    protected override void OnCardClicked()
    {
        GameObject.Find("ObjectMagnet").GetComponent<CircleCollider2D>().radius *= 2f;
    }
}
