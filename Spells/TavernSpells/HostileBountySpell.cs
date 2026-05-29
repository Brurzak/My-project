// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.HostileBountySpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class HostileBountySpell : ITavernSpell
{
  public const string CardId = "BG33_812";
  public const string Text = "Give three friendly minions +{0} Attack.4Give three friendly minions +{0}/+{1}.";

  public void Cast(Entity source, Simulator simulator, Minion? _)
  {
    int tavernSpellAtkBuff = (source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent).TavernSpellAtkBuff;
    foreach (Minion randomElement in source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().GetRandomElements<Minion>(3))
      randomElement.IncreaseStats(4 + tavernSpellAtkBuff, 0, source);
  }
}
