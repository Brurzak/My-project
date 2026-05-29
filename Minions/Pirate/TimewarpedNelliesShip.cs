// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.TimewarpedNelliesShip
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class TimewarpedNelliesShip(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_074t";
  public const string Text = "At the start of each turn, <b>Discover</b> a Pirate to crew the ship. <b>Deathrattle:</b> Summon and get that Pirate.";
  public const string GoldenText = "At the start of each turn, <b>Discover</b> 2 Pirates to crew the ship. <b>Deathrattle:</b> Summon and get those Pirates.";

  public Action<Minion> GetDeathrattle() => TimewarpedNelliesShip.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden) => (Action<Minion>) (minion => { });
}
