using System;
using System.Collections.Generic;
using DG.Tweening;
using post;
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
    [SerializeField] private Sprite activeTab;
    [SerializeField] private Sprite inactiveTab;
    [SerializeField] private ScrollBarController scrollBarController;
    [SerializeField] private PostFeed _feed;

    public float ScrollDelay;
    public bool HasObjectViewerOpen;
    private float _scrollPercent;
    [SerializeField] private float scrollLength;

    private readonly Vector2 activeSize = new Vector2(500, 50);
    private readonly Vector2 inactiveSize = new Vector2(250, 40);

    private int lastId;
    private GameObject currentWebSite;
    private Tweener tween;
    public event Action<float> OnScroll;

    public float ScrollPercent
    {
        get => _scrollPercent; private set
        {
            _scrollPercent = value;
            OnScroll?.Invoke(_scrollPercent);
        }
    }
    public float ScrollLength { get => scrollLength; private set => scrollLength = value; }

    private void Start()
    {
        currentWebSite = mainSite.website;
        CalculateScrollLength(() => scrollBarController.OnTabChanges(currentWebSite));
    }

    public void CalculateScrollLength(Action callback = null)
    {
        _feed.UpdateFeed(this, () =>
         {
             float webSiteHeight = currentWebSite.GetComponent<RectTransform>().rect.height;
             if (lastId == 0)
             {
                 scrollLength = Mathf.Min(webSiteHeight, _feed.FeedSize - Mathf.Abs(_feed.FeedStartHeight));
             }
             else
             {
                 scrollLength = webSiteHeight;
             }
             callback?.Invoke();
         });
    }

    public void ActivateWebSiteTab(string name)
    {
        FindTuple(name).tab.SetActive(true);
    }

    public void ActivateWebSiteTab(int id)
    {
        SoundManager.PlaySFX("New_Tab");
        FindTuple(id).tab.SetActive(true);
    }

    public void OnTabClick(int id)
    {
        if (id != lastId)
        {

            DisableTab();
            ActivateTab(id);
            lastId = id;
            CalculateScrollLength(() =>
            {
                scrollBarController.OnTabChanges(currentWebSite);
                UpdateTabPosition();
            });
        }
    }

    private void DisableTab()
    {
        TupleTabWebSite lastTuple;
        if (lastId != 0)
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

    private TupleTabWebSite FindTuple(string name)
    {
        return otherSites.Find(tupleSite => tupleSite.name.Contains(name));
    }

    private TupleTabWebSite FindTuple(int id)
    {
        return otherSites.Find(tupleSite => tupleSite.id == id);
    }

    private void OnWebsiteScroll(InputValue inputValue)
    {
        if (HasObjectViewerOpen)
        {
            return;
        }

        float scrollValue = inputValue.Get<float>();
        if (scrollValue != 0)
        {
            float cameraViewHeight = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().pixelHeight;
            float targetValue = Mathf.Clamp(currentWebSite.GetComponent<RectTransform>().anchoredPosition.y - scrollValue, 0, scrollLength - cameraViewHeight);
            targetValue = Mathf.Max(targetValue, 0);
            ScrollPercent = targetValue;
            Vector3 targetPosition = new Vector3(currentWebSite.GetComponent<RectTransform>().anchoredPosition.x, targetValue, 0);
            if (tween == null || !tween.IsActive() || tween.IsComplete())
            {
                tween = currentWebSite.GetComponent<RectTransform>().DOAnchorPos3D(targetPosition, 0.3f).SetEase(Ease.InOutSine);
            }
            else
            {
                tween.ChangeEndValue(targetPosition, this.ScrollDelay, true).SetEase(Ease.OutSine);
            }
        }
    }

    private void Update()
    {
        if (tween != null && tween.IsActive() && !tween.IsComplete())
        {
            scrollBarController.UpdateValues();
        }
    }

    private void UpdateTabPosition()
    {
        Vector3 tabPosition = otherSites[0].tab.GetComponent<RectTransform>().anchoredPosition;
        if (lastId == 0)
        {
            otherSites[0].tab.GetComponent<RectTransform>().anchoredPosition = new Vector2(mainSite.tab.GetComponent<RectTransform>().anchoredPosition.x + 585, tabPosition.y);
        }
        else
        {
            if (lastId == 1)
            {
                otherSites[0].tab.GetComponent<RectTransform>().anchoredPosition = new Vector2(mainSite.tab.GetComponent<RectTransform>().anchoredPosition.x + 460, tabPosition.y);
            }
            else
            {
                otherSites[0].tab.GetComponent<RectTransform>().anchoredPosition = new Vector2(mainSite.tab.GetComponent<RectTransform>().anchoredPosition.x + 335, tabPosition.y);
            }
        }
        for (int i = 1; i < otherSites.Count; i++)
        {
            tabPosition = otherSites[i].tab.GetComponent<RectTransform>().anchoredPosition;
            if (lastId == i)
            {
                otherSites[i].tab.GetComponent<RectTransform>().anchoredPosition = new Vector2(otherSites[i - 1].tab.GetComponent<RectTransform>().anchoredPosition.x + 385, tabPosition.y);
            }
            else
            {
                if (lastId == i + 1)
                {
                    otherSites[i].tab.GetComponent<RectTransform>().anchoredPosition = new Vector2(otherSites[i - 1].tab.GetComponent<RectTransform>().anchoredPosition.x + 385, tabPosition.y);
                }
                else
                {
                    otherSites[i].tab.GetComponent<RectTransform>().anchoredPosition = new Vector2(otherSites[i - 1].tab.GetComponent<RectTransform>().anchoredPosition.x + 260, tabPosition.y);
                }
            }
        }
    }
}
