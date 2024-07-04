using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonHandler : MonoBehaviour
{
    public Button spawnButton;
    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();

        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner component not found on the GameObject.");
            return;
        }

        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(OnSpawnButtonClicked);
        }
        else
        {
            Debug.LogError("Spawn Button is not assigned.");
        }
    }

    public void OnSpawnButtonClicked()
    {
        enemySpawner.SpawnEnemy(spawnPosition);
        spawnButton.interactable = false;
    }
}
