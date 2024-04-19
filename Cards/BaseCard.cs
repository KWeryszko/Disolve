using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class BaseCard : Node2D
{
    [Export]
    public int AttackValue { get => attackValue; set => attackValue = value; }
    [Export]
    public int ArmourValue { get => armourValue; set => armourValue = value; }
    [Export]
    public int HealValue { get => healValue; set => healValue = value; }
    [Export]
    public int ApCost { get => apCost; set => apCost = value; }
    [Export]
    public int ID { get => id; set => id = value; }
    [Export]
    public int[] NextCardsID { get => nextCardsID; set => nextCardsID = value; }
    [Export]
    public int[] SpecialEffectsID { get => specialEffectsID; set => specialEffectsID = value; }
    public void ExportCard()
    {
        
        String tempID;
        if (id < 10) tempID = "00"+id.ToString();
        else if (id <100) tempID = "0"+id.ToString();
        else tempID = id.ToString();
        try
        {
            StreamReader sr = new StreamReader("//" + tempID + "karta.txt");
            sr.Close();
            throw new Exception("Duplicate ID exception");        
        }
        catch (IOException e)
        {
            //Console.WriteLine(e.ToString());
            StreamWriter sw = new StreamWriter("//" + tempID + "karta.txt");
            WriteToFile(sw);
            sw.Close();
        }
        
    }
    private void WriteToFile(StreamWriter sw)
    {
        sw.WriteLine("cos");
        sw.Close();
    }
    public void ImportCard()
    {
        attackValue = 0;
        armourValue = 0;
        healValue = 0;
        apCost = 0;
        nextCardsID = new int[0];
        specialEffectsID = new int[0];
        //sprite=GetNode<Sprite2D>("path") or =new Sprite2D
    }
    protected int attackValue, armourValue, healValue, apCost, id; // string name?
    protected double AgilityScaling, StrengthScaling, IntelligenceScaling; //attribute scaling
    protected int[] nextCardsID;
    protected List<Effects> specialEffects = new List<Effects>(0);
    protected int[] specialEffectsID, specialEffectsValue, specialEffectLife;
    protected string[] specialEffectspath;

    Sprite2D sprite; //GetNode<Sprite2D>("path")
}
