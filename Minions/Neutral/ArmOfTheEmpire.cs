// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.ArmOfTheEmpire
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class ArmOfTheEmpire(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacked,
  IEntity
{
  public const string CardId = "BGS_110";
  public const string Text = "Whenever a friendly <b>Taunt</b> minion is attacked, give it +{0} Attack permanently.";
  public const string GoldenText = "Whenever a friendly <b>Taunt</b> minion is attacked, give it +{0} Attack permanently.";

  public Action OnFriendlyMinionIsAttacked(Minion friendlyMinion, Minion attacker)
  {
    return (Action) (() =>
    {
      if (!friendlyMinion.taunt)
        return;
      friendlyMinion.IncreaseStats(this.DoubleIfGolden(3), 0);
    });
  }
}
