// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.LivingAzerite
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class LivingAzerite(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMinionMadeGolden,
  IEntity,
  IOnAfterTavernSpellCast
{
  public const string CardId = "BG28_707";
  public const string Text = "Whenever you cast a Tavern spell, give Elementals in the Tavern +{0}/+{1} this game.";
  public const string GoldenText = "Whenever you cast a Tavern spell, give Elementals in the Tavern +{0}/+{1} this game.";

  public Action? OnMinionMadeGolden()
  {
    return (Action) (() =>
    {
      this.ScriptDataNum1 *= 2;
      this.ScriptDataNum2 *= 2;
    });
  }

  public Action? OnAfterTavernSpellCast(Minion? target)
  {
    return (Action) (() =>
    {
      if (!this.FriendlyEntities.Any<Entity>((Func<Entity, bool>) (x => x is AzeritePortrait)))
        return;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsElemental())).ToList<Minion>())
        minion.IncreaseStats(this.ScriptDataNum1, this.ScriptDataNum2, (Entity) this);
    });
  }
}
