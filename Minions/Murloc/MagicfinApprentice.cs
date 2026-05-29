// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.MagicfinApprentice
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Factory;
using BobsBuddy.Simulation;
using BobsBuddy.Spells.TavernSpells;
using HearthDb;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class MagicfinApprentice(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG33_890t";
  public const string Text = "<b>Battlecry:</b> This casts {0}. <i>(This minion can't be tripled.)</i>";

  public Action OnBattlecry()
  {
    return (Action) (() =>
    {
      if (this.ScriptDataNum1 == 0)
        return;
      string cardId;
      if (Cards.DbfIdToCardId.TryGetValue(this.ScriptDataNum1, out cardId))
      {
        ITavernSpell byCardId = TavernSpellFactory.CreateByCardId(cardId);
        if (byCardId != null)
        {
          this.Simulator.CastTavernSpell(byCardId, (Entity) this);
          return;
        }
      }
      this.Simulator.CastTavernSpell((ITavernSpell) new NullTavernSpell(), (Entity) this);
    });
  }
}
