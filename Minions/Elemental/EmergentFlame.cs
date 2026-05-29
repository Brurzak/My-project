// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.EmergentFlame
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class EmergentFlame(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG27_018";
  public const string Text = "<b>Battlecry:</b> Give an Elemental +1/+1. <i>(Improved by each <b>Refresh</b> this turn!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give an Elemental +2/+2. <i>(Improved by each <b>Refresh</b> this turn!)</i>";

  public Action? OnBattlecry()
  {
    throw new UnsupportedInteractionException("Unsupported Battlecry", (Entity) this);
  }
}
