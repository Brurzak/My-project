// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.CoralSpear
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells.TavernSpells;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class CoralSpear(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterSpellcraftSpellCast,
  IEntity
{
  public const string CardId = "BG35_MagicItem_925";
  private static ITavernSpell _mightOfStormwindSpell = (ITavernSpell) new MightOfStormwindSpell();

  public Action? OnAfterSpellcraftSpellCast(Minion? target)
  {
    return (Action) (() => this.Simulator.CastTavernSpell(CoralSpear._mightOfStormwindSpell, (Entity) this));
  }
}
