// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.ArcaneCannoneer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class ArcaneCannoneer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAttack,
  IEntity
{
  public const string CardId = "BG31_928";
  public const string Text = "Whenever this attacks, deal {1} damage to the target. <i>(Improved by every 4 spells you've cast this game!)</i> 4[x]Whenever this attacks, deal {1} damage to the target. <i>(Cast {2}/4 spells to improve!)</i>";
  public const string GoldenText = "Whenever this attacks, deal {1} damage to the target. <i>(Improved by every 4 spells you've cast this game!)</i> 4[x]Whenever this attacks, deal {1} damage to the target. <i>(Cast {2}/4 spells to improve!)</i>";

  public Action? OnAttack(Minion target)
  {
    return (Action) (() => this.Simulator.ProcessDamage((IEnumerable<Damage>) new Damage[1]
    {
      new Damage(this.ScriptDataNum2, target, (Entity) this)
    }));
  }
}
