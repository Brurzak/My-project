// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.MasterOfRealities
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class MasterOfRealities(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG21_036";
  public const string Text = "<b><b>Taunt</b>.</b> After a friendly Elemental gains stats, gain +{0}/+{1}.";
  public const string GoldenText = "<b><b>Taunt</b>.</b> After a friendly Elemental gains stats, gain +{0}/+{1}.";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (!buffed.IsElemental())
        return;
      this.IncreaseStats(this.DoubleIfGolden(2));
    });
  }
}
