using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthRect = null;
    [SerializeField]
    [Tooltip("The canvas width of a single health unit.")]
    private int widthPerHealthUnit = 100;

    private float totalHealth;
    private float currentHealth;
    private bool isInitialized;
    private RectTransform rectTransform;

    private Transform camTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        camTransform = Camera.main.transform;

        gameObject.SetActive(false);
    }

    public void Initialize(float health, Transform parent_transform, float heigth, int widthPerHealthUnit = 50)
    {
        isInitialized = true;

        this.widthPerHealthUnit = widthPerHealthUnit;
        totalHealth = health;
        currentHealth = health;

        rectTransform.SetParent(parent_transform, false);
        rectTransform.localPosition = new Vector3(0f, heigth, 0f);

        rectTransform.sizeDelta = toSizeDelta(totalHealth);
        healthRect.sizeDelta = toSizeDelta(currentHealth);

        gameObject.SetActive(false);
    }

    public void ApplyDamage(float damage)
    {
        gameObject.SetActive(true);
        currentHealth -= damage;
        healthRect.sizeDelta = toSizeDelta(currentHealth);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camTransform.position, camTransform.up);
    }

    private Vector2 toSizeDelta(float health_value)
    {
        return new Vector2(widthPerHealthUnit * health_value, 50);
    }
}
