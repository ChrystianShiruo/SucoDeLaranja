using System;
using UnityEngine;

public class CardStateSelected : CardState {

    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.GetComponent<Collider2D>().enabled = false;

        Type oldType = oldState.GetType();

        Debug.Log(oldType.ToString());

        if(oldType == typeof(CardStateFacingDown)) {
            Debug.Log("ShowCard");
            card.ShowCard();
        }
    }
}
