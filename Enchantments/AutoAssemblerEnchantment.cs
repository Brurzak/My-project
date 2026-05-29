// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.AutoAssemblerEnchantment
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Minions.Mech;
using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Enchantments;

public class AutoAssemblerEnchantment(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_172e";

  public Action<Minion>? GetDeathrattle() => AutoAssembler.Deathrattle(false);
}
