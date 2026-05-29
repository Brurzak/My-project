// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.Scourfin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class Scourfin(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_360";
  public const string Text = "<b>Deathrattle:</b> Give a random minion in your hand +{0}/+{1}.";
  public const string GoldenText = "<b>Deathrattle:</b> Give a random minion in your hand +{0}/+{1}.";

  public Action<Minion> GetDeathrattle() => Scourfin.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      MinionCardEntity minionCardEntity;
      if (!minion.FriendlyHandMinions(false).TryGetRandom<MinionCardEntity>(out minionCardEntity))
        return;
      int num = golden ? 14 : 7;
      minionCardEntity.Data.IncreaseStats(num, num);
    });
  }
}
