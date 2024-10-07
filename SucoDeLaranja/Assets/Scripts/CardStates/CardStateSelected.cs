using System;
using UnityEngine;

public class CardStateSelected : CardState {
    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.GetComponent<Collider2D>().enabled = false;

        Type oldType = oldState.GetType();
        if(oldType == typeof(CardStateFacingDown)) {
            card.ShowCard();
        }


    }
}
