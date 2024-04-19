using Godot;

public partial class Attribute : Node2D
{

    public override void _Ready()
    {
        baseAttribute = startingAttribute;
        currentAttribute = startingAttribute;
        if (attributeName == AttributeNames.ActionPoints)   attributeRegeneration = 3;
    }
    [Export]
    public int StartingAttribute { get => startingAttribute; set => startingAttribute = value;  }
    [Export]
    public AttributeNames type { get=>attributeName; set => attributeName = value; }
    [Export]
    public int AttributeRegeneration { get => attributeRegeneration; set => attributeRegeneration = value; }
    public void AddBaseAttribute(int attributeIncrease) { baseAttribute += attributeIncrease; }
    public void DecreaseBaseAttribute(int attributeDecrease) { if (baseAttribute - attributeDecrease < 0) baseAttribute = 0; else baseAttribute -= attributeDecrease; }
    public void BuffAttribute(int buff) {
        if (overflow > buff)
        {
            overflow -= buff;
        }
        else
        {
            currentAttribute += buff-overflow;
            overflow = 0;
        }
    }
    public void DebuffAttribute(int debuff) {
        if (debuff > currentAttribute)
        {
            currentAttribute = 0;
            overflow += debuff - currentAttribute;
        }
        else currentAttribute -= debuff;
    }
    public void ChangeAttributeRegeneration(int change) { attributeRegeneration += change; }
    public void RestoreAttribute() { currentAttribute = baseAttribute; } //restores current attribute value to default
    public void RegenerateAttribute() { currentAttribute += attributeRegeneration; } //changes current attribute value by a set amount at the start of a turn
    
    //######    getters and setters    #####\\
    public int getattributeregeneration() { return attributeRegeneration; }
    public int getcurrentAttribute() { return currentAttribute; }
    public int getbaseAttribute() { return baseAttribute; }
    public string getattributeName() { return attributeName.ToString(); }
    public int getattributeNameValue() { return attributeName.GetHashCode(); }

    //######    data members    ######\\
    private int currentAttribute;
    private int baseAttribute;
    private int startingAttribute=1;
    private int attributeRegeneration = 0;  //used to add atribute points at the start of a turn
    private int overflow = 0;               //dodatkowa zmienna zapisujaca na minusie
    private AttributeNames attributeName;
    public enum AttributeNames
    {
        None = 0,
        Agility = 1,
        Strength = 2,
        Intelligence = 3,
        ActionPoints= 4
    }
}
