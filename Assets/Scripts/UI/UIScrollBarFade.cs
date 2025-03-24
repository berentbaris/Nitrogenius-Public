using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollBarFade : MonoBehaviour
{
    public Image topFade;
    public Image bottomFade;

    public void OnScrollBarValueChange(float value)
    {
        if (value >= 1)
        {
            topFade.enabled = false;
        }
        if (value <= 0)
        {
            bottomFade.enabled = false;
        }
        if (value < 1)
        {
            topFade.enabled = true;
        }
        if (value > 0)
        {
            bottomFade.enabled = true;
        }
    }
}