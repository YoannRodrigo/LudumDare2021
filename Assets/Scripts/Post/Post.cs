using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace post
{
    public class Post : MonoBehaviour
    {
        [SerializeField] private PostData _infos;
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _username;
        [SerializeField] private TextMeshProUGUI _date;
        [SerializeField] private TextMeshProUGUI _textContent;
        [SerializeField] private TextMeshProUGUI _reactions;
        [SerializeField] private GameObject _model3D;

        private void Awake()
        {
            FillPostWithInfos(_infos);
        }
        public void FillPostWithInfos(PostData infos)
        {
            _infos = infos;
            _username.text = infos.Username;
            _date.text = infos.Date;
            FillTextContent(infos.Content.Text);
            FillImageContent(infos.Content.Image);
            FillModel3DContent(infos.Content.model3D);
            _reactions.text = infos.ReactionAmount.ToString();
        }

        private void FillTextContent(string text)
        {
            if (text != "")
            {
                _textContent.text = text;
                _textContent.enabled = true;
            }
            else
            {
                _textContent.enabled = false;
            }
        }

        private void FillImageContent(Sprite image)
        {
            if (image != null)
            {
                _image.sprite = image;
                _image.enabled = true;
            }
            else
            {
                _image.enabled = false;
            }
        }

        private void FillModel3DContent(GameObject model3D)
        {
            if (model3D != null)
            {
                _model3D = Instantiate(model3D);
                _model3D.SetActive(true);
            }
            else
            {
                _model3D.SetActive(false);
            }
        }
    }
}