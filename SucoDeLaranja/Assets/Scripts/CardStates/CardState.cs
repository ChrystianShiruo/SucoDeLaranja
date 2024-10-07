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
    
    public virtual void Execute() {
    }

    public virtual void Enter(Card card, ICardState oldState) {
        _card = card;
        Debug.Log($"Enter {this}");
    }

    public virtual void Exit() {
        Debug.Log($"Exit {this}");
    }
}

public interface ICardState {

    void Execute();// TODO: change to coroutine?
    void Enter(Card card, ICardState oldState);
    void Exit();
}