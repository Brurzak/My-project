// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.LurkingLeviathan
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class LurkingLeviathan(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG35_602";
  public const string Text = "Whenever you summon Beast, give it +{1} Attack and improve this permanently.";
  public const string GoldenText = "Whenever you summon Beast, give it +{1} Attack and improve this permanently.";
  private int _inCombatCount;

  public Action OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (summoned.IsDead() || !summoned.IsBeast())
        return;
      summoned.IncreaseStats((1 + this.ScriptDataNum1 + this._inCombatCount) * this.DoubleIfGolden(2), 0);
      ++this._inCombatCount;
    });
  }
}
