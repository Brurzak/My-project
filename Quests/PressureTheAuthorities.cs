// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.PressureTheAuthorities
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Quests;

public class PressureTheAuthorities(QuestData data, Simulator simulator, bool controlledByPlayer) : 
  Quest(data, simulator, controlledByPlayer),
  IOnFriendlyMinionAfterAttack,
  IEntity,
  IOnAfterAttackStep,
  IOnFriendlyMinionBuffed
{
  public const string CardId = "BG27_Quest_801";

  public void OnAfterAttackStep() => this.CheckTotalAttack();

  public Action? OnFrienlyMinionAfterAttack(Minion attacker, Minion target)
  {
    return (Action) (() => this.CheckTotalAttack());
  }

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() => this.CheckTotalAttack());
  }

  private void CheckTotalAttack()
  {
    if (this.IsComplete || this.FriendlySide.Count == 0)
      return;
    this.IncrementProgress(this.FriendlySide.Aggregate<Minion, int>(0, (Func<int, Minion, int>) ((sum, minion) => sum + minion.attack())) - this.Progress);
  }
}
