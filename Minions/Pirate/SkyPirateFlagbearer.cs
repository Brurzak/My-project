// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.SkyPirateFlagbearer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class SkyPirateFlagbearer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_119";
  public const string Text = "<b>Start of Combat:</b> Give your other Pirates \"<b>Deathrattle:</b> Summon a Scallywag.\"";
  public const string GoldenText = "<b>Start of Combat:</b> Give your other Pirates \"<b>Deathrattle:</b> Summon a Golden Scallywag.\"";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsPirate() && x != this)))
        minion.AdditionalDeathrattles.Add(SkyPirateFlagbearer.Deathrattle(this.golden));
    });
  }

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(new Summon("BGS_061", golden)));
  }
}
