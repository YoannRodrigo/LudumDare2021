using System;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
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
    private int lastId;

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
            }
            else
            {
                FindTuple(id).website.SetActive(true);
            }

            lastId = id;
        }
    }

    private TupleTabWebSite FindTuple(string name)
    {
        return otherSites.Find(tupleSite => tupleSite.name.Contains(name));
    }
    
    private TupleTabWebSite FindTuple(int id)
    {
        return otherSites.Find(tupleSite => tupleSite.id == id);
    }
}
