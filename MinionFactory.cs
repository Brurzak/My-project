// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.MinionFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace BobsBuddy.Factory;

public class MinionFactory : EntityFactory<Minion>
{
  public static List<string> cardIDsWithCleave = new List<string>()
  {
    "BG_LOOT_078",
    "BG_GVG_113",
    "BG26_817",
    "BG31_HERO_811t10",
    "BG34_Treasure_994",
    "BG34_Giant_680"
  };
  public static List<string> cardIdsWithMegaWindfury = new List<string>();
  public static List<string> cardIdsWithoutPremiumImplementations = new List<string>()
  {
    "BG_GVG_113",
    "ICC_038",
    "EX1_577",
    "BGS_034",
    "BG_LOOT_078",
    "BG_BOT_911",
    "BG_DAL_077",
    "NAX3_01",
    "BGS_040"
  };
  public static List<string> CardIdsPutricidePool1 = new List<string>()
  {
    "BG26_RLK_833",
    "BG28_300",
    "BG26_RLK_117",
    "BG_ICC_099",
    "BG_RLK_957",
    "BG26_RLK_824",
    "BG26_tt_004",
    "BG30_850",
    "BG_GIL_681",
    "BG33_115",
    "BG26_LOOT_534",
    "BG25_010",
    "BG25_005",
    "BG26_GIL_513",
    "BG25_003",
    "BG33_113",
    "BG33_111",
    "BG26_361",
    "BG28_306",
    "BG_LOE_012",
    "BG32_324",
    "BG_GIL_655",
    "BG25_014",
    "BG28_304",
    "BG26_ICC_065",
    "BG26_ICC_027",
    "BG25_009"
  };
  public static List<string> CardIdsPutricidePool2 = new List<string>()
  {
    "BG_ICC_092",
    "BG_EX1_059",
    "BG25_001",
    "BG_RLK_958",
    "BG26_OG_248",
    "BG_ULD_205",
    "BG_ICC_094",
    "BG_ULD_275",
    "BG26_ICC_026",
    "BG26_FP1_005",
    "BG26_RLK_119",
    "BG25_004",
    "BG26_CFM_636",
    "BG26_ICC_028",
    "BG_BT_703t",
    "BG_FP1_014t",
    "BG25_050",
    "BG_ICC_032",
    "BG26_ULD_274"
  };
  public static readonly Dictionary<Type, List<FieldInfo>> InternalFields = new Dictionary<Type, List<FieldInfo>>();

  public List<Card> BaconPoolMinionsFilteredLobbyRaces { get; private set; }

  public List<Card> BaconPoolMinionsFilteredLobbyRacesPlayerTier { get; private set; }

  public List<Card> BaconPoolMinionsFilteredLobbyRacesOpponentTier { get; private set; }

  static MinionFactory()
  {
    foreach (Type entityType in EntityFactory<Minion>.EntityTypes)
    {
      Type minion = entityType;
      List<FieldInfo> list = minion.GetRuntimeFields().Where<FieldInfo>((Func<FieldInfo, bool>) (x => x.DeclaringType == minion && !x.IsStatic)).ToList<FieldInfo>();
      if (list.Count > 0)
        MinionFactory.InternalFields[minion] = list;
    }
  }

  public MinionFactory(Simulator simulator)
    : base(simulator)
  {
    this.BaconPoolMinionsFilteredLobbyRaces = Cards.BaconPoolMinions.Values.ToList<Card>();
    this.BaconPoolMinionsFilteredLobbyRacesPlayerTier = this.BaconPoolMinionsFilteredLobbyRaces;
    this.BaconPoolMinionsFilteredLobbyRacesOpponentTier = this.BaconPoolMinionsFilteredLobbyRaces;
  }

