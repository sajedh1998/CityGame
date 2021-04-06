using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float hitPoint = 100f;
    public Slider healthBar;

    public void Update()
    {
        healthBar.value = hitPoint;
    }
    public void TakeDamage(float damage)
    {
        hitPoint -= damage;
        if (hitPoint <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
