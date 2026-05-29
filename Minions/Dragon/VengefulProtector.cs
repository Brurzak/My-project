// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.VengefulProtector
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class VengefulProtector(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_247";
  public const string Text = "<b>Divine Shield</b> <b>Rally:</b> Give your other minions +{0}/+{1}.";
  public const string GoldenText = "<b>Divine Shield</b> <b>Rally:</b> Give your other minions +{0}/+{1}.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int attackBuff = isGolden ? 6 : 3;
      int healthBuff = isGolden ? 6 : 3;
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x != minion)).ToList<Minion>())
        minion1.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
