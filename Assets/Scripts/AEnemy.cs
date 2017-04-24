using Assets.Scripts;
using UnityEngine;

public class AEnemy : MonoBehaviour, IEnemy
{
    public float Health = 3;

    [SerializeField]
    private HealthDisplay healthDisplayPrefab = null;
    [SerializeField]
    private float healthDisplaySpawnHeight = 3f;
    [SerializeField]
    private int healthDisplayHealthUnitWidth = 100;

    private HealthDisplay healthDisplay;

    protected virtual void Start()
    {
        healthDisplay = Instantiate(healthDisplayPrefab);
        healthDisplay.Initialize(Health, transform, healthDisplaySpawnHeight, healthDisplayHealthUnitWidth);
    }


    public virtual void TakeDamage(float amount, eDamageType type)
    {
        Health -= amount;
        if (Health <= 0)
            Destroy(gameObject);

        healthDisplay.ApplyDamage(amount);

    }
}
