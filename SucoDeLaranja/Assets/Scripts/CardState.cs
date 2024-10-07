public class CardState {

    public CardData cardData;
    public State state;

    public enum State {
        Hidden,
        Selected,
        Paired,
        Showing
    }

    public CardState(CardData data) {
        this.cardData = data;
        state = State.Hidden;
    }
}