// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TimewarpedDuskmaw
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TimewarpedDuskmaw(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_PreMadeChamp_020";
  public const string Text = "<b>Avenge ({0}):</b> Give your Dragons +{1}/+{2}.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Give your Dragons +{1}/+{2}.";

  public int AvengeRequirement => 1;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(6);
      int healthBuff = this.DoubleIfGolden(4);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsDragon() && x.IsAlive())))
        minion.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
