// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.LighterFighter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class LighterFighter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG28_968";
  public const string Text = "<b>Deathrattle:</b> Deal 4 damage to the lowest-Health enemy minion, twice.";
  public const string GoldenText = "<b>Deathrattle:</b> Deal 8 damage to the lowest-Health enemy minion, twice.";

  public Action<Minion> GetDeathrattle() => LighterFighter.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int amount = golden ? 8 : 4;
      for (int index = 0; index < 2; ++index)
      {
        IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>> source = minion.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).Select(x => new
        {
          Health = x.health(),
          Minion = x
        }).GroupBy(x => x.Health).OrderBy<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>, int>(x => x.Key).FirstOrDefault<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>>();
        var data;
        if (source != null && source.ToList().TryGetRandom(out data))
          minion.Simulator.ProcessDamage(amount, data.Minion, (Entity) minion);
      }
    });
  }
}
