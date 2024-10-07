using System;
using UnityEngine;
using System.Collections;
/* States:
 *  PreGame //initial pre game state where all cards are facing up
 *  FacingDown
 *  Selected
 *  Paired
 */
public class CardState : ICardState {

    protected Card _card;
    protected ICardState _oldState;


    public virtual IEnumerator Execute() {        

        yield return null;
    }

    public virtual void Enter(Card card, ICardState oldState) {
        _card = card;
        _oldState = oldState;
        Debug.Log($"Enter {this}");
    }

    public virtual void Exit() {
        Debug.Log($"Exit {this}");
    }
}

public interface ICardState {

    IEnumerator Execute();
    void Enter(Card card, ICardState oldState);
    void Exit();
}