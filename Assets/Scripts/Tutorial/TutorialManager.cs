using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableEvents.Events;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public UIPanelController tutorialPanelController;
    public TextMeshProUGUI modalText;
    public FloatVariable fadeDuration;
    private WaitForSeconds fadeWait;
    public Button TutorialProceedButton;
    [TextArea]
    public List<string> tutorialModalList = new List<string>();
    public BoolVariable tutorialEnabled;
    private int currenTutorialSegment = 0;
    public IntScriptableEvent tutorialProceedEvent;

    private void Awake()
    {
        fadeWait = new WaitForSeconds(fadeDuration.Value);
    }

    public void OnStartGame()
    {
        if (tutorialEnabled.Value == true)
        {
            tutorialPanelController.OpenPanel();
            tutorialProceedEvent.Raise(currenTutorialSegment);
        }
        else
        {
            tutorialPanelController.DisablePanel();
        }
    }

    public void OnTutorialNext()
    {
        currenTutorialSegment++;
        if (currenTutorialSegment >= tutorialModalList.Count)
        {
            return;
        }

        tutorialPanelController.OpenPanel();
        StartCoroutine(FadeWait());
        TutorialProceedButton.enabled = false;
    }

    private IEnumerator FadeWait()
    {
        yield return fadeWait;
        tutorialProceedEvent.Raise(currenTutorialSegment);
        modalText.text = tutorialModalList[currenTutorialSegment];

        if (currenTutorialSegment == 8)
        {
            tutorialPanelController.GetComponent<RectTransform>().localPosition = new Vector3(0, -160, 0);
        }

        yield return fadeWait;
        TutorialProceedButton.enabled = true;
    }
}