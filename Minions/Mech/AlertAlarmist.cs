// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.AlertAlarmist
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class AlertAlarmist(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG35_340";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> The next Tavern spell you buy costs ({0}) less.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> The next Tavern spell you buy costs ({0}) less.";

  public Action<Minion>? GetDeathrattle() => AlertAlarmist.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden) => (Action<Minion>) (_ => { });
}
