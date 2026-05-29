// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.KeyboardIgniter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class KeyboardIgniter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG26_522";
  public const string Text = "<b>Battlecry:</b> Give your other Demons +2/+2 and deal 2 damage to your hero.";
  public const string GoldenText = "<b>Battlecry:</b> Give your other Demons +2/+2 and deal 2 damage to your hero, twice.";

  public Action? OnBattlecry()
  {
    throw new UnsupportedInteractionException("Unsupported Battlecry", (Entity) this);
  }
}
