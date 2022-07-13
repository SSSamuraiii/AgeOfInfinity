using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDead;

    private int healthMax;
    private int health;

    public HealthSystem(int HealthMax)
    {
        this.healthMax = HealthMax;
        health = healthMax;
    }

    public void SetHealthAmount(int health)
    {
        this.health = health;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public int GetHealthAmount()
    {
        return health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);

        if(health <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Die()
    {
        if (OnDead != null) OnDead(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
