// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.Waveling
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class Waveling(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_856";
  public const string Text = "<b>Deathrattle:</b> After the Tavern is <b>Refreshed</b> this game, give a random minion in it +{0}/+{1}.";
  public const string GoldenText = "<b>Deathrattle:</b> After the Tavern is <b>Refreshed</b> this game, give a random minion in it +{0}/+{1} twice.";

  public Action<Minion>? GetDeathrattle() => (Action<Minion>) (minion => { });
}
