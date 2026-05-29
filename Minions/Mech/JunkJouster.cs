// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.JunkJouster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class JunkJouster(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMagnetized,
  IEntity
{
  public const string CardId = "BG34_175";
  public const string Text = "After you <b>Magnetize</b> a minion, give your minions +{0}/+{1}.";
  public const string GoldenText = "After you <b>Magnetize</b> a minion, give your minions +{0}/+{1}.";

  public Action? OnFriendlyMagnetized(
    Minion friendly,
    Card _,
    Entity source,
    int extraAttack,
    int extraHealth)
  {
    return (Action) (() =>
    {
      int scriptDataNum1 = this.ScriptDataNum1;
      int scriptDataNum2 = this.ScriptDataNum2;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(scriptDataNum1), this.DoubleIfGolden(scriptDataNum2));
    });
  }
}
