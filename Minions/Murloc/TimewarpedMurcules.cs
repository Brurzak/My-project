// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.TimewarpedMurcules
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class TimewarpedMurcules(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG34_Giant_207";
  public const string Text = "<b>Divine Shield</b> Whenever this kills a minion, give the left-most minion in your hand +{0}/+{1}.";
  public const string GoldenText = "<b>Divine Shield</b> Whenever this kills a minion, give the left-most minion in your hand +{0}/+{1}.";

  public Action? OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() =>
    {
      if (friendly != this)
        return;
      MinionCardEntity minionCardEntity = this.FriendlyHandMinions(false).FirstOrDefault<MinionCardEntity>();
      if (minionCardEntity == null)
        return;
      int num = this.DoubleIfGolden(4);
      minionCardEntity.Data.IncreaseStats(num, num);
    });
  }
}
