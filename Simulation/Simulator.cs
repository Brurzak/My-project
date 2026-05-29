// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Simulator
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Anomalies;
using BobsBuddy.Enchantments;
using BobsBuddy.Factory;
using BobsBuddy.HeroPowers;
using BobsBuddy.Minions.Beast;
using BobsBuddy.Quests;
using BobsBuddy.Quests.Rewards;
using BobsBuddy.Spells;
using BobsBuddy.Spells.SpellcraftSpells;
using BobsBuddy.Spells.TavernSpells;
using BobsBuddy.Trinkets;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Simulation;

public class Simulator
{
  private readonly Stack<Minion> _attackTargets = new Stack<Minion>();
  public readonly MinionFactory MinionFactory;
  public readonly TrinketFactory TrinketFactory;
  public readonly QuestFactory questFactory;
  public readonly QuestRewardFactory questRewardFactory;
  public readonly ObjectiveFactory ObjectiveFactory;
  public readonly AnomalyFactory AnomalyFactory;
  public readonly EnchantmentFactory EnchantmentFactory;
  public List<Race>? availableRaces;
  private static readonly HashSet<int> _defaultAvailableTiers = new HashSet<int>()
  {
    1,
    2,
    3,
    4,
    5,
    6
  };
  public CloningGallery playerCloningGallery = new CloningGallery();
  public CloningGallery opponentCloningGallery = new CloningGallery();
  public Player? PlayerTeammateInput;
  public Player? OpponentTeammateInput;
  public int DamageCap;
  internal GameState state = new GameState((Input) null);
  internal Input Input = new Input();
  internal Anomaly? Anomaly;
  public bool friendlyLost;
  public bool opponentLost;
  private static readonly List<Summon> _snakeSummons = new List<Summon>()
  {
    (Summon) "EX1_554t",
    (Summon) "EX1_554t",
    (Summon) "EX1_554t"
  };

  internal void AttackWithMinion(Minion attacker, Minion? target = null, bool preserveTarget = false)
  {
    int num1 = 0;
    while (num1 < 4)
    {
      int num2 = this.MinionHasMegaWindfury(attacker) ? 4 : (attacker.windfury ? 2 : 1);
      if (num1 >= num2)
        break;
      if (num1 > 0 && target != null && !preserveTarget)
        target = (Minion) null;
      using (this.state.TriggerScope.New($"ActiveMinion minion={attacker}"))
      {
        if (!this.PerformAttack(attacker, target))
          break;
        ++num1;
        this.ResolveTriggersInCurrentScope();
        this.ResolveReborn();
        this.ResolveAllDeathsAndTriggers(true);
      }
      foreach (Entity allEntity in this.GetAllEntities(attacker.FriendlySide))
      {
        if (allEntity is IOnAfterAttackStep onAfterAttackStep)
          onAfterAttackStep.OnAfterAttackStep();
      }
      foreach (Entity allEntity in this.GetAllEntities(attacker.OpposingSide))
      {
        if (allEntity is IOnAfterAttackStep onAfterAttackStep)
          onAfterAttackStep.OnAfterAttackStep();
      }
    }
  }

  public Minion? CurrentAttackTarget
  {
    get => this._attackTargets.Count <= 0 ? (Minion) null : this._attackTargets.Peek();
  }

  private bool PerformAttack(Minion attacker, Minion? target = null)
  {
    if (!attacker.IsAlive())
      return false;
    if (target == null)
      target = attacker.ChooseAttackTarget();
    if (target == null || !target.IsAlive())
      return false;
    this._attackTargets.Push(target);
    if (attacker.stealth)
    {
      attacker.stealth = false;
      List<Trigger> triggerList = new List<Trigger>();
      foreach (Entity friendlyEntity in attacker.FriendlyEntities)
      {
        if (friendlyEntity is IOnFriendlyMinionLostStealth minionLostStealth)
          triggerList.TryAdd<Trigger>((Trigger) (minionLostStealth.OnFriendlyMinionLostStealth(attacker), (IEntity) attacker));
      }
      if (triggerList.Any<Trigger>())
      {
        using (this.state.SummonScope.New("StealthTriggers"))
          this.ResolveTriggers(triggerList);
      }
    }
    int val1 = 1;
    int num1 = 0;
    foreach (Minion minion in attacker.FriendlySide)
    {
      if (minion.CardID == "BG34_Giant_376")
        val1 = Math.Max(val1, minion.golden ? 3 : 2);
    }
    foreach (Entity friendlyQuestReward in attacker.FriendlyQuestRewards)
    {
      if (friendlyQuestReward.CardID == "BG33_Reward_021")
        ++num1;
    }
    int num2 = val1 + num1;
    this.OnAttackSecretCheck(target);
    foreach (Entity friendlyEntity in attacker.FriendlyEntities)
    {
      if ((!(friendlyEntity is Minion minion) || minion.IsAlive()) && friendlyEntity is IOnFriendlyMinionIsAttacking minionIsAttacking)
        this.RegisterTrigger(minionIsAttacking.OnFriendlyMinionIsAttacking(attacker, target), (IEntity) friendlyEntity);
    }
    foreach (IOnAttack trigger in attacker.GetTriggers<IOnAttack>())
      this.RegisterTrigger(trigger.OnAttack(target), (IEntity) trigger);
    foreach (IOnRally trigger in attacker.GetTriggers<IOnRally>())
    {
      IOnRally t = trigger;
      for (int index = 0; index < num2; ++index)
        this.RegisterTrigger((Action) (() =>
        {
          Action<Minion> action = t.OnRally(attacker.golden, target);
          if (action == null)
            return;
          action(attacker);
        }), (IEntity) t);
    }
    foreach (Action<Minion> additionalRally in attacker.AdditionalRallies)
    {
      Action<Minion> r = additionalRally;
      for (int index = 0; index < num2; ++index)
        this.RegisterTrigger((Action) (() => r(attacker)), (IEntity) attacker);
    }
    this.RegisterTrigger(target.OnIsAttacked(attacker), (IEntity) target);
    foreach (Entity friendlyEntity in target.FriendlyEntities)
    {
      if ((!(friendlyEntity is Minion minion) || minion.IsAlive()) && friendlyEntity is IOnFriendlyMinionIsAttacked minionIsAttacked)
        this.RegisterTrigger(minionIsAttacked.OnFriendlyMinionIsAttacked(target, attacker), (IEntity) friendlyEntity);
    }
    this.ResolveTriggersInCurrentScope();
    this.ResolveAllDeathsAndTriggers(true);
    if (target.IsAlive())
    {
      int amount = attacker.attack();
      List<Damage> damageGroup = new List<Damage>()
      {
        new Damage(amount, target, (Entity) attacker)
      };
      if (attacker.cleave)
      {
        Minion leftNeighbor = target.GetLeftNeighbor();
        if (leftNeighbor != null)
          damageGroup.Add(new Damage(amount, leftNeighbor, (Entity) attacker));
        Minion rightNeighbor = target.GetRightNeighbor();
        if (rightNeighbor != null)
          damageGroup.Add(new Damage(amount, rightNeighbor, (Entity) attacker));
      }
      if (attacker.ImmuneWhileAttackingCount > 0)
        --attacker.ImmuneWhileAttackingCount;
      else if (!attacker.ImmuneWhileAttacking)
        damageGroup.Add(new Damage(target.attack(), attacker, (Entity) target));
      this.ProcessDamage((IEnumerable<Damage>) damageGroup);
    }
    if (attacker is IOnAfterAttack onAfterAttack)
      this.RegisterTrigger(onAfterAttack.OnAfterAttack(target), (IEntity) attacker);
    if (attacker.AttachedModularEntity is IOnAfterAttack attachedModularEntity)
      this.RegisterTrigger(attachedModularEntity.OnAfterAttack(target), (IEntity) attacker.AttachedModularEntity);
    this.RegisterTrigger(target.OnWasAttacked(), (IEntity) attacker);
    foreach (Entity friendlyEntity in attacker.FriendlyEntities)
    {
      if ((!(friendlyEntity is Minion minion) || minion.IsAlive()) && friendlyEntity is IOnFriendlyMinionAfterAttack minionAfterAttack)
        this.RegisterTrigger(minionAfterAttack.OnFrienlyMinionAfterAttack(attacker, target), (IEntity) friendlyEntity);
    }
    this._attackTargets.Pop();
    return true;
  }

  private bool IsLastAttackerOnSide(Minion minion)
  {
    Minion minion1 = minion.FriendlySide == this.playerSide ? this.state.Player.SideLastAttacker : this.state.Opponent.SideLastAttacker;
    return minion == minion1;
  }

  private bool MinionHasMegaWindfury(Minion minion)
  {
    if (minion.megaWindfury)
      return true;
    if (minion.windfury)
    {
      foreach (Entity entity in minion.FriendlySide)
      {
        if (entity.CardID == "DAL_742")
          return true;
      }
    }
    return false;
  }

  public void TriggerRally(Minion minion)
  {
    IOnRally m = minion as IOnRally;
    if (m == null)
      return;
    int val1 = 1;
    int num1 = 0;
    foreach (Minion minion1 in minion.FriendlySide)
    {
      if (minion1.CardID == "BG34_Giant_376")
        val1 = Math.Max(val1, minion1.golden ? 3 : 2);
    }
    foreach (Entity friendlyQuestReward in minion.FriendlyQuestRewards)
    {
      if (friendlyQuestReward.CardID == "BG33_Reward_021")
        ++num1;
    }
    int num2 = val1 + num1;
    for (int index = 0; index < num2; ++index)
    {
      Minion target;
      if (!m.OpposingSide.TryGetRandom<Minion>(out target))
        break;
      this.RegisterTrigger((Action) (() =>
      {
        Action<Minion> action = m.OnRally(minion.golden, target);
        if (action == null)
          return;
        action(minion);
      }), (IEntity) minion);
      this.ResolveTriggersInCurrentScope();
    }
  }

  public HashSet<int> AvailableTiers
  {
    get
    {
      return this.Anomaly is IAvailableTiersOverride anomaly ? anomaly.AvailableTiersOverride : Simulator._defaultAvailableTiers;
    }
  }

  public GameState.PlayerState PlayerState => this.state.Player;

  public GameState.PlayerState OpponentState => this.state.Opponent;

  public Player PlayerInput { get; private set; } = new Player((Input) null);

  public Player OpponentInput { get; private set; } = new Player((Input) null);

  public int Turn { get; private set; }

  public Simulator()
  {
    this.MinionFactory = new MinionFactory(this);
    this.TrinketFactory = new TrinketFactory(this);
    this.questFactory = new QuestFactory(this);
    this.questRewardFactory = new QuestRewardFactory(this);
    this.ObjectiveFactory = new ObjectiveFactory(this);
    this.AnomalyFactory = new AnomalyFactory(this);
    this.EnchantmentFactory = new EnchantmentFactory(this);
  }

  public List<Minion> playerSide => this.state.Player.Side;

  public List<Minion> opponentSide => this.state.Opponent.Side;

  public bool eitherSideWon => this.playerSide.Count == 0 || this.opponentSide.Count == 0;

  public bool noValidAttackers
  {
    get
    {
      return !this.playerSide.Any<Minion>((Func<Minion, bool>) (x => x.IsValidAttacker())) && !this.opponentSide.Any<Minion>((Func<Minion, bool>) (x => x.IsValidAttacker()));
    }
  }

  internal void SetupSimulation(Input input)
  {
    this.state = new GameState(input);
    this.DamageCap = input.DamageCap;
    this.Turn = input.turn;
    this.Input = input;
    this.Anomaly = input.Anomaly?.Clone(this);
    if (input.availableRaces.Any<Race>())
    {
      this.availableRaces = input.availableRaces;
      this.MinionFactory.InitializeLobbyRaces(input.availableRaces);
    }
    this.MinionFactory.InitializePlayerTiers(input.Player.Tier, input.Opponent.Tier);
    this.PlayerInput = input.Player;
    this.OpponentInput = input.Opponent;
    if (!input.isDuos)
      return;
    this.PlayerTeammateInput = input.PlayerTeammate;
    this.OpponentTeammateInput = input.OpponentTeammate;
  }

  internal void SetupPlayerCloningGallery(
    Player inputPlayer,
    GameState.PlayerState playerState,
    CloningGallery cloningGallery,
    bool friendly)
  {
    cloningGallery.Secrets.Clear();
    cloningGallery.Secrets.AddRange((IEnumerable<Simulator.Secrets>) inputPlayer.Secrets);
    cloningGallery.Minions.Clear();
    foreach (Minion minion in inputPlayer.Side)
      cloningGallery.Minions.Add(minion.Clone(this));
    cloningGallery.Quests.Clear();
    cloningGallery.Quests.AddRange(inputPlayer.Quests.Select<QuestData, Quest>((Func<QuestData, Quest>) (x => this.questFactory.Create(x, friendly))));
    cloningGallery.QuestRewards.Clear();
    cloningGallery.QuestRewards.AddRange(inputPlayer.Quests.Select<QuestData, QuestReward>((Func<QuestData, QuestReward>) (x => this.questRewardFactory.Create(x.RewardCardId, friendly, x.RewardScriptDataNum1, x.RewardScriptDataNum2))));
    cloningGallery.Objectives.Clear();
    cloningGallery.Objectives.AddRange(inputPlayer.Objectives.Select<Objective, Objective>((Func<Objective, Objective>) (x => x.Clone(this))));
    cloningGallery.Trinkets.Clear();
    cloningGallery.Trinkets.AddRange(inputPlayer.Trinkets.Select<Trinket, Trinket>((Func<Trinket, Trinket>) (x => x.Clone(this))));
    cloningGallery.Hand.Clear();
    cloningGallery.Hand.AddRange(inputPlayer.Hand.Select<CardEntity, CardEntity>((Func<CardEntity, CardEntity>) (x => x.Clone(x.Source, this))));
    playerState.GlobalModifier = new GlobalModifier(this, friendly);
    playerState.GlobalModifier.InitalizeStartingBonusValues(inputPlayer.UndeadAttackBonus, inputPlayer.BeastAttackBonus, inputPlayer.BeastHealthBonus, inputPlayer.WhelpAttackBonus, inputPlayer.WhelpHealthBonus, inputPlayer.HauntedAtkBuff, inputPlayer.HauntedHealthBuff);
    playerState.Side.AddRange((IEnumerable<Minion>) cloningGallery.Minions);
    playerState.Objectives.AddRange((IEnumerable<Objective>) cloningGallery.Objectives);
    playerState.Trinkets.AddRange((IEnumerable<Trinket>) cloningGallery.Trinkets);
    playerState.Quests.AddRange((IEnumerable<Quest>) cloningGallery.Quests);
    playerState.HeroPowers.AddRange(inputPlayer.HeroPowers.Select<HeroPowerData, HeroPower>((Func<HeroPowerData, HeroPower>) (x => this.GetHeroPower(x, friendly))).Where<HeroPower>((Func<HeroPower, bool>) (x => x != null)).Cast<HeroPower>());
    for (int index = 0; index < playerState.Quests.Count; ++index)
    {
      if (playerState.Quests[index].IsComplete && cloningGallery.QuestRewards.Count > index)
        playerState.QuestRewards.Add(cloningGallery.QuestRewards[index].Clone());
    }
  }

  internal void ResetPlayerStateAndUnbuffCloningGallery(
    GameState.PlayerState playerState,
    CloningGallery cloningGallery)
  {
    this.RemovePassiveEffectsSide((IReadOnlyList<Minion>) cloningGallery.Minions);
    playerState.Hand.Clear();
    playerState.Side.Clear();
    playerState.Quests.Clear();
    playerState.QuestRewards.Clear();
    playerState.Objectives.Clear();
    playerState.Trinkets.Clear();
    playerState.HeroPowers.Clear();
    foreach (Minion minion in cloningGallery.Minions)
    {
      minion.maxAttack = minion.baseAttack;
      minion.maxHealth = minion.baseHealth;
    }
  }

  public void SetupPlayerBoard(
    Player inputPlayer,
    GameState.PlayerState playerState,
    CloningGallery cloningGallery,
    bool friendly)
  {
    playerState.GlobalModifier = new GlobalModifier(this, friendly);
    playerState.GlobalModifier.InitalizeStartingBonusValues(inputPlayer.UndeadAttackBonus, inputPlayer.BeastAttackBonus, inputPlayer.BeastHealthBonus, inputPlayer.WhelpAttackBonus, inputPlayer.WhelpHealthBonus, inputPlayer.HauntedAtkBuff, inputPlayer.HauntedHealthBuff);
    playerState.Tier = inputPlayer.Tier;
    playerState.Health = inputPlayer.Health;
    playerState.Secrets.AddRange((IEnumerable<Simulator.Secrets>) cloningGallery.Secrets);
    playerState.Side.AddRange(cloningGallery.Minions.Select<Minion, Minion>((Func<Minion, Minion>) (x => x.CloneAsOriginal())));
    playerState.Hand.AddRange(cloningGallery.Hand.Select<CardEntity, CardEntity>((Func<CardEntity, CardEntity>) (x => x.Clone(x.Source))));
    if (friendly)
    {
      foreach (CardEntity cardEntity in playerState.Hand)
      {
        if (cardEntity is MinionCardEntity minionCardEntity)
          this.RemovePassiveEffects(minionCardEntity.Data);
      }
    }
    playerState.Quests.AddRange(cloningGallery.Quests.Select<Quest, Quest>((Func<Quest, Quest>) (x => x.Clone())));
    for (int index = 0; index < playerState.Quests.Count; ++index)
    {
      if (playerState.Quests[index].IsComplete && cloningGallery.QuestRewards.Count > index)
        playerState.QuestRewards.Add(cloningGallery.QuestRewards[index].Clone());
    }
    playerState.Objectives.AddRange(cloningGallery.Objectives.Select<Objective, Objective>((Func<Objective, Objective>) (x => x.Clone())));
    playerState.Trinkets.AddRange(cloningGallery.Trinkets.Select<Trinket, Trinket>((Func<Trinket, Trinket>) (x => x.Clone())));
    playerState.HeroPowers.AddRange(inputPlayer.HeroPowers.Select<HeroPowerData, HeroPower>((Func<HeroPowerData, HeroPower>) (x => this.GetHeroPower(x, friendly))).Where<HeroPower>((Func<HeroPower, bool>) (x => x != null)).Cast<HeroPower>());
  }

  public void SetupFight(Input input)
  {
    this.state = new GameState(input);
    this.SetupPlayerCloningGallery(input.Player, this.PlayerState, this.playerCloningGallery, true);
    this.SetupPlayerCloningGallery(input.Opponent, this.OpponentState, this.opponentCloningGallery, false);
    this.ResetPlayerStateAndUnbuffCloningGallery(this.PlayerState, this.playerCloningGallery);
    this.ResetPlayerStateAndUnbuffCloningGallery(this.OpponentState, this.opponentCloningGallery);
    this.SetupPlayerBoard(input.Player, this.state.Player, this.playerCloningGallery, true);
    this.SetupPlayerBoard(input.Opponent, this.state.Opponent, this.opponentCloningGallery, false);
    this.ResolveStartOfCombatEffects();
    this.state.IsStartOfCombatResolved = true;
    foreach (Minion minion in this.playerSide.Concat<Minion>((IEnumerable<Minion>) this.opponentSide))
    {
      if (minion.Simulator != this)
        throw new BadCloneException((Entity) minion);
    }
  }

  public void SetupFight(
    Player inputPlayer,
    GameState.PlayerState playerState,
    CloningGallery cloningGallery,
    bool friendly,
    bool resolveEffects = true)
  {
    this.state.IsStartOfCombatResolved = false;
    this.state.SetPlayerCounters(playerState, inputPlayer);
    this.SetupPlayerCloningGallery(inputPlayer, playerState, cloningGallery, friendly);
    this.ResetPlayerStateAndUnbuffCloningGallery(playerState, cloningGallery);
    this.SetupPlayerBoard(inputPlayer, playerState, cloningGallery, friendly);
    if (!resolveEffects)
      return;
    this.ResolveStartOfCombatEffects(playerState, friendly);
    this.state.IsStartOfCombatResolved = true;
  }

  private HeroPower? GetHeroPower(HeroPowerData data, bool isPlayer)
  {
    data = data.Clone(this);
    string cardId = data.CardId;
    HeroPower heroPower;
    if (cardId != null)
    {
      switch (cardId.Length)
      {
        case 14:
          switch (cardId[12])
          {
            case '0':
              switch (cardId)
              {
                case "BG20_HERO_100p":
                  heroPower = (HeroPower) new RokaraHeroPower("BG20_HERO_100p", this, isPlayer, data);
                  goto label_46;
                case "BG22_HERO_200p":
                  heroPower = (HeroPower) new IniStormcoilHeroPower("BG22_HERO_200p", this, isPlayer, data);
                  goto label_46;
              }
              break;
            case '1':
              if (cardId == "BG23_HERO_201p")
              {
                heroPower = (HeroPower) new OzumatHeroPower("BG23_HERO_201p", this, isPlayer, data);
                goto label_46;
              }
              break;
            case '2':
              switch (cardId)
              {
                case "BG22_HERO_002p":
                  heroPower = (HeroPower) new DrekTharHeroPower("BG22_HERO_002p", this, isPlayer, data);
                  goto label_46;
                case "BG20_HERO_282p":
                  heroPower = (HeroPower) new TamsinRoameHeroPower("BG20_HERO_282p", this, isPlayer, data);
                  goto label_46;
              }
              break;
            case '3':
              switch (cardId)
              {
                case "BG22_HERO_003p":
                  heroPower = (HeroPower) new VanndarStormpikeHeroPower("BG22_HERO_003p", this, isPlayer, data);
                  goto label_46;
                case "BG25_HERO_103p":
                  heroPower = (HeroPower) new TeronGorefiendHeroPower("BG25_HERO_103p", this, isPlayer, data);
                  goto label_46;
              }
              break;
            case '5':
              if (cardId == "BG22_HERO_305p")
              {
                heroPower = (HeroPower) new OnyxiaHeroPower("BG22_HERO_305p", this, isPlayer, data);
                goto label_46;
              }
              break;
          }
          break;
        case 15:
          if (cardId == "BGDUO_HERO_101p")
          {
            heroPower = (HeroPower) new FlobbidinousFloopHeroPower("BGDUO_HERO_101p", this, isPlayer, data);
            goto label_46;
          }
          break;
        case 17:
          switch (cardId[16 /*0x10*/])
          {
            case '1':
              switch (cardId)
              {
                case "BG22_HERO_000p_t1":
                  heroPower = (HeroPower) new TavishAimLeftHeroPower("BG22_HERO_000p_t1", this, isPlayer, data);
                  goto label_46;
                case "BG22_HERO_001p_t1":
                  heroPower = (HeroPower) new BrukanEarthHeroPower("BG22_HERO_001p_t1", this, isPlayer, data);
                  goto label_46;
              }
              break;
            case '2':
              switch (cardId)
              {
                case "BG22_HERO_000p_t2":
                  heroPower = (HeroPower) new TavishAimLowHeroPower("BG22_HERO_000p_t2", this, isPlayer, data);
                  goto label_46;
                case "BG22_HERO_001p_t2":
                  heroPower = (HeroPower) new BrukanFireHeroPower("BG22_HERO_001p_t2", this, isPlayer, data);
                  goto label_46;
              }
              break;
            case '3':
              switch (cardId)
              {
                case "BG22_HERO_000p_t3":
                  heroPower = (HeroPower) new TavishAimHighHeroPower("BG22_HERO_000p_t3", this, isPlayer, data);
                  goto label_46;
                case "BG22_HERO_001p_t3":
                  heroPower = (HeroPower) new BrukanWaterHeroPower("BG22_HERO_001p_t3", this, isPlayer, data);
                  goto label_46;
              }
              break;
            case '4':
              switch (cardId)
              {
                case "BG22_HERO_000p_t4":
                  heroPower = (HeroPower) new TavishAimRightHeroPower("BG22_HERO_000p_t4", this, isPlayer, data);
                  goto label_46;
                case "BG22_HERO_001p_t4":
                  heroPower = (HeroPower) new BrukanLightningHeroPower("BG22_HERO_001p_t4", this, isPlayer, data);
                  goto label_46;
              }
              break;
          }
          break;
        case 18:
          if (cardId == "BG22_HERO_000p_Alt")
          {
            heroPower = (HeroPower) new TavishLockAndLoadHeroPower("BG22_HERO_000p_Alt", this, isPlayer, data);
            goto label_46;
          }
          break;
        case 19:
          switch (cardId[17])
          {
            case '0':
              if (cardId == "TB_BaconShop_HP_103")
              {
                heroPower = (HeroPower) new YShaarjHeroPower("TB_BaconShop_HP_103", this, isPlayer, data);
                goto label_46;
              }
              break;
            case '5':
              if (cardId == "TB_BaconShop_HP_053")
              {
                heroPower = (HeroPower) new RafaamHeroPower("TB_BaconShop_HP_053", this, isPlayer, data);
                goto label_46;
              }
              break;
            case '6':
              switch (cardId)
              {
                case "TB_BaconShop_HP_061":
                  heroPower = (HeroPower) new DeathwingHeroPower("TB_BaconShop_HP_061", this, isPlayer, data);
                  goto label_46;
                case "TB_BaconShop_HP_069":
                  heroPower = (HeroPower) new IllidanHeroPower("TB_BaconShop_HP_069", this, isPlayer, data);
                  goto label_46;
              }
              break;
            case '8':
              if (cardId == "TB_BaconShop_HP_086")
              {
                heroPower = (HeroPower) new AlAkirHeroPower("TB_BaconShop_HP_086", this, isPlayer, data);
                goto label_46;
              }
              break;
          }
          break;
        case 20:
          if (cardId == "TB_BaconShop_HP_037a")
          {
            heroPower = (HeroPower) new QueenTogwaggleHeroPower("TB_BaconShop_HP_037a", this, isPlayer, data);
            goto label_46;
          }
          break;
      }
    }
    heroPower = new HeroPower(data.CardId, this, isPlayer, data);
label_46:
    return heroPower;
  }

  public Output SimulateForInput(Input input, int iterations, int maxDuration = 1500)
  {
    this.SetupSimulation(input);
    List<int> results = new List<int>();
    DateTime now = DateTime.Now;
    Simulator.ExitConditions exitConditions = Simulator.ExitConditions.CompletedSimulations;
    for (int index = 0; index < iterations; ++index)
    {
      int num = this.SimulateFight(input);
      results.Add(num);
      if (DateTime.Now.Subtract(now).TotalMilliseconds > (double) maxDuration)
      {
        exitConditions = Simulator.ExitConditions.Time;
        break;
      }
    }
    Output output = new Output((IEnumerable<int>) results, input.Player.Health, input.Opponent.Health);
    output.myExitCondition = exitConditions;
    output.PullStatsFromResult();
    return output;
  }

