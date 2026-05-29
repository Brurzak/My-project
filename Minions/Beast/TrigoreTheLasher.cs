// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TrigoreTheLasher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TrigoreTheLasher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionTakeDamage,
  IEntity
{
  public const string CardId = "BG29_807";
  public const string Text = "Whenever another friendly Beast takes damage, gain +{1} Health permanently.";
  public const string GoldenText = "Whenever another friendly Beast takes damage, gain +{1} Health permanently.";

  public Action? OnFriendlyMinionTakeDamage(Minion target, int value)
  {
    return (Action) (() =>
    {
      if (this == target || !target.IsBeast())
        return;
      this.IncreaseStats(0, this.DoubleIfGolden(2));
    });
  }
}
