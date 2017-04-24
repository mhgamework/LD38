using Assets.Scripts;
using UnityEngine;

public class AEnemy : MonoBehaviour, IEnemy
{
    public float Health = 3;



    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0) Destroy(gameObject);



    }
}
