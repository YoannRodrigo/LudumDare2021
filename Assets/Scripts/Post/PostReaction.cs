using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace post
{
    public class PostReaction : MonoBehaviour
    {
        [SerializeField] private Post _post;
        [SerializeField] private GameObject _unreactedImage;
        [SerializeField] private GameObject _reactedImage;
        [SerializeField] private TextMeshProUGUI _reactionNumber;
        private bool _hasreacted;

        public void SetReaction(bool hasReacted, int reactionsNumber)
        {
            _reactionNumber.text = reactionsNumber.ToString();
            if (hasReacted)
            {
                _unreactedImage.SetActive(false);
                _reactedImage.SetActive(true);
            }
            else
            {
                _unreactedImage.SetActive(true);
                _reactedImage.SetActive(false);
            }
        }

        public void ReactToPost()
        {
            _post.ReactToPost();
        }
    }
}