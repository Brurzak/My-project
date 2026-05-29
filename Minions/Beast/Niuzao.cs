// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.Niuzao
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class Niuzao(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG27_822";
  public const string Text = "<b>Rally:</b> Deal damage equal to this minion's Attack to a random enemy minion other than the target.";
  public const string GoldenText = "<b>Rally:</b> Deal damage equal to this minion's Attack to 2 random enemy minions other than the target.";

  public Action<Minion> OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 2 : 1;
      List<Minion> excludeTargets = new List<Minion>()
      {
        target
      };
      for (int index = 0; index < num; ++index)
      {
        Minion target1;
        if (minion.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => !excludeTargets.Contains(x))).ToList<Minion>().TryGetRandom<Minion>(out target1))
        {
          excludeTargets.Add(target1);
          minion.Simulator.ProcessDamage(minion.attack(), target1, (Entity) minion);
        }
      }
    });
  }
}
