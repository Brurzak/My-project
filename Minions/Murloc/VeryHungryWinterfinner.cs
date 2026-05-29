// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.VeryHungryWinterfinner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class VeryHungryWinterfinner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG29_300";
  public const string Text = "<b>Taunt</b> Whenever this takes damage, give a minion in your hand +2/+1.";
  public const string GoldenText = "<b>Taunt</b> Whenever this takes damage, give a minion in your hand +4/+2.";

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() =>
    {
      MinionCardEntity minionCardEntity;
      if (!this.FriendlyHandMinions(false).TryGetRandom<MinionCardEntity>(out minionCardEntity))
        return;
      minionCardEntity.Data.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(1));
    });
  }
}
