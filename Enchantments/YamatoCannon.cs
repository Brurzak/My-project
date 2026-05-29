// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.YamatoCannon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Enchantments;

public class YamatoCannon(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG31_HERO_801ptce";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion highestHealthMinion = this.Simulator.GetHighestHealthMinion(this.OpposingSide);
      if (highestHealthMinion == null)
        return;
      this.Simulator.ProcessDamage(this.ScriptDataNum1, highestHealthMinion, (Entity) this);
    });
  }
}
