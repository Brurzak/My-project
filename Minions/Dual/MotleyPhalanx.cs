// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.MotleyPhalanx
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class MotleyPhalanx(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG27_080";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Give a friendly minion of each type +{0}/+{1} permanently.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Give a friendly minion of each type +{0}/+{1} permanently.";

  public Action<Minion> GetDeathrattle() => MotleyPhalanx.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int attackBuff = golden ? 2 : 1;
      int healthBuff = golden ? 2 : 1;
      foreach (Minion minion1 in minion.FriendlySide.GetRandomPerRace())
        minion1.IncreaseStats(attackBuff, healthBuff, (Entity) minion);
    });
  }
}
