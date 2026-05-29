// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.ChampionOfSargeras
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class ChampionOfSargeras(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG27_016";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Minions in the Tavern have +{0}/+{1} this game.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Minions in the Tavern have +{0}/+{1} this game.";

  public Action? OnBattlecry() => (Action) null;

  public Action<Minion> GetDeathrattle() => (Action<Minion>) (_ => { });
}
