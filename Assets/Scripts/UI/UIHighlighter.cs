using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    private Color originalColor;
    public ColorVariable highlightColor;
    public Selectable selectable;
    private float fadeDuration = 0.1f;

    private void Awake()
    {
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectable.interactable == true)
        {
            image.DOColor(highlightColor.Value, fadeDuration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectable.interactable == true)
        {
            image.DOColor(originalColor, fadeDuration);
        }
    }

    public void Reset()
    {
        image.DOColor(originalColor, 0);
    }

    public void ChangeOriginalColor(Color color)
    {
        originalColor = color;
    }
}