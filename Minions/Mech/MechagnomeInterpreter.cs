// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.MechagnomeInterpreter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class MechagnomeInterpreter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMagnetized,
  IEntity
{
  public const string CardId = "BG31_177";
  public const string Text = "Whenever you play or <b>Magnetize</b> a Mech, give it +{0}/+{1}.";
  public const string GoldenText = "Whenever you play or <b>Magnetize</b> a Mech, give it +{0}/+{1}.";

  public Action? OnFriendlyMagnetized(
    Minion friendly,
    Card _,
    Entity source,
    int extraAttack,
    int extraHealth)
  {
    return (Action) (() => friendly.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(1)));
  }
}
