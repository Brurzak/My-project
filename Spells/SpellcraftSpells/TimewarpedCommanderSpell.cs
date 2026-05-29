// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.TimewarpedCommanderSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class TimewarpedCommanderSpell : ISpellcraftSpell
{
  public const string CardId = "BG34_Giant_210t";

  public bool CanTargetMinion => true;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    if (target == null || !target.IsAlive())
      return;
    int by = (source.ControlledByPlayer ? (IEnumerable<Minion>) simulator.playerSide : (IEnumerable<Minion>) simulator.opponentSide).Count<Minion>((Func<Minion, bool>) (x => x.IsNaga() && x.IsAlive())) * (golden ? 4 : 2);
    target.IncreaseStats(by);
  }
}
