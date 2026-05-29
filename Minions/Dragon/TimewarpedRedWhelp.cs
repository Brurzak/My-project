// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TimewarpedRedWhelp
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TimewarpedRedWhelp(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_091";
  public const string Text = "<b>Start of Combat:</b> Deal {0} damage to two random enemy minions. <i>(Improves after you play a Dragon!)</i>";
  public const string GoldenText = "<b>Start of Combat:</b> Deal {0} damage to four random enemy minions. <i>(Improves after you play a Dragon!)</i>";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int scriptDataNum1 = this.ScriptDataNum1;
      for (int index = 0; index < this.DoubleIfGolden(2) && this.health() > 0; ++index)
      {
        Minion target;
        if (this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target))
          this.Simulator.ProcessDamage(scriptDataNum1, target, (Entity) this);
      }
    });
  }
}
