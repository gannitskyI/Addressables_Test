using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawner : MonoBehaviour
{
    public string enemyPrefabAddressableKey = "EnemyPrefab";
    private GameObject spawnedEnemy;
    private bool isSpawning = false;

    public void SpawnEnemy(Vector3 spawnPosition)
    {
        if (isSpawning)
        {
            Debug.Log("An enemy is already being spawned. Cannot spawn another.");
            return;
        }

        if (spawnedEnemy != null)
        {
            Debug.Log("Destroying previously spawned enemy.");
            DestroySpawnedEnemy();
        }

        isSpawning = true;
        SpawnEnemyAsync(spawnPosition);
    }

    private async void SpawnEnemyAsync(Vector3 spawnPosition)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(enemyPrefabAddressableKey);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject enemyPrefab = handle.Result;
            Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
            spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogError($"Failed to load enemy prefab with key: {enemyPrefabAddressableKey}");
        }

        isSpawning = false;
    }

    public void DestroySpawnedEnemy()
    {
        if (spawnedEnemy != null)
        {
            Destroy(spawnedEnemy);
            spawnedEnemy = null;
        }
    }
}
