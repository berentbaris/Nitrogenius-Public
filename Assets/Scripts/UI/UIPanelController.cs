using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPanelController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public FloatVariable fadeDuration;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OpenPanel()
    {
        canvasGroup.DOFade(1, fadeDuration.Value).SetDelay(fadeDuration.Value);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void ClosePanel()
    {
        canvasGroup.DOFade(0, fadeDuration.Value);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void DisablePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}