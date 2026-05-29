// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.QuestReward
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class QuestReward(string cardId, Simulator simulator, bool controlledByPlayer) : Entity(cardId, simulator, controlledByPlayer)
{
  public int ScriptDataNum1 { get; set; }

  public int ScriptDataNum2 { get; set; }

  public QuestReward Clone(Simulator? simulator = null)
  {
    QuestReward questReward = (simulator ?? this.Simulator).questRewardFactory.Create(this.CardID, this.ControlledByPlayer);
    questReward.CloneFromBaseEntity((Entity) this);
    questReward.ScriptDataNum1 = this.ScriptDataNum1;
    questReward.ScriptDataNum2 = this.ScriptDataNum2;
    return questReward;
  }
}
