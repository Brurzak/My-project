// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.AdvancedBallistics
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Enchantments;

public class AdvancedBallistics(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IOnAttack,
  IEntity
{
  public const string CardId = "BG31_HERO_801ptde";

  public Action? OnAttack(Minion target)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => this.AttachedTo != x)))
      {
        if (this.AttachedTo != minion)
          minion.IncreaseStats(this.ScriptDataNum1, 0);
      }
    });
  }
}
