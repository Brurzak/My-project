// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.FacelessDisciple
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class FacelessDisciple(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG24_719";
  public const string Text = "<b>Battlecry:</b> Transform a minion into one from a Tier higher.";
  public const string GoldenText = "<b>Battlecry:</b> Transform a minion into one from 2 Tiers higher.";

  public Action? OnBattlecry()
  {
    throw new UnsupportedInteractionException("Unsupported Battlecry", (Entity) this);
  }
}
