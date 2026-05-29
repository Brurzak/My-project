// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.SeafloorRecruiter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells.TavernSpells;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class SeafloorRecruiter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_925";
  public const string Text = "<b>Rally:</b> Cast Chef's Choice on the minion to the right.";
  public const string GoldenText = "<b>Rally:</b> Cast Chef's Choice on the minion to the right twice.";
  private static ITavernSpell _chefsChoiceSpell = (ITavernSpell) new ChefsChoiceSpell();

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      Minion target1 = minion.IsAlive() ? minion.GetRightNeighbor() : minion.LastKnownNeighbors.Item2;
      if (target1 == null || !target1.IsAlive() || target1.IsNoType())
        return;
      this.Simulator.CastTavernSpell(SeafloorRecruiter._chefsChoiceSpell, (Entity) this, target1);
      if (!this.golden)
        return;
      this.Simulator.CastTavernSpell(SeafloorRecruiter._chefsChoiceSpell, (Entity) this, target1);
    });
  }
}
