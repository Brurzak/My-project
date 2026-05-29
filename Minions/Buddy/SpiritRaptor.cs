// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.SpiritRaptor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.HeroPowers;
using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class SpiritRaptor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG22_HERO_001_Buddy";
  public const string Text = "After you call upon a new Element, this remembers it. <b>Deathrattle:</b> Call upon those Elements.";
  public const string GoldenText = "After you call upon a new Element, this remembers it. <b>Deathrattle:</b> Call upon those Elements twice.";

  public Action<Minion>? GetDeathrattle() => SpiritRaptor.Deathrattle(this.golden);

  public static Action<Minion>? Deathrattle(bool golden)
  {
    return !golden ? (Action<Minion>) null : (Action<Minion>) (minion =>
    {
      foreach (Action<Minion> action in minion.AdditionalDeathrattles.ToList<Action<Minion>>())
      {
        if (action.Method.DeclaringType == typeof (BrukanInvocationDeathrattles))
          action(minion);
      }
    });
  }
}
