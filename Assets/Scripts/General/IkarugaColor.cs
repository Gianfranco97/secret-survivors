using UnityEngine;

public class IkarugaColor : MonoBehaviour
{
    public bool isDark = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite darkSprite;
    [SerializeField] private Sprite lightSprite;

    private void Start()
    {
        MatchColor();
    }

    public void MatchColor()
    {
        spriteRenderer.sprite = isDark ? darkSprite : lightSprite;
    }

    public void SetColor(bool newValueIsDark)
    {
        isDark = newValueIsDark;
        MatchColor();
    }

    public void SwitchColor()
    {
        SetColor(!isDark);
    }
}
