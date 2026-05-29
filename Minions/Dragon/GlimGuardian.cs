// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.GlimGuardian
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class GlimGuardian(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG29_888";
  public const string Text = "<b>Rally:</b> Gain +{0} Attack.";
  public const string GoldenText = "<b>Rally:</b> Gain +{0} Attack.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int attackBuff = isGolden ? 4 : 2;
      minion.IncreaseStats(attackBuff, 0);
    });
  }
}
