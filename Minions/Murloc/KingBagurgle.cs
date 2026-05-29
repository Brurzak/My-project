// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.KingBagurgle
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class KingBagurgle(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGS_030";
  public const string Text = "<b>Battlecry:</b> Give all other Murlocs in your hand and board +{0}/+{1}.";
  public const string GoldenText = "<b>Battlecry:</b> Give all other Murlocs in your hand and board +{0}/+{1}.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsMurloc() && x.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(3));
      foreach (MinionCardEntity minionCardEntity in this.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (x => x.Data.IsMurloc())).ToList<MinionCardEntity>())
        minionCardEntity.Data.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(3));
    });
  }
}
