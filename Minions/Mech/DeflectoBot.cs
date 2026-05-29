// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.DeflectoBot
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class DeflectoBot(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BGS_071";
  public const string Text = "<b>Divine Shield</b> Whenever you summon a Mech during combat, gain +{0} Attack and <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Divine Shield</b> Whenever you summon a Mech during combat, gain +{0} Attack and <b>Divine Shield</b>.";

  public Action OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (summoned == this || !summoned.IsMech())
        return;
      this.IncreaseStats(this.DoubleIfGolden(2), 0);
      this.div = 1;
    });
  }
}
