using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ModelViewer : MonoBehaviour
{
    public static ModelViewer Instance;
    [SerializeField] private SceneReference _scene;
    [SerializeField] private Transform _holder;
    private GameObject _modelDisplayed;
    private bool _isPressed = false;
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
    private void OnPress()
    {
        _isPressed = !_isPressed;
    }
    private void OnMove(InputValue value)
    {
        if (!_isPressed)
        {
            return;
        }
        Vector2 delta = value.Get<Vector2>();
        Vector3 newRotation = new Vector3(delta.y,-delta.x);
        newRotation.x *= 20 * Time.deltaTime;
        _modelDisplayed.transform.Rotate(newRotation,Space.World);
    }
}
