using Godot;

public partial class Attribute : Node2D
{
    [Export]
    public int StartingAttribute
    {
        get => startingAttribute;
        private set
        {
            startingAttribute = value;
            if (StartingAttribute > startingAttribute)
            {
                StartingAttribute = startingAttribute;
            }
        }
    }
    public int CurrentAttribute
    {
        get => CurrentAttribute;
        private set
        {
            var previousAttribute = currentAttribute;
            currentAttribute = Mathf.Clamp(value, 0, StartingAttribute);
            var attributeUpdate = new AttributeUpdate
            {
                PreviousAttribute = previousAttribute,
                CurrentAttribute = currentAttribute,
                StartingAttribute = StartingAttribute,
                //IsHeal = previousHealth <= currentHealth
            };

        }
    }
    public int startingAttribute = 3;
    public int currentAttribute = 3;

    public override void _Ready()
    {
        CallDeferred(nameof(InitializeAttribute));

    }
    private void InitializeAttribute()
    {
        CurrentAttribute = StartingAttribute;
    }
    public void AddAttribute()
    {
        StartingAttribute++;
        //startingAttribute = StartingAttribute;
        CurrentAttribute = StartingAttribute;
        //currentAttribute = CurrentAttribute;
    }
    public void RemoveAttribute()
    {
        StartingAttribute--;
        //startingAttribute = StartingAttribute;
        CurrentAttribute = StartingAttribute;
        //currentAttribute = CurrentAttribute;
    }
    public void BuffAttribute()
    {
        currentAttribute++;
        //currentAttribute = CurrentAttribute;
    }
    public void DebuffAttribute()
    {
        currentAttribute--;
        //currentAttribute = CurrentAttribute;
    }
    public void RestoreAttribute()
    {
        CurrentAttribute = StartingAttribute;
        //currentAttribute = CurrentAttribute;
    }
    public int getAttribute(){return currentAttribute;}
    public partial class AttributeUpdate : RefCounted
    {
        public int StartingAttribute;
        public int CurrentAttribute;
        public int PreviousAttribute;
    }
    

	
}
