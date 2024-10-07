
using System.Collections;
using UnityEngine;
/* States:
 *  PreGame //initial pre game state where all cards are facing up
 *  FacingDown
 *  Selected
 *  Paired
 */
public interface ICardState {

    void Execute();// TODO: change to coroutine?
    void Enter(Card card, ICardState oldState);
    void Exit();
}
public class CardState : ICardState {

    protected Card _card;
    
    public virtual void Execute() {
    }

    public virtual void Enter(Card card, ICardState oldState) {
        _card = card;
    }

    public virtual void Exit() {
    }
}

public class CardStatePreGame : CardState {

    public override void Execute() {
    }

    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.GetComponent<Collider2D>().enabled = false;
    }

    public override void Exit() {
    }
}

public class CardStateFacingDown : CardState {

    public override void Execute() {
    }

    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        if(oldState.GetType() == typeof(CardStatePreGame)) {
            card.HideCard();
        }
        _card.GetComponent<Collider2D>().enabled = true;
    }

    public override void Exit() {
    }
}

public class CardStateSelected : CardState {
    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.GetComponent<Collider2D>().enabled = false;
    }
}

public class CardStatePaired : CardState {
    public override void Enter(Card card, ICardState oldState) {
        base.Enter(card, oldState);
        _card.GetComponent<Collider2D>().enabled = false;
    }
}
