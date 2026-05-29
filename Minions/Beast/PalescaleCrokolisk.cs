// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.PalescaleCrokolisk
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class PalescaleCrokolisk(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IAvenge
{
  public const string CardId = "BG21_001";
  public const string Text = "<b>Avenge (2) and Deathrattle:</b> Give another friendly Beast +6/+6.";
  public const string GoldenText = "<b>Avenge (2) and Deathrattle:</b> Give another friendly Beast +12/+12.";

  public int AvengeRequirement => 2;

  public Action? OnAvenge() => (Action) (() => this.GetDeathrattle()((Minion) this));

  public Action<Minion> GetDeathrattle() => PalescaleCrokolisk.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion minion1;
      if (!minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsBeast() && x != minion)).ToList<Minion>().TryGetRandom<Minion>(out minion1))
        return;
      minion1.IncreaseStats(golden ? 12 : 6);
    });
  }
}
