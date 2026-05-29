// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.RoaringRecruiter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class RoaringRecruiter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG29_816";
  public const string Text = "Whenever another friendly Dragon attacks, give it +{0}/+{1}.";
  public const string GoldenText = "Whenever another friendly Dragon attacks, give it +{0}/+{1}.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      if (!attacker.IsDragon() || attacker == this)
        return;
      attacker.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(1));
    });
  }
}
