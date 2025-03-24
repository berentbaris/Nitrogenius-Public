using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGraph : MonoBehaviour
{
    private RectTransform graphContainer;
    public TextMeshProUGUI NameText;
    public LineRenderer graphLine;
    public GameObject graphLabelX;
    public GameObject graphNode;

    private List<GameObject> graphNodes = new List<GameObject>();
    private List<GameObject> grapLabels = new List<GameObject>();

    private void Awake()
    {
        graphContainer = GetComponent<RectTransform>();

        for (int i = 0; i < 15; i++)
        {
            GameObject graphNodeGO = Instantiate(graphNode);
            graphNodeGO.transform.SetParent(graphContainer, false);
            RectTransform graphNodeRect = graphNode.GetComponent<RectTransform>();
            graphNodeRect.sizeDelta = new Vector2(11, 11);
            graphNodeRect.anchorMin = Vector2.zero;
            graphNodeRect.anchorMax = Vector2.zero;
            graphNodes.Add(graphNodeGO);
            graphNodeGO.SetActive(false);
        }

        for (int i = 0; i < 15; i++)
        {
            GameObject labelx = Instantiate(graphLabelX);
            labelx.transform.SetParent(graphContainer);
            labelx.GetComponent<TextMeshProUGUI>().text = (2023 + i).ToString();
            labelx.transform.localScale = new Vector3(1, 1, 1);
            grapLabels.Add(labelx);
            labelx.SetActive(false);
        }
    }

    public void DisplayGraph(List<float> valueList, string graphName)
    {
        CleanGraph();
        graphLine.enabled = true;
        NameText.text = graphName;
        float graphHeight = graphContainer.sizeDelta.y;
        float xSize = graphContainer.sizeDelta.x / (valueList.Count - 1);

        float yMaximum = 0f;

        foreach (int value in valueList)
        {
            if(value > yMaximum)
            {
                yMaximum = value;
            }
        }
        yMaximum = yMaximum * 1.2f;

        var Nodepoints = new Vector3[valueList.Count];

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            CreateGraphNode(i, new Vector2(xPosition, yPosition), valueList[i]);
            CreateGraphLabel(i, new Vector2(xPosition, 0));
            Nodepoints[i] = new Vector3(graphContainer.anchoredPosition.x + xPosition, graphContainer.anchoredPosition.y + yPosition, 0);
        }

        graphLine.positionCount = valueList.Count;
        graphLine.SetPositions(Nodepoints);
    }


    private void CleanGraph()
    {
        foreach (var item in graphNodes)
        {
            item.SetActive(false);
        }

        foreach (var item in grapLabels)
        {
            item.SetActive(false);
        }
    }

    private void CreateGraphLabel(int number, Vector2 anchoredPosition)
    {
        grapLabels[number].SetActive(true);
        RectTransform graphNodeRect = grapLabels[number].GetComponent<RectTransform>();
        graphNodeRect.anchoredPosition = anchoredPosition;
    }

    private void CreateGraphNode(int number, Vector2 anchoredPosition, float value)
    {
        graphNodes[number].SetActive(true);
        RectTransform graphNodeRect = graphNodes[number].GetComponent<RectTransform>();
        graphNodeRect.anchoredPosition = anchoredPosition;
        graphNodes[number].GetComponent<UIGraphNode>().NodeValue = Mathf.RoundToInt(value);
    }
}