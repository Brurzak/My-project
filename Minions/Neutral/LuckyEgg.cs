// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.LuckyEgg
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

internal class LuckyEgg(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG30_104";
  public const string Text = "<b>Battlecry:</b> <b>Discover</b> a Golden Tier 3 minion to transform into.";
  public const string GoldenText = "<b>Battlecry:</b> <b>Discover</b> a Golden Tier 4 minion to transform into.";

  public Action? OnBattlecry()
  {
    throw new UnsupportedInteractionException("Unsupported Battlecry", (Entity) this);
  }
}
