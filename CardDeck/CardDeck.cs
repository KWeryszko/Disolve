using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

public partial class CardDeck : Node2D
{
    [Signal]
    public delegate int CardClickedEventHandler(); //returns clicked card id\\
    public CardDeck() { }
    public CardDeck(int[] cardIDs)
    {
        foreach (var id in cardIDs)
        {
            cards.Add(new BaseCard(id)); //or use AddCard function
        }
    }
    public CardDeck(BaseCard[] cards)
    {
        foreach (var card in cards)
        {
            this.cards.Add(card); //or use AddCard function
        }
    }
    public override void _Ready()
    {
        //Creates buttons and connects signals\\
        funkcje.Add(Card1ButtonClick);
        funkcje.Add(Card2ButtonClick);
        funkcje.Add(Card3ButtonClick);
        funkcje.Add(Card4ButtonClick);
        funkcje.Add(Card5ButtonClick);
        for (int i = 0; i < 5;i++) { buttons.Add(new CardButton()); buttons[i].ButtonUp += funkcje[i]; }
    }
    public void AddCardById(int id)
    {
        cards.Add(new BaseCard(id));
        if (debug) { GD.Print("dodano karte"); }
    }
    public void AddCard(BaseCard card)
    {
        cards.Add(card);
        
    }
    public void RemoveCard(int index) 
    {
        try
        {
            cards.RemoveAt(index);
        }
        catch (IndexOutOfRangeException)
        {
            
            GD.Print("IndexOutOfRange");
        }
        if (debug) { GD.Print("usunieto karte"); }
    }
    public void ChooseCardUpgrade(int upgradeableCardIndex)
    {
        List<BaseCard> tempCards = new();
        foreach (int id in cards[upgradeableCardIndex].Stats.NextCardsID)
        {
            tempCards.Add(new BaseCard(id));
        }
        var upgradeID = tempCards[ShowCards(tempCards)].Stats.ID;
        UpgradeCard(upgradeableCardIndex, upgradeID);
        //input function? ex. Create a new list of Cards consisting of possible upgrades and show them to the player once upgrade is chosen the card updates to it
    }
    public int ShowCards(List<BaseCard> cards, int verticalOffset = 0) //return index number of chosen card // now with temporary code! // VO - number of pixels from the bottom
    {
        int verticalPosition = 648 - 150 - verticalOffset;
        List<Vector2> positions = new List<Vector2>(0);
        childPaths = new string[cards.Count];

        switch (cards.Count)
        {   
            case 0:
                break;
            case 1:                //display 1     [       []       ]
                positions.Add(new Vector2((1152 / 2) - 50, verticalPosition)); //648 //1152 to akurat rozmiar okienka      
                break;
            case 2:                //display 2     [      [][]      ]
                positions.Add(new Vector2((1152 / 2) - 100, verticalPosition));
                positions.Add(new Vector2((1152 / 2), verticalPosition));
                break;
            case 3:                //display 3     [     [][][]     ]
                positions.Add(new Vector2((1152 / 2) - 150, verticalPosition));
                positions.Add(new Vector2((1152 / 2) - 50, verticalPosition));
                positions.Add(new Vector2((1152 / 2) + 50, verticalPosition));
                break;
            case 4:                //display 4     [    [][][][]    ]
                positions.Add(new Vector2((1152 / 2) - 200, verticalPosition));
                positions.Add(new Vector2((1152 / 2) - 100, verticalPosition));
                positions.Add(new Vector2((1152 / 2), verticalPosition));
                positions.Add(new Vector2((1152 / 2) + 100, verticalPosition));
                break;
            case 5:                //display 5     [   [][][][][]   ]
                positions.Add(new Vector2((1152 / 2) - 250, verticalPosition));
                positions.Add(new Vector2((1152 / 2) - 150, verticalPosition));
                positions.Add(new Vector2((1152 / 2) - 50, verticalPosition));
                positions.Add(new Vector2((1152 / 2) + 50, verticalPosition));
                positions.Add(new Vector2((1152 / 2) + 150, verticalPosition));
                break;
            default:
                throw new Exception("CardCountOverflowException");

        }
        for (int i = 0; i < cards.Count; i++)
        {
            AddChild(cards[i]);
            childPaths[i] = GetPathTo(cards[i]);
            cards[i].SetGlobalPosition(positions[i]);

            //adding and setting the position of buttons\\
            if (buttons[i].GetParent() == null) { cards[i].AddChild(buttons[i]); }
            else { buttons[i].Reparent(cards[i]); }
            buttons[i].SetGlobalPosition(cards[i].GetGlobalPosition());
            buttons[i].setParentCardIndex(i);
            
            GD.Print(cards[i].ID);
        }
        

        return 0;
    }
    public void Card1ButtonClick()    {        CardButtonClick(0);    }
    public void Card2ButtonClick()    {        CardButtonClick(1);    }
    public void Card3ButtonClick()    {        CardButtonClick(2);    }
    public void Card4ButtonClick()    {        CardButtonClick(3);    }
    public void Card5ButtonClick()    {        CardButtonClick(4);    }
    private void CardButtonClick(int indx) {
        RemoveChildren();
        //GetParent<BattleScene>().setCardInUse(cards[indx]);
        GetNode<BattleScene>(GetPathTo(GetParent())).transfertodp(TransferCard(indx)); //do poprawy
        
        ShowCards(cards);
        buttons[getCards().Count].Visible=false;
    }
    public void DisableButtons()
    {
        foreach (var button in buttons)
        {
            button.Visible = false;
        }

    }//turns buttons invisible\\
    public void EnableButtons()
    {
        foreach (var button in buttons)
        {
            button.Visible = true;
        }
    }//turns buttons visible\\
    public void Shuffle(int count)
    {
        Random rand = new Random();
        int indx1, indx2;
        BaseCard temp;
        for (int i = 0; i < count; i++)
        {
            indx1=rand.Next(cards.Count);
            indx2=rand.Next(cards.Count);

            temp = cards[indx1];
            cards[indx1] = cards[indx2];
            cards[indx2] = temp;
        }
    }
    private void UpgradeCard(int cardIndex, int upgradedCardID)
    {
        try
        {
            cards[cardIndex].ID = upgradedCardID;
            cards[cardIndex].Update();
        }
        catch (IndexOutOfRangeException)
        { 
            GD.Print("index out of bounds");
        }
    }
    public BaseCard TransferCard(int index)
    {
        BaseCard tempCard=GetCard(index);
        RemoveCard(index);
        return tempCard;
    }
    public int TransferCardById(int index)
    {
        BaseCard tempCard = GetCard(index);
        RemoveCard(index);
        return tempCard.ID;
    }
    public List<BaseCard> getCards()
    {
        return cards;
    }
    public void RemoveChildren()    { 
        foreach (var path in childPaths)
        {
            RemoveChild(GetNode(path)); 
        }   
    }
    public bool IsEmpty() {  return cards.Count == 0; }
    public BaseCard GetCard(int index)
    {
        BaseCard tempCard=new BaseCard();
        try
        {
            tempCard = cards[index];
            return tempCard;
        }catch (IndexOutOfRangeException)
        { 
            GD.Print("IndexOutOfRange"); 
            return tempCard;
        }
    }
    private List<BaseCard> cards=new(0);
    List<CardButton> buttons = new List<CardButton>(0);
    List<Action> funkcje=new List<Action>(0);
    private bool debug = true;
    private string[] childPaths;
}
