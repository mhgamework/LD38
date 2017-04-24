using Assets.Scripts;
using UnityEngine;

public class AEnemy : MonoBehaviour, IEnemy
{
    public float Health = 3;

    [SerializeField]
    private HealthDisplay healthDisplayPrefab = null;
    [SerializeField]
    private float healthDisplaySpawnHeight = 3f;

    private HealthDisplay healthDisplay;

    protected virtual void Start()
    {
        healthDisplay = Instantiate(healthDisplayPrefab);
        healthDisplay.Initialize(Health, transform, healthDisplaySpawnHeight);
    }


    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
            Destroy(gameObject);

        healthDisplay.ApplyDamage(amount);

    }
}
