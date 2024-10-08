using System;

[System.Serializable]
public class CardInstance {

    public CardData cardData;
    public CardState state;


    public CardInstance(CardData data) {
        this.cardData = data;

        state = state == null ? new CardStatePreGame() : (CardState)Utils.CreateNewInstance(state.StateName);
    }
    public CardInstance(CardData data, Type stateType) {

        this.cardData = data;
        state = (CardState)Utils.CreateNewInstance(stateType);
    }
}