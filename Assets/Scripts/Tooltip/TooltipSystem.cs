using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;
    public Tooltip tooltip;
    public CanvasGroup tooltipCanvas;
    public UIGraph graph;
    public CanvasGroup graphCanvas;
    public Transform rootTransform;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void ShowDefault(string content, Vector2 position, string header = "")
    {
        instance.graphCanvas.DOFade(0, 0f);
        instance.tooltip.SetText(content, header);
        instance.tooltipCanvas.DOFade(1, 0.2f);
        instance.rootTransform.position = position;
    }

    public static void ShowGraph(List<float> listToGraph, Vector2 position, string nameOfGraph)
    {
        instance.tooltipCanvas.DOFade(0, 0f);
        instance.graph.DisplayGraph(listToGraph, nameOfGraph);
        instance.graphCanvas.DOFade(1, 0.2f);
        instance.rootTransform.position = position;
    }

    public static void Hide()
    {
        instance.tooltipCanvas.DOFade(0, 0.2f);
        instance.graphCanvas.DOFade(0, 0.2f);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
    }
}