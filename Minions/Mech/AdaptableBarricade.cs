// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.AdaptableBarricade
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class AdaptableBarricade(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacked,
  IEntity
{
  public const string CardId = "BG27_022";
  public const string Text = "<b><b>Taunt</b>.</b> Whenever this is attacked, it shifts its stats to survive with 1 Health <i>(if possible).</i>";
  public const string GoldenText = "<b><b>Taunt</b>.</b> Whenever this is attacked, it shifts its stats to survive with 1 Health <i>(if possible).</i>";

  public Action? OnFriendlyMinionIsAttacked(Minion friendlyMinion, Minion attacker)
  {
    return (Action) (() =>
    {
      if (friendlyMinion != this || !this.hasDiv && (attacker.poisonous || attacker.venomous))
        return;
      int num1 = this.attack() + this.health();
      if (!this.hasDiv && attacker.attack() >= num1)
        return;
      int num2 = (this.hasDiv ? 0 : attacker.attack()) + 1;
      this.IncreaseStats(num1 - num2 - this.attack(), num2 - this.health());
    });
  }
}
