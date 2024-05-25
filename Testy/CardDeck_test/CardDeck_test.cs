using Godot;
using System;

public partial class CardDeck_test : Node2D
{
    public override void _Ready()
    {
        cardDeck = GetNode<CardDeck>(GetPathTo(GetChild(0)));
    }
    public override void _Process(double delta)
    {
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        
        if (Input.IsActionJustPressed("ui_left"))
        {
            cardDeck.RemoveCard(0);
        }
        else if (Input.IsActionJustPressed("ui_right"))
        {
            cardDeck.AddCardById(1);
        }
        if (Input.IsActionJustPressed("ui_up"))
        {
            cardDeck.ShowCards(cardDeck.getCards());
        }
        if (Input.IsActionJustPressed("ui_down"))
        {
            cardDeck.RemoveChildren();
        }

    }
    CardDeck cardDeck;
}
