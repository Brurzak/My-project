// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.ScarletSurvivor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class ScarletSurvivor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG35_814";
  public const string Text = "Once this reaches {0} Attack, gain <b>Divine Shield</b>.6Once this reaches {0} Attack, gain <b>Divine Shield</b>. <i>(Done!)</i>";
  public const string GoldenText = "Once this reaches {0} Attack, gain <b>Divine Shield</b>.6Once this reaches {0} Attack, gain <b>Divine Shield</b>. <i>(Done!)</i>";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (buffed != this || !this.IsAlive() || this.hasDiv || attackChange == 0 || this.ScriptDataNum2 > 0 || this.attack() < 6)
        return;
      this.div = 1;
      this.ScriptDataNum2 = 1;
    });
  }
}
