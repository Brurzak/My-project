// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.FriendlyGeist
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class FriendlyGeist(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_880";
  public const string Text = "<b>Deathrattle:</b> Your Tavern spells give an extra +{0} Attack this game.";
  public const string GoldenText = "<b>Deathrattle:</b> Your Tavern spells give an extra +{0} Attack this game.";

  public Action<Minion> GetDeathrattle() => FriendlyGeist.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden) => (Action<Minion>) (minion => { });
}
