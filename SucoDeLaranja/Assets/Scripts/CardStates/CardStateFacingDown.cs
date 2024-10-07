using System;
using UnityEngine;

public class CardStateFacingDown : CardState {

    public override void Execute() {
    }

    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);

        Type oldType = oldState.GetType();
        if(oldType == typeof(CardStatePreGame)) {
            card.HideCard();
        }else if(oldType == typeof(CardStateSelected)) {
            card.HideCard();
        }


            _card.GetComponent<Collider2D>().enabled = true;
    }

    public override void Exit() {
    }
}
