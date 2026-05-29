// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.WannabeGargoyle
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class WannabeGargoyle(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IRebornBehavior
{
  public const string CardId = "BG30_109";
  public const string Text = "<b>Reborn</b> This is <b>Reborn</b> with full Attack.";
  public const string GoldenText = "<b>Reborn</b> This is <b>Reborn</b> with full Attack and Health.";

  public RebornBehavior RebornBehavior
  {
    get => !this.golden ? RebornBehavior.KeepMaxAttack : RebornBehavior.KeepMaxStats;
  }
}
