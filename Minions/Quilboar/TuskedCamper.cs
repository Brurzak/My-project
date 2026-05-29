// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TuskedCamper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TuskedCamper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_886";
  public const string Text = "<b>Rally:</b> This plays a <b>Blood Gem</b> on itself.";
  public const string GoldenText = "<b>Rally:</b> This plays 2 <b>Blood Gems</b> on itself.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      minion.Simulator.CastBloodGem(minion, (Entity) minion);
      if (!isGolden)
        return;
      minion.Simulator.CastBloodGem(minion, (Entity) minion);
    });
  }
}
