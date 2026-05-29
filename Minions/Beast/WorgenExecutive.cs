// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.WorgenExecutive
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class WorgenExecutive(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_650";
  public const string Text = "<b>Rally:</b> After the Tavern is <b>Refreshed</b> this game, give its right-most minion +{0}/+{1}.";
  public const string GoldenText = "<b>Rally:</b> After the Tavern is <b>Refreshed</b> this game, give its right-most minion +{0}/+{1}.";

  public Action<Minion> OnRally(bool isGolden, Minion target) => (Action<Minion>) (minion => { });
}
