[System.Serializable]
public class CardInstance {

    public CardData cardData;
    public CardState state;

    /// <summary>
    /// FacingDown:
    /// </summary>
    

    public CardInstance(CardData data) {
        this.cardData = data;
        state = new CardStatePreGame();
    }
}