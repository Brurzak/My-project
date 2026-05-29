// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.PeckishFeldrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class PeckishFeldrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG27_009";
  public const string Text = "<b>Battlecry: Consume</b> 3 minions in the Tavern.";
  public const string GoldenText = "<b>Battlecry:</b> Consume 3 minions in the Tavern to gain double their stats.";

  public Action? OnBattlecry() => (Action) null;
}
