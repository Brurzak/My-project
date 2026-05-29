// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.TimewarpedPlunderer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class TimewarpedPlunderer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_PreMadeChamp_067";
  public const string Text = "<b>Deathrattle:</b> Increase your maximum Gold by {0}.";
  public const string GoldenText = "<b>Deathrattle:</b> Increase your maximum Gold by {0}.";

  public Action<Minion>? GetDeathrattle() => (Action<Minion>) (minion => { });
}
