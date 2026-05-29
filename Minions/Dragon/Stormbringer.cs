// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Stormbringer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class Stormbringer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG26_966";
  public const string Text = "After a different friendly Dragon gains Attack, this also gains it.";
  public const string GoldenText = "After a different friendly Dragon gains Attack, this also gains it twice.";

  public Action? OnFriendlyMinionBuffed(
    Minion minion,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return !minion.IsDragon() || attackChange == 0 || minion.CardID == this.CardID ? (Action) null : (Action) (() => this.IncreaseStats(this.DoubleIfGolden(attackChange), 0));
  }
}
