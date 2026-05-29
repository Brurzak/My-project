// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Tarecgosa
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class Tarecgosa(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG21_015";
  public const string Text = "This permanently keeps <b><b>Bonus Keyword</b>s</b> and stats gained in combat.";
  public const string GoldenText = "This permanently keeps <b><b>Bonus Keyword</b>s</b> and double stats gained in combat.";

  public override void IncreaseStats(int attackBuff, int healthBuff, Entity? source)
  {
    base.IncreaseStats(this.golden ? attackBuff * 2 : attackBuff, this.golden ? healthBuff * 2 : healthBuff);
  }
}
