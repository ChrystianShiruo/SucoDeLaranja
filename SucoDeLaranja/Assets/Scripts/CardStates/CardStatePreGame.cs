using UnityEngine;
using System.Collections;

public class CardStatePreGame : CardState {


    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.GetComponent<Collider2D>().enabled = false;
    }

    public override void Exit() {
    }
}
