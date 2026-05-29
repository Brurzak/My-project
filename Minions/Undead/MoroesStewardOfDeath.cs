// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.MoroesStewardOfDeath
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class MoroesStewardOfDeath(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG28_304";
  public const string Text = "<b><b>Reborn</b>. Deathrattle:</b> Give your Undead +4/+6.";
  public const string GoldenText = "<b><b>Reborn</b>. Deathrattle:</b> Give your Undead +8/+12.";

  public Action<Minion> GetDeathrattle() => MoroesStewardOfDeath.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsUndead() && x.IsAlive())).ToList<Minion>())
        minion1.IncreaseStats(golden ? 8 : 4, golden ? 12 : 6);
    });
  }
}
