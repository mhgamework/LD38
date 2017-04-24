using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Level001;
using UnityEngine;

public class PlayerHealthScript : Singleton<PlayerHealthScript>
{
    public float MaxHealth;
    public float Health;
    public GameObject OnDamageAnim;

    public bool debug_die = false;
    public bool debug_damage = false;

    // Use this for initialization
    void Start()
    {

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

        AEnemy.DoOnDamageAnim(OnDamageAnim, PlanetCamera.Instance.PlayerTransform);

        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        TimelineService.Instance.RestoreCheckpoint();
    }
}
