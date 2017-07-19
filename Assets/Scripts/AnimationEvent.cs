﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {
  public void OnAnimationFinishDestroy() {
    if (PhotonNetwork.player.IsMasterClient)
      PhotonNetwork.Destroy(gameObject);
  }
}

