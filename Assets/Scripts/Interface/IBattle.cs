﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBattle {
    PlayerHp Hp { get; }
    int KillExp { get; }
    int KillGold { get; }
  }
}
