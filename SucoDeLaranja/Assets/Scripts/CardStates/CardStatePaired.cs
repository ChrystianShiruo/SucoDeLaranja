using UnityEngine;

public class CardStatePaired : CardState {
    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.GetComponent<Collider2D>().enabled = false;
        card.ShowCard();

    }
}
