using Godot;
using System;
using System.Collections.Generic;

public partial class BattleScene : Node2D
{
    public override void _Ready()
    {
        drawPile = GetChild<CardDeck>(1);
        //Add all card from deck (inventory) to draw pile

        drawPile.AddCardById(1);
        drawPile.AddCardById(2);
        drawPile.AddCardById(2);
        drawPile.AddCardById(2);
        drawPile.AddCardById(1);
        drawPile.AddCardById(1);

        hand = GetChild<CardDeck>(2);
        discardPile = GetChild<CardDeck>(3);
        NTB = GetChild<Button>(4);
        turnCounter= GetChild<TurnCounter>(5);
        //RCB\\
        RCB = GetChild<ReturnCardButton>(6);
        RCB.ButtonUp += ReturnCardInUse;
        RCB.Visible = false;
        //EB\\
        EB = GetChild<EnemyButton>(7);
        EB.ButtonUp += ChooseEnemy;
        EB.Visible = false;
        //ENEMY\\
        enemy = GetChild<BaseEnemy>(8);
        enemy.CharacterDied += DeathsignalReceiverEnemy;

        //PLAYER\\
        player = GetChild<BaseEnemy>(9);
        player.CharacterDied += DeathsignalReceiverPlayer;

        turnStart = true;
        turnCounter.AddToCounter();

    }
    public override void _Process(double delta)
    {
        
        if (turnStart)
        {
            DrawCards();
            hand.ShowCards(hand.getCards());
            turnStart = false;
        }
        if (enemyTurn)
        {
            EnemyTurn();           
        }
        if (Input.IsActionJustPressed("LeftMouseClick") && turnEnd)
        {

            RemoveChild(cardInUse);
            player.ReceiveCardPlayedByOpponet(cardInUse, enemy.getAttributes());
            NTB.Visible = true;
            turnEnd = false;
            turnStart = true;
        };//Waits until player is able to read text on the card played by enemy
        //jezeli nacisniemy guzik konczy sie tura, wszystkie karty ida do DSP

    }
    private void EnemyTurn()
    {
        BaseCard temp = enemy.ChooseCardToPlayC();
        this.cardInUse = temp;
        AddChild(cardInUse);
        int damage =
             (int)(temp.Stats.AttackValue * temp.Stats.StrengthScaling * enemy.getAttributes()[0]) +
            +(int)(temp.Stats.AttackValue * temp.Stats.AgilityScaling * enemy.getAttributes()[1]) +
            +(int)(temp.Stats.AttackValue * temp.Stats.IntelligenceScaling * enemy.getAttributes()[2]);
        cardInUse.UpdateVisibleStats(damage);
        cardInUse.SetGlobalPosition(new Vector2((1152 / 2) - 50, 100));
        NTB.Visible=false;       

        
        turnCounter.AddToCounter(); //maybe moove to if(turnstart)
        enemyTurn = false;
        turnEnd= true;
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
    private void DrawCards()
    {
        for (int i = 0; i < 5; i++)         //nie zakladamy ze gracz bedzie mial mniej niz 5 kart
        {
            if (!drawPile.IsEmpty())
            {
                hand.AddCard(drawPile.TransferCard(drawPile.getCards().Count - 1));
            }
            else
            {
                while (!discardPile.IsEmpty())
                {
                    drawPile.AddCard(discardPile.TransferCard(discardPile.getCards().Count - 1));
                }
                drawPile.Shuffle(100);
                hand.AddCard(drawPile.TransferCard(drawPile.getCards().Count - 1));
            }
        }
    }
    public void DeathsignalReceiverPlayer()
    {
        GD.Print("Zostalo mi odebrane jedyne co dla mnie istotne");
        GetTree().ChangeSceneToFile("res://GameOverScene/GameOverScene.tscn");
    }
    public void DeathsignalReceiverEnemy()
    {
        GD.Print("Zostalo mi odebrane jedyne co dla mnie istotne");
        GetTree().ChangeSceneToFile("res://BattleVictory/BattleVictory.tscn");
    }

    CardDeck drawPile, hand, discardPile;
    Button NTB;
    ReturnCardButton RCB;
    EnemyButton EB;
    TurnCounter turnCounter;
    private BaseCard cardInUse;
    private bool turnStart=true, enemyTurn, turnEnd=false;
    private BaseEnemy enemy, player;
    private int[] handButtonBuffer;
}
