using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace post
{
    [CreateAssetMenu(fileName = "Post Data", menuName = "ScriptableObjects/Post", order = 1)]
    public class PostData : ScriptableObject
    {
        [SerializeField] public Sprite Avatar;
        [SerializeField] public string Username;
        [SerializeField] public ContentData Content;
        [SerializeField] public int ReactionAmount;
        [SerializeField] public PostData[] Comments;
        [SerializeField] public string Date;
    }
}