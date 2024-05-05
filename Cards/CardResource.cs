using Godot;
using System;
[GlobalClass]
public partial class CardResource : Resource
{   
    [Export]
    public int ID { get => id; set => id = value; }

    //Base Card Stats\\
    [Export]
    public int AttackValue { get => attackValue; set => attackValue = value; }
    [Export]
    public int ArmourValue { get => armourValue; set => armourValue = value; }
    [Export]
    public int HealValue { get => healValue; set => healValue = value; }
    [Export]
    public int ApCost { get => apCost; set => apCost = value; }
    [Export]
    public string Name { get => name; set => name = value; }

    //Attribute Scaling\\
    [Export]
    public double AgilityScaling { get => agilityScaling; set => agilityScaling = value; }
    [Export]
    public double StrengthScaling {  get => strengthScaling; set => strengthScaling = value; }
    [Export]
    public double IntelligenceScaling {  get => intelligenceScaling; set => intelligenceScaling = value; }
    //Upgrade Path\\
    [Export]
    public int[] NextCardsID { get => nextCardsID; set => nextCardsID = value; }

    //Special Effects\\
    [Export]
    public int[] SpecialEffectsID { get => specialEffectsID; set => specialEffectsID = value; }
    [Export]
    public int[] SpecialEffectsValue { get => specialEffectsValue; set => specialEffectsValue = value; }
    [Export]
    public int[] SpecialEffectsLife { get => specialEffectsLife; set => specialEffectsLife = value; }
    //id , attVal, armVal, healVal, apCost, name, aglSc, strSc, intSc, nxtC, SpEfID, SpEfVal, SpEfLfe
    public CardResource() : this(0, 0, 0, 0, 0, null, 0, 0, 0, null, null, null, null) { }
    [Export]
    public string SpritePath { get => spritePath; set => spritePath = value; }
    public CardResource(int id, int attackValue, int armourValue, int healValue, int apCost, string name, double agilityScaling, double strengthScaling, double intelligenceScaling, int[] nextCardsID, int[] specialEffectsID, int[] specialEffectsValue, int[] specialEffectsLife)
    {
        ID = id;
        AttackValue = attackValue;
        ArmourValue = armourValue;
        HealValue = healValue;
        ApCost = apCost;
        Name = name;
        AgilityScaling = agilityScaling;
        StrengthScaling = strengthScaling;
        IntelligenceScaling = intelligenceScaling;
        NextCardsID = nextCardsID;
        SpecialEffectsID = specialEffectsID;
        SpecialEffectsValue = specialEffectsValue;
        SpecialEffectsLife = specialEffectsLife;
        
        
    }
    public override string ToString()
    {
        return "id " + id + "\nattackValue " + attackValue + "\narmourValue " + armourValue + "\nhealValue " + healValue + "\napCost " + apCost +
            "\nname " + name + "\nagilityScaling " + agilityScaling + "\nstrengthScaling " + strengthScaling + "\nintelligenceScaling " +
            intelligenceScaling + "\nnextCardsID " + nextCardsID + "\nspecialEffectsID " + specialEffectsID + "\nspecialEffectsValue " +
            specialEffectsValue + "\nspecialEffectsLife " + specialEffectsLife;
    }

    private int id;
    //Base Card Stats\\
    private int attackValue, armourValue, healValue, apCost;
    private string name;

    //Attribute Scaling\\
    private double agilityScaling, strengthScaling, intelligenceScaling;

    //Upgrade Path\\
    private int[] nextCardsID;    

    //Special Effects\\
    //private List<Effects> specialEffects = new List<Effects>(0);  // can be made into separate resource in Effects folder
    private int[] specialEffectsID, specialEffectsValue, specialEffectsLife;

    //Card Texture\\
    private string spritePath;
}