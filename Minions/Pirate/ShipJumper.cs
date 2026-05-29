// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.ShipJumper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class ShipJumper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG35_700";
  public const string Text = "<b>Deathrattle:</b> Summon a 1/1 Sky Pirate and give it this minion's Attack. It attacks immediately.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a 1/1 Sky Pirate and give it double this minion's Attack. It attacks immediately.";

  public Action<Minion> GetDeathrattle() => ShipJumper.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      // ISSUE: explicit non-virtual call
      IEnumerable<Trigger> source2 = minion.Simulator.state.TriggerScope.Current.Data.Where<Trigger>((Func<Trigger, bool>) (x => !x.Resolved && (x.Source is Minion source3 ? __nonvirtual (source3.CardID) : (string) null) == "BG35_700"));
      using (minion.Simulator.state.TriggerScope.New(nameof (ShipJumper)))
      {
        minion.Simulator.ResolveTriggers(source2.ToList<Trigger>());
        minion.Simulator.ResolveTriggersInCurrentScope();
        int num = minion.attack();
        minion.TrySummonMinion(new Summon("BGS_061t")
        {
          SetStats = new (int, int)?((1 + num, 1))
        });
        if (!golden)
          return;
        minion.TrySummonMinion(new Summon("BGS_061t")
        {
          SetStats = new (int, int)?((1 + num, 1))
        });
      }
    });
  }
}
