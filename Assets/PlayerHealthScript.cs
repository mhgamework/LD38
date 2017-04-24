using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Level001;
using UnityEngine;

public class PlayerHealthScript : Singleton<PlayerHealthScript>
{
    public float MaxHealth;
    public float Health;

    public bool debug_die = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debug_die)
        {
            debug_die = false;
            Die();
        }
    }

    public void TakeDamage(float strikeDamage)
    {
        Debug.Log("Auch!");
        Health -= strikeDamage;
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        Area1Controller.Instance.RestoreCheckpoint();
    }
}
