// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.MonstrousMacaw
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class MonstrousMacaw(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BGS_078";
  public const string Text = "<b>Rally:</b> Trigger your left-most <b>Deathrattle</b> <i>(except this minion's)</i>.";
  public const string GoldenText = "<b>Rally:</b> Trigger your left-most <b>Deathrattle</b> twice <i>(except this minion's)</i>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      bool flag = minion.FriendlyEntities.Any<Entity>((Func<Entity, bool>) (x => x is MacawPortrait));
      int num = isGolden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        if (flag)
        {
          Minion target1 = minion.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.GetTriggers<IBattlecry>().Any<IBattlecry>() && x.IsAlive()));
          if (target1 != null)
            minion.Simulator.InvokeBattlecry(target1);
        }
        Minion minion1 = minion.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x != minion && x.HasDeathrattle() && x.IsAlive()));
        if (minion1 == null)
          break;
        minion.Simulator.TriggerDeathrattles(minion1);
        minion.Simulator.ResolveTriggersInCurrentScope();
      }
    });
  }
}
