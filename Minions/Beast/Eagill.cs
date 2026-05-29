// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.Eagill
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class Eagill(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG28_630";
  public const string Text = "<b>Battlecry:</b> Give another random friendly minion and a minion in your hand +2/+3.";
  public const string GoldenText = "<b>Battlecry:</b> Give another random friendly minion and a minion in your hand +4/+6.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      Minion minion;
      if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x != this)).ToList<Minion>().TryGetRandom<Minion>(out minion))
        minion.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(3));
      MinionCardEntity minionCardEntity;
      if (!this.FriendlyHandMinions(false).TryGetRandom<MinionCardEntity>(out minionCardEntity))
        return;
      minionCardEntity.Data.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(3));
    });
  }
}
