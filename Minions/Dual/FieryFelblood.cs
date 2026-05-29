// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.FieryFelblood
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class FieryFelblood(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG29_877";
  public const string Text = "<b>Deathrattle:</b> Minions in the Tavern have +2 Attack this game.";
  public const string GoldenText = "<b>Deathrattle:</b> Minions in the Tavern have +4 Attack this game.";

  public Action<Minion> GetDeathrattle() => (Action<Minion>) (_ => { });
}
