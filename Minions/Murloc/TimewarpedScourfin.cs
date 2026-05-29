// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.TimewarpedScourfin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class TimewarpedScourfin(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_017";
  public const string Text = "<b><b>Taunt</b>.</b> <b>Deathrattle:</b> Give a random minion in your hand +{0}/+{1} and summon it for this combat only.";
  public const string GoldenText = "<b><b>Taunt</b>.</b> <b>Deathrattle:</b> Give a random minion in your hand +{0}/+{1} and summon it for this combat only.";

  public Action<Minion> GetDeathrattle() => TimewarpedScourfin.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int by = golden ? 14 : 7;
      MinionCardEntity minionCardEntity;
      if (!minion.FriendlyHandMinions(false).TryGetRandom<MinionCardEntity>(out minionCardEntity))
        return;
      minionCardEntity.Data.IncreaseStats(by);
      if (!minionCardEntity.CanSummon || minion.TrySummonMinion((Summon) minionCardEntity.Data.Clone()).Count <= 0)
        return;
      minionCardEntity.CanSummon = false;
    });
  }
}
