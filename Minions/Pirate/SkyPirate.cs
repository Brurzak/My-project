// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.SkyPirate
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class SkyPirate(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnSummoned,
  IEntity
{
  public const string CardId = "BGS_061t";
  public const string Text = "";
  public const string GoldenText = "";

  public Action? OnSummoned()
  {
    return (Action) (() =>
    {
      if (this.OriginalMinion == this)
      {
        while (this.FriendlySide.Count < 7)
        {
          // ISSUE: explicit non-virtual call
          Trigger trigger = this.Simulator.state.TriggerScope.Current.Data.FirstOrDefault<Trigger>((Func<Trigger, bool>) (x => !x.Resolved && (x.Source is Minion source2 ? __nonvirtual (source2.CardID) : (string) null) == "BG_DAL_575"));
          if (trigger != null)
            trigger.Invoke();
          else
            break;
        }
        this.Simulator.ResolveTriggersInCurrentScope();
      }
      this.Simulator.AttackWithMinion((Minion) this);
    });
  }
}
