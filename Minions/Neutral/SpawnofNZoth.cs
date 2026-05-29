// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.SpawnofNZoth
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class SpawnofNZoth(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_OG_256";
  public const string Text = "<b>Deathrattle:</b> Give your minions +1/+1.";
  public const string GoldenText = "<b>Deathrattle:</b> Give your minions +2/+2.";

  public Action<Minion> GetDeathrattle() => SpawnofNZoth.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int by = golden ? 2 : 1;
      foreach (Minion minion1 in minion.FriendlySide)
        minion1.IncreaseStats(by);
    });
  }
}
