using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI content;
    public LayoutElement layoutElement;
    public int characterWrapLimit;

    public void SetText(string contentText, string headerText = "")
    {
        content.text = contentText;
        if (string.IsNullOrEmpty(headerText))
        {
            header.gameObject.SetActive(false);
        }
        else
        {
            header.text = headerText;
            header.gameObject.SetActive(true);
        }

        int headerlength = header.text.Length;
        int contentlength = content.text.Length;
        layoutElement.enabled = (headerlength > characterWrapLimit || contentlength > characterWrapLimit) ? true : false;
    }
}