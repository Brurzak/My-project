// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.IndomitableMount
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class IndomitableMount(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG30_105";
  public const string Text = "<b>Deathrattle:</b> Summon a random Beast from Tiers 2, 3, and 4.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a random Golden Beast from Tiers 2, 3, and 4.";
  private static readonly List<Card> _deathRattleOptions = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.Race == 20 || x.SecondaryRace == 20 || x.Race == 26 || x.SecondaryRace == 26)).ToList<Card>();
  private static readonly Dictionary<int, List<Card>> _deathRattleOptionsByTier = new Dictionary<int, List<Card>>()
  {
    [2] = IndomitableMount._deathRattleOptions.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 2)).ToList<Card>(),
    [3] = IndomitableMount._deathRattleOptions.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 3)).ToList<Card>(),
    [4] = IndomitableMount._deathRattleOptions.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 4)).ToList<Card>()
  };

  public Action<Minion> GetDeathrattle() => IndomitableMount.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Card card;
      if (IndomitableMount._deathRattleOptionsByTier[2].TryGetRandom<Card>(out card))
        minion.TrySummonMinion(new Summon(card.Id, golden));
      if (IndomitableMount._deathRattleOptionsByTier[3].TryGetRandom<Card>(out card))
        minion.TrySummonMinion(new Summon(card.Id, golden));
      if (!IndomitableMount._deathRattleOptionsByTier[4].TryGetRandom<Card>(out card))
        return;
      minion.TrySummonMinion(new Summon(card.Id, golden));
    });
  }
}
