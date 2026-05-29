// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.SinrunnerBlanchy
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class SinrunnerBlanchy(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IRebornBehavior
{
  public const string CardId = "BG24_005";
  public const string Text = "<b>Reborn</b>. This is <b>Reborn</b> with full stats and <b>Bonus Keywords</b>.";
  public const string GoldenText = "<b>Reborn</b>. This is <b>Reborn</b> with full stats and <b>Bonus Keywords</b>.";

  public RebornBehavior RebornBehavior
  {
    get => RebornBehavior.KeepMaxStats | RebornBehavior.KeepEnchantments;
  }
}
