using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace post
{
    public class PostFeed : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private Post[] _posts;

        private void Start()
        {
            foreach (Post post in _posts)
            {
                post.FillPostWithInfos();
            }
            StartCoroutine(Layout());
        }

        private IEnumerator Layout()
        {
            _layoutGroup.enabled = false;
            yield return null;
            _layoutGroup.enabled = true;
            yield return null;
            _layoutGroup.enabled = false;
        }
    }
}