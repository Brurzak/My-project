// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.EnDjinnBlazer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class EnDjinnBlazer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_865";
  public const string Text = "<b>Battlecry:</b> After the Tavern is <b>Refreshed</b> this game, give a random minion in it +{0}/+{1}.";
  public const string GoldenText = "<b>Battlecry:</b> After the Tavern is <b>Refreshed</b> this game, give a random minion in it +{0}/+{1} twice.";

  public Action? OnBattlecry() => (Action) null;
}
