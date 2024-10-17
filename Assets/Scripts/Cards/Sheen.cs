using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sheen :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private Animator animator;
    private Image image;
    [SerializeField] private Sprite imageTransparent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.enabled = true;
        transform.parent.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.enabled = false;
        image.sprite = imageTransparent;
        transform.parent.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
