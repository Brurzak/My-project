// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.VeteransTactics
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class VeteransTactics : ISpellcraftSpell
{
  public const string CardId = "BG34_407t";

  public bool CanTargetMinion => false;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    List<Minion> source1 = source.ControlledByPlayer ? simulator.playerSide : simulator.opponentSide;
    int healthBuff = source1.Sum<Minion>((Func<Minion, int>) (x => !x.IsAlive() ? 0 : x.BonusKeywordCount()));
    foreach (Minion minion in source1)
      minion.IncreaseStats(2 * healthBuff, healthBuff);
  }
}
