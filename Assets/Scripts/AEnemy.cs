using Assets.Scripts;
using UnityEngine;

public class AEnemy : MonoBehaviour, IEnemy
{
    public float Health = 3;
    public GameObject OnDeathAnim;
    public GameObject OnDamageAnim;

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

        healthDisplay.ApplyDamage(amount);

        DoOnDamageAnim(OnDamageAnim, transform);

        if (Health <= 0)
        {
            Destroy(gameObject);
            if (OnDeathAnim)
            {
                var inst = Instantiate(OnDeathAnim);
                inst.gameObject.SetActive(true);
                inst.transform.position = transform.position;
                inst.transform.up = OnDeathAnim.transform.position.normalized;
            }
        }
    }

    public static void DoOnDamageAnim(GameObject onDamageAnim, Transform parent)
    {
        if (onDamageAnim != null)
        {
            var hit = Instantiate(onDamageAnim, parent);
            hit.gameObject.SetActive(true);
            hit.transform.position = parent.position;
            hit.transform.up = onDamageAnim.transform.position.normalized;
        }
    }
}
