// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.UltraCapacitor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Enchantments;

public class UltraCapacitor(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IRebornBehavior
{
  public const string CardId = "BG31_HERO_801ptje";

  public RebornBehavior RebornBehavior
  {
    get => RebornBehavior.KeepEnchantments | RebornBehavior.KeepMaxHealth;
  }
}
