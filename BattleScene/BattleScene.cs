using Godot;
using System;
using System.Collections.Generic;

public partial class BattleScene : Node2D
{
    public override void _Ready()
    {
        drawPile = GetChild<CardDeck>(0);
        //Add all card from deck (inventory) to draw pile

        drawPile.AddCardById(1);
        drawPile.AddCardById(2);
        drawPile.AddCardById(2);
        drawPile.AddCardById(2);
        drawPile.AddCardById(1);
        drawPile.AddCardById(1);

        hand = GetChild<CardDeck>(1);
        discardPile = GetChild<CardDeck>(2);
        NTB = GetChild<Button>(3);
        turnCounter= GetChild<TurnCounter>(4);
        //RCB\\
        RCB = GetChild<ReturnCardButton>(5);
        RCB.ButtonUp += ReturnCardInUse;
        RCB.Visible = false;
        //EB\\
        EB = GetChild<EnemyButton>(6);
        EB.ButtonUp += ChooseEnemy;
        EB.Visible = false;
        //ENEMY\\
        enemy = GetChild<BaseEnemy>(7);
        enemy.CharacterDied += DeathsignalReceiver;

        //PLAYER\\
        player = GetChild<BaseEnemy>(8);
        player.CharacterDied += DeathsignalReceiver;

        turnStart = true;
        turnCounter.AddToCounter();

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
        if (enemyTurn)
        {
            
            
            GD.Print("TURA WROGA panstwa podziemnego");
            GD.Print(enemy.ChooseCardToPlayC().Stats.ID);
            player.ReceiveCardPlayedByOpponet(enemy.ChooseCardToPlayC(), enemy.getAttributes());
            turnStart = true;
            turnCounter.AddToCounter(); //maybe moove to if(turnstart)
            enemyTurn = false;
        }//placeholder

        //jezeli nacisniemy guzik konczy sie tura, wszystkie karty ida do DSP

    }
    private void OnButtonClick()  //next turn\\
    {
        hand.RemoveChildren();
        while (!hand.IsEmpty())
        {
            discardPile.AddCard(hand.TransferCard(hand.getCards().Count - 1));
        }
        hand.EnableButtons();
        enemyTurn = true; //moves on to enemy turn    
    }
    public void setCardInUse(BaseCard cardInUse)
    {
        //creates card to be displayed\\
        this.cardInUse = cardInUse;
        AddChild(cardInUse);
        cardInUse.SetGlobalPosition(new Vector2((1152 / 2) - 50, 100));
        
        RCB.Visible = true; //sets the return button to visible so it can be clicked\\
        NTB.Visible = false;
        EB.Visible = true;
        //savings buttons to buffer and turning them off\\
        handButtonBuffer=hand.getCurrentActiveButtons();
        hand.DisableButtons();

    }
    private void ReturnCardInUse()
    {
        hand.RemoveChildren();
        
        hand.AddCardById(cardInUse.ID);

        RemoveChild(cardInUse);

        RCB.Visible=false;
        NTB.Visible = true;
        EB.Visible=false;

        GD.Print("HAND HAS" + hand.getCards().Count);
        //turning the buttons back on\\
        hand.ActivateSelectButtons(handButtonBuffer);
        hand.SetLastButtonVisible();
        hand.ShowCards(hand.getCards());
    }
    private void ChooseEnemy()
    {
        enemy.ReceiveCardPlayedByOpponet(cardInUse, new int[] {1,0,0});//placeholder
        
        discardPile.AddCard(cardInUse);
        RemoveChild(cardInUse);
        RCB.Visible = false;
        NTB.Visible = true;
        EB.Visible = false;
        
        GD.Print("HAND HAS" + hand.getCards().Count);
        //turning the buttons back on\\
        hand.ActivateSelectButtons(handButtonBuffer);
    }
    public void DeathsignalReceiver()
    {
        GD.Print("Zostalo mi odebrane jedyne co dla mnie istotne");
    }

    CardDeck drawPile, hand, discardPile;
    Button NTB;
    ReturnCardButton RCB;
    EnemyButton EB;
    TurnCounter turnCounter;
    private BaseCard cardInUse;
    private bool turnStart=true, enemyTurn;
    private BaseEnemy enemy, player;
    private int[] handButtonBuffer;
}
