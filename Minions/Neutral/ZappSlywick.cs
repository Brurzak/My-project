// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.ZappSlywick
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

public class ZappSlywick(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BGS_022";
  public const string Text = "<b>Windfury</b> This minion always attacks the enemy minion with the lowest Attack.";
  public const string GoldenText = "<b>Windfury</b> This minion always attacks the enemy minion with the lowest Attack.";

  public override Minion? ChooseAttackTarget()
  {
    List<Minion> list = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
    var data;
    return list.Any<Minion>() && list.Select(x => new
    {
      Minion = x,
      Attack = x.attack()
    }).GroupBy(x => x.Attack).OrderBy<IGrouping<int, \u003C\u003Ef__AnonymousType1<Minion, int>>, int>(x => x.Key).First<IGrouping<int, \u003C\u003Ef__AnonymousType1<Minion, int>>>().ToList().TryGetRandom(out data) ? data.Minion : base.ChooseAttackTarget();
  }
}
