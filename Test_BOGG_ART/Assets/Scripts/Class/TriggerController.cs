using UnityEngine;
using DG.Tweening;

public class TriggerController : MonoBehaviour
{
    public CanvasGroup uiElementCanvasGroup; 
    public Transform uiElementTransform; 
    private bool isPlayerInside = false;  
    private void Start()
    { 
        uiElementCanvasGroup.alpha = 0;
        uiElementCanvasGroup.interactable = false;
        uiElementCanvasGroup.blocksRaycasts = false;
        uiElementTransform.localScale = Vector3.zero;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            if (!isPlayerInside)  
            {
                isPlayerInside = true;
                ShowUIElement();  
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            if (isPlayerInside) 
            {
                isPlayerInside = false;
                HideUIElement(); 
            }
        }
    }

    private void ShowUIElement()
    {
        uiElementCanvasGroup.DOFade(1, 0.5f); 
        uiElementTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);  
        uiElementCanvasGroup.interactable = true;
        uiElementCanvasGroup.blocksRaycasts = true;
    }

    private void HideUIElement()
    {
        uiElementCanvasGroup.DOFade(0, 0.5f);  
        uiElementTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);  
        uiElementCanvasGroup.interactable = false;
        uiElementCanvasGroup.blocksRaycasts = false;
    }
}
