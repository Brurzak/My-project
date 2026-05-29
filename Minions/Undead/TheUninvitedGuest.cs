// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TheUninvitedGuest
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TheUninvitedGuest(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG29_875";
  public const string Text = "<b>Start of Combat:</b> Give your other minions \"<b>Deathrattle:</b> Summon a 3/2 Shadow.\"";
  public const string GoldenText = "<b>Start of Combat:</b> Give your other minions \"<b>Deathrattle:</b> Summon a 6/4 Shadow.\"";

  public static Action<Minion> SummonDeathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(new Summon("BG29_875t", golden)));
  }

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this)).ToList<Minion>())
        minion.AdditionalDeathrattles.Add(TheUninvitedGuest.SummonDeathrattle(this.golden));
    });
  }
}
