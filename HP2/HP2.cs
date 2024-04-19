using Godot;
using System;

public partial class HP2 : Node2D
{
	public override void _Ready()
	{
		currentHealth = startingHealth;
        maxHealth = startingHealth;
	}
    [Export]
    public int StartingHealth { get => startingHealth; set => startingHealth = value; }

    public void Damage(int damage){	currentHealth -= damage;	} //can be changed to int to return damage taken: return damage;
	public void Heal(int heal) { //can be changed to int to return amount healed: return heal;
        if (currentHealth + heal > maxHealth) RestoreHealth();
        else Damage(-heal); 
    }
    public void AddmaxHealth (int healthIncrease){  maxHealth += healthIncrease; }
    //if statement can be added to forbid maxhp from faling below 0 or 1: if (maxHealth != 1) maxHealth -=healthDecrease;
    public void DecreasemaxHealth (int healthDecrease){ maxHealth -= healthDecrease; if (maxHealth < currentHealth) currentHealth = maxHealth; }
	public bool IsAlive(){ return currentHealth > 0; }
    public void RestoreHealth() { currentHealth = maxHealth; }
	public int getmaxHealth(){ return maxHealth;	}
	public int getcurrentHealth(){	return currentHealth;	}
    private int maxHealth;
    private int currentHealth;
    private int startingHealth=10;
}
