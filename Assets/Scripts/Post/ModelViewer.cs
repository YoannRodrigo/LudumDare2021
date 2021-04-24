using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelViewer : MonoBehaviour
{
    public static ModelViewer Instance;
    [SerializeField] private SceneReference _scene;
    [SerializeField] private Transform _holder;
    private GameObject _modelDisplayed;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void DisplayModel(GameObject model)
    {
        _modelDisplayed = Instantiate(model);
        _modelDisplayed.transform.parent = _holder;
        _modelDisplayed.transform.position = Vector3.zero;
        _modelDisplayed.transform.rotation = Quaternion.identity;
        _modelDisplayed.transform.localScale = Vector3.one;
    }

    public void CloseViewer()
    {
        SceneManager.UnloadSceneAsync(_scene);
    }
}
