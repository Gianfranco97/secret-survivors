using UnityEngine;

public class BossHyperLaser : MonoBehaviour
{
    public bool isDark = false;
    [SerializeField] private GameObject lasersContainer;

    private void Start()
    {
        isDark = Random.Range(0, 2) == 0;
    }

    public void StartLaser()
    {
        isDark = !isDark;
        SFXManager.instance.PlaySound("BossLaser");
        IkarugaColor[] colors = lasersContainer.GetComponentsInChildren<IkarugaColor>();
        foreach (IkarugaColor color in colors)
        {
            color.SetColor(isDark);
        }
        lasersContainer.SetActive(true);
    }

    public void StopLaser()
    {
        lasersContainer.SetActive(false);
    }
}
