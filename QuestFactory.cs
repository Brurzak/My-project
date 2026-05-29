// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.QuestFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Quests;
using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Factory;

public class QuestFactory(Simulator simulator) : EntityFactory<Quest>(simulator)
{
  public Quest Create(QuestData data, bool controlledByPlayer)
  {
    EntityFactory<Quest>.Constructor constructor;
    if (!EntityFactory<Quest>.Constructors.TryGetValue(data.QuestCardId, out constructor))
      return (Quest) new UnknownQuest(data, this._simulator, controlledByPlayer);
    return constructor((object) data, (object) this._simulator, (object) controlledByPlayer);
  }
}
