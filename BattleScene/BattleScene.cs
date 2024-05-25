using Godot;
using System;

public partial class BattleScene : Node2D
{
    public override void _Ready()
    {
        drawPile = GetChild<CardDeck>(0);
        //Add all card from deck (inventory) to draw pile

        drawPile.AddCardById(1);
        drawPile.AddCardById(1);
        drawPile.AddCardById(1);
        drawPile.AddCardById(1);
        drawPile.AddCardById(1);
        drawPile.AddCardById(1);

        hand = GetChild<CardDeck>(1);
        discardPile = GetChild<CardDeck>(2);
        NTB = GetChild<Button>(3);
        turnCounter= GetChild<TurnCounter>(4);
       
        turnStart = true;
        turnCounter.AddToCounter();

        cardInUse = new BaseCard(1);  //Setting up card to be used\\
        AddChild(cardInUse);
        cardInUse.SetGlobalPosition(new Vector2((1152 / 2) - 50, 100));
        cardInUse.Visible = false;
    }
    public override void _Process(double delta)
    {
        
        if (turnStart)
        {
            for (int i = 0; i < 5; i++)         //nie zakladamy ze gracz bedzie mial mniej niz 5 kart
            {
                if (!drawPile.IsEmpty())
                {
                    hand.AddCard(drawPile.TransferCard(drawPile.getCards().Count-1));
                }
                else 
                {
                    while (!discardPile.IsEmpty())
                    {
                        drawPile.AddCard(discardPile.TransferCard(discardPile.getCards().Count-1));
                    }
                    drawPile.Shuffle(100);
                    hand.AddCard(drawPile.TransferCard(drawPile.getCards().Count - 1));
                }
            }
            hand.ShowCards(hand.getCards());
            turnStart = false;
        }
        //jezeli nacisniemy guzik konczy sie tura, wszystkie karty ida do DSP
        
    }
    private void OnButtonClick()
    {
        hand.RemoveChildren();
        while (!hand.IsEmpty())
        {
            discardPile.AddCard(hand.TransferCard(hand.getCards().Count - 1));
        }
        hand.EnableButtons();
        turnStart = true;
        turnCounter.AddToCounter();

    }
    public void transfertodp(BaseCard card)
    {
        setCardInUse(card);
        discardPile.AddCard(card);
    }
    public void setCardInUse(BaseCard cardInUse)
    {
        this.cardInUse.Visible = true;
        this.cardInUse = cardInUse;
        hand.DisableButtons();
    }
    private void TransferCIUToDiscard()
    {
        discardPile.AddCard(cardInUse);
        cardInUse.Visible = false;
    }
    CardDeck drawPile, hand, discardPile;
    Button NTB;
    TurnCounter turnCounter;
    private BaseCard cardInUse;
    private bool turnStart=true;
}
