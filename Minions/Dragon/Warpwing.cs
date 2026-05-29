// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Warpwing
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class Warpwing : Minion
{
  public const string CardId = "BG24_004";
  public const string Text = "<b>Immune</b> while attacking.";
  public const string GoldenText = "<b>Immune</b> while attacking.";

  public Warpwing(string cardId, bool controlledByPlayer, Simulator simulator)
    : base(cardId, controlledByPlayer, simulator)
  {
    this.ImmuneWhileAttacking = true;
  }
}
