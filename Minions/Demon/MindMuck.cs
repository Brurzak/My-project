// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.MindMuck
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class MindMuck(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG23_357";
  public const string Text = "<b>Battlecry:</b> Choose a friendly Demon. It consumes a minion in the Tavern to gain its stats.";
  public const string GoldenText = "<b>Battlecry:</b> Choose a friendly Demon. It consumes a minion in the Tavern to gain double its stats.";

  public Action? OnBattlecry() => (Action) null;
}
