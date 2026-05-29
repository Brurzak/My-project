// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.WispInTheShell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class WispInTheShell(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_841";
  public const string Text = "<b>Battlecry:</b> Give a friendly minion +2 Health. <i>(Improved by each friendly minion that died last combat!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give a friendly minion +4 Health. <i>(Improved by each friendly minion that died last combat!)</i>";

  public Action? OnBattlecry()
  {
    throw new UnsupportedInteractionException("Unsupported Battlecry", (Entity) this);
  }
}
