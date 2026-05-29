// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.AmberGuardian
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class AmberGuardian(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG24_500";
  public const string Text = "<b>Taunt</b> <b>Start of Combat:</b> Give another friendly Dragon +{0}/+{1} and <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Taunt</b> <b>Start of Combat:</b> Give two other friendly Dragons +{0}/+{1} and <b>Divine Shield</b>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> alreadyBuffed = new List<Minion>();
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        List<Minion> minionList = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsDragon() && x != this && !alreadyBuffed.Contains(x))).ToList<Minion>();
        if (minionList.Count == 0)
          break;
        List<Minion> list = minionList.Where<Minion>((Func<Minion, bool>) (x => !x.hasDiv)).ToList<Minion>();
        if (list.Count > 0)
          minionList = list;
        Minion random = minionList.GetRandom<Minion>();
        random.IncreaseStats(2);
        random.div = 1;
        alreadyBuffed.Add(random);
      }
    });
  }
}
