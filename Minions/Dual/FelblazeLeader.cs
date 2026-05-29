// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.FelblazeLeader
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class FelblazeLeader(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_311";
  public const string Text = "<b>Rally:</b> Give minions in the Tavern +{0}/+{1} this game.";
  public const string GoldenText = "<b>Rally:</b> Give minions in the Tavern +{0}/+{1} this game twice.";

  public Action<Minion>? OnRally(bool isGolden, Minion target) => (Action<Minion>) (minion => { });
}
