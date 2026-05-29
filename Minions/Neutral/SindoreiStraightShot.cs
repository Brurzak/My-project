// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.SindoreiStraightShot
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class SindoreiStraightShot(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG25_016";
  public const string Text = "<b>Divine Shield</b>, <b>Windfury</b> <b>Rally:</b> Remove <b>Reborn</b> and <b>Taunt</b> from the target.";
  public const string GoldenText = "<b>Divine Shield</b>, <b>Windfury</b> <b>Rally:</b> Remove <b>Reborn</b> and <b>Taunt</b> from the target.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      if (target.reborn)
      {
        target.reborn = false;
        List<Trigger> triggerList = new List<Trigger>();
        foreach (Entity friendlyEntity in target.FriendlyEntities)
        {
          if (friendlyEntity is IOnFriendlyMinionLostReborn minionLostReborn2)
            triggerList.TryAdd<Trigger>((Trigger) (minionLostReborn2.OnFriendlyMinionLostReborn(target), (IEntity) this));
        }
        if (triggerList.Any<Trigger>())
        {
          using (minion.Simulator.state.SummonScope.New("RebornTriggers"))
            minion.Simulator.ResolveTriggers(triggerList);
        }
      }
      if (!target.taunt)
        return;
      target.taunt = false;
      List<Trigger> triggerList1 = new List<Trigger>();
      foreach (Entity friendlyEntity in target.FriendlyEntities)
      {
        if (friendlyEntity is IOnFriendlyMinionLostTaunt friendlyMinionLostTaunt2)
          triggerList1.TryAdd<Trigger>((Trigger) (friendlyMinionLostTaunt2.OnFriendlyMinionLostTaunt(target), (IEntity) this));
      }
      if (!triggerList1.Any<Trigger>())
        return;
      using (minion.Simulator.state.SummonScope.New("TauntTriggers"))
        minion.Simulator.ResolveTriggers(triggerList1);
    });
  }
}
