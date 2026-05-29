// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Summon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Factory;
using HearthDb;
using HearthDb.Enums;

#nullable enable
namespace BobsBuddy.Simulation;

public class Summon
{
  public string? CardId { get; }

  public Card? Card { get; }

  public Minion? Minion { get; }

  public bool MakeGolden { get; }

  public (int, int)? SetStats { get; set; }

  public bool GiveReborn { get; set; }

  public bool GiveTaunt { get; set; }

  public Summon(string cardId, bool makeGolden = false, bool giveReborn = false, bool giveTaunt = false)
  {
    this.CardId = cardId;
    Card card;
    if (Cards.All.TryGetValue(MinionFactory.GetFixedCardId(cardId), out card))
      this.Card = card;
    this.MakeGolden = makeGolden;
    this.GiveReborn = giveReborn;
    this.GiveTaunt = giveTaunt;
  }

  public Summon(Card card, bool makeGolden = false, bool giveReborn = false, bool giveTaunt = false)
  {
    this.Card = card;
    this.MakeGolden = makeGolden;
    this.GiveReborn = giveReborn;
    this.GiveTaunt = giveTaunt;
  }

  public Summon(Minion minion) => this.Minion = minion;

  public Race PrimaryRace
  {
    get
    {
      Card card = this.Card;
      if (card != null)
        return card.Race;
      Minion minion = this.Minion;
      return minion == null ? (Race) 25 : minion.PrimaryRace;
    }
  }

  public Race SecondayRace
  {
    get
    {
      Card card = this.Card;
      if (card != null)
        return card.SecondaryRace;
      Minion minion = this.Minion;
      return minion == null ? (Race) 25 : minion.SecondaryRace;
    }
  }

  public int Tier
  {
    get
    {
      Card card = this.Card;
      if (card != null)
        return card.TechLevel;
      Minion minion = this.Minion;
      return minion == null ? 1 : minion.tier;
    }
  }

  public static Summon FromCard(Card card) => new Summon(card);

  public static implicit operator Summon(Card card) => new Summon(card);

  public static Summon FromMinion(Minion minion) => new Summon(minion);

  public static implicit operator Summon(Minion minion) => new Summon(minion);

  public static Summon FromCardId(string cardId, bool makeGolden = false, bool giveReborn = false)
  {
    return new Summon(cardId, makeGolden, giveReborn);
  }

  public static implicit operator Summon(string cardId) => new Summon(cardId);
}
