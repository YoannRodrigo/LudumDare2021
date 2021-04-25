using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace post
{
    [CustomEditor(typeof(PostData))]
    public class PostDataEditor : Editor
    {
        private SerializedProperty _username;
        private SerializedProperty _date;
        private SerializedProperty _avatar;
        private SerializedProperty _content;
        private SerializedProperty _reactionAmount;
        private SerializedProperty _commentsAmount;
        private SerializedProperty _repweetsAmount;
        private SerializedProperty _hasReacted;
        private SerializedProperty _comments;

        private void OnEnable()
        {
            _username = serializedObject.FindProperty("Username");
            _avatar = serializedObject.FindProperty("Avatar");
            _content = serializedObject.FindProperty("Content");
            _reactionAmount = serializedObject.FindProperty("ReactionAmount");
            _commentsAmount = serializedObject.FindProperty("CommentsAmount");
            _repweetsAmount = serializedObject.FindProperty("RepweetAmount");
            _comments = serializedObject.FindProperty("Comments");
            _date = serializedObject.FindProperty("Date");
            _hasReacted = serializedObject.FindProperty("HasPlayerReacted");

        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_avatar, new GUIContent("Avatar"));
            EditorGUILayout.PropertyField(_username, new GUIContent("Username"));
            EditorGUILayout.PropertyField(_date, new GUIContent("Date"));
            EditorGUILayout.PropertyField(_content, new GUIContent("Content"));
            EditorGUILayout.PropertyField(_reactionAmount, new GUIContent("Reactions"));
            EditorGUILayout.PropertyField(_commentsAmount, new GUIContent("Comments"));
            EditorGUILayout.PropertyField(_repweetsAmount, new GUIContent("Repweets"));
            EditorGUILayout.PropertyField(_hasReacted, new GUIContent("Has Player Reacted"));
            EditorGUILayout.PropertyField(_comments, new GUIContent("Comments"));
            DisplayComments();
            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayComments()
        {
            if (GUILayout.Button("Create Comment"))
            {
                PostData newComment = ScriptableObject.CreateInstance("PostData") as PostData;
                string[] splitedPath = AssetDatabase.GetAssetPath(serializedObject.targetObject).Split('/');
                string finalPath = "";
                for (int i = 0; i < splitedPath.Length - 1; i++)
                {
                    finalPath += splitedPath[i];
                    finalPath += "/";
                }
                AssetDatabase.CreateAsset(newComment, finalPath + "/" + serializedObject.targetObject.name + " Comment " + (_comments.arraySize + 1) + ".asset");
                AssetDatabase.SaveAssets();
                _comments.arraySize += 1;
                _comments.GetArrayElementAtIndex(_comments.arraySize - 1).objectReferenceValue = newComment;
            }
        }
    }
}