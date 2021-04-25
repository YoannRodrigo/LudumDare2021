using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WebSiteManager : MonoBehaviour
{
    [Serializable]
    public class Tuple<T1, T2, T3, T4>
    {
        public T3 name;
        public T4 id;
        public T1 tab;
        public T2 website;
    }
    
    [Serializable]
    public class TupleTabWebSite : Tuple<GameObject, GameObject, string, int>
    {
    }

    [SerializeField] private TupleTabWebSite mainSite;
    [SerializeField] private List<TupleTabWebSite> otherSites;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Sprite activeTab;
    [SerializeField] private Sprite inactiveTab;

    private readonly Vector2 activeSize = new Vector2(500, 50);
    private readonly Vector2 inactiveSize = new Vector2(250, 40);

    private int lastId;
    private GameObject currentWebSite;
    private Tweener tween;

    private void Start()
    {
        currentWebSite = mainSite.website;
        UpdateScrollBar();
        UpdateScrollBarPosition();
    }

    public void ActivateWebSiteTab(string name)
    {
        FindTuple(name).tab.SetActive(true);
    }
    
    public void ActivateWebSiteTab(int id)
    {
        FindTuple(id).tab.SetActive(true);
    }

    public void OnTabClick(int id)
    {
        if (id != lastId)
        {
            
            DisableTab();
            ActivateTab(id);
            UpdateScrollBar();
            UpdateScrollBarPosition();
            lastId = id;
            UpdateTabPosition();
        }
    }

    private void DisableTab()
    {
        TupleTabWebSite lastTuple;
        if(lastId!=0)
        {
            lastTuple = FindTuple(lastId);
                
        }
        else
        {
            lastTuple = mainSite;
        }
        lastTuple.tab.GetComponent<Button>().image.sprite = inactiveTab;
        lastTuple.tab.GetComponent<RectTransform>().sizeDelta = inactiveSize;
        lastTuple.website.SetActive(false);
    }

    private void ActivateTab(int id)
    {
        TupleTabWebSite currentTuple;
        if (id == 0)
        {
            currentTuple = mainSite;
                
        }
        else
        {
            currentTuple = FindTuple(id);
                
        }
        currentTuple.tab.GetComponent<Button>().image.sprite = activeTab;
        currentTuple.tab.GetComponent<RectTransform>().sizeDelta = activeSize;
        currentTuple.website.SetActive(true);
        currentWebSite = currentTuple.website;
    }

    private void UpdateScrollBar()
    {
        float webSiteHeight = currentWebSite.GetComponent<RectTransform>().rect.height;
        scrollbar.size = Mathf.Clamp(-webSiteHeight/5400f+1.2f, 0.2f, 1);
    }

    private void UpdateScrollBarPosition()
    {
        float webSiteHeight = currentWebSite.GetComponent<RectTransform>().rect.height;
        float maxHeight = webSiteHeight / 2f;
        scrollbar.value = currentWebSite.transform.localPosition.y / webSiteHeight + 1f / 2f;
    }

    private TupleTabWebSite FindTuple(string name)
    {
        return otherSites.Find(tupleSite => tupleSite.name.Contains(name));
    }
    
    private TupleTabWebSite FindTuple(int id)
    {
        return otherSites.Find(tupleSite => tupleSite.id == id);
    }

    public void OnScrollBar(float values)
    {
        if (tween == null || !tween.IsActive() || tween.IsComplete())
        {
            float webSiteHeight = currentWebSite.GetComponent<RectTransform>().rect.height;
            float cameraViewHeight = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().pixelHeight;
            float targetValue = Mathf.Clamp((webSiteHeight - cameraViewHeight) * values - (webSiteHeight - cameraViewHeight) / 2f, -(webSiteHeight - cameraViewHeight) / 2f,
                (webSiteHeight - cameraViewHeight) / 2f);
            Vector3 targetPosition = new Vector3(currentWebSite.transform.localPosition.x, targetValue, currentWebSite.transform.localPosition.z);
            currentWebSite.transform.localPosition = targetPosition;
        }
    }
    
    private void OnScroll(InputValue inputValue)
    {
        float scrollValue = inputValue.Get<float>();
        if(scrollValue != 0)
        {
            float webSiteHeight = currentWebSite.GetComponent<RectTransform>().rect.height;
            float cameraViewHeight = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().pixelHeight;
            float targetValue = Mathf.Clamp(currentWebSite.transform.localPosition.y + scrollValue,-(webSiteHeight - cameraViewHeight)/2f, (webSiteHeight - cameraViewHeight)/2f);
            Vector3 targetPosition = new Vector3(currentWebSite.transform.localPosition.x, targetValue ,currentWebSite.transform.localPosition.z);
            if (tween == null || !tween.IsActive() || tween.IsComplete())
            {
                tween = currentWebSite.transform.DOLocalMove(targetPosition, 0.8f).SetEase(Ease.InOutSine);
            }
            else
            {
                tween.ChangeEndValue(targetPosition, 0.8f, true).SetEase(Ease.OutSine);
            }
        }
    }

    private void Update()
    {
        if (tween != null && tween.IsActive() && !tween.IsComplete())
        {
           UpdateScrollBarPosition();
        }
    }

    private void UpdateTabPosition()
    {
        Vector3 tabPosition = otherSites[0].tab.GetComponent<RectTransform>().position;
        if (lastId == 0)
        {
            otherSites[0].tab.GetComponent<RectTransform>().position = new Vector3(mainSite.tab.GetComponent<RectTransform>().position.x + 635,tabPosition.y,tabPosition.z);
        }
        else
        {
            if (lastId == 1)
            {
                otherSites[0].tab.GetComponent<RectTransform>().position = new Vector3(mainSite.tab.GetComponent<RectTransform>().position.x + 510,tabPosition.y,tabPosition.z);
            }
            else
            {
                otherSites[0].tab.GetComponent<RectTransform>().position = new Vector3(mainSite.tab.GetComponent<RectTransform>().position.x + 385,tabPosition.y,tabPosition.z);
            }
        }
        for(int i = 1; i <otherSites.Count; i++)
        {
            tabPosition = otherSites[i].tab.GetComponent<RectTransform>().position;
            if (lastId == i)
            {
                otherSites[i].tab.GetComponent<RectTransform>().position = new Vector3(otherSites[i-1].tab.GetComponent<RectTransform>().position.x + 385,tabPosition.y,tabPosition.z);
            }
            else
            {
                if (lastId == i + 1)
                {
                    otherSites[i].tab.GetComponent<RectTransform>().position = new Vector3(otherSites[i - 1].tab.GetComponent<RectTransform>().position.x + 385, tabPosition.y, tabPosition.z);
                }
                else
                {
                    otherSites[i].tab.GetComponent<RectTransform>().position = new Vector3(otherSites[i - 1].tab.GetComponent<RectTransform>().position.x + 260, tabPosition.y, tabPosition.z);
                }
            }
        }
    }
}
