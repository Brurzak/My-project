// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.SrTombDiver
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class SrTombDiver(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_41_Buddy";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Make your right-most minion Golden.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Make your two right-most minions Golden.";

  public Action<Minion> GetDeathrattle() => this.Deathrattle(this.golden);

  public Action<Minion> Deathrattle(bool isGolden)
  {
    return (Action<Minion>) (minion =>
    {
      int count = minion.FriendlySide.Count;
      if (count > 0)
        minion.FriendlySide[count - 1].TryMakeGolden(true, (Entity) this);
      if (!isGolden || count <= 1)
        return;
      minion.FriendlySide[count - 2].TryMakeGolden(true, (Entity) this);
    });
  }
}
