﻿using Assets.Scripts;
using UnityEngine;

public class AEnemy : MonoBehaviour, IEnemy
{
    public float Health = 3;
    public GameObject OnDeathAnim;
    public GameObject OnDamageAnim;


    public void TakeDamage(float amount)
    {
        Health -= amount;

        DoOnDamageAnim(OnDamageAnim, transform);

        if (Health <= 0)
        {
            Destroy(gameObject);
            var inst = Instantiate(OnDeathAnim);
            inst.gameObject.SetActive(true);
            inst.transform.position = transform.position;
            inst.transform.up = OnDeathAnim.transform.position.normalized;
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