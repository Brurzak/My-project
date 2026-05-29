// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedBonker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedBonker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_Giant_102";
  public const string Text = "<b>Windfury</b> <b>Rally:</b> This plays {0} permanent <b>Blood Gems</b> on all your other minions.";
  public const string GoldenText = "<b>Windfury</b> <b>Rally:</b> This plays {0} permanent <b>Blood Gems</b> on all your other minions.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      foreach (Minion target1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x != minion)).ToList<Minion>())
      {
        minion.Simulator.CastBloodGem(target1, (Entity) minion);
        minion.Simulator.CastBloodGem(target1, (Entity) minion);
        if (isGolden)
        {
          minion.Simulator.CastBloodGem(target1, (Entity) minion);
          minion.Simulator.CastBloodGem(target1, (Entity) minion);
        }
      }
    });
  }
}
