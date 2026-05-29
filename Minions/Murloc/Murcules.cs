// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.Murcules
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class Murcules(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG27_023";
  public const string Text = "Whenever this kills a minion, give a minion in your hand +2/+2.";
  public const string GoldenText = "Whenever this kills a minion, give a minion in your hand +4/+4.";

  public Action? OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() =>
    {
      MinionCardEntity minionCardEntity;
      if (friendly != this || !this.FriendlyHandMinions(false).TryGetRandom<MinionCardEntity>(out minionCardEntity))
        return;
      int num = this.DoubleIfGolden(2);
      minionCardEntity.Data.IncreaseStats(num, num);
    });
  }
}
