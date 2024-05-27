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
        AddChild(graphicsControl);

        //bubble with attack value\\
        graphicsControl.AddChild(new TextureRect());
        graphicsControl.GetChild<TextureRect>(0).Texture= ResourceLoader.Load<Texture2D>("Cards/Resources/CardAttackImg.png");
        graphicsControl.GetChild<TextureRect>(0).ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        graphicsControl.GetChild<TextureRect>(0).Size=new Vector2(40,40);
        graphicsControl.GetChild<TextureRect>(0).AddChild(new Label());
        graphicsControl.GetChild<TextureRect>(0).GetChild<Label>(0).Text = Stats.AttackValue.ToString();
        graphicsControl.GetChild<TextureRect>(0).GetChild<Label>(0).Position = new Vector2(15, 10);

        //scroll with name\\
        graphicsControl.AddChild(new TextureRect());
        graphicsControl.GetChild<TextureRect>(1).Texture = ResourceLoader.Load<Texture2D>("Cards/Resources/CardNameImg.png");
        graphicsControl.GetChild<TextureRect>(1).ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        graphicsControl.GetChild<TextureRect>(1).Size = new Vector2(100, 40);
        graphicsControl.GetChild<TextureRect>(1).Position = new Vector2(0, 100); //where the name is on the card
        graphicsControl.GetChild<TextureRect>(1).AddChild(new Label());
        graphicsControl.GetChild<TextureRect>(1).GetChild<Label>(0).Text = Stats.Name;
        graphicsControl.GetChild<TextureRect>(1).GetChild<Label>(0).Position = new Vector2(0, 10);
    }
    public void Update()
    {
        if (!Stats.ID.Equals(id))
        {
            try { Stats = GD.Load<CardResource>("res://Cards/" + id.ToString() + ".tres"); }
            catch (IOException) { GD.Print("no card with given id"); id = Stats.ID; }
        }
    }
    public void UpdateVisibleStats(int scaledAttackValue)
    {
        graphicsControl.GetChild<TextureRect>(0).GetChild<Label>(0).Text = scaledAttackValue.ToString();
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
    public Vector2 GetGlobalPosition() { return globalPosition; }
    public void CreateTexture() { }//
    private int id=0;
    private bool debug = false;
    private TextureRect textureRect = new();
    private Control graphicsControl = new();
    Vector2 globalPosition;
}
