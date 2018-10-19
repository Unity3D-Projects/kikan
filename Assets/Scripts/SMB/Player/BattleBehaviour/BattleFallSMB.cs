﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleFallSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<BattlePlayer>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if (!_player.State.Rigor)
          AirMove();

        if ( _player.Hp.Cur <= 0         ) { _player.StateTransfer.TransitTo( "Die"        , animator ); return; }
        if ( _player.BuffState.Stun      ) { _player.StateTransfer.TransitTo( "Stun"       , animator ); return; }
        if ( ShouldTransitToSkill()      ) { _player.StateTransfer.TransitTo( "Skill"      , animator ); return; }
        if ( ShouldTransitToLadder()     ) { _player.StateTransfer.TransitTo( "Ladder"     , animator ); return; }
        if ( ShouldTransitToLieDown()    ) { _player.StateTransfer.TransitTo( "LieDown"    , animator ); return; }
        if ( ShouldTransitToGroundJump() ) { _player.StateTransfer.TransitTo( "GroundJump" , animator ); return; }
        if ( ShouldTransitToWalk()       ) { _player.StateTransfer.TransitTo( "Walk"       , animator ); return; }
        if ( ShouldTransitToIdle()       ) { _player.StateTransfer.TransitTo( "Idle"       , animator ); return; }
      }
    }

    private void AirMove() {
      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown) {
        _player.Movement.AirMoveLeft();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown) {
        _player.Movement.AirMoveRight();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }
    }

    private bool ShouldTransitToSkill() {
      bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Ctrl  ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Space ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Alt   ) == SkillState.Using );

      return SkillFlag;
    }

    private bool ShouldTransitToLadder() {
      bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      bool LadderFlag      = ( OnlyUpKeyDown   && !_player.State.LadderTopEdge    ) ||
                             ( OnlyDownKeyDown && !_player.State.LadderBottomEdge );

      return _player.State.Ladder && LadderFlag;
    }

    private bool ShouldTransitToWalk() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

      return _player.State.Ground && WalkFlag;
    }

    private bool ShouldTransitToLieDown() {
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      bool LieDownFlag     = OnlyDownKeyDown && !_player.State.LadderTopEdge;

      return _player.State.Ground && LieDownFlag;
    }

    private bool ShouldTransitToGroundJump() {
      return _player.State.Ground && Input.GetButton("Jump");
    }

    private bool ShouldTransitToIdle() {
      return _player.State.Ground;
    }

    private BattlePlayer _player;
  }
}

