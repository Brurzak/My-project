// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.DrBoombox
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class DrBoombox(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_165";
  public const string Text = "<b>Deathrattle:</b> Deal 7 damage to the 2 nearest enemy minions.";
  public const string GoldenText = "<b>Deathrattle:</b> Deal 14 damage to the 2 nearest enemy minions.";

  public Action<Minion> GetDeathrattle() => DrBoombox.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> alreadyDamaged = new List<Minion>();
      DamageRandomTarget();
      DamageRandomTarget();

      void DamageRandomTarget()
      {
        IEnumerable<Minion> source = minion.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && !alreadyDamaged.Contains(x)));
        if (source.Count<Minion>() == 0)
          return;
        double targetIndex = Targeting.GetOppositeIndex(minion);
        Minion random = source.GroupBy<Minion, double>((Func<Minion, double>) (x => Math.Abs((double) x.BoardPosition() - targetIndex))).OrderBy<IGrouping<double, Minion>, double>((Func<IGrouping<double, Minion>, double>) (x => x.Key)).First<IGrouping<double, Minion>>().ToList<Minion>().GetRandom<Minion>();
        minion.Simulator.ProcessDamage(golden ? 14 : 7, random, (Entity) minion);
        alreadyDamaged.Add(random);
      }
    });
  }
}
