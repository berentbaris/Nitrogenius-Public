using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    private UIPanelController panelController;
    public int tutorialSegment;

    private void Awake()
    {
        panelController = GetComponent<UIPanelController>();
    }

    public void OnTutorialProceed(int segment)
    {
        if (segment == 0)
        {
            panelController.DisablePanel();
        }

        if (segment == tutorialSegment)
        {
            panelController.OpenPanel();
        }
    }
}