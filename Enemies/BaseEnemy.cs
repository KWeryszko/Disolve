using Godot;
using System;
using System.Collections.Generic;
[GlobalClass]
public abstract partial class BaseEnemy : Node2D //Base class for all enemies 
{
    //each enemy needs to be a scene with following children:\\
    //0 - AnimatedSprite2D; 1. - HP2; 2 - Armour; 3 - Attribute(strength); 4 - Attribute(agility); 5 - Attribute(intelligence) 6 - Attribute(AP)
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
        //animation player\\
        if (hp.IsAlive())   sprite.Play();
        else                sprite.Stop();

        
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
    public void ReceiveCardPlayedByOpponet(BaseCard card, int[] playerStats = null)
    {   
        if (playerStats == null) playerStats = new int[] {0,0,0};
        int damage = 
             (int)(card.Stats.AttackValue * card.Stats.StrengthScaling * playerStats[0]) +
            +(int)(card.Stats.AttackValue * card.Stats.AgilityScaling * playerStats[1]) +
            +(int)(card.Stats.AttackValue * card.Stats.IntelligenceScaling * playerStats[2]);
        hp.Damage(armour.Damage(damage)); //deals damage to armour and excess to hp
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
        //checks if character is alive after receiving damage\\
        if(!hp.IsAlive()) EmitSignal(SignalName.CharacterDied);
        UpdateHpBar();

    }

    public void ReceiveCardPlayedByOpponet(int cardID, int[] playerStats = null)    { ReceiveCardPlayedByOpponet(new BaseCard(cardID), playerStats);    }// it just works!

    protected void SetHpBar()
    {
        hpBar.MaxValue = getMaxHP();
        hpBar.Value = getMaxHP();
        hpBar.ShowPercentage = false;
        hpBar.Size = new Vector2(100, 20);
        StyleBoxFlat fill = GD.Load<StyleBoxFlat>("res://Enemies/PlayerBattleScene/Resources/HpBarFill.tres");
        StyleBoxFlat background = GD.Load<StyleBoxFlat>("res://Enemies/PlayerBattleScene/Resources/HpBarBackground.tres");
        hpBar.AddThemeStyleboxOverride("fill", fill);
        hpBar.AddThemeStyleboxOverride("background", background);

        AddChild(hpBar);
        hpBar.AddChild(new Label());
        hpBar.Size = new Vector2(200, 30);
        hpBar.GetChild<Label>(0).Position = new Vector2(0, 3); //change to center hp
        

        hpBar.GetChild<Label>(0).Text = "HP: " + getCurrentHP() + "/" + getMaxHP();

    }
    public void UpdateHpBar() { hpBar.Value = getCurrentHP(); hpBar.GetChild<Label>(0).Text = "HP: " + getCurrentHP() + "/" + getMaxHP(); }
    protected void ConnectAttributesToChildren()
    {
        sprite = GetChild<AnimatedSprite2D>(0);
        hp = GetChild<HP2>(1);
        armour = GetChild<Armour>(2);
        strength = GetChild<Attribute>(3);
        agility = GetChild<Attribute>(4);
        intelligence = GetChild<Attribute>(5);
        actionPoints = GetChild<Attribute>(6);
    }
    protected void CreateNewAttributes()
    {
        sprite = new();
        hp = new();
        armour = new();
        strength = new();
        agility = new();
        intelligence = new();
        actionPoints = new();
    }
    public int getCurrentHP()//unnecesery if we create label locally
    {
        return hp.getcurrentHealth();
    }
    public int getMaxHP()
    {
        return hp.getmaxHealth();
    }
    public int[] getAttributes()
    {
        return new int[] { strength.getcurrentAttribute(), agility.getcurrentAttribute(), intelligence.getcurrentAttribute() };
    }
    protected int[] cardIDs;
    protected AnimatedSprite2D sprite;
    protected HP2 hp;
    protected Armour armour;
    protected Attribute strength, agility, intelligence, actionPoints;
    protected List<Effects> effects = new(0);
    private ProgressBar hpBar=new();
}
