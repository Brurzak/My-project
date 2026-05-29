// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.TimewarpedAstrogill
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class TimewarpedAstrogill(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffedWhileInHand,
  IEntity
{
  public const string CardId = "BG34_Giant_801";
  public const string Text = "While this is in your hand, after a different friendly Murloc gains stats, gain +{0}/+{1}.";
  public const string GoldenText = "While this is in your hand, after a different friendly Murloc gains stats, gain +{0}/+{1}.";

  public Action? OnFriendlyMinionBuffedWhileInHand(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (!buffed.IsMurloc() || !(buffed.CardID != "BG34_Giant_801"))
        return;
      this.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(2));
    });
  }
}
