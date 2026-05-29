// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.TrinketFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;

#nullable enable
namespace BobsBuddy.Factory;

public class TrinketFactory(Simulator simulator) : EntityFactory<Trinket>(simulator)
{
  public Trinket Create(string cardId, bool controlledByPlayer)
  {
    EntityFactory<Trinket>.Constructor constructor;
    if (!EntityFactory<Trinket>.Constructors.TryGetValue(cardId, out constructor))
      return new Trinket(cardId, this._simulator, controlledByPlayer);
    return constructor((object) cardId, (object) this._simulator, (object) controlledByPlayer);
  }
}
