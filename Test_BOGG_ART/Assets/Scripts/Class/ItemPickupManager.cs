using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickupManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item")) // Adjust this tag based on your game's item tag
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        // Reload current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
