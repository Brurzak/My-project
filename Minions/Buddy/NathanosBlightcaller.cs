// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.NathanosBlightcaller
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class NathanosBlightcaller(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG23_HERO_306_Buddy";
  public const string Text = "<b>Battlecry:</b> Sell a friendly minion. Split its stats evenly amongst its neighbors.";
  public const string GoldenText = "<b>Battlecry:</b> Sell a friendly minion. Split double its stats evenly amongst its neighbors.";

  public Action? OnBattlecry()
  {
    throw new UnsupportedInteractionException("Unsupported Battlecry", (Entity) this);
  }
}
