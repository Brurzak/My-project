// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.TimewarpedExpeditioner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class TimewarpedExpeditioner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG34_Giant_317";
  public const string Text = "<b>Taunt</b>, <b>Divine Shield</b>. After this gains stats, also give the stats to the two left-most minions in your hand.";
  public const string GoldenText = "<b>Taunt</b>, <b>Divine Shield</b>. After this gains stats, also give twice the stats to the two left- most minions in your hand.";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (buffed != this || attackChange <= 0 && healthChange <= 0)
        return;
      int attackBuff = this.DoubleIfGolden(attackChange);
      int healthBuff = this.DoubleIfGolden(healthChange);
      foreach (MinionCardEntity minionCardEntity in this.FriendlyHandMinions(false).Take<MinionCardEntity>(2).ToArray<MinionCardEntity>())
        minionCardEntity?.Data.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
