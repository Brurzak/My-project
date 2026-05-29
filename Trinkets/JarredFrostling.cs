// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.JarredFrostling
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

public class JarredFrostling(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_952";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsElemental())).ToList<Minion>();
      for (int index = 0; index < 2 && list.Any<Minion>(); ++index)
      {
        Minion minion;
        if (list.TryGetRandom<Minion>(out minion))
        {
          minion.AdditionalDeathrattles.Add(JarredFrostling.Deathrattle());
          list.Remove(minion);
        }
      }
    });
  }

  public static Action<Minion> Deathrattle()
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion((Summon) "BG26_537"));
  }
}
