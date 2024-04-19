using Godot;
using System;

public partial class Attribute_Effects : Effects    //if value < 0 -> debuff value>0 -> buff
{//value is always positive, class changes it on its own
    public Attribute_Effects(int life, int value, int namesHashcode, string[] AttributePath) :base(life, namesHashcode, value, AttributePath)
    {
        type = (Names)namesHashcode;
        attribute = GetNode<Attribute>(AttributePath[0]);

        switch (type)
        {
            case Names.ApRegeneration:
                EffectTypeFunction = ApRegenerationInitialize;
                EffectTypeFunctionDeath = ApRegenerationDeath;
                break;
            case Names.MaxAp:
                EffectTypeFunction = MaxApInitialize;
                EffectTypeFunctionDeath = MaxApDeath;
                break;
            case Names.Strength:
                EffectTypeFunction = BaseAttributeInitialize;
                EffectTypeFunctionDeath = BaseAttributeDeath;
                break;
            case Names.Agility:
                EffectTypeFunction = BaseAttributeInitialize;
                EffectTypeFunctionDeath = BaseAttributeDeath;
                break;
            case Names.Intelligence:
                EffectTypeFunction = BaseAttributeInitialize;
                EffectTypeFunctionDeath = BaseAttributeDeath;
                break;
            default:
                break;
        }
        EffectTypeFunction(value > 0);
    }
    private void ApRegenerationInitialize(bool buff)//changes regen //begining of life
    {
        attribute.ChangeAttributeRegeneration(value);
    }
    private void MaxApInitialize(bool buff)        //ap robi w chuchu niebiezpieczne rzeczy
    {
        if (buff) attribute.AddBaseAttribute(value);
        else attribute.DecreaseBaseAttribute(value);

    }
    private void BaseAttributeInitialize(bool buff)// str=agl=int
    {
        if (buff) attribute.BuffAttribute(value);
        else attribute.DebuffAttribute(value);

    }
    private void ApRegenerationDeath(bool buff)//changes regen //begining of life
    {
        attribute.ChangeAttributeRegeneration(-value);
    }
    private void MaxApDeath(bool buff)        //ap robi w chuchu niebiezpieczne rzeczy
    {
        if (buff) attribute.DecreaseBaseAttribute(value);
        else attribute.AddBaseAttribute(value);

    }
    private void BaseAttributeDeath(bool buff)// str=agl=int
    {
        if (buff) attribute.DebuffAttribute(value);
        else attribute.BuffAttribute(value);

    }
    public override void TurnBeginAction()
    {
        throw new NotImplementedException();
    }
    public override void TurnEndAction()
    {
        life--;
        if (life == 0) EffectTypeFunctionDeath(value > 0);       
    }
    private Attribute attribute;
    private Names type;
    private static Action<bool> EffectTypeFunction = null;
    private static Action<bool> EffectTypeFunctionDeath = null;
    new private enum Names
    {
        None = 0,
        ApRegeneration = 1,
        MaxAp = 2,
        Strength = 3,
        Agility = 4,
        Intelligence = 5,
    }
}
