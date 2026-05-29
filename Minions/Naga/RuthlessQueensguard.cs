// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.RuthlessQueensguard
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells.TavernSpells;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class RuthlessQueensguard(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle,
  IOnRally
{
  public const string CardId = "BG34_926";
  public const string Text = "<b>Battlecry, Deathrattle, and Rally:</b> Cast Queen's Command.";
  public const string GoldenText = "<b>Battlecry, Deathrattle, and Rally:</b> Cast Queen's Command twice.";
  private static ITavernSpell _queensCommandSpell = (ITavernSpell) new QueensCommandSpell();

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.Simulator.CastTavernSpell(RuthlessQueensguard._queensCommandSpell, (Entity) this);
      if (!this.golden)
        return;
      this.Simulator.CastTavernSpell(RuthlessQueensguard._queensCommandSpell, (Entity) this);
    });
  }

  public Action<Minion> GetDeathrattle() => RuthlessQueensguard.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.Simulator.CastTavernSpell(RuthlessQueensguard._queensCommandSpell, (Entity) minion);
      if (!golden)
        return;
      minion.Simulator.CastTavernSpell(RuthlessQueensguard._queensCommandSpell, (Entity) minion);
    });
  }

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      minion.Simulator.CastTavernSpell(RuthlessQueensguard._queensCommandSpell, (Entity) minion);
      if (!isGolden)
        return;
      minion.Simulator.CastTavernSpell(RuthlessQueensguard._queensCommandSpell, (Entity) minion);
    });
  }
}
