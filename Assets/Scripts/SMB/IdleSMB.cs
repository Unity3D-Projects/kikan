﻿using UnityEngine;
using System.Collections;

public class IdleSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null)
      _rigidState = animator.GetComponent<RigidState>();

    Debug.Log("idle");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
    bool OnlyUpKeyDown    = Input.GetKey(KeyCode.UpArrow)    && !Input.GetKey(KeyCode.DownArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool LieDownFlag = OnlyDownKeyDown && !_rigidState.LadderTopEdge;
    bool ClimbFlag = (OnlyUpKeyDown && !_rigidState.LadderTopEdge) ||
                     (OnlyDownKeyDown && !_rigidState.LadderBottomEdge);

    if (_rigidState.Ladder) {
      if (ClimbFlag) { ActTransition("Climb", animator); return; }
    }

    if (_rigidState.Ground) {
      if (OnlyLeftKeyDown || OnlyRightKeyDown) { ActTransition("Walk", animator);       return; }
      if (LieDownFlag)                         { ActTransition("LieDown", animator);    return; }
      if (JumpButtonDown)                      { ActTransition("GroundJump", animator); return; }
    }

    if (_rigidState.Air) {
      ActTransition("Fall", animator); return;
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Idle", false);
  }

  private RigidState _rigidState;
}
