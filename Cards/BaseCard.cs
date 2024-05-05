 using Godot;
using System.IO;


public partial class BaseCard : Node2D
{
    [Export]
    public CardResource Stats = new();
    [Export]
    public int ID { get => id; set => id = value; }
    public BaseCard() :this(0) { }
    public BaseCard(int id)
    {   
        this.id = id;
        if (!Stats.ID.Equals(id))
        {
            Stats = GD.Load<CardResource>("res://Cards/" + id.ToString() + ".tres");//path to resource, how to get it to always work? 
        }
    }
    public override void _Ready()
    {
        if (!Stats.ID.Equals(id))
        {
            Stats = GD.Load<CardResource>("res://Cards/" + id.ToString() + ".tres");//path to resource, how to get it to always work? 
        }
        if (debug)
        {
            GD.Print(Stats);
        }
        AddChild(textureRect);
        textureRect.Texture = ResourceLoader.Load<Texture2D>(Stats.SpritePath);
        button.Size = new Vector2(100,100);
    }
    public void Update()
    {
        if (!Stats.ID.Equals(id))
        {
            try { Stats = GD.Load<CardResource>("res://Cards/" + id.ToString() + ".tres"); }
            catch (IOException) { GD.Print("no card with given id"); id = Stats.ID; }
        }
    }
    public void ToggleVisibility()
    {  
        Visible = !Visible;
    }
    public override void _Draw()
    {
        DrawSetTransform(GlobalPosition = globalPosition);
    }
    public void SetGlobalPosition(Vector2 position) { globalPosition = position; }
    public void CreateTexture() { }//
    private int id=0;
    private bool debug = false;
    private TextureRect textureRect= new();
    private Button button=new();
    Vector2 globalPosition;
}
