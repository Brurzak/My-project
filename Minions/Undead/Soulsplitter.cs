// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.Soulsplitter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class Soulsplitter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG25_023";
  public const string Text = "<b>Reborn</b> <b>Start of Combat:</b> Give a friendly Undead <b>Reborn</b>.";
  public const string GoldenText = "<b>Reborn</b> <b>Start of Combat:</b> Give 2 friendly Undead <b>Reborn</b>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        Minion minion;
        if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsUndead() && !x.reborn)).ToList<Minion>().TryGetRandom<Minion>(out minion))
          minion.reborn = true;
      }
    });
  }
}
