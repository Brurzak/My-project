// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.CaduceusReactor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Enchantments;

public class CaduceusReactor(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG31_HERO_801ptee";

  public Action<Minion>? GetDeathrattle()
  {
    return (Action<Minion>) (minion => this.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.IsAlive()))?.IncreaseStats(this.ScriptDataNum1, this.ScriptDataNum1));
  }
}
