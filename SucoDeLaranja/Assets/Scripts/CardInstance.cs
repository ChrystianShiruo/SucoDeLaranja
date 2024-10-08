using System;

[System.Serializable]
public class CardInstance {

    public CardData cardData;
    public CardState state;


    public CardInstance(CardData data) {
        this.cardData = data;

        state = state == null ? new CardStatePreGame() : (CardState)Utils.CreateNewInstance(state.StateName);
        //state = new CardStatePreGame();
    }
    public CardInstance(CardData data, Type stateType) {
        UnityEngine.Debug.Log("loaded CardInstance!!");

        this.cardData = data;
        state = (CardState)Utils.CreateNewInstance(stateType);
    }
}