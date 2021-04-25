using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    private GameObject currentWebSite;
    private float cameraHeight;
    private float percentage;
    private float webSiteHeight;
    
    private void Start()
    {
        cameraHeight = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().pixelHeight;
    }

    public void OnTabChanges(GameObject currentWebSite)
    {
        SetCurrentWebSite(currentWebSite);
        UpdateValues();
        UpdateScrollBarSize();
    }
    private void SetCurrentWebSite(GameObject currentWebSite)
    {
        this.currentWebSite = currentWebSite;
        webSiteHeight = currentWebSite.GetComponent<RectTransform>().rect.height;
    }
    
    public void OnScrollBarWebsite(float values)
    {
        percentage = values;
        float targetValue = (webSiteHeight -cameraHeight)* values;
        Vector3 targetPosition = new Vector3(currentWebSite.transform.localPosition.x, targetValue, currentWebSite.transform.localPosition.z);
        currentWebSite.GetComponent<RectTransform>().anchoredPosition = targetPosition;
    }

    public void UpdateValues()
    {
        if(webSiteHeight - cameraHeight > 0.0f)
        {
            percentage = currentWebSite.GetComponent<RectTransform>().anchoredPosition.y / (webSiteHeight - cameraHeight);
            scrollbar.value = percentage;
        }
    }
    
    private void UpdateScrollBarSize()
    {
        scrollbar.size = Mathf.Clamp(-webSiteHeight/5400f+1.2f, 0.2f, 1);
    }
}
