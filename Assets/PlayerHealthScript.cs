using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : Singleton<PlayerHealthScript>
{
    public float MaxHealth;
    public float Health;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float strikeDamage)
    {
        Debug.Log("Auch!");
        Health -= strikeDamage;
        if (Health < 0)
            Die();
    }

    private void Die()
    {
        throw new System.NotImplementedException();
    }
}
