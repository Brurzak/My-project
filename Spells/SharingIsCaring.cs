// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SharingIsCaring
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells;

public class SharingIsCaring(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG31_889";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion minion = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().FirstOrDefault<Minion>();
      List<Minion> list = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      if (minion == null || !list.Any<Minion>())
        return;
      double targetIndex = Targeting.GetOppositeIndex(minion);
      Minion random = list.GroupBy<Minion, double>((Func<Minion, double>) (x => Math.Abs((double) x.BoardPosition() - targetIndex))).OrderBy<IGrouping<double, Minion>, double>((Func<IGrouping<double, Minion>, double>) (x => x.Key)).First<IGrouping<double, Minion>>().ToList<Minion>().GetRandom<Minion>();
      if (random == null)
        return;
      minion.IncreaseStats(random.attack(), random.health());
    });
  }
}
