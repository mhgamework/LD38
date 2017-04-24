using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Level001;
using UnityEngine;

public class PlayerHealthScript : Singleton<PlayerHealthScript>
{
    public float MaxHealth;
    public GameObject OnDamageAnim;

    public float Health
    {
        get { return health; }
        set
        {
            if (value == MaxHealth)
                healthDisplay.Initialize(MaxHealth, playerTransform, 4f);
            health = value;
        }
    }
    private float health;

    public bool debug_die = false;
    public bool debug_damage = false;

    [SerializeField]
    private HealthDisplay playerHealthDisplayPrefab = null;
    [SerializeField]
    private Transform playerTransform = null;

    private HealthDisplay healthDisplay;

    // Use this for initialization
    void Start()
    {
        healthDisplay = Instantiate(playerHealthDisplayPrefab);
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (debug_die)
        {
            Health = 0;
            debug_die = false;
            Die();
        }
        if (debug_damage)
        {
            debug_damage = false;
            TakeDamage(1);
        }
    }

    public void TakeDamage(float strikeDamage)
    {
        Debug.Log("Auch!");
        Health -= strikeDamage;
        healthDisplay.ApplyDamage(strikeDamage);
        AEnemy.DoOnDamageAnim(OnDamageAnim, PlanetCamera.Instance.PlayerTransform);

        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        TimelineService.Instance.RestoreCheckpoint();
    }

    public void RestoreHealth()
    {
        Health = MaxHealth;
    }
}
