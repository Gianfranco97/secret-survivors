using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    [SerializeField] private GameObject cardsContainer;
    public int lifeCardsToGenerate = 19;
    public int addFirePointCardsToGenerate = 6;

    private void CleanActualCards()
    {
        foreach (Transform child in cardsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private List<GameObject> GetAvailableCardsList()
    {
        List<GameObject> availableCards = new List<GameObject>(cards);
        List<string> cardsToExclude = new List<string>();

        if (lifeCardsToGenerate == 0)
        {
            cardsToExclude.Add("CardLife");
        }

        if (addFirePointCardsToGenerate == 0)
        {
            cardsToExclude.Add("CardAddFirePoint");
        }

        return availableCards.Where(card => !cardsToExclude.Contains(card.name)).ToList();
    }

    private List<GameObject> GetRandomCards()
    {
        List<GameObject> availableCards = GetAvailableCardsList();
        List<GameObject> randomCards = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            randomCards.Add(availableCards[randomIndex]);
            availableCards.RemoveAt(randomIndex);
        }

        return randomCards;
    }

    private void ShowCards(List<GameObject> cardsToGenerate)
    {
        foreach (GameObject card in cardsToGenerate)
        {
            Instantiate(card, cardsContainer.transform);
        }
    }

    public void GenerateCards()
    {
        CleanActualCards();
        ShowCards(GetRandomCards());
    }
}
