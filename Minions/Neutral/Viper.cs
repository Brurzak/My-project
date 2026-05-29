// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Viper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class Viper : Minion
{
  public const string CardId = "BG31_HERO_811t8";
  public const string Text = "<b>Venomous Immune</b> while attacking. <i>(Morphs each turn!)</i><b>Venomous Immune</b> while attacking.";
  public const string GoldenText = "<b>Venomous Immune</b> while attacking. <i>(Morphs each turn!)</i><b>Venomous Immune</b> while attacking.";

  public Viper(string cardId, bool controlledByPlayer, Simulator simulator)
    : base(cardId, controlledByPlayer, simulator)
  {
    this.ImmuneWhileAttacking = true;
  }
}
