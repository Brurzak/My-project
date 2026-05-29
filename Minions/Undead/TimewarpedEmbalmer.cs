// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TimewarpedEmbalmer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TimewarpedEmbalmer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMinionMadeGolden,
  IEntity
{
  public const string CardId = "BG34_Giant_332";
  public const string Text = "One minion you summon each turn gains <b>Reborn</b>. <i>({0} left!)</i>";
  public const string GoldenText = "Two minions you summon each turn gain <b>Reborn</b>. <i>({0} left!)</i>";

  public Action? OnMinionMadeGolden() => (Action) (() => this.ScriptDataNum1 = 2);

  public override Action? OnMinionSummonedByFriendly(
    Minion summoned,
    Entity? source,
    bool wasReborn)
  {
    return (Action) (() =>
    {
      if (summoned.reborn || this.ScriptDataNum1 <= 0)
        return;
      summoned.reborn = true;
      --this.ScriptDataNum1;
    });
  }
}
