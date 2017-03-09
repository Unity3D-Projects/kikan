﻿using UnityEngine;
using System.Collections;

public class ManjiShift : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D collider) {
    var target = collider.gameObject;

    if (target.tag == "Enemy") {
      // damage = _atk * _power;
      target.GetComponent<HealthSystem>().IsDamaged(20);
      target.GetComponent<Enemy>().ShowHealthBar();
    }
  }

  [SerializeField] private BoxCollider2D _collider;
}

