using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Timer : MonoBehaviour
{
    public FloatVariable remainingTime;
    public FloatVariable turnTime;
    public TextMeshProUGUI UI_Timer_text;
    public Image timerFillImage;
    private float fillImageWidth;
    private Coroutine timerCoroutine;

    private void Awake()
    {
        fillImageWidth = timerFillImage.rectTransform.sizeDelta.x;
    }

    public void OnNewTurn()
    {
        timerCoroutine = StartCoroutine(TimeDisplay());
    }

    private IEnumerator TimeDisplay()
    {
        yield return new WaitForSeconds(0.1f);

        while (remainingTime.Value > 0)
        {
            timerFillImage.rectTransform.sizeDelta = new Vector2(remainingTime.Value / turnTime.Value * fillImageWidth, timerFillImage.rectTransform.sizeDelta.y);
            yield return null;
        }
    }
}