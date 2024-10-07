using System;
using System.Collections;
using UnityEngine;

public class CardStateFacingDown : CardState {

    public override IEnumerator Execute() {
        yield return base.Execute();
        //_card.HideCard();

        Type oldType = _oldState.GetType();
        _card.HideCard();
        //if(oldType == typeof(CardStatePreGame)) {
        //    _card.HideCard();
        //} else if(oldType == typeof(CardStateSelected)) {
        //    yield return new WaitUntil(() => _card.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f);
        //    _card.HideCard();
        //}
        yield return new WaitUntil(() => _card.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f);

        _card.GetComponent<Collider2D>().enabled = true;
    }

    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        //_card.HideCard();

    }

    public override void Exit() {
    }
}
