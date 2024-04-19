using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
//Add max hp buff/debuff
public partial class HP_Effects : Effects
{
    public HP_Effects(int life, int value, int namesHashcode, string[] ArmourPath) : base(life, namesHashcode, value, ArmourPath) {
        type = (Names)namesHashcode;
        armour = GetNode<Armour>(ArmourPath[0]);
        valueDecrease = value / life;

        switch (type)
        {
            case Names.Regeneration:
                EffectTypeFunction = Regeneration;
                break;
            case Names.Poison:
                EffectTypeFunction = Poison;
                break;
            case Names.Burn:
                EffectTypeFunction = Burn;
                break;
            case Names.Bleed:
                EffectTypeFunction = Bleed;
                break;
            default:
                break;
        }
    }
    private bool Regeneration(bool flag) {
        if (flag)
        {
            armour.Damage(-value, true);
            life--;
        }
        return isAlive(); 
    }
    private bool Poison(bool flag) {
        if (!flag)
        {
            armour.Damage(value, true);
            life--;
        }
        return isAlive(); 
    }
    private bool Burn(bool flag) {      //deals decreasing damage rounded up
        if (!flag)
        {
            armour.Damage(value, false);
            life--;
            value -= valueDecrease;
        }
        
        return isAlive(); 
    }
    private bool Bleed(bool flag) {
        if (!flag)
        {
            armour.Damage(value, false);
            life--;
        }
        return isAlive(); 
    }
    public override void TurnBeginAction()
    {
        EffectTypeFunction(true);
        //funkcja flag=1
    }
    public override void TurnEndAction()
    {
        EffectTypeFunction(false);
        //funkcja flag =0
    }

    private int valueDecrease;
    private Names type = 0;
    private Armour armour;
    private static Func<bool, bool> EffectTypeFunction = null;
    new private enum Names
    {
        None = 0,
        Regeneration = 1,
        Poison = 2,
        Burn = 3,
        Bleed = 4
    }
}