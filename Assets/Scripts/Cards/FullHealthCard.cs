public class FullHealthCard : Card
{
    protected override void OnCardClicked()
    {
        PlayerLifeManager.Instance.FullHealth();
    }
}
