// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.DarkcrestStrategistSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class DarkcrestStrategistSpell : ISpellcraftSpell
{
  public const string CardId = "BG31_920t";

  public bool CanTargetMinion => false;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    List<Card> list = source.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(source.ControlledByPlayer, (Race) 92).Where<Card>((Func<Card, bool>) (x => x.TechLevel == 1)).ToList<Card>();
    int num = golden ? 2 : 1;
    for (int index = 0; index < num; ++index)
    {
      Card card;
      if (!list.TryGetRandom<Card>(out card))
        source.AddMinionToFriendlyHand();
      else
        source.AddMinionToFriendlyHand(card.Id);
    }
  }
}
