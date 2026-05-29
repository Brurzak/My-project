// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.NeedlingCrone
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class NeedlingCrone(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG32_432";
  public const string Text = "Your <b>Blood Gems</b> give twice their stats during combat.";
  public const string GoldenText = "Your <b>Blood Gems</b> give three times their stats during combat.";
}
