// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedTwirler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells.TavernSpells;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedTwirler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterBloodGemCast,
  IEntity
{
  public const string CardId = "BG34_Giant_105";
  public const string Text = "After you play a <b>Blood Gem</b> on this, cast Blood Gem Barrage.";
  public const string GoldenText = "After you play a <b>Blood Gem</b> on this, cast Blood Gem Barrage twice.";
  private static ITavernSpell _bloodGemBarrage = (ITavernSpell) new NullTavernSpell();

  public Action? OnAfterBloodGemCast(Minion? target)
  {
    return target != this ? (Action) null : (Action) (() =>
    {
      this.Simulator.CastTavernSpell(TimewarpedTwirler._bloodGemBarrage, (Entity) this);
      if (!this.golden)
        return;
      this.Simulator.CastTavernSpell(TimewarpedTwirler._bloodGemBarrage, (Entity) this);
    });
  }
}
