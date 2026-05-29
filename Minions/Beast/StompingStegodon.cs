// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.StompingStegodon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class StompingStegodon(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity,
  IOnRally
{
  public const string CardId = "BG33_840";
  public const string Text = "<b>Rally:</b> Give your other Beasts +{0} Attack and this <b>Rally</b>.4[x]<b>Rally:</b> Give your other Beasts +{0}/+{1} and this <b>Rally</b>.";
  public const string GoldenText = "<b>Rally:</b> Give your other Beasts +{0} Attack and this <b>Rally</b>.8[x]<b>Rally:</b> Give your other Beasts +{0}/+{1} and this <b>Rally</b>.";

  public void OnCombatStartSetup()
  {
    if (this.golden && this.StegodonGoldenRalliesGranted == 0)
    {
      ++this.StegodonGoldenRalliesGranted;
    }
    else
    {
      if (this.golden || this.StegodonRalliesGranted != 0)
        return;
      ++this.StegodonRalliesGranted;
    }
  }

  public Action? OnStartOfCombat() => (Action) (() => { });

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion => StompingStegodon.Rally()(minion));
  }

  public static Action<Minion> Rally()
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsBeast() && x != minion)).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      int attackBuff = minion.StegodonRalliesGranted * 4 + minion.StegodonGoldenRalliesGranted * 8;
      foreach (Minion minion1 in list)
      {
        if (minion1.StegodonRalliesGranted == 0 && minion1.StegodonGoldenRalliesGranted == 0)
          minion1.AdditionalRallies.Add(StompingStegodon.Rally());
        minion1.IncreaseStats(attackBuff, 0);
        minion1.StegodonRalliesGranted += minion.StegodonRalliesGranted;
        minion1.StegodonGoldenRalliesGranted += minion.StegodonGoldenRalliesGranted;
      }
    });
  }
}
