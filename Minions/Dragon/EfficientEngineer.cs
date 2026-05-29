// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.EfficientEngineer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class EfficientEngineer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_301";
  public const string Text = "<b>Battlecry:</b> Trigger a friendly minion's end of turn effects <i>(except Young Murk-Eye)</i>.";
  public const string GoldenText = "<b>Battlecry:</b> Trigger a friendly minion's end of turn effects twice <i>(except Young Murk-Eye)</i>.";

  public Action? OnBattlecry()
  {
    throw new UnsupportedInteractionException("Unsupported Battlecry", (Entity) this);
  }
}
