using Godot;
using System;

public partial class Armour : Node2D
{
	public override void _Ready()
	{
		currentArmour = 0;
	}
	public int Damage(int  damage,bool ignoresArmour=false) {       //returns excess damage //addtional functionality: flag to deal damage only to armour, returns 0
		if (ignoresArmour) return damage; //deals damage ignoring armour
		if (currentArmour - damage < 0)
		{
			ClearArmour();
			return damage - currentArmour;
		}
		else currentArmour -= damage;
		return 0;
	}
	public void AddArmour(int addedArmor) { currentArmour += addedArmor; }
	public bool HasArmour() { return currentArmour > 0; }
	public void ClearArmour() {  currentArmour = 0; }
	public int getcurrentArmour() { return currentArmour; }
	private int currentArmour;
}