  private void Fight()
  {
    for (int index = 0; index < 500 && (this.Input.isDuos || !this.noValidAttackers && !this.eitherSideWon) && (!this.Input.isDuos || !this.noValidAttackers || !this.friendlyLost || !this.opponentLost); ++index)
      this.OneRound();
  }

  private void SetupDuoDuringRound()
  {
    if (!this.Input.isDuos || !this.eitherSideWon)
      return;
    if (this.playerSide.Count == 0 && !this.friendlyLost && this.opponentSide.Count == 0 && !this.opponentLost)
    {
      this.friendlyLost = true;
      this.opponentLost = true;
      if (this.Input.PlayerTeammate != null)
      {
        this.SyncHealth(this.Input.Player, this.Input.PlayerTeammate);
        this.SetupFight(this.Input.PlayerTeammate, this.state.Player, this.playerCloningGallery, true, false);
      }
      if (this.Input.OpponentTeammate != null)
      {
        this.SyncHealth(this.Input.Opponent, this.Input.OpponentTeammate);
        this.SetupFight(this.Input.OpponentTeammate, this.state.Opponent, this.opponentCloningGallery, false, false);
      }
      this.ResolveStartOfCombatEffects();
      this.state.IsStartOfCombatResolved = true;
    }
    if (this.playerSide.Count == 0 && !this.friendlyLost)
    {
      this.friendlyLost = true;
      if (this.Input.PlayerTeammate != null)
      {
        this.SyncHealth(this.Input.Player, this.Input.PlayerTeammate);
        this.SetupFight(this.Input.PlayerTeammate, this.state.Player, this.playerCloningGallery, true);
      }
    }
    if (this.opponentSide.Count == 0 && !this.opponentLost)
    {
      this.opponentLost = true;
      if (this.Input.OpponentTeammate != null)
      {
        this.SyncHealth(this.Input.Opponent, this.Input.OpponentTeammate);
        this.SetupFight(this.Input.OpponentTeammate, this.state.Opponent, this.opponentCloningGallery, false);
      }
    }
    if (this.opponentSide.Count == 0 && this.opponentLost && this.playerSide.Count > 0 && !this.friendlyLost)
    {
      int num = 7 - this.PlayerState.Side.Count;
      if (this.PlayerTeammateInput != null)
        this.PlayerState.Side.AddRange(this.PlayerTeammateInput.Side.GetRange(0, num > this.PlayerTeammateInput.Side.Count ? this.PlayerTeammateInput.Side.Count : num).Select<Minion, Minion>((Func<Minion, Minion>) (x => x.Clone())));
    }
    if (this.playerSide.Count != 0 || !this.friendlyLost || this.opponentSide.Count <= 0 || this.opponentLost)
      return;
    int num1 = 7 - this.OpponentState.Side.Count;
    if (this.OpponentTeammateInput == null)
      return;
    this.OpponentState.Side.AddRange(this.OpponentTeammateInput.Side.GetRange(0, num1 > this.OpponentTeammateInput.Side.Count ? this.OpponentTeammateInput.Side.Count : num1).Select<Minion, Minion>((Func<Minion, Minion>) (x => x.Clone())));
  }

  private void SyncHealth(Player inputPlayer, Player inputPlayerTeammate)
  {
    int num = Math.Min(inputPlayer.Health, inputPlayerTeammate.Health);
    if (num <= 0)
      num = Math.Max(inputPlayer.Health, inputPlayerTeammate.Health);
    inputPlayer.Health = num;
    inputPlayerTeammate.Health = num;
  }

  private int SimulateFight(Input input)
  {
    if (input.isDuos)
    {
      this.friendlyLost = false;
      this.opponentLost = false;
    }
    this.SetupFight(input);
    this.Fight();
    int num = 0;
    if (this.state.Player.Health <= 0 && input.Player.Health > 0)
      return -(input.Player.Health - this.state.Player.Health);
    if (this.state.Opponent.Health <= 0 && input.Opponent.Health > 0)
      return input.Opponent.Health - this.state.Opponent.Health;
    if (this.eitherSideWon)
    {
      foreach (Minion minion in this.playerSide)
        num += minion.tier;
      foreach (Minion minion in this.opponentSide)
        num -= minion.tier;
      if (num > 0)
        num += this.state.Player.Tier;
      else if (num < 0)
        num -= this.state.Opponent.Tier;
      if (this.DamageCap > 0)
      {
        if (num > this.DamageCap)
          num = this.DamageCap;
        else if (num < -this.DamageCap)
          num = -this.DamageCap;
      }
    }
    return num;
  }

