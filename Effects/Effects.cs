using Godot;
using System;

public abstract partial class Effects : Node2D
{
    protected Effects(int life, int namesHashcode, int value, string[] paths) {
        this.life = life;
        this.namesHashcode = namesHashcode;
        this.value = value;
        this.paths = paths;
        //Default constructor for every derivative class
    }
    public abstract void TurnBeginAction();
        //Action happening at the begining of a turn
    public abstract void TurnEndAction();
        //Action happening at the end of a turn
    public bool isAlive()
    {
        return life!=0;
    }

    protected int life; //number of turn the effect is alive
    protected int value;
    protected string[] paths;
    protected int namesHashcode;
    protected enum Names
    {
        None = 0
    }

}
