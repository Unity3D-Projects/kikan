﻿using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (!photonView.isMine) {
      var target = collider.gameObject;

      if (target.tag == "Enemy") {
        int damage = status.atk + _power;
        target.GetComponent<EnemyHealth>().IsDamaged(damage);
        target.GetComponent<Enemy>().ShowHealthBar();
      }

      if (target.tag == "Player") {
        Debug.Log("OnTrigger");
        //int damage = status.atk + _power;
        var hs = target.GetComponent<PlayerHealth>();
        hs.IsDamaged(100);
      }
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
}

