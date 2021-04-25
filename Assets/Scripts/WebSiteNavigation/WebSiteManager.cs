using System;
using System.Collections.Generic;
using DG.Tweening;
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
            if(lastId!=0)
            {
                FindTuple((lastId)).website.SetActive(false);
            }
            else
            {
                mainSite.website.SetActive(false);
            }
            if (id == 0)
            {
                mainSite.website.SetActive(true);
                currentWebSite = mainSite.website;
            }
            else
            {
                FindTuple(id).website.SetActive(true);
                currentWebSite = FindTuple(id).website;
            }
            UpdateScrollBar();
            UpdateScrollBarPosition();
            lastId = id;
        }
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
    
    private void OnWebsiteScroll(InputValue inputValue)
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
}
