using System;
using System.Collections;
using UnityEngine;

public class CardStateFacingDown : CardState {

    public override IEnumerator Execute() {
        //_card.HideCard();

        Type oldType = _oldState.GetType();
        Debug.Log(oldType);

        if(oldType == GetType()) {
            Debug.Log("FaceDownTrigger");
            _card.TriggerAnimation("FaceDownTrigger");
            //_card.HideCard();

        } else {
            //_card.HideCard();
        }

        yield return base.Execute();

        yield return new WaitUntil(() => _card.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.99f);

        _card.GetComponent<Collider2D>().enabled = true;
    }

    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.HideCard();

    }

}
