// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.SelflessHero
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class SelflessHero(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IBattlecry
{
  public const string CardId = "BG_OG_221";
  public const string Text = "<b>Deathrattle:</b> Give a random friendly minion <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Give 2 random friendly minions <b>Divine Shield</b>.";

  public override IEnumerable<T> GetTriggers<T>()
  {
    SelflessHero selflessHero = this;
    if (selflessHero is T trigger1 && typeof (T) == typeof (IBattlecry))
    {
      // ISSUE: explicit non-virtual call
      if (__nonvirtual (selflessHero.FriendlyEntities).Any<Entity>((Func<Entity, bool>) (x => x is SelflessPortrait)))
        yield return trigger1;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      foreach (T trigger in selflessHero.\u003C\u003En__0<T>().Where<T>(new Func<T, bool>(selflessHero.\u003CGetTriggers\u003Eb__4_1<T>)))
        yield return trigger;
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      foreach (T trigger in selflessHero.\u003C\u003En__1<T>())
        yield return trigger;
    }
  }

  public Action? OnBattlecry()
  {
    return (Action) (() => SelflessHero.TryAddDivToMinions(this.FriendlySide, this.golden ? 2 : 1));
  }

  public Action<Minion> GetDeathrattle() => SelflessHero.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => SelflessHero.TryAddDivToMinions(minion.FriendlySide, golden ? 2 : 1));
  }

  private static void TryAddDivToMinions(List<Minion> side, int count)
  {
    Minion minion;
    for (int index = 0; index < count && side.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && !x.hasDiv)).ToList<Minion>().TryGetRandom<Minion>(out minion); ++index)
      minion.div = 1;
  }
}
