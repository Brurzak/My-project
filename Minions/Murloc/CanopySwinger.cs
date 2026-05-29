// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.CanopySwinger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class CanopySwinger(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG33_896";
  public const string Text = "<b>Battlecry:</b> Give all other Murlocs in your hand and board +{0} Attack.";
  public const string GoldenText = "<b>Battlecry:</b> Give all other Murlocs in your hand and board +{0} Attack.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(4);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive() && x.IsMurloc())))
        minion.IncreaseStats(attackBuff, 0);
      foreach (MinionCardEntity minionCardEntity in this.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (x => x.Data.IsMurloc())).ToList<MinionCardEntity>())
        minionCardEntity.Data.IncreaseStats(attackBuff, 0);
    });
  }
}
