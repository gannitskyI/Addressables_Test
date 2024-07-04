using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;  
using UnityEngine.ResourceManagement.AsyncOperations;  

public class Enemy : MonoBehaviour, IAttackable
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthBar;
    public string deathEffectAddressableKey = "DeathEffectPrefab";  
    public string itemDropAddressableKey = "ItemDropPrefab";  

    private void Start()
    {
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("HealthBar reference not set in Enemy script.");
        }
    }

    public void Die()
    { 
        ApplyDeathEffect();
        SpawnItemDrop();
 
        Destroy(gameObject);
    }

    private void ApplyDeathEffect()
    {
        LoadAndInstantiateAsync(deathEffectAddressableKey, transform.position, Quaternion.identity, 3f);
    }

    private void SpawnItemDrop()
    { 
        Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
        LoadAndInstantiateAsync(itemDropAddressableKey, transform.position, spawnRotation);
    }

    private void LoadAndInstantiateAsync(string addressableKey, Vector3 position, Quaternion rotation, float destroyAfterSeconds = 0f)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(addressableKey);
        handle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject prefab = op.Result;
                GameObject instance = Instantiate(prefab, position, rotation);
                if (destroyAfterSeconds > 0f)
                {
                    Destroy(instance, destroyAfterSeconds);
                }
            }
            else
            {
                Debug.LogWarning($"Failed to load asset with key: {addressableKey}");
            }
        };
    }
}
