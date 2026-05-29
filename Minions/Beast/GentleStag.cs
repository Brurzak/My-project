// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.GentleStag
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class GentleStag(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG31_369";
  public const string Text = "Whenever you summon a minion in combat, give your right-most Beast +3/+3 permanently.";
  public const string GoldenText = "Whenever you summon a minion in combat, give your right-most Beast +6/+6 permanently.";

  public Action OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      Minion minion = this.FriendlySide.LastOrDefault<Minion>((Func<Minion, bool>) (m => m.IsAlive() && m.IsBeast()));
      if (minion == null)
        return;
      int by = this.DoubleIfGolden(3);
      minion.IncreaseStats(by, (Entity) this);
    });
  }
}
