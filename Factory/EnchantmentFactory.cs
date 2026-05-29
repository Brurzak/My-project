// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.EnchantmentFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Enchantments;
using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Factory;

public class EnchantmentFactory(Simulator simulator) : EntityFactory<Enchantment>(simulator)
{
  public Enchantment? Create(string cardId, bool controlledByPlayer)
  {
    EntityFactory<Enchantment>.Constructor constructor;
    if (!EntityFactory<Enchantment>.Constructors.TryGetValue(cardId, out constructor))
      return (Enchantment) null;
    return constructor((object) cardId, (object) this._simulator, (object) controlledByPlayer);
  }
}
