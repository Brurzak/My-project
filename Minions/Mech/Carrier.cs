// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.Carrier
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class Carrier(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG31_HERO_802pt1";
  public const string Text = "<b>Avenge (4):</b> Summon a {1}/{1} Interceptor. Then improve this permanently.";
  public const string GoldenText = "<b>Avenge (4):</b> Summon a {1}/{1} Interceptor. Then improve this permanently.";
  private int _avengeCount;

  public int AvengeRequirement => 4;

  private static int StatusIncreaseFactor => 7;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      int num1 = this.ScriptDataNum1 + this._avengeCount * this.DoubleIfGolden(Carrier.StatusIncreaseFactor);
      List<Minion> source = this.TrySummonMinion(new Summon("BG31_HERO_802pt1t", this.golden));
      if (source.Any<Minion>())
      {
        int num2 = this.DoubleIfGolden(7);
        source[0].SetStats(new int?(num2 + num1), new int?(num2 + num1));
      }
      ++this._avengeCount;
    });
  }
}
