using Godot;
using System;
using System.Collections.Generic;

public abstract partial class BaseEnemy : Node2D //Base class for all enemies 
{
    //each enemy needs to be a scene with following children:\\
    //0 - AnimatedSprite2D; 1. - HP2; 2 - Armour; 3 - Attribute(strength); 4 - Attribute(agility); 5 - Attribute(intelligence)
    [Signal]
    public delegate void CharacterDiedEventHandler(); 
    public override void _Ready() // override ready in children to set cards
    {
        //SelectEnemyCards(new int[] { 1, 3, 4, 5, 6, 7, });

        //ConnectAttributesToChildren();
        //OR
        //CreateNewAttributes();
    }
    public override void _Process(double delta)
    {
        if (!hp.IsAlive())
        {
            EmitSignal(SignalName.CharacterDied);
            GD.Print("AAAAAA I DIED!");  //dying message\\ //maybe send signal?
        }
        //add animation
        
    }
    protected void SelectEnemyCards(int[] cardIDs)
    {
        this.cardIDs = cardIDs;
    }
    public BaseCard ChooseCardToPlayC()
    {
        Random random = new Random();
        return new BaseCard(cardIDs[random.Next(cardIDs.Length)]);
    }
    public int ChooseCardToPlayI()
    {
        Random random = new();
        return cardIDs[random.Next(cardIDs.Length)];
    }
    public void ReceiveCardPlayedByOpponet(BaseCard card)//add player stats for scaling? //smth like playerAgl, playerStd, playerInt
    {
        hp.Damage(armour.Damage(card.Stats.AttackValue)); //deals damage to armour and excess to hp
        int effectId;
        for(int i = 0; i < card.Stats.SpecialEffectsID.Length; i++)
        {
            string[] paths = new string[1];
            paths[0] = GetPathTo(armour); //Abomination destined for abortion

            effectId = card.Stats.SpecialEffectsID[i];
            switch (effectId)
            {

                case 0: //REGEN ignores armour and deals negative damage xd // change Effects to accept Nodepath insted of string   
                    effects.Add(new HP_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 1,paths));
                    break;
                case 1: //Poison Alice Cooper reference?
                    effects.Add(new HP_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 2, paths));
                    break;
                case 2: //Burn - deep purple
                    effects.Add(new HP_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 3, paths));
                    break;
                case 3: //bleed - i don't know
                    effects.Add(new HP_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 4, paths));
                    break;
                case 4: //ApRegen (stun / haste)
                    paths[0] = GetPathTo(actionPoints);
                    effects.Add(new Attribute_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 1, paths));
                    break;
                //case 5: //MaxAP
                // paths[0] = GetPathTo(Ap);
                //effects.Add(new Attribute_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 2, paths))
                case 6: //strength
                    paths[0] = GetPathTo(strength);
                    effects.Add(new Attribute_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 3, paths));
                    break;
                case 7: //agility
                    paths[0] = GetPathTo(agility);
                    effects.Add(new Attribute_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 4, paths));
                    break;
                case 8: //intelligence
                    paths[0] = GetPathTo(intelligence);
                    effects.Add(new Attribute_Effects(card.Stats.SpecialEffectsLife[i], card.Stats.SpecialEffectsValue[i], 5, paths));
                    break;
            }   
        }

    }
    public void ReceiveCardPlayedByOpponet(int cardID)    { ReceiveCardPlayedByOpponet(new BaseCard(cardID));    }// it just works!
    protected void ConnectAttributesToChildren()
    {
        sprite = GetChild<AnimatedSprite2D>(0);
        hp = GetChild<HP2>(1);
        armour = GetChild<Armour>(2);
        strength = GetChild<Attribute>(3);
        agility = GetChild<Attribute>(4);
        intelligence = GetChild<Attribute>(5);
    }
    protected void CreateNewAttributes()
    {
        sprite = new();
        hp = new();
        armour = new();
        strength = new();
        agility = new();
        intelligence = new();
    }
    protected int[] cardIDs;
    protected AnimatedSprite2D sprite;
    protected HP2 hp;
    protected Armour armour;
    protected Attribute strength, agility, intelligence, actionPoints;
    protected List<Effects> effects = new(0);
}