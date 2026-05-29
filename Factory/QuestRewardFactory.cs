// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.QuestRewardFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Quests.Rewards;
using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Factory;

public class QuestRewardFactory(Simulator simulator) : EntityFactory<QuestReward>(simulator)
{
  public QuestReward Create(string cardId, bool controlledByPlayer)
  {
    EntityFactory<QuestReward>.Constructor constructor;
    if (!EntityFactory<QuestReward>.Constructors.TryGetValue(cardId, out constructor))
      return new QuestReward(cardId, this._simulator, controlledByPlayer);
    return constructor((object) cardId, (object) this._simulator, (object) controlledByPlayer);
  }

  public QuestReward Create(
    string cardId,
    bool controlledByPlayer,
    int scriptDataNum1,
    int scriptDataNum2)
  {
    QuestReward questReward = this.Create(cardId, controlledByPlayer);
    questReward.ScriptDataNum1 = scriptDataNum1;
    questReward.ScriptDataNum2 = scriptDataNum2;
    return questReward;
  }
}
