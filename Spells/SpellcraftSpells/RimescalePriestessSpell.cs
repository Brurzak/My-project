// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.RimescalePriestessSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class RimescalePriestessSpell : ISpellcraftSpell
{
  public const string CardId = "BG33_319t";

  public bool CanTargetMinion => false;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    if (!source.ControlledByPlayer)
    {
      List<Minion> opponentSide = simulator.opponentSide;
    }
    else
    {
      List<Minion> playerSide = simulator.playerSide;
    }
    source.AddSpellToFriendlyHand();
    if (!golden)
      return;
    source.AddSpellToFriendlyHand();
  }
}