  public void OneRound()
  {
    if (this.state.ActiveSide == null)
      throw new Exception("activeSide must be defined");
    this.CheckForStealth();
    if (!this.state.ActiveSide.Any<Minion>((Func<Minion, bool>) (x => x.IsValidAttacker())))
      this.state.ActiveSide = this.state.ActiveSide == this.playerSide ? (IReadOnlyList<Minion>) this.opponentSide : (IReadOnlyList<Minion>) this.playerSide;
    if (!this.state.ActiveSide.Any<Minion>((Func<Minion, bool>) (x => x.IsValidAttacker())))
    {
      if (!this.Input.isDuos || this.friendlyLost && this.opponentLost)
        return;
      bool flag = this.playerSide.Count > 0;
      if (!this.friendlyLost & this.opponentSide.Count > 0)
        this.playerSide.Clear();
      if (!this.opponentLost & flag)
        this.opponentSide.Clear();
      this.SetupDuoDuringRound();
    }
    else
    {
      Minion attacker;
      for (attacker = this.state.ActiveSide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => !x.hasAttacked)); attacker != null && !attacker.IsValidAttacker(); attacker = this.state.ActiveSide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => !x.hasAttacked)))
        attacker.hasAttacked = true;
      if (attacker == null)
      {
        foreach (Minion minion in (IEnumerable<Minion>) this.state.ActiveSide)
          minion.hasAttacked = false;
        attacker = this.state.ActiveSide.First<Minion>((Func<Minion, bool>) (x => x.IsValidAttacker()));
      }
      if (this.state.ActiveSide == this.playerSide)
        this.state.Player.SideLastAttacker = attacker;
      else
        this.state.Opponent.SideLastAttacker = attacker;
      this.AttackWithMinion(attacker);
      attacker.hasAttacked = true;
      this.state.ActiveSide = this.state.ActiveSide == this.playerSide ? (IReadOnlyList<Minion>) this.opponentSide : (IReadOnlyList<Minion>) this.playerSide;
      this.SetupDuoDuringRound();
    }
  }

  public void XRounds(int rounds)
  {
    for (int index = 0; index < rounds; ++index)
      this.OneRound();
  }

  private void CheckForStealth()
  {
    IEnumerable<Minion> source = this.playerSide.Concat<Minion>((IEnumerable<Minion>) this.opponentSide);
    foreach (Minion minion in source)
    {
      if (minion.attack() == 0 && minion.stealth)
        minion.stealth = false;
    }
    if (!source.All<Minion>((Func<Minion, bool>) (x => x.stealth)))
      return;
    foreach (Minion minion in source)
      minion.stealth = false;
  }

  internal void ResolveAllDeathsAndTriggers(bool resolveReborn)
  {
    bool flag1 = this.ParentScopeHasDeathrattle(this.state.TriggerScope.Current);
    using (this.state.SummonScope.New("DeathsAndTriggers reborn=" + (flag1 ? "Skip (Parent Deathrattle)" : resolveReborn.ToString())))
    {
      bool flag2 = true;
      while (flag2)
      {
        flag2 = this.ResolveDeaths() | this.ResolveTriggersInCurrentScope();
        if (resolveReborn && !flag1)
          flag2 |= this.ResolveReborn();
      }
    }
  }

  internal void RegisterTrigger(Trigger trigger)
  {
    int lastIndex = this.state.TriggerScope.Data.FindLastIndex((Predicate<Trigger>) (x => x.Source == trigger.Source));
    if (lastIndex != -1)
      this.state.TriggerScope.Data.Insert(lastIndex + 1, trigger);
    else
      this.state.TriggerScope.Data.Add(trigger);
  }

  internal void RegisterTrigger(Action? trigger, IEntity? source, bool isDeathrattle = false)
  {
    if (trigger == null)
      return;
    this.RegisterTrigger(new Trigger(trigger, source, isDeathrattle));
  }

  internal bool ResolveTriggersInCurrentScope()
  {
    return this.ResolveTriggers(this.state.TriggerScope.Data);
  }

  internal bool ParentScopeHasDeathrattle(Scope<List<Trigger>>.ScopeData scope)
  {
    if (scope.Parent == null)
      return false;
    return scope.Parent.Data.Any<Trigger>((Func<Trigger, bool>) (x => x.IsDeathrattle)) || this.ParentScopeHasDeathrattle(scope.Parent);
  }

  public bool ResolveTriggers(List<Trigger> triggers)
  {
    if (!triggers.Any<Trigger>())
      return false;
    foreach (Trigger trigger in triggers.ToList<Trigger>())
    {
      if (!trigger.Resolved)
      {
        trigger.Invoke();
        if (trigger.IsDeathrattle && trigger.Source != null)
        {
          ++(trigger.Source.ControlledByPlayer ? this.state.Player : this.state.Opponent).DeathrattleCounter;
          foreach (Entity friendlyEntity in trigger.Source.FriendlyEntities)
          {
            if (friendlyEntity is IOnFriendlyDeathrattle friendlyDeathrattle)
              this.RegisterTrigger(friendlyDeathrattle.OnFriendlyDeathrattle(), (IEntity) friendlyEntity);
          }
        }
      }
      triggers.Remove(trigger);
    }
    return true;
  }

  private void RemovePassiveEffectsSide(IReadOnlyList<Minion> side)
  {
    foreach (Minion minion in (IEnumerable<Minion>) side)
      this.RemovePassiveEffects(minion);
  }

  private void RemovePassiveEffects(Minion minion)
  {
    int num1 = minion.SelfAttackPassive();
    int num2 = minion.SelfHealthPassive();
    foreach (Entity friendlyEntity in minion.FriendlyEntities)
    {
      if (friendlyEntity is IPassiveAttackBonus passiveAttackBonus)
        num1 += passiveAttackBonus.PassiveAttackBonusFor(minion);
      if (friendlyEntity is IPassiveHealthBonus passiveHealthBonus)
        num2 += passiveHealthBonus.PassiveHealthBonusFor(minion);
    }
    foreach (Enchantment enchantment in (IEnumerable<Enchantment>) minion.Enchantments)
    {
      if (enchantment is IPassiveAttackBonus passiveAttackBonus)
        num1 += passiveAttackBonus.PassiveAttackBonusFor(minion);
      if (enchantment is IPassiveHealthBonus passiveHealthBonus)
        num2 += passiveHealthBonus.PassiveHealthBonusFor(minion);
    }
    minion.baseAttack -= num1;
    minion.baseHealth -= num2;
  }

  public bool AddCardToHand(List<Minion> side, CardEntity card, Entity? source)
  {
    if (side == this.playerSide)
    {
      if (this.state.Player.HandSize >= 10)
        return false;
      this.state.Player.Hand.Add(card);
    }
    else
    {
      if (this.state.Opponent.HandSize >= 10)
        return false;
      this.state.Opponent.Hand.Add(card);
    }
    foreach (Entity allEntity in this.GetAllEntities(side))
    {
      if (allEntity is IOnCardAddedToFriendlyHand addedToFriendlyHand)
        this.RegisterTrigger(addedToFriendlyHand.OnCardAddedToFriendlyHand(card, source), (IEntity) allEntity);
    }
    return true;
  }

  internal Minion? GetLowestHealthMinion(List<Minion> side)
  {
    return side.Count == 0 ? (Minion) null : side.GroupBy<Minion, int>((Func<Minion, int>) (x => x.health())).OrderBy<IGrouping<int, Minion>, int>((Func<IGrouping<int, Minion>, int>) (x => x.Key)).First<IGrouping<int, Minion>>().ToList<Minion>().GetRandom<Minion>();
  }

  internal Minion? GetHighestHealthMinion(List<Minion> side)
  {
    return side.Count == 0 ? (Minion) null : side.GroupBy<Minion, int>((Func<Minion, int>) (x => x.health())).OrderBy<IGrouping<int, Minion>, int>((Func<IGrouping<int, Minion>, int>) (x => x.Key)).Last<IGrouping<int, Minion>>().ToList<Minion>().GetRandom<Minion>();
  }

  internal Minion? GetLowestAttackMinion(List<Minion> side)
  {
    return side.Count == 0 ? (Minion) null : side.GroupBy<Minion, int>((Func<Minion, int>) (x => x.attack())).OrderBy<IGrouping<int, Minion>, int>((Func<IGrouping<int, Minion>, int>) (x => x.Key)).First<IGrouping<int, Minion>>().ToList<Minion>().GetRandom<Minion>();
  }

  internal Minion? GetHighestAttackMinion(List<Minion> side)
  {
    return side.Count == 0 ? (Minion) null : side.GroupBy<Minion, int>((Func<Minion, int>) (x => x.attack())).OrderBy<IGrouping<int, Minion>, int>((Func<IGrouping<int, Minion>, int>) (x => x.Key)).Last<IGrouping<int, Minion>>().ToList<Minion>().GetRandom<Minion>();
  }

  public Action? OnMinionSummoned(Minion minion)
  {
    return this.PlayerState.HeroPowers.Any<HeroPower>((Func<HeroPower, bool>) (hp => hp.Data.CardId == "TB_BaconShop_HP_107" && minion.ControlledByPlayer)) || this.OpponentState.HeroPowers.Any<HeroPower>((Func<HeroPower, bool>) (hp => hp.Data.CardId == "TB_BaconShop_HP_107" && !minion.ControlledByPlayer)) ? (Action) (() =>
    {
      minion.IncreaseStats(1, 2);
      minion.taunt = true;
    }) : (Action) null;
  }

  public IEnumerable<Entity> GetAllEntities(List<Minion> side)
  {
    return this.GetAllEntities(side == this.playerSide);
  }

  public IEnumerable<Entity> GetAllEntities(bool playerSide)
  {
    if (this.Anomaly != null)
      yield return (Entity) this.Anomaly;
    Minion minion;
    if (playerSide)
    {
      if (this.state.Player.GlobalModifier != null)
        yield return (Entity) this.state.Player.GlobalModifier;
      foreach (Entity heroPower in this.state.Player.HeroPowers)
        yield return heroPower;
      foreach (Entity allEntity in this.state.Player.Objectives.ToList<Objective>())
        yield return allEntity;
      foreach (Entity allEntity in this.state.Player.Trinkets.ToList<Trinket>())
        yield return allEntity;
      foreach (Entity allEntity in this.state.Player.Quests.ToList<Quest>())
        yield return allEntity;
      foreach (Entity allEntity in this.state.Player.QuestRewards.ToList<QuestReward>())
        yield return allEntity;
      foreach (Minion minion1 in this.state.Player.Side.ToList<Minion>())
      {
        minion = minion1;
        yield return (Entity) minion;
        if (minion.AttachedModularEntity != null)
          yield return (Entity) minion.AttachedModularEntity;
        minion = (Minion) null;
      }
      foreach (CardEntity cardEntity in this.state.Player.Hand.ToList<CardEntity>())
      {
        if (cardEntity is MinionCardEntity minionCardEntity && minionCardEntity.Data is ISummonFromHand)
          yield return (Entity) minionCardEntity.Data;
      }
    }
    else
    {
      if (this.state.Opponent.GlobalModifier != null)
        yield return (Entity) this.state.Opponent.GlobalModifier;
      foreach (Entity heroPower in this.state.Opponent.HeroPowers)
        yield return heroPower;
      foreach (Entity allEntity in this.state.Opponent.Objectives.ToList<Objective>())
        yield return allEntity;
      foreach (Entity allEntity in this.state.Opponent.Trinkets.ToList<Trinket>())
        yield return allEntity;
      foreach (Entity allEntity in this.state.Opponent.Quests.ToList<Quest>())
        yield return allEntity;
      foreach (Entity allEntity in this.state.Opponent.QuestRewards.ToList<QuestReward>())
        yield return allEntity;
      foreach (Minion minion2 in this.state.Opponent.Side.ToList<Minion>())
      {
        minion = minion2;
        yield return (Entity) minion;
        if (minion.AttachedModularEntity != null)
          yield return (Entity) minion.AttachedModularEntity;
        minion = (Minion) null;
      }
      foreach (CardEntity cardEntity in this.state.Opponent.Hand.ToList<CardEntity>())
      {
        if (cardEntity is MinionCardEntity minionCardEntity && minionCardEntity.Data is ISummonFromHand)
          yield return (Entity) minionCardEntity.Data;
      }
    }
  }

  public IEnumerable<Damage> ProcessDamage(int amount, Minion target, Entity? source)
  {
    return this.ProcessDamage((IEnumerable<Damage>) new List<Damage>()
    {
      new Damage(amount, target, source)
    });
  }

  public IEnumerable<Damage> ProcessDamage(IEnumerable<Damage> damageGroup)
  {
    List<Trigger> triggerList1 = new List<Trigger>();
    foreach (Damage damage in damageGroup)
    {
      if (damage.Amount != 0)
      {
        if (damage.Target.hasDiv)
        {
          --damage.Target.div;
          List<Trigger> triggerList2 = new List<Trigger>();
          foreach (Entity friendlyEntity in damage.Target.FriendlyEntities)
          {
            if (friendlyEntity is IOnFriendlyMinionLostDiv friendlyMinionLostDiv)
              triggerList2.TryAdd<Trigger>((Trigger) (friendlyMinionLostDiv.OnFriendlyMinionLostDiv(damage.Target), (IEntity) friendlyEntity));
          }
          if (triggerList2.Any<Trigger>())
          {
            using (this.state.SummonScope.New("DivineShieldTriggers"))
              this.ResolveTriggers(triggerList2);
          }
        }
        else
        {
          Action action;
          if (damage.Target is ITryPreventDamage target1 && target1.TryPreventDamage(damage.Amount, out action))
          {
            if (action != null)
              triggerList1.TryAdd<Trigger>((Trigger) (action, (IEntity) damage.Target));
          }
          else
          {
            bool flag = damage.Target.IsAlive();
            int overkillDamage = Math.Max(0, damage.Amount - damage.Target.health());
            damage.Target.baseHealth -= damage.Amount;
            damage.Target.LastKnownPosition = damage.Target.BoardPosition();
            if (damage.Target is IOnTakeDamage target)
              triggerList1.TryAdd<Trigger>((Trigger) (target.OnTakeDamage(damage.Amount), (IEntity) damage.Target));
            foreach (Entity friendlyEntity in damage.Target.FriendlyEntities)
            {
              if (friendlyEntity is IOnFriendlyMinionTakeDamage minionTakeDamage)
                triggerList1.TryAdd<Trigger>((Trigger) (minionTakeDamage.OnFriendlyMinionTakeDamage(damage.Target, damage.Amount), (IEntity) friendlyEntity));
            }
            if (damage.Source is Minion source1)
            {
              foreach (Entity friendlyEntity in source1.FriendlyEntities)
              {
                if (friendlyEntity is IOnFriendlyMinionDealsDamage minionDealsDamage)
                  triggerList1.TryAdd<Trigger>((Trigger) (minionDealsDamage.OnFriendlyMinionDealsDamage(source1, damage.Target, damage.Amount), (IEntity) friendlyEntity));
              }
            }
            if (source1 != null && source1.venomous)
            {
              damage.Target.baseHealth = -10000;
              source1.venomous = false;
              foreach (Entity friendlyEntity in source1.FriendlyEntities)
              {
                if (friendlyEntity is IOnFriendlyMinionLostVenomous minionLostVenomous)
                  triggerList1.TryAdd<Trigger>((Trigger) (minionLostVenomous.OnFriendlyMinionLostVenomous(source1), (IEntity) friendlyEntity));
              }
            }
            else if (source1 != null && source1.poisonous)
              damage.Target.baseHealth = -10000;
            this.OnDamageSecretCheck(damage);
            if (damage.Target.TimesTakenDamage == 0)
              triggerList1.TryAdd<Trigger>((Trigger) (damage.Target.OnFirstTimeTakenDamage(), (IEntity) damage.Target));
            else if (damage.Target.TimesTakenDamage == 1)
              triggerList1.TryAdd<Trigger>((Trigger) (damage.Target.OnSecondTimeTakenDamage(), (IEntity) damage.Target));
            ++damage.Target.TimesTakenDamage;
            if (source1 != null & flag && damage.Target.IsDead())
            {
              triggerList1.TryAdd<Trigger>((Trigger) (damage.Target.OnKilled((Entity) source1), (IEntity) damage.Target));
              if (overkillDamage > 0 && source1.FriendlySide == this.state.ActiveSide)
                triggerList1.TryAdd<Trigger>((Trigger) (source1.OnOverkill(damage.Target, overkillDamage), (IEntity) damage.Source));
              foreach (Entity friendlyEntity in source1.FriendlyEntities)
              {
                if (friendlyEntity is IOnFriendlyMinionKilledEnemy minionKilledEnemy)
                  triggerList1.TryAdd<Trigger>((Trigger) (minionKilledEnemy.OnFriendlyMinionKilledEnemy(source1, damage.Target), (IEntity) friendlyEntity));
              }
            }
            damage.ActualDamageDealt = damage.Amount;
            if (damage.Source is HeroPower source2)
            {
              foreach (Minion minion in source2.FriendlySide)
                triggerList1.TryAdd<Trigger>((Trigger) (minion.OnAfterHeroPowerDamage(source2.CardID, damage.ActualDamageDealt), (IEntity) minion));
            }
          }
        }
      }
    }
    if (triggerList1.Any<Trigger>())
    {
      using (this.state.SummonScope.New("DamageTriggers"))
        this.ResolveTriggers(triggerList1);
    }
    return damageGroup;
  }

  public void Destroy(Minion target, Entity? source)
  {
    if (!target.IsAlive())
      return;
    List<Trigger> triggerList = new List<Trigger>();
    target.baseHealth = -10000;
    if (source != null)
    {
      triggerList.TryAdd<Trigger>((Trigger) (target.OnKilled(source), (IEntity) target));
      foreach (Entity friendlyEntity in source.FriendlyEntities)
      {
        if (friendlyEntity is IOnFriendlyMinionKilledEnemy minionKilledEnemy && source is Minion friendly)
          triggerList.TryAdd<Trigger>((Trigger) (minionKilledEnemy.OnFriendlyMinionKilledEnemy(friendly, target), (IEntity) friendlyEntity));
      }
    }
    if (triggerList.Any<Trigger>())
    {
      using (this.state.SummonScope.New("DestroyTriggers"))
        this.ResolveTriggers(triggerList);
    }
    this.ResolveAllDeathsAndTriggers(true);
  }

  internal bool ResolveDeaths()
  {
    bool flag = false;
    return !SafeRandom.NextBool() ? flag | this.ResolveDeathsForSide(this.opponentSide) | this.ResolveDeathsForSide(this.playerSide) : flag | this.ResolveDeathsForSide(this.playerSide) | this.ResolveDeathsForSide(this.opponentSide);
  }

  private bool ResolveDeathsForSide(List<Minion> side)
  {
    List<Minion> minionList = side == this.playerSide ? this.state.Player.DeadMinions : this.state.Opponent.DeadMinions;
    List<Minion> list1 = side.Where<Minion>((Func<Minion, bool>) (x => x.IsDead())).ToList<Minion>();
    if (this.state.TriggerScope.Current.Parent != null)
    {
      Scope<List<Trigger>>.ScopeData scopeData = this.state.TriggerScope.Current;
      while ((scopeData = scopeData.Parent) != null)
      {
        foreach (Minion minion1 in list1.ToList<Minion>())
        {
          Minion minion = minion1;
          IEnumerable<Trigger> source = scopeData.Data.Where<Trigger>((Func<Trigger, bool>) (x => x.Source == minion));
          if (source.Any<Trigger>())
            this.ResolveTriggers(source.ToList<Trigger>());
          if (scopeData.Name == "TwilightHatchling" && minion.CardID != "BG34_630t" && (scopeData.Parent == null || scopeData.Parent.Data.Where<Trigger>((Func<Trigger, bool>) (x => x.Source?.CardID == "TB_BaconShop_HP_105t")).Count<Trigger>() <= 0))
            list1.Remove(minion);
          if (!minion.IsDead())
            list1.Remove(minion);
        }
      }
    }
    if (list1.Count == 0)
      return false;
    foreach (Minion minion in list1.ToList<Minion>())
    {
      foreach (Minion target in side)
      {
        if (!list1.Contains(target))
        {
          int val2 = minion is IPassiveHealthBonus passiveHealthBonus ? passiveHealthBonus.PassiveHealthBonusFor(target) : 0;
          if (val2 > 0)
          {
            int val1 = target.vanillaHealth - (target.health() - val2);
            if (val1 > 0)
              target.baseHealth += Math.Min(val1, val2);
          }
        }
      }
    }
    List<IAvenge> list2 = (side == this.playerSide ? (IEnumerable) this.state.Player.HeroPowers : (IEnumerable) this.state.Opponent.HeroPowers).OfType<IAvenge>().ToList<IAvenge>();
    foreach (Minion minion in list1)
    {
      minion.baseHealth = -10000;
      --minion.div;
      foreach (Entity source in this.GetAllEntities(side).ToList<Entity>())
      {
        if (!((IEnumerable<Entity>) list1).Contains<Entity>(source) && source is IOnFriendlyMinionDied friendlyMinionDied)
          this.RegisterTrigger(friendlyMinionDied.OnFriendlyMinionDied(minion, minion.GetLeftNeighbor(), minion.GetRightNeighbor()), (IEntity) source);
      }
      this.TriggerDeathrattles(minion);
      foreach (IAvenge avenge in list2)
        this.HandleAvenge(avenge, minion);
      foreach (Entity entity1 in this.GetAllEntities(side).ToList<Entity>())
      {
        Entity entity = entity1;
        if (!((IEnumerable<Entity>) list1).Contains<Entity>(entity) && !list2.Any<IAvenge>((Func<IAvenge, bool>) (ahp => ahp.Equals((object) entity))) && entity is IAvenge avenge)
          this.HandleAvenge(avenge, minion);
      }
      if (minion.reborn)
      {
        Func<List<Minion>> action = minion.OnReborn();
        if (action != null)
          this.state.Reborn.Add(new RebornTrigger((Func<IEnumerable<Minion>>) action, (Entity) minion));
      }
      this.OnMinionDieSecretCheck(minion);
      foreach (Entity source in this.GetAllEntities(side).ToList<Entity>())
      {
        if (!((IEnumerable<Entity>) list1).Contains<Entity>(source) && source is IOnAfterFriendlyMinionDied friendlyMinionDied)
          this.RegisterTrigger(friendlyMinionDied.OnAfterFriendlyMinionDied(minion, minion.GetLeftNeighbor(), minion.GetRightNeighbor()), (IEntity) source);
      }
      foreach (CardEntity cardEntity in side == this.playerSide ? this.state.Player.Hand : this.state.Opponent.Hand)
      {
        if (cardEntity is MinionCardEntity minionCardEntity && minionCardEntity.Data is IHandOnAfterFriendlyMinionDied data)
          this.RegisterTrigger(data.HandOnAfterFriendlyMinionDied(minion), (IEntity) minionCardEntity.Data);
      }
    }
    foreach (Minion minion in list1)
      minion.LastKnownNeighbors = (minion.GetLeftNeighbor(), minion.GetRightNeighbor());
    foreach (Minion minion in list1)
    {
      minion.LastKnownPosition = minion.BoardPosition();
      this.UpdateSummonCounter(side, minion.LastKnownPosition, -1);
      side.Remove(minion);
      minionList.Add(minion);
    }
    return true;
  }

  internal void TriggerDeathrattles(Minion minion)
  {
    List<IDeathrattle> list1 = minion.GetTriggers<IDeathrattle>().ToList<IDeathrattle>();
    if (list1.All<IDeathrattle>((Func<IDeathrattle, bool>) (x => x.GetDeathrattle() == null)) && !minion.AdditionalDeathrattles.Any<Action<Minion>>())
      return;
    int val1 = 1;
    int num1 = 0;
    foreach (Minion minion1 in minion.FriendlySide)
    {
      if (minion1.IsAlive())
      {
        string cardId = minion1.CardID;
        if (cardId == "BG27_518" || cardId == "BG34_Giant_376")
          val1 = Math.Max(val1, minion1.golden ? 3 : 2);
        if (minion1.CardID == "BG25_354")
          num1 += minion1.golden ? 2 : 1;
      }
    }
    foreach (Entity friendlyObjective in minion.FriendlyObjectives)
    {
      if (friendlyObjective.CardID == "BG28_843")
        val1 = Math.Max(val1, 2);
    }
    foreach (Entity friendlyQuestReward in minion.FriendlyQuestRewards)
    {
      if (friendlyQuestReward.CardID == "BG27_Reward_803")
        ++num1;
    }
    foreach (DeathlyPhylactery deathlyPhylactery in minion.FriendlyTrinkets.OfType<DeathlyPhylactery>())
    {
      if (!deathlyPhylactery.Activated)
      {
        ++num1;
        deathlyPhylactery.Activated = true;
      }
    }
    if (this.Anomaly is EchoesOfArgus)
      ++num1;
    int num2 = val1 + num1;
    Queue<Action<Minion>> actionQueue = new Queue<Action<Minion>>();
    List<Action<Minion>> list2 = minion.AdditionalDeathrattles.ToList<Action<Minion>>();
    for (int index = 0; index < num2; ++index)
    {
      foreach (IDeathrattle source in list1)
      {
        Action<Minion> deathrattle = source.GetDeathrattle();
        if (deathrattle != null)
          this.RegisterTrigger((Action) (() => deathrattle(minion)), (IEntity) source, true);
      }
      foreach (Action<Minion> action in list2)
      {
        Action<Minion> additionalDeathrattle = action;
        if (additionalDeathrattle.Method.DeclaringType.DeclaringType == typeof (Leapfrogger))
          actionQueue.Enqueue(additionalDeathrattle);
        else
          this.RegisterTrigger((Action) (() => additionalDeathrattle(minion)), (IEntity) minion, true);
      }
      while (actionQueue.Count > 0)
      {
        Action<Minion> leapfroggerDeathrattle = actionQueue.Dequeue();
        this.RegisterTrigger((Action) (() => leapfroggerDeathrattle(minion)), (IEntity) minion, true);
      }
    }
  }

  public bool ResolveReborn()
  {
    if (!this.state.Reborn.Any<RebornTrigger>())
      return false;
    List<RebornTrigger> list1 = this.state.Reborn.ToList<RebornTrigger>();
    this.state.Reborn.Clear();
    foreach (RebornTrigger rebornTrigger in list1)
    {
      // ISSUE: explicit non-virtual call
      List<Entity> list2 = rebornTrigger.Source is Minion source1 ? __nonvirtual (source1.FriendlyEntities).ToList<Entity>() : (List<Entity>) null;
      if (rebornTrigger.Invoke().Any<Minion>() && list2 != null)
      {
        foreach (Entity source2 in list2)
        {
          if (source2 is IOnAfterFriendlyMinionReborn friendlyMinionReborn && (!(source2 is Minion minion) || minion.OriginalMinion != source1.OriginalMinion))
            this.RegisterTrigger(friendlyMinionReborn.OnAfterFriendlyMinionReborn(source1), (IEntity) source2);
        }
      }
    }
    return true;
  }

  private void HandleAvenge(IAvenge avenge, Minion source)
  {
    if (avenge.AvengeRequirement <= 0)
      return;
    ++avenge.AvengeCounter;
    if (avenge.AvengeCounter % avenge.AvengeRequirement != 0)
      return;
    this.RegisterTrigger(avenge.OnAvenge(), (IEntity) source);
  }

  public void CastBloodGem(Minion target, Entity source)
  {
    int num1 = 1 + (source.ControlledByPlayer ? this.state.Player.BloodGemAtkBuff : this.state.Opponent.BloodGemAtkBuff);
    int num2 = 1 + (source.ControlledByPlayer ? this.state.Player.BloodGemHealthBuff : this.state.Opponent.BloodGemHealthBuff);
    int val1 = 1;
    foreach (Minion minion in target.FriendlySide)
    {
      if (minion.CardID == "BG32_432")
        val1 = Math.Max(val1, minion.golden ? 3 : 2);
    }
    int num3 = num1 * val1;
    int num4 = num2 * val1;
    target.IncreaseStats(num3, num4);
    target.AddBloodGemStats(num3, num4);
    foreach (Entity friendlyEntity in source.FriendlyEntities)
    {
      if (friendlyEntity is IOnAfterBloodGemCast afterBloodGemCast)
        this.RegisterTrigger(afterBloodGemCast.OnAfterBloodGemCast(target), (IEntity) friendlyEntity);
    }
  }

  public void CastSpellcraftSpell(
    ISpellcraftSpell? spellcraftSpell,
    Entity source,
    bool golden,
    Minion? target)
  {
    int val1 = 1;
    int num1 = 0;
    foreach (Minion minion in source.FriendlySide)
    {
      if (!minion.IsDead() && target != null && minion.CardID == "BG35_883")
        val1 = Math.Max(val1, minion.golden ? 3 : 2);
    }
    foreach (Trinket friendlyTrinket in source.FriendlyTrinkets)
    {
      if (friendlyTrinket.CardID == "BG30_MagicItem_434" && friendlyTrinket.ScriptDataNum1 == 1)
      {
        ++num1;
        --friendlyTrinket.ScriptDataNum1;
      }
    }
    int num2 = val1 + num1;
    for (int index = 0; index < num2; ++index)
    {
      spellcraftSpell?.Cast(source, golden, this, target);
      ++(source.ControlledByPlayer ? source.Simulator.state.Player : source.Simulator.state.Opponent).AnySpellCounter;
      foreach (Entity friendlyEntity in source.FriendlyEntities)
      {
        if (friendlyEntity is IOnAfterSpellcraftSpellCast spellcraftSpellCast)
          this.RegisterTrigger(spellcraftSpellCast.OnAfterSpellcraftSpellCast(target), (IEntity) friendlyEntity);
        if (friendlyEntity is IOnAfterAnySpellCast afterAnySpellCast)
          this.RegisterTrigger(afterAnySpellCast.OnAfterAnySpellCast(source, target), (IEntity) friendlyEntity);
      }
    }
  }

  public void CastTavernSpell(ITavernSpell tavernSpell, Entity source, Minion? target = null)
  {
    int val1 = 1;
    int num1 = 0;
    foreach (Minion minion in source.FriendlySide)
    {
      if (!minion.IsDead())
      {
        if (minion.CardID == "BG34_922")
          num1 += minion.golden ? 2 : 1;
        if (target != null && minion.CardID == "BG35_883")
          val1 = Math.Max(val1, minion.golden ? 3 : 2);
      }
    }
    foreach (Trinket friendlyTrinket in source.FriendlyTrinkets)
    {
      if (friendlyTrinket.CardID == "BG30_MagicItem_434" && friendlyTrinket.ScriptDataNum1 == 1)
      {
        ++num1;
        --friendlyTrinket.ScriptDataNum1;
      }
    }
    foreach (Entity friendlyQuestReward in source.FriendlyQuestRewards)
    {
      if (friendlyQuestReward.CardID == "BG28_Reward_501")
        ++num1;
    }
    int num2 = val1 + num1;
    for (int index = 0; index < num2; ++index)
    {
      tavernSpell.Cast(source, this, target);
      GameState.PlayerState playerState = source.ControlledByPlayer ? source.Simulator.state.Player : source.Simulator.state.Opponent;
      ++playerState.TavernSpellCounter;
      ++playerState.AnySpellCounter;
      foreach (Entity friendlyEntity in source.FriendlyEntities)
      {
        if (friendlyEntity is IOnAfterTavernSpellCast afterTavernSpellCast)
          this.RegisterTrigger(afterTavernSpellCast.OnAfterTavernSpellCast(target), (IEntity) friendlyEntity);
        if (friendlyEntity is IOnAfterAnySpellCast afterAnySpellCast)
          this.RegisterTrigger(afterAnySpellCast.OnAfterAnySpellCast(source, target), (IEntity) friendlyEntity);
      }
    }
  }

  public void InvokeBattlecry(Minion target)
  {
    if (!(target is IBattlecry battlecry))
      return;
    int val1 = 1;
    int num1 = 0;
    foreach (Minion minion in target.FriendlySide)
    {
      string cardId = minion.CardID;
      if (cardId == "BG_LOE_077" || cardId == "BG27_518" || cardId == "BG34_Giant_376")
        val1 = Math.Max(val1, minion.golden ? 3 : 2);
    }
    foreach (Entity friendlyObjective in target.FriendlyObjectives)
    {
      if (friendlyObjective.CardID == "BG28_509")
        val1 = Math.Max(val1, 2);
    }
    foreach (Entity friendlyQuestReward in target.FriendlyQuestRewards)
    {
      if (friendlyQuestReward.CardID == "BG27_Reward_802")
        ++num1;
    }
    foreach (WarDrum warDrum in target.FriendlyTrinkets.OfType<WarDrum>())
    {
      if (warDrum.RemainingTriggers > 0)
      {
        num1 += 2;
        warDrum.RemainingTriggers = 0;
      }
    }
    if (this.Anomaly is EchoesOfArgus)
      ++num1;
    int num2 = val1 + num1;
    for (int index = 0; index < num2; ++index)
    {
      this.RegisterTrigger(battlecry.OnBattlecry(), (IEntity) target);
      foreach (Entity allEntity in this.GetAllEntities(target.ControlledByPlayer))
      {
        if (allEntity is IOnFriendlyBattlecry friendlyBattlecry)
          this.RegisterTrigger(friendlyBattlecry.OnFriendlyBattlecry(), (IEntity) target);
      }
      this.ResolveTriggersInCurrentScope();
    }
  }

  public void DealDamageToPlayer(GameState.PlayerState target, int amount, Entity? source = null)
  {
    target.Health -= amount;
    if (target.Side.Any<Minion>((Func<Minion, bool>) (x => x is IOnFriendlyHeroDamagedRewind)))
      target.Health += amount;
    foreach (Minion source1 in target.Side)
    {
      if (source1 is IOnFriendlyHeroDamaged friendlyHeroDamaged)
        this.RegisterTrigger(friendlyHeroDamaged.OnFriendlyHeroDamaged(amount, source), (IEntity) source1);
    }
    this.ResolveTriggersInCurrentScope();
  }

  public void MagnetizeMech(
    Minion target,
    string magneticCardId,
    Entity source,
    int extraAttack = 0,
    int extraHealth = 0)
  {
    Card card = HearthdbUtil.CardFromID(magneticCardId);
    if (card == null || !target.IsMech() && card.Entity.GetTag((GameTag) 2859) != target.PrimaryRace && card.Entity.GetTag((GameTag) 2859) != target.SecondaryRace)
      return;
    int val1 = 1;
    foreach (Minion minion in target.FriendlySide)
    {
      if (minion.CardID == "BG34_177")
        val1 = Math.Max(val1, minion.golden ? 3 : 2);
    }
    Minion fromCard = this.MinionFactory.CreateFromCard(card, target.ControlledByPlayer);
    int num1 = fromCard.SelfAttackPassive();
    int num2 = fromCard.SelfHealthPassive();
    for (int index = 0; index < val1; ++index)
    {
      Simulator.AddMagnetic(target, card, num1 + extraAttack, num2 + extraHealth);
      foreach (Entity allEntity in this.GetAllEntities(target.ControlledByPlayer))
      {
        if (allEntity is IOnFriendlyMagnetized friendlyMagnetized)
          this.RegisterTrigger(friendlyMagnetized.OnFriendlyMagnetized(target, card, source, extraAttack, extraHealth), (IEntity) target);
      }
      if (fromCard is IOnMagnetize onMagnetize)
        this.RegisterTrigger(onMagnetize.OnMagnetize(), (IEntity) target);
      this.ResolveTriggersInCurrentScope();
    }
  }

  private static void AddMagnetic(
    Minion target,
    Card magnetic,
    int passiveAttack,
    int passiveHealth)
  {
    int num1 = magnetic.Attack + passiveAttack;
    int num2 = magnetic.Health + passiveHealth;
    target.baseAttack += num1;
    target.baseHealth += num2;
    target.maxAttack += num1;
    target.maxHealth += num2;
    target.taunt |= magnetic.Taunt;
    target.div = Math.Max(target.div, magnetic.DivineShield ? 1 : 0);
    target.cleave |= MinionFactory.cardIDsWithCleave.Contains(magnetic.Id);
    target.poisonous |= magnetic.Poisonous;
    target.venomous |= magnetic.Venomous;
    target.windfury |= magnetic.Windfury;
    target.stealth |= magnetic.Entity.GetTag((GameTag) 191) > 0;
    target.megaWindfury |= magnetic.MegaWindfury;
    target.cannotAttack |= magnetic.CantAttack;
    target.reborn |= magnetic.Reborn;
  }

  public void ResolveStartOfCombatEffects()
  {
    foreach (Entity allEntity in this.GetAllEntities(true))
    {
      foreach (IOnStartOfCombat trigger in allEntity.GetTriggers<IOnStartOfCombat>())
        trigger.OnCombatStartSetup();
    }
    foreach (Entity allEntity in this.GetAllEntities(false))
    {
      foreach (IOnStartOfCombat trigger in allEntity.GetTriggers<IOnStartOfCombat>())
        trigger.OnCombatStartSetup();
    }
    bool flag1 = SafeRandom.NextBool();
    PromoPortrait firstTriggerTwiceOnce = flag1 ? this.state.Player.Trinkets.OfType<PromoPortrait>().FirstOrDefault<PromoPortrait>() : this.state.Opponent.Trinkets.OfType<PromoPortrait>().FirstOrDefault<PromoPortrait>();
    PromoPortrait secondTriggerTwiceOnce = flag1 ? this.state.Opponent.Trinkets.OfType<PromoPortrait>().FirstOrDefault<PromoPortrait>() : this.state.Player.Trinkets.OfType<PromoPortrait>().FirstOrDefault<PromoPortrait>();
    ValdrakkenWindChimes firstTriggerTwice = flag1 ? this.state.Player.Trinkets.OfType<ValdrakkenWindChimes>().FirstOrDefault<ValdrakkenWindChimes>() : this.state.Opponent.Trinkets.OfType<ValdrakkenWindChimes>().FirstOrDefault<ValdrakkenWindChimes>();
    ValdrakkenWindChimes secondTriggerTwice = flag1 ? this.state.Opponent.Trinkets.OfType<ValdrakkenWindChimes>().FirstOrDefault<ValdrakkenWindChimes>() : this.state.Player.Trinkets.OfType<ValdrakkenWindChimes>().FirstOrDefault<ValdrakkenWindChimes>();
    if (this.Anomaly != null)
    {
      foreach (IOnStartOfCombat trigger in this.Anomaly.GetTriggers<IOnStartOfCombat>())
      {
        this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
        this.ResolveTriggersInCurrentScope();
        this.ResolveAllDeathsAndTriggers(true);
      }
    }
    List<QuestReward> questRewardList1 = flag1 ? this.state.Player.QuestRewards : this.state.Opponent.QuestRewards;
    List<QuestReward> questRewardList2 = flag1 ? this.state.Opponent.QuestRewards : this.state.Player.QuestRewards;
    (List<QuestReward>, PromoPortrait, ValdrakkenWindChimes)[] valueTupleArray1 = new (List<QuestReward>, PromoPortrait, ValdrakkenWindChimes)[2]
    {
      (questRewardList1, firstTriggerTwiceOnce, firstTriggerTwice),
      (questRewardList2, secondTriggerTwiceOnce, secondTriggerTwice)
    };
    foreach ((List<QuestReward>, PromoPortrait, ValdrakkenWindChimes) valueTuple in valueTupleArray1)
    {
      foreach (Entity entity in valueTuple.Item1)
      {
        foreach (IOnStartOfCombat trigger in entity.GetTriggers<IOnStartOfCombat>())
        {
          this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
          PromoPortrait promoPortrait = valueTuple.Item2;
          if (promoPortrait != null && !promoPortrait.Activated)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
            valueTuple.Item2.Activated = true;
          }
          if (valueTuple.Item3 != null)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
          }
        }
      }
    }
    List<Trinket> trinketList1 = flag1 ? this.state.Player.Trinkets : this.state.Opponent.Trinkets;
    List<Trinket> trinketList2 = flag1 ? this.state.Opponent.Trinkets : this.state.Player.Trinkets;
    (List<Trinket>, PromoPortrait, ValdrakkenWindChimes)[] valueTupleArray2 = new (List<Trinket>, PromoPortrait, ValdrakkenWindChimes)[2]
    {
      (trinketList1, firstTriggerTwiceOnce, firstTriggerTwice),
      (trinketList2, secondTriggerTwiceOnce, secondTriggerTwice)
    };
    foreach ((List<Trinket>, PromoPortrait, ValdrakkenWindChimes) valueTuple in valueTupleArray2)
    {
      foreach (Entity entity in valueTuple.Item1)
      {
        foreach (IOnStartOfCombat trigger in entity.GetTriggers<IOnStartOfCombat>())
        {
          this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
          PromoPortrait promoPortrait = valueTuple.Item2;
          if (promoPortrait != null && !promoPortrait.Activated)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
            valueTuple.Item2.Activated = true;
          }
          if (valueTuple.Item3 != null)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
          }
        }
      }
    }
    foreach ((HeroPower heroPower, PromoPortrait triggerTwiceOnce, ValdrakkenWindChimes triggerTwice) orderedHeroPower in this.GetOrderedHeroPowers(flag1, firstTriggerTwiceOnce, secondTriggerTwiceOnce, firstTriggerTwice, secondTriggerTwice))
    {
      if (orderedHeroPower.heroPower != null)
      {
        foreach (IOnStartOfCombat trigger in orderedHeroPower.heroPower.GetTriggers<IOnStartOfCombat>())
        {
          this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
          PromoPortrait triggerTwiceOnce = orderedHeroPower.triggerTwiceOnce;
          if (triggerTwiceOnce != null && !triggerTwiceOnce.Activated)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
            orderedHeroPower.triggerTwiceOnce.Activated = true;
          }
          if (orderedHeroPower.triggerTwice != null)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
          }
        }
      }
    }
    List<Objective> objectiveList1 = flag1 ? this.state.Player.Objectives : this.state.Opponent.Objectives;
    List<Objective> objectiveList2 = flag1 ? this.state.Opponent.Objectives : this.state.Player.Objectives;
    (List<Objective>, PromoPortrait, ValdrakkenWindChimes)[] valueTupleArray3 = new (List<Objective>, PromoPortrait, ValdrakkenWindChimes)[2]
    {
      (objectiveList1, firstTriggerTwiceOnce, firstTriggerTwice),
      (objectiveList2, secondTriggerTwiceOnce, secondTriggerTwice)
    };
    foreach ((List<Objective>, PromoPortrait, ValdrakkenWindChimes) valueTuple in valueTupleArray3)
    {
      foreach (Entity entity in valueTuple.Item1)
      {
        foreach (IOnStartOfCombat trigger in entity.GetTriggers<IOnStartOfCombat>())
        {
          this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
          PromoPortrait promoPortrait = valueTuple.Item2;
          if (promoPortrait != null && !promoPortrait.Activated)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
            valueTuple.Item2.Activated = true;
          }
          if (valueTuple.Item3 != null)
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
          }
        }
      }
    }
    List<CardEntity> cardEntityList1 = flag1 ? this.state.Player.Hand : this.state.Opponent.Hand;
    List<CardEntity> cardEntityList2 = flag1 ? this.state.Opponent.Hand : this.state.Player.Hand;
    (List<CardEntity>, PromoPortrait, ValdrakkenWindChimes)[] valueTupleArray4 = new (List<CardEntity>, PromoPortrait, ValdrakkenWindChimes)[2]
    {
      (cardEntityList1, firstTriggerTwiceOnce, firstTriggerTwice),
      (cardEntityList2, secondTriggerTwiceOnce, secondTriggerTwice)
    };
    foreach ((List<CardEntity>, PromoPortrait, ValdrakkenWindChimes) valueTuple in valueTupleArray4)
    {
      foreach (CardEntity cardEntity in valueTuple.Item1)
      {
        if (cardEntity is MinionCardEntity minionCardEntity && minionCardEntity.Data is ISummonFromHand data && data is IOnStartOfCombat)
        {
          foreach (IOnStartOfCombat trigger in minionCardEntity.Data.GetTriggers<IOnStartOfCombat>())
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
            PromoPortrait promoPortrait = valueTuple.Item2;
            if (promoPortrait != null && !promoPortrait.Activated)
            {
              this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
              this.ResolveTriggersInCurrentScope();
              this.ResolveAllDeathsAndTriggers(true);
              valueTuple.Item2.Activated = true;
            }
            if (valueTuple.Item3 != null)
            {
              this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
              this.ResolveTriggersInCurrentScope();
              this.ResolveAllDeathsAndTriggers(true);
            }
          }
        }
      }
    }
    bool flag2 = false;
    if (this.state.ActiveSide == null)
      this.state.ActiveSide = this.playerSide.Count <= this.opponentSide.Count ? (this.opponentSide.Count <= this.playerSide.Count ? (flag1 ? (IReadOnlyList<Minion>) this.playerSide : (IReadOnlyList<Minion>) this.opponentSide) : (IReadOnlyList<Minion>) this.opponentSide) : (IReadOnlyList<Minion>) this.playerSide;
    else
      flag2 = true;
    List<Minion> minionList1 = flag1 ? this.playerSide : this.opponentSide;
    List<Minion> minionList2 = flag1 ? this.opponentSide : this.playerSide;
    this.OnStartOfCombatSecretCheck(flag1);
    this.OnStartOfCombatSecretCheck(!flag1);
    if (!((IEnumerable<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>) this.GetOrderedHeroPowers(flag1, firstTriggerTwiceOnce, secondTriggerTwiceOnce, firstTriggerTwice, secondTriggerTwice)).Any<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>((Func<(HeroPower, PromoPortrait, ValdrakkenWindChimes), bool>) (x => x.heroPower is TeronGorefiendHeroPower heroPower && heroPower.IsActive())) && (this.playerSide.Count == 0 || this.opponentSide.Count == 0))
      return;
    List<Minion> list1 = minionList1.Where<Minion>((Func<Minion, bool>) (m => m.GetTriggers<IOnStartOfCombat>().Any<IOnStartOfCombat>((Func<IOnStartOfCombat, bool>) (t => t.OnStartOfCombat() != null)))).ToList<Minion>();
    List<Minion> list2 = minionList2.Where<Minion>((Func<Minion, bool>) (m => m.GetTriggers<IOnStartOfCombat>().Any<IOnStartOfCombat>((Func<IOnStartOfCombat, bool>) (t => t.OnStartOfCombat() != null)))).ToList<Minion>();
    bool flag3 = true;
    for (int index = 0; (list1.Count > 0 || list2.Count > 0) && index < 500; ++index)
    {
      list1 = list1.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      list2 = list2.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      Minion minion = (Minion) null;
      if (flag3)
      {
        if (list1.Count > 0)
        {
          minion = list1[0];
          list1.RemoveAt(0);
        }
      }
      else if (list2.Count > 0)
      {
        minion = list2[0];
        list2.RemoveAt(0);
      }
      if (minion != null)
      {
        foreach (IOnStartOfCombat trigger in minion.GetTriggers<IOnStartOfCombat>())
          this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
        this.ResolveTriggersInCurrentScope();
        this.ResolveAllDeathsAndTriggers(true);
        if (flag3 && firstTriggerTwice != null)
        {
          foreach (IOnStartOfCombat trigger in minion.GetTriggers<IOnStartOfCombat>())
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
        }
        else if (!flag3 && secondTriggerTwice != null)
        {
          foreach (IOnStartOfCombat trigger in minion.GetTriggers<IOnStartOfCombat>())
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
        }
        if (flag3 && firstTriggerTwiceOnce != null && !firstTriggerTwiceOnce.Activated)
        {
          foreach (IOnStartOfCombat trigger in minion.GetTriggers<IOnStartOfCombat>())
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
          firstTriggerTwiceOnce.Activated = true;
        }
        else if (!flag3 && secondTriggerTwiceOnce != null && !secondTriggerTwiceOnce.Activated)
        {
          foreach (IOnStartOfCombat trigger in minion.GetTriggers<IOnStartOfCombat>())
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
          secondTriggerTwiceOnce.Activated = true;
        }
      }
      flag3 = !flag3;
    }
    foreach (Entity allEntity in this.GetAllEntities(minionList1))
    {
      foreach (IOnAfterStartOfCombat trigger in allEntity.GetTriggers<IOnAfterStartOfCombat>())
        trigger.OnAfterStartOfCombat();
    }
    foreach (Entity allEntity in this.GetAllEntities(minionList2))
    {
      foreach (IOnAfterStartOfCombat trigger in allEntity.GetTriggers<IOnAfterStartOfCombat>())
        trigger.OnAfterStartOfCombat();
    }
    if (flag2)
      return;
    if (this.playerSide.Count > this.opponentSide.Count)
      this.state.ActiveSide = (IReadOnlyList<Minion>) this.playerSide;
    else if (this.opponentSide.Count > this.playerSide.Count)
      this.state.ActiveSide = (IReadOnlyList<Minion>) this.opponentSide;
    else
      this.state.ActiveSide = flag1 ? (IReadOnlyList<Minion>) this.playerSide : (IReadOnlyList<Minion>) this.opponentSide;
  }

  public void ResolveStartOfCombatEffects(GameState.PlayerState playerState, bool friendly)
  {
    foreach (Entity allEntity in this.GetAllEntities(friendly))
    {
      foreach (IOnStartOfCombat trigger in allEntity.GetTriggers<IOnStartOfCombat>())
        trigger.OnCombatStartSetup();
    }
    bool flag1 = SafeRandom.NextBool();
    if (this.Anomaly != null)
    {
      foreach (IOnStartOfCombat trigger in this.Anomaly.GetTriggers<IOnStartOfCombat>())
      {
        this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
        this.ResolveTriggersInCurrentScope();
        this.ResolveAllDeathsAndTriggers(true);
      }
    }
    foreach (Entity questReward in playerState.QuestRewards)
    {
      foreach (IOnStartOfCombat trigger in questReward.GetTriggers<IOnStartOfCombat>())
      {
        this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
        this.ResolveTriggersInCurrentScope();
        this.ResolveAllDeathsAndTriggers(true);
      }
    }
    foreach (Entity heroPower in playerState.HeroPowers)
    {
      foreach (IOnStartOfCombat trigger in heroPower.GetTriggers<IOnStartOfCombat>())
      {
        this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
        this.ResolveTriggersInCurrentScope();
        this.ResolveAllDeathsAndTriggers(true);
      }
    }
    foreach (Entity objective in playerState.Objectives)
    {
      foreach (IOnStartOfCombat trigger in objective.GetTriggers<IOnStartOfCombat>())
      {
        this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
        this.ResolveTriggersInCurrentScope();
        this.ResolveAllDeathsAndTriggers(true);
      }
    }
    foreach (Entity trinket in playerState.Trinkets)
    {
      foreach (IOnStartOfCombat trigger in trinket.GetTriggers<IOnStartOfCombat>())
      {
        this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
        this.ResolveTriggersInCurrentScope();
        this.ResolveAllDeathsAndTriggers(true);
      }
    }
    foreach (CardEntity cardEntity in playerState.Hand)
    {
      if (cardEntity is MinionCardEntity minionCardEntity && minionCardEntity.Data is ISummonFromHand data && data is IOnStartOfCombat)
      {
        foreach (IOnStartOfCombat trigger in minionCardEntity.Data.GetTriggers<IOnStartOfCombat>())
        {
          this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
          this.ResolveTriggersInCurrentScope();
          this.ResolveAllDeathsAndTriggers(true);
        }
      }
    }
    this.state.ActiveSide = this.playerSide.Count <= this.opponentSide.Count ? (this.opponentSide.Count <= this.playerSide.Count ? (flag1 ? (IReadOnlyList<Minion>) this.playerSide : (IReadOnlyList<Minion>) this.opponentSide) : (IReadOnlyList<Minion>) this.opponentSide) : (IReadOnlyList<Minion>) this.playerSide;
    this.OnStartOfCombatSecretCheck(friendly);
    if (!playerState.HeroPowers.Any<HeroPower>((Func<HeroPower, bool>) (x => x is TeronGorefiendHeroPower gorefiendHeroPower && gorefiendHeroPower.IsActive())) && playerState.Side.Count == 0)
      return;
    List<Minion> list = playerState.Side.Where<Minion>((Func<Minion, bool>) (m => m.GetTriggers<IOnStartOfCombat>().Any<IOnStartOfCombat>((Func<IOnStartOfCombat, bool>) (t => t.OnStartOfCombat() != null)))).ToList<Minion>();
    for (int index = 0; list.Count > 0 && index < 500; ++index)
    {
      list = list.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      if (list.Count != 0)
      {
        Minion minion = list[0];
        list.RemoveAt(0);
        if (minion != null)
        {
          foreach (IOnStartOfCombat trigger in minion.GetTriggers<IOnStartOfCombat>())
          {
            this.RegisterTrigger(trigger.OnStartOfCombat(), (IEntity) trigger);
            this.ResolveTriggersInCurrentScope();
            this.ResolveAllDeathsAndTriggers(true);
          }
        }
      }
      else
        break;
    }
    bool flag2 = false;
    foreach (Entity allEntity in this.GetAllEntities(playerState.Side))
    {
      foreach (IOnAfterStartOfCombat trigger in allEntity.GetTriggers<IOnAfterStartOfCombat>())
      {
        trigger.OnAfterStartOfCombat();
        flag2 = true;
      }
    }
    if (!flag2)
      return;
    if (this.playerSide.Count > this.opponentSide.Count)
      this.state.ActiveSide = (IReadOnlyList<Minion>) this.playerSide;
    else if (this.opponentSide.Count > this.playerSide.Count)
      this.state.ActiveSide = (IReadOnlyList<Minion>) this.opponentSide;
    else
      this.state.ActiveSide = flag1 ? (IReadOnlyList<Minion>) this.playerSide : (IReadOnlyList<Minion>) this.opponentSide;
  }

  private bool ContainsSpecialHeroPower(IEnumerable<HeroPower> heroPowers)
  {
    return heroPowers.Any<HeroPower>((Func<HeroPower, bool>) (hp => hp is IllidanHeroPower || hp is BrukanLightningHeroPower));
  }

  private (HeroPower heroPower, PromoPortrait? triggerTwiceOnce, ValdrakkenWindChimes? triggerTwice)[] GetOrderedHeroPowers(
    bool playerFirst,
    PromoPortrait? firstTriggerTwiceOnce,
    PromoPortrait? secondTriggerTwiceOnce,
    ValdrakkenWindChimes? firstTriggerTwice,
    ValdrakkenWindChimes? secondTriggerTwice)
  {
    List<HeroPower> list1 = (playerFirst ? (IEnumerable<HeroPower>) this.state.Player.HeroPowers : (IEnumerable<HeroPower>) this.state.Opponent.HeroPowers).ToList<HeroPower>();
    List<HeroPower> list2 = (playerFirst ? (IEnumerable<HeroPower>) this.state.Opponent.HeroPowers : (IEnumerable<HeroPower>) this.state.Player.HeroPowers).ToList<HeroPower>();
    if (!this.ContainsSpecialHeroPower((IEnumerable<HeroPower>) list1) && !this.ContainsSpecialHeroPower((IEnumerable<HeroPower>) list2))
      return ((IEnumerable<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>) list1.Select<HeroPower, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>((Func<HeroPower, int, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>) ((x, index) => (x, index == 0 ? firstTriggerTwiceOnce : (PromoPortrait) null, firstTriggerTwice))).ToArray<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>()).Concat<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>((IEnumerable<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>) list2.Select<HeroPower, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>((Func<HeroPower, int, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>) ((x, index) => (x, index == 0 ? secondTriggerTwiceOnce : (PromoPortrait) null, secondTriggerTwice))).ToArray<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>()).ToArray<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>();
    List<(HeroPower, PromoPortrait, ValdrakkenWindChimes)> result = new List<(HeroPower, PromoPortrait, ValdrakkenWindChimes)>();
    bool firstTriggerTwiceOnceSet = false;
    bool secondTriggerTwiceOnceSet = false;
    ProcessHeroPowers(list1, HeroPowerIds.ResolveBeforeIllidan, ref firstTriggerTwiceOnceSet, firstTriggerTwiceOnce, firstTriggerTwice);
    ProcessHeroPowers(list2, HeroPowerIds.ResolveBeforeIllidan, ref secondTriggerTwiceOnceSet, secondTriggerTwiceOnce, secondTriggerTwice);
    ProcessSpecificHeroPower<IllidanHeroPower>(list1, ref firstTriggerTwiceOnceSet, firstTriggerTwiceOnce, firstTriggerTwice);
    ProcessSpecificHeroPower<IllidanHeroPower>(list2, ref secondTriggerTwiceOnceSet, secondTriggerTwiceOnce, secondTriggerTwice);
    ProcessHeroPowers(list1, HeroPowerIds.ResolveBeforeBrukanLightning, ref firstTriggerTwiceOnceSet, firstTriggerTwiceOnce, firstTriggerTwice);
    ProcessHeroPowers(list2, HeroPowerIds.ResolveBeforeBrukanLightning, ref secondTriggerTwiceOnceSet, secondTriggerTwiceOnce, secondTriggerTwice);
    ProcessSpecificHeroPower<BrukanLightningHeroPower>(list1, ref firstTriggerTwiceOnceSet, firstTriggerTwiceOnce, firstTriggerTwice);
    ProcessSpecificHeroPower<BrukanLightningHeroPower>(list2, ref secondTriggerTwiceOnceSet, secondTriggerTwiceOnce, secondTriggerTwice);
    result.AddRange(list1.Select<HeroPower, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>((Func<HeroPower, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>) (heroPower => (heroPower, firstTriggerTwiceOnceSet ? (PromoPortrait) null : firstTriggerTwiceOnce, firstTriggerTwice))));
    result.AddRange(list2.Select<HeroPower, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>((Func<HeroPower, (HeroPower, PromoPortrait, ValdrakkenWindChimes)>) (heroPower => (heroPower, secondTriggerTwiceOnceSet ? (PromoPortrait) null : secondTriggerTwiceOnce, secondTriggerTwice))));
    return result.ToArray();

    void ProcessHeroPowers(
      List<HeroPower> heroPowers,
      string[] triggerSet,
      ref bool triggerTwiceOnceSet,
      PromoPortrait? triggerTwiceOnce,
      ValdrakkenWindChimes? triggerTwice)
    {
      foreach (HeroPower heroPower in heroPowers.ToList<HeroPower>().Where<HeroPower>((Func<HeroPower, bool>) (heroPower => ((IEnumerable<string>) triggerSet).Contains<string>(heroPower.CardID))))
      {
        result.Add((heroPower, triggerTwiceOnceSet ? (PromoPortrait) null : triggerTwiceOnce, triggerTwice));
        heroPowers.Remove(heroPower);
        triggerTwiceOnceSet = true;
      }
    }

    void ProcessSpecificHeroPower<T>(
      List<HeroPower> heroPowers,
      ref bool triggerTwiceOnceSet,
      PromoPortrait? triggerTwiceOnce,
      ValdrakkenWindChimes? triggerTwice)
      where T : HeroPower
    {
      foreach (T obj in heroPowers.ToList<HeroPower>().OfType<T>())
      {
        result.Add(((HeroPower) obj, triggerTwiceOnceSet ? (PromoPortrait) null : triggerTwiceOnce, triggerTwice));
        heroPowers.Remove((HeroPower) obj);
        triggerTwiceOnceSet = true;
      }
    }
  }

  public List<Minion> TrySummonRandomMinions(
    List<Summon> options,
    int summonCount,
    List<Minion> side,
    int insertIndex,
    Entity? source)
  {
    List<Minion> minionList = new List<Minion>();
    options = this.RestrictSummonsByPlayerTier((IEnumerable<Summon>) options, side == this.playerSide).Where<Summon>((Func<Summon, bool>) (x => this.AvailableTiers.Contains(x.Tier))).ToList<Summon>();
    if (!this.Input.isDuos)
      options = options.Where<Summon>((Func<Summon, bool>) (x =>
      {
        Card card = x.Card;
        return card != null && card.Entity.GetTag((GameTag) 3166) == 0;
      })).ToList<Summon>();
    if (this.availableRaces != null)
    {
      options = options.Where<Summon>((Func<Summon, bool>) (x => this.availableRaces.Contains(x.PrimaryRace) || this.availableRaces.Contains(x.SecondayRace) || x.PrimaryRace == null && x.SecondayRace == null || x.PrimaryRace == 26 || x.SecondayRace == 26)).ToList<Summon>();
      if (!options.Any<Summon>())
        return minionList;
    }
    List<string> _banned = new List<string>()
    {
      "BG26_801",
      "BG26_350"
    };
    options = options.Where<Summon>((Func<Summon, bool>) (x => !_banned.Contains(x.CardId ?? "") && !_banned.Contains(x.Card?.Id ?? "") && !_banned.Contains(x.Minion?.CardID ?? ""))).ToList<Summon>();
    return !options.Any<Summon>() ? minionList : this.TrySummonMinions((IEnumerable<Summon>) Enumerable.Range(1, summonCount).Select<int, Summon>((Func<int, Summon>) (x => options.GetRandom<Summon>())).ToList<Summon>(), side, insertIndex, source);
  }

  public List<Minion> TrySummonMinions(
    IEnumerable<Summon> summons,
    List<Minion> side,
    int insertIndex,
    Entity? source)
  {
    List<Minion> minionList = new List<Minion>();
    foreach (Summon summon in summons)
    {
      List<Minion> collection = this.TrySummonMinion(summon, side, insertIndex, source);
      minionList.AddRange((IEnumerable<Minion>) collection);
    }
    return minionList;
  }

  public List<Minion> TrySummonMinion(
    Summon summon,
    List<Minion> side,
    int insertIndex,
    Entity? source,
    bool isReborn = false)
  {
    List<Minion> collection = new List<Minion>();
    if (side.Count >= 7)
    {
      foreach (Entity allEntity in this.GetAllEntities(side))
      {
        if (allEntity is IOnFriendlyMinionFailedToSummonNoSpace failedToSummonNoSpace)
          this.RegisterTrigger(failedToSummonNoSpace.OnFriendlyMinionFailedToSummonNoSpace(summon), (IEntity) allEntity);
      }
      return collection;
    }
    if (insertIndex < 0)
      insertIndex = 0;
    if (insertIndex > 7)
      insertIndex = 7;
    Minion minion1;
    if (summon.Card != null)
      minion1 = this.MinionFactory.CreateFromCard(summon.Card, side == this.playerSide);
    else if (summon.CardId != null)
      minion1 = this.MinionFactory.CreateFromCardId(summon.CardId, side == this.playerSide);
    else
      minion1 = summon.Minion != null ? summon.Minion : throw new ArgumentException("SummonOption must define Card or Minion", "options");
    if (summon.MakeGolden || this.Anomaly is TheGoldenArena)
      minion1.TryMakeGolden(false, (Entity) this.Anomaly);
    if (summon.SetStats.HasValue)
      minion1.SetStats(new int?(summon.SetStats.Value.Item1), new int?(summon.SetStats.Value.Item2));
    if (summon.GiveReborn)
      minion1.reborn = true;
    if (summon.GiveTaunt)
      minion1.taunt = true;
    if (this.Anomaly is IncubationMutation && minion1.PrimaryRace == null)
      minion1.PrimaryRace = (Race) 26;
    if (this.Anomaly is ColorfulCamaraderie && minion1.IsBuddy)
      minion1.PrimaryRace = (Race) 26;
    if (source is Minion minion2 && minion2.hasAttacked && !this.IsLastAttackerOnSide(minion2))
      minion1.hasAttacked = true;
    if (!this.state.IsStartOfCombatResolved)
    {
      foreach (IOnStartOfCombat trigger in minion1.GetTriggers<IOnStartOfCombat>())
        trigger.OnCombatStartSetup();
    }
    int index = this.UpdateSummonCounter(side, insertIndex, 1);
    side.Insert(index, minion1);
    collection.Add(minion1);
    List<Trigger> triggerList1 = new List<Trigger>();
    triggerList1.TryAdd<Trigger>((Trigger) (this.OnMinionSummoned(minion1), (IEntity) null));
    foreach (Entity allEntity in this.GetAllEntities(side))
    {
      if (allEntity != minion1 && allEntity is IOnFriendlyMinionSummoned friendlyMinionSummoned)
        triggerList1.TryAdd<Trigger>((Trigger) (friendlyMinionSummoned.OnFriendlyMinionSummoned(minion1, source), (IEntity) allEntity));
    }
    this.ResolveTriggers(triggerList1);
    List<Trigger> triggerList2 = new List<Trigger>();
    if (minion1 is IOnSummoned onSummoned)
      triggerList2.TryAdd<Trigger>((Trigger) (onSummoned.OnSummoned(), (IEntity) minion1));
    if (source != null)
    {
      foreach (Entity friendlyEntity in source.FriendlyEntities)
      {
        if (friendlyEntity != minion1)
        {
          Action action = friendlyEntity.OnMinionSummonedByFriendly(minion1, source, isReborn);
          if (action != null)
          {
            Trigger trigger = new Trigger(action, (IEntity) friendlyEntity, false);
            triggerList2.Add(trigger);
            this.RegisterTrigger(trigger);
          }
        }
      }
    }
    this.ResolveTriggers(triggerList2);
    List<Trigger> triggerList3 = new List<Trigger>();
    foreach (Entity allEntity in this.GetAllEntities(side))
    {
      if (allEntity != minion1 && allEntity is IOnAfterFriendlyMinionSummoned friendlyMinionSummoned)
        triggerList3.TryAdd<Trigger>((Trigger) (friendlyMinionSummoned.OnAfterFriendlyMinionSummoned(minion1, source), (IEntity) allEntity));
    }
    this.ResolveTriggers(triggerList3);
    (side == this.playerSide ? this.state.Player.SummonedMinions : this.state.Opponent.SummonedMinions).AddRange((IEnumerable<Minion>) collection);
    foreach (Minion minion3 in collection)
    {
      if (minion3.Simulator != this)
        throw new BadCloneException((Entity) minion3);
    }
    return collection;
  }

  private int UpdateSummonCounter(List<Minion> side, int insertIndex, int change)
  {
    int? nullable = new int?();
    for (Scope<SummonCounters>.ScopeData scopeData = this.state.SummonScope.Current; scopeData != null; scopeData = scopeData.Parent)
    {
      Dictionary<int, int> dictionary = side == this.playerSide ? scopeData.Data.Player : scopeData.Data.Opponent;
      int num;
      dictionary.TryGetValue(insertIndex, out num);
      dictionary[insertIndex] = Math.Max(0, num + change);
      nullable.GetValueOrDefault();
      if (!nullable.HasValue)
        nullable = new int?(insertIndex + num);
    }
    return Math.Min(nullable.GetValueOrDefault(), side.Count);
  }

  public void TrySummonQuestReward(QuestReward questReward, bool controlledByPlayer)
  {
    List<Trigger> triggerList = new List<Trigger>();
    if (controlledByPlayer)
    {
      this.state.Player.QuestRewards.Add(questReward);
      if (questReward is IOnSummoned onSummoned)
        triggerList.TryAdd<Trigger>((Trigger) (onSummoned.OnSummoned(), (IEntity) questReward));
    }
    else
    {
      this.state.Opponent.QuestRewards.Add(questReward);
      if (questReward is IOnSummoned onSummoned)
        triggerList.TryAdd<Trigger>((Trigger) (onSummoned.OnSummoned(), (IEntity) questReward));
    }
    this.ResolveTriggers(triggerList);
  }

  public IEnumerable<Summon> RestrictSummonsByPlayerTier(
    IEnumerable<Summon> summons,
    bool controlledByPlayer)
  {
    int tier = controlledByPlayer ? this.PlayerState.Tier : this.OpponentState.Tier;
    return summons.Where<Summon>((Func<Summon, bool>) (x => x.Tier <= tier));
  }

  private List<Simulator.Secrets> GetSecretSideSecrets(Minion minion)
  {
    return !minion.ControlledByPlayer ? this.state.Opponent.Secrets : this.state.Player.Secrets;
  }

  private void OnAttackSecretCheck(Minion wasAttacked)
  {
    List<Simulator.Secrets> secretSideSecrets = this.GetSecretSideSecrets(wasAttacked);
    foreach (Simulator.Secrets secret in secretSideSecrets.ToList<Simulator.Secrets>())
    {
      switch (secret)
      {
        case Simulator.Secrets.AutodefenseMatrix:
          this.RegisterSecretTrigger(this.CheckAutodefenseMatrix(wasAttacked), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.PackTactics:
          this.RegisterSecretTrigger(this.CheckPackTactics(wasAttacked), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.SnakeTrap:
          this.RegisterSecretTrigger(this.CheckSnakeTrap(wasAttacked), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.SplittingImage:
          this.RegisterSecretTrigger(this.CheckSplittingImage(wasAttacked), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.VenomstrikeTrap:
          this.RegisterSecretTrigger(this.CheckVenomStrike(wasAttacked), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.BetterVenomstrikeTrap:
          this.RegisterSecretTrigger(this.CheckBetterVenomStrike(wasAttacked), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.BetterAutodefenseMatrix:
          this.RegisterSecretTrigger(this.CheckBetterAutodefenseMatrix(wasAttacked), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.BetterPackTactics:
          this.RegisterSecretTrigger(this.CheckBetterPackTactics(wasAttacked), secretSideSecrets, secret);
          continue;
        default:
          continue;
      }
    }
  }

  private void OnMinionDieSecretCheck(Minion minionDied)
  {
    List<Simulator.Secrets> secretSideSecrets = this.GetSecretSideSecrets(minionDied);
    foreach (Simulator.Secrets secret in secretSideSecrets.ToList<Simulator.Secrets>())
    {
      switch (secret)
      {
        case Simulator.Secrets.Avenge:
          this.RegisterSecretTrigger(this.CheckAvenge(minionDied), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.Redemption:
          this.RegisterSecretTrigger(this.CheckRedemption(minionDied), secretSideSecrets, secret);
          continue;
        case Simulator.Secrets.BetterRedemption:
          this.RegisterSecretTrigger(this.CheckBetterRedemption(minionDied), secretSideSecrets, secret);
          continue;
        default:
          continue;
      }
    }
  }

  private void OnDamageSecretCheck(Damage damage)
  {
    List<Simulator.Secrets> secretSideSecrets = this.GetSecretSideSecrets(damage.Target);
    foreach (Simulator.Secrets secret in secretSideSecrets.ToList<Simulator.Secrets>())
    {
      if (secret == Simulator.Secrets.Reckoning)
        this.RegisterSecretTrigger(this.CheckReckoning(damage), secretSideSecrets, secret);
    }
  }

  private void RegisterSecretTrigger(
    Func<Action?> triggerWrapper,
    List<Simulator.Secrets> side,
    Simulator.Secrets secret)
  {
    if (triggerWrapper == null)
      return;
    this.RegisterTrigger((Action) (() =>
    {
      if (!side.Contains(secret))
        return;
      Action action = triggerWrapper();
      if (action == null)
        return;
      side.Remove(secret);
      action();
    }), (IEntity) null);
  }

  private Func<Action?> CheckAutodefenseMatrix(Minion target)
  {
    return (Func<Action>) (() => !target.hasDiv ? (Action) (() => target.div = 1) : (Action) null);
  }

  private Func<Action?> CheckBetterAutodefenseMatrix(Minion target)
  {
    return (Func<Action>) (() => !target.hasDiv ? (Action) (() => target.div = 2) : (Action) null);
  }

  private Func<Action?> CheckPackTactics(Minion target)
  {
    return (Func<Action>) (() =>
    {
      if (target.FriendlySide.Count >= 7)
        return (Action) null;
      return (Action) (() => this.TrySummonMinion(new Summon(target.Clone())
      {
        SetStats = new (int, int)?((3, 3))
      }, target.FriendlySide, target.BoardPosition() + 1, (Entity) null));
    });
  }

  private Func<Action?> CheckBetterPackTactics(Minion target)
  {
    return (Func<Action>) (() =>
    {
      if (target.FriendlySide.Count >= 7)
        return (Action) null;
      Summon copy = new Summon(target.Clone());
      return (Action) (() => this.TrySummonMinion(copy, target.FriendlySide, target.FriendlySide.Count, (Entity) null));
    });
  }

  private Func<Action?> CheckSnakeTrap(Minion target)
  {
    return (Func<Action>) (() => target.FriendlySide.Count < 7 ? (Action) (() => this.TrySummonMinions((IEnumerable<Summon>) Simulator._snakeSummons, target.FriendlySide, target.FriendlySide.Count, (Entity) null)) : (Action) null);
  }

  private Func<Action?> CheckSplittingImage(Minion target)
  {
    return (Func<Action>) (() => target.FriendlySide.Count<Minion>((Func<Minion, bool>) (x => x.IsAlive())) < 7 ? (Action) (() => target.TrySummonMinion((Summon) target.Clone())) : (Action) null);
  }

  private Func<Action?> CheckVenomStrike(Minion target)
  {
    return (Func<Action>) (() => target.FriendlySide.Count < 7 ? (Action) (() => this.TrySummonMinion((Summon) "BG_EX1_170", target.FriendlySide, target.FriendlySide.Count, (Entity) null)) : (Action) null);
  }

  private Func<Action?> CheckBetterVenomStrike(Minion target)
  {
    return (Func<Action>) (() => target.FriendlySide.Count < 7 ? (Action) (() => this.TrySummonMinion(new Summon("BG_EX1_170", giveReborn: true), target.FriendlySide, target.FriendlySide.Count, (Entity) null)) : (Action) null);
  }

  private Func<Action?> CheckAvenge(Minion minionDied)
  {
    Minion target;
    return (Func<Action>) (() => minionDied.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target) ? (Action) (() => target.IncreaseStats(3, 2)) : (Action) null);
  }

  private Func<Action?> CheckRedemption(Minion minionDied)
  {
    return (Func<Action>) (() => minionDied.FriendlySide.Count < 7 ? (Action) (() => minionDied.OnRedemption()) : (Action) null);
  }

  private Func<Action?> CheckBetterRedemption(Minion minionDied)
  {
    return (Func<Action>) (() => minionDied.FriendlySide.Count < 7 ? (Action) (() =>
    {
      Minion minion = minionDied.Clone();
      minion.baseAttack = minion.maxAttack;
      minion.baseHealth = minion.maxHealth;
      this.TrySummonMinion((Summon) minion, minion.FriendlySide, minion.FriendlySide.Count, (Entity) null);
    }) : (Action) null);
  }

  private Func<Action?> CheckReckoning(Damage damage)
  {
    return (Func<Action>) (() =>
    {
      if (damage.Amount >= 3)
      {
        Minion sourceMinion = damage.Source as Minion;
        if (sourceMinion != null && sourceMinion.IsAlive())
          return (Action) (() => this.Destroy(sourceMinion, (Entity) null));
      }
      return (Action) null;
    });
  }

  private void OnStartOfCombatSecretCheck(bool isPlayer)
  {
    List<Simulator.Secrets> secretsList1 = isPlayer ? this.state.Player.Secrets : this.state.Opponent.Secrets;
    if (!isPlayer)
    {
      List<Minion> playerSide = this.playerSide;
    }
    else
    {
      List<Minion> opponentSide = this.opponentSide;
    }
    List<Simulator.Secrets> secretsList2 = new List<Simulator.Secrets>();
    foreach (int num in secretsList1)
      ;
    foreach (Simulator.Secrets secrets in secretsList2)
      secretsList1.Remove(secrets);
  }

  public enum ExitConditions
  {
    Time,
    Converge,
    CompletedSimulations,
  }

  public enum Secrets
  {
    Unknown,
    AutodefenseMatrix,
    Avenge,
    CompetitiveSpirit,
    IceBlock,
    PackTactics,
    Reckoning,
    Redemption,
    SnakeTrap,
    SplittingImage,
    VenomstrikeTrap,
    BetterVenomstrikeTrap,
    BetterAutodefenseMatrix,
    BetterRedemption,
    BetterPackTactics,
  }
}
