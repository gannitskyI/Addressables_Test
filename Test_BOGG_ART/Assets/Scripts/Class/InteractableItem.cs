using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InteractableItem : MonoBehaviour, IInteractable
{ 
    public void Interact()
    {
        SceneLoader.Instance.ReloadScene();  
        Destroy(gameObject);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerController.Instance.EnableInteraction(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerController.Instance.DisableInteraction(this);
        }
    }

}
