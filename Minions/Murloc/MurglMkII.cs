// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.MurglMkII
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class MurglMkII(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG29_991";
  public const string Text = "<b>Avenge (4):</b> Give minions in your hand and board +1/+1 permanently.";
  public const string GoldenText = "<b>Avenge (4):</b> Give minions in your hand and board +2/+2 permanently.";

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      int by = this.DoubleIfGolden(1);
      foreach (Minion minion in this.FriendlySide)
        minion.IncreaseStats(by);
      foreach (MinionCardEntity friendlyHandMinion in this.FriendlyHandMinions(false))
        friendlyHandMinion.Data.IncreaseStats(by);
    });
  }
}