  public void InitializeLobbyRaces(List<Race> availableRaces)
  {
    List<Card> list = this.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.Race == 26 || x.SecondaryRace == 26)).ToList<Card>();
    foreach (Race availableRace in availableRaces)
    {
      Race race = availableRace;
      list.AddRange(this.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.Race == race || x.SecondaryRace == race)));
    }
    this.BaconPoolMinionsFilteredLobbyRaces = new List<Card>((IEnumerable<Card>) new HashSet<Card>((IEnumerable<Card>) list));
  }

  public void InitializePlayerTiers(int playerTier, int opponentTier)
  {
    this.BaconPoolMinionsFilteredLobbyRacesPlayerTier = this.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.TechLevel <= playerTier)).ToList<Card>();
    this.BaconPoolMinionsFilteredLobbyRacesOpponentTier = this.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.TechLevel <= opponentTier)).ToList<Card>();
  }

  public List<Card> MinionPoolOptionsPlayer(bool controlledByPlayer)
  {
    return !controlledByPlayer ? this.BaconPoolMinionsFilteredLobbyRacesOpponentTier : this.BaconPoolMinionsFilteredLobbyRacesPlayerTier;
  }

  public List<Card> MinionPoolOptionsPlayerAndRace(bool controlledByPlayer, Race race)
  {
    return this.MinionPoolOptionsPlayer(controlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Race == race || x.SecondaryRace == race || x.Race == 26 || x.SecondaryRace == 26)).ToList<Card>();
  }

  internal static string GetFixedCardId(string cardId)
  {
    Card card;
    if (!Cards.All.TryGetValue(cardId, out card) || card.TechLevel == 0)
      cardId = "BG_" + cardId;
    return cardId;
  }

  public Minion CreateFromCardId(string cardId, bool controlledByPlayer)
  {
    string str = cardId;
    cardId = MinionFactory.GetFixedCardId(cardId);
    Card card;
    if (Cards.All.TryGetValue(cardId, out card))
      return this.CreateFromCard(card, controlledByPlayer);
    if (!EntityFactory<Minion>.Constructors.ContainsKey(str))
      return new Minion(cardId, controlledByPlayer, this._simulator);
    Minion fromCardId = this.Create(str, controlledByPlayer);
    if (fromCardId.baseHealth == 0)
      fromCardId.baseHealth = 1;
    return fromCardId;
  }

  public Minion Create(string cardId, bool controlledByPlayer)
  {
    string str;
    if (Cards.TripleToNormalCardIds.TryGetValue(cardId, out str))
      cardId = str;
    EntityFactory<Minion>.Constructor constructor;
    if (!EntityFactory<Minion>.Constructors.TryGetValue(cardId, out constructor))
      return new Minion(cardId, controlledByPlayer, this._simulator);
    return constructor((object) cardId, (object) controlledByPlayer, (object) this._simulator);
  }

  public Minion CreateFromCard(Card card, bool controlledByPlayer)
  {
    Minion fromCard = this.Create(card.Id, controlledByPlayer);
    fromCard.minionName = card.Name;
    fromCard.PrimaryRace = card.Race;
    fromCard.SecondaryRace = card.SecondaryRace;
    fromCard.baseAttack = card.Attack;
    fromCard.baseHealth = card.Health;
    fromCard.vanillaHealth = card.Health;
    fromCard.vanillaAttack = card.Attack;
    fromCard.maxAttack = card.Attack;
    fromCard.maxHealth = card.Health;
    fromCard.taunt = card.Taunt;
    fromCard.div = card.DivineShield ? 1 : 0;
    fromCard.cleave = MinionFactory.cardIDsWithCleave.Contains(fromCard.CardID);
    fromCard.poisonous = card.Poisonous;
    fromCard.venomous = card.Venomous;
    fromCard.windfury = card.Windfury;
    fromCard.stealth = card.Entity.GetTag((GameTag) 191) > 0;
    fromCard.megaWindfury = card.MegaWindfury || card.Entity.GetTag((GameTag) 189) > 1;
    fromCard.cannotAttack = card.CantAttack;
    fromCard.tier = card.TechLevel;
    fromCard.reborn = card.Reborn;
    fromCard.IsBuddy = card.Entity.GetTag((GameTag) 2154) == 1;
    fromCard.IsWhelp = card.Entity.GetTag((GameTag) 2355) > 0;
    return fromCard;
  }

  public static Minion CombineMinions(Minion minion1, Minion minion2)
  {
    Minion minion = minion1.Clone();
    minion.baseAttack += minion2.baseAttack;
    minion.baseHealth += minion2.baseHealth;
    minion.vanillaAttack += minion2.vanillaAttack;
    minion.vanillaHealth += minion2.vanillaHealth;
    minion.maxAttack += minion2.maxAttack;
    minion.maxHealth += minion2.maxHealth;
    minion.taunt |= minion2.taunt;
    minion.div = Math.Max(minion1.div, minion2.div);
    minion.cleave |= minion2.cleave;
    minion.poisonous |= minion2.poisonous;
    minion.venomous |= minion2.venomous;
    minion.windfury |= minion2.windfury;
    minion.stealth |= minion2.stealth;
    minion.megaWindfury |= minion2.megaWindfury;
    minion.cannotAttack |= minion2.cannotAttack;
    minion.reborn |= minion2.reborn;
    return minion;
  }

  public static string TryGetPremiumIdFromNormal(string normalId) => Cards.TryGetTripleId(normalId);

  public static bool HasGoldenCardId(string cardId)
  {
    return Cards.NormalToTripleCardIds.ContainsKey(cardId);
  }
}
