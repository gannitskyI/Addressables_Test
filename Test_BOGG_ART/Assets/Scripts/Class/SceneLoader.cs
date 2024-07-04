using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.ResourceManagement.ResourceProviders;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup buttonCanvasGroup;  
    
    private static SceneLoader instance;
    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(SceneLoader).Name;
                    instance = obj.AddComponent<SceneLoader>();
                }
            }
            return instance;
        }
    }

    private AsyncOperationHandle<SceneInstance> sceneLoadHandle;
    private void Start()
    {
        ShowButton();
    }
    private bool ValidateButtonCanvasGroup()
    {
        if (buttonCanvasGroup == null)
        {
            Debug.LogError("Button CanvasGroup is not assigned!");
            return false;
        }
        return true;
    }

    private void HideButton(Action onComplete)
    {
        buttonCanvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            buttonCanvasGroup.interactable = false;
            buttonCanvasGroup.blocksRaycasts = false;
            Debug.Log("Button hidden.");
            onComplete?.Invoke();
        });
    }

    private void ShowButton()
    {
        buttonCanvasGroup.alpha = 1;
        buttonCanvasGroup.interactable = true;
        buttonCanvasGroup.blocksRaycasts = true;
    }
    public void ReloadScene()
    {
        if (sceneLoadHandle.IsValid())
        {
            Addressables.UnloadSceneAsync(sceneLoadHandle, true).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Scene unloaded successfully.");
                    LoadScene();  
                }
                else
                {
                    Debug.LogError($"Failed to unload scene.");
                }
            };
        }
        else
        {
            Debug.LogWarning("No scene loaded to reload.");
        }
    }

    private void LoadScene()
    {
        if (!ValidateButtonCanvasGroup()) return;
        HideButton(() =>
        {
            sceneLoadHandle = Addressables.LoadSceneAsync("ForestScene", LoadSceneMode.Additive);
            sceneLoadHandle.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Scene loaded successfully.");
                }
                else
                {
                    Debug.LogError($"Failed to load scene.");
                }
            };
        });
    }
}
