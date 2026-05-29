// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.AranasiAlchemist
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class AranasiAlchemist(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_325";
  public const string Text = "<b>Taunt</b>, <b>Reborn</b> <b>Deathrattle:</b> Give minions in the Tavern +{1} Health this game.";
  public const string GoldenText = "<b>Taunt</b>, <b>Reborn</b> <b>Deathrattle:</b> Give minions in the Tavern +{1} Health this game.";

  public Action<Minion> GetDeathrattle() => (Action<Minion>) (_ => { });
}
