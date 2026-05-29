// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.Scallywag
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class Scallywag(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BGS_061";
  public const string Text = "<b>Deathrattle:</b> Summon a 1/1 Pirate. It attacks immediately.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a 2/2 Pirate. It attacks immediately.";

  public Action<Minion> GetDeathrattle() => Scallywag.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      // ISSUE: explicit non-virtual call
      IEnumerable<Trigger> source2 = minion.Simulator.state.TriggerScope.Current.Data.Where<Trigger>((Func<Trigger, bool>) (x => !x.Resolved && (x.Source is Minion source3 ? __nonvirtual (source3.CardID) : (string) null) == "BGS_061"));
      using (minion.Simulator.state.TriggerScope.New(nameof (Scallywag)))
      {
        minion.Simulator.ResolveTriggers(source2.ToList<Trigger>());
        minion.Simulator.ResolveTriggersInCurrentScope();
        minion.TrySummonMinion(new Summon("BGS_061t", golden));
      }
    });
  }
}
