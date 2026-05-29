// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.TrainingCertificate
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class TrainingCertificate(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_962";
  private const int TotalTargets = 2;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count == 0)
        return;
      List<IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>>> list1 = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).Select(x => new
      {
        Attack = x.attack(),
        Minion = x
      }).GroupBy(x => x.Attack).OrderBy<IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>>, int>(x => x.Key).ToList<IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>>>();
      IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>> source1 = list1.FirstOrDefault<IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>>>();
      IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>> source2 = list1.Skip<IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>>>(1).FirstOrDefault<IGrouping<int, \u003C\u003Ef__AnonymousType3<int, Minion>>>();
      if (source1 == null)
        return;
      List<Minion> list2 = source1.Select(x => x.Minion).ToList<Minion>();
      if (list2.Count > 1)
      {
        for (int index = 0; index < 2; ++index)
        {
          Minion minion;
          if (list2.TryGetRandom<Minion>(out minion))
          {
            minion.SetStats(new int?(minion.attack() * 2), new int?(minion.health() * 2));
            list2.Remove(minion);
          }
        }
      }
      else
      {
        Minion minion1 = list2.First<Minion>();
        minion1.SetStats(new int?(minion1.attack() * 2), new int?(minion1.health() * 2));
        Minion minion2;
        if (source2 == null || !source2.Select(x => x.Minion).ToList<Minion>().TryGetRandom<Minion>(out minion2))
          return;
        minion2.SetStats(new int?(minion2.attack() * 2), new int?(minion2.health() * 2));
      }
    });
  }
}
