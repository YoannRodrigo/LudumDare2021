using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace post
{
    public class GlitchPost : Post
    {
        [SerializeField] private WebSiteManager _websiteManager;
        [SerializeField] private GameObject[] _normalPost;
        [SerializeField] private GameObject[] _glitchedPost;
        [SerializeField, Range(0, 1)] private float _detectRange;
        [SerializeField] private PostData _infosGlitch;
        [SerializeField] private GameObject _containerMediaGlitch;
        [SerializeField] private Image _avatarGlitch;
        [SerializeField] private Image _imageGlitch;
        [SerializeField] private TextMeshProUGUI _usernameGlitch;
        [SerializeField] private TextMeshProUGUI _dateGlitch;
        [SerializeField] private TextMeshProUGUI _textContentGlitch;
        [SerializeField] private PostReaction _reactionsGlitch;
        [SerializeField] private TextMeshProUGUI _commentsAmountGlitch;
        [SerializeField] private TextMeshProUGUI _repweetsAmountGlitch;

        private bool _isGlitchPostActive = false;


        protected override void OnValidate()
        {
            base.OnValidate();
            FillGlitchPostWithInfos();
        }
        private void Start()
        {
            _websiteManager.OnScroll += Glitch;
        }

        public void FillGlitchPostWithInfos()
        {
            _usernameGlitch.text = _infosGlitch.Username;
            _avatarGlitch.sprite = _infosGlitch.Avatar;
            _dateGlitch.text = _infosGlitch.Date;
            FillTextContent(_infosGlitch.Content.Text);
            FillImageContent(_infosGlitch.Content.Image);
            _reactionsGlitch.SetReaction(_infosGlitch.HasPlayerReacted, _infosGlitch.ReactionAmount);
            _commentsAmountGlitch.text = _infosGlitch.CommentsAmount.ToString();
            _repweetsAmountGlitch.text = _infosGlitch.RepweetAmount.ToString();
        }

        private void FillTextContent(string text)
        {
            if (text != "")
            {
                _textContentGlitch.text = text;
                _textContentGlitch.enabled = true;
            }
            else
            {
                _textContentGlitch.enabled = false;
            }
        }

        private void FillImageContent(Sprite image)
        {
            if (image != default(Sprite))
            {
                _imageGlitch.sprite = image;
                _imageGlitch.enabled = true;
                _containerMediaGlitch.SetActive(true);
            }
            else
            {
                _containerMediaGlitch.SetActive(false);
                _imageGlitch.enabled = false;
            }
        }

        private void FillModel3DContent(GameObject model3D)
        {
            if (model3D != null)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(_modelViewerScene, LoadSceneMode.Additive);
                operation.completed += (op) => ModelViewer.Instance.DisplayModel(model3D);
            }
        }
        private void Glitch(float scrollPercent)
        {
            if (scrollPercent >= .66f - _detectRange && scrollPercent <= .66f + _detectRange)
            {
                foreach (GameObject container in _normalPost)
                {
                    container.SetActive(false);
                }
                foreach (GameObject container in _glitchedPost)
                {
                    container.SetActive(true);
                }
                _isGlitchPostActive = true;
            }
            else
            {
                foreach (GameObject container in _glitchedPost)
                {
                    container.SetActive(false);
                }
                foreach (GameObject container in _normalPost)
                {
                    container.SetActive(true);
                }
                _isGlitchPostActive = false;
            }
        }

        public override void ReactToPost()
        {
            if (_isGlitchPostActive)
            {
                onFav.Invoke();
                _infosGlitch.HasPlayerReacted = !_infosGlitch.HasPlayerReacted;
                _infosGlitch.ReactionAmount = _infosGlitch.HasPlayerReacted ? _infosGlitch.ReactionAmount + 1 : _infosGlitch.ReactionAmount - 1;
                _reactionsGlitch.SetReaction(_infosGlitch.HasPlayerReacted, _infosGlitch.ReactionAmount);
            }
            else
            {
                base.ReactToPost();
            }
        }

    }
}