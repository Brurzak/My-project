// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.VashjirVitality
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Enchantments;

public class VashjirVitality(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG32_MagicItem_932e";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() => this.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x == this.AttachedTo))?.IncreaseStats(0, this.ScriptDataNum1));
  }
}
