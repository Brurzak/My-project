// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Quest
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Quests;

public abstract class Quest : Entity
{
  private readonly QuestData _data;

  public Quest(QuestData data, Simulator simulator, bool controlledByPlayer)
    : base(data.QuestCardId, simulator, controlledByPlayer)
  {
    this._data = data;
    this.Progress = data.QuestProgress;
  }

  public int Progress { get; private set; }

  public bool IsComplete => this.Progress >= this._data.QuestProgressTotal;

  protected void IncrementProgress(int amount = 1)
  {
    if (this.IsComplete)
      return;
    this.Progress += amount;
    if (!this.IsComplete)
      return;
    this.Simulator.TrySummonQuestReward(this.Simulator.questRewardFactory.Create(this._data.RewardCardId, this.ControlledByPlayer), this.ControlledByPlayer);
  }

  public Quest Clone(Simulator? simulator = null)
  {
    Quest quest = (simulator ?? this.Simulator).questFactory.Create(this._data, this.ControlledByPlayer);
    quest.CloneFromBaseEntity((Entity) this);
    quest.Progress = this.Progress;
    return quest;
  }

  public override string ToString()
  {
    return $"[{this.CardID}, {this.Progress}/{this._data.QuestProgressTotal} - {this._data.RewardCardId}]";
  }
}
