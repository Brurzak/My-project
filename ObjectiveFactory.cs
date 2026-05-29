// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.ObjectiveFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells;

#nullable enable
namespace BobsBuddy.Factory;

public class ObjectiveFactory(Simulator simulator) : EntityFactory<Objective>(simulator)
{
  public Objective Create(string cardId, bool controlledByPlayer)
  {
    EntityFactory<Objective>.Constructor constructor;
    if (!EntityFactory<Objective>.Constructors.TryGetValue(cardId, out constructor))
      return new Objective(cardId, this._simulator, controlledByPlayer);
    return constructor((object) cardId, (object) this._simulator, (object) controlledByPlayer);
  }
}
