// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minion
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Enchantments;
using BobsBuddy.Factory;
using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

#nullable enable
namespace BobsBuddy;

public class Minion : Entity
{
  public List<Action<Minion>> AdditionalDeathrattles = new List<Action<Minion>>();
  public List<Action<Minion>> AdditionalRallies = new List<Action<Minion>>();
  public string? minionName;
  public int tier = 1;
  private int _div;
  public bool hasAttacked;
  public int LastKnownPosition;
  public (Minion?, Minion?) LastKnownNeighbors;
  private const int SafeMaxStatValue = 1073741823 /*0x3FFFFFFF*/;
  public Entity? KilledBy;

  public Minion(string cardId, bool controlledByPlayer, Simulator simulator)
    : base(cardId, simulator, controlledByPlayer)
  {
    this.OriginalMinion = this;
  }

  [IgnoreDataMember]
  public Minion OriginalMinion { get; private set; }

  public virtual Race PrimaryRace { get; set; }

  public virtual Race SecondaryRace { get; set; }

  public int baseHealth { get; set; }

  public int baseAttack { get; set; }

  public bool taunt { get; set; }

  public bool stealth { get; set; }

  public int div
  {
    get => this._div;
    set
    {
      this._div = value;
      if (this._div <= 0)
        return;
      foreach (Entity friendlyEntity in this.FriendlyEntities)
      {
        if (friendlyEntity is IOnFriendlyMinionGainDiv friendlyMinionGainDiv)
          this.Simulator.RegisterTrigger(friendlyMinionGainDiv.OnFriendlyMinionGainDiv(this), (IEntity) friendlyEntity);
      }
    }
  }

  public bool hasDiv => this.div > 0;

  public bool cleave { get; set; }

  public bool poisonous { get; set; }

  public bool venomous { get; set; }

  public bool windfury { get; set; }

  public bool megaWindfury { get; set; }

  public bool golden { get; set; }

  public bool reborn { get; set; }

  public bool cannotAttack { get; set; }

  public bool receivesLichKingPower { get; set; }

  public int StegodonRalliesGranted { get; set; }

  public int StegodonGoldenRalliesGranted { get; set; }

  public int TimesTakenDamage { get; set; }

  public bool HasWingmen { get; set; }

  public bool IsBuddy { get; set; }

  public bool IsWhelp { get; set; }

  public int ScriptDataNum1 { get; set; }

  public int ScriptDataNum2 { get; set; }

  public int ScriptDataNum3 { get; set; }

  public int ScriptDataNum4 { get; set; }

  public bool ImmuneWhileAttacking { get; set; }

  public int ImmuneWhileAttackingCount { get; set; }

  public int DamageMultiplier { get; set; } = 1;

  public int vanillaHealth { get; set; }

  public int vanillaAttack { get; set; }

  public int maxAttack { get; set; }

  public int maxHealth { get; set; }

  public int AvengeCounter { get; set; }

  public (int, int) StatsFromBloodGems { get; set; } = (0, 0);

  public int game_id { get; set; }

  public override string ToString()
  {
    string str = $"{this.minionName ?? this.GetType().Name} {this.attack().ToString()}/{this.health().ToString()}";
    if (this.hasAttacked)
      str += ", z";
    if (this.golden)
      str += ", G";
    if (this.taunt)
      str += ", Taunt";
    if (this.hasDiv)
      str += ", Div";
    if (this.cleave)
      str += ", Cleave";
    if (this.poisonous)
      str += ", Poison";
    if (this.venomous)
      str += ", Venomous";
    if (this.windfury)
      str += ", Windfury";
    if (this.megaWindfury)
      str += ", MegaWindfury";
    if (this.stealth)
      str += ", Stealth";
    if (this.cannotAttack)
      str += ", CantAttack";
    if (this.reborn)
      str += ", Reborn";
    if (this.DamageMultiplier != 1)
      str += $", Damage*{this.DamageMultiplier}";
    if (this.ScriptDataNum1 > 0)
      str += $", ScriptDataNum1={this.ScriptDataNum1}";
    if (this.ScriptDataNum2 > 0)
      str += $", ScriptDataNum2={this.ScriptDataNum2}";
    if (this.ScriptDataNum3 > 0)
      str += $", ScriptDataNum3={this.ScriptDataNum3}";
    if (this.ScriptDataNum4 > 0)
      str += $", ScriptDataNum4={this.ScriptDataNum4}";
    if (this.AdditionalDeathrattles.Count > 0)
      str = $"{str}, Deathrattles=[{string.Join(", ", this.AdditionalDeathrattles.Select<Action<Minion>, string>((Func<Action<Minion>, string>) (x => FormatUtils.GetCleanMethodIdentifier((MethodBase) x.Method))))}]";
    if (this.AdditionalRallies.Count > 0)
      str = $"{str}, Rallies=[{string.Join(", ", this.AdditionalRallies.Select<Action<Minion>, string>((Func<Action<Minion>, string>) (x => FormatUtils.GetCleanMethodIdentifier((MethodBase) x.Method))))}]";
    if (this.AttachedTo != null)
      str = $"{str}, AttachedTo={this.AttachedTo.minionName ?? this.AttachedTo.GetType().Name}";
    if (this.AttachedModularEntity != null)
      str = $"{str}, AttachedModularEntity={this.AttachedModularEntity.minionName ?? this.AttachedModularEntity.GetType().Name}";
    if (this.Enchantments.Any<Enchantment>())
      str = $"{str}, Enchantments=[{string.Join(", ", this.Enchantments.Select<Enchantment, string>((Func<Enchantment, string>) (x => $"{x.GetType().Name}({x.ScriptDataNum1}, {x.ScriptDataNum2})")))}]";
    return $"[{str}]";
  }

  private bool HasBonusKeyword(string keywordName)
  {
    switch (keywordName)
    {
      case "div":
        return this.hasDiv;
      case "venomous":
        return this.venomous;
      case "stealth":
        return this.stealth;
      case "taunt":
        return this.taunt;
      case "windfury":
        return this.windfury;
      case "reborn":
        return this.reborn;
      default:
        throw new ArgumentException("Unknown keyword", nameof (keywordName));
    }
  }

  public int BonusKeywordCount()
  {
    return ((IEnumerable<bool>) new bool[6]
    {
      this.hasDiv,
      this.venomous,
      this.stealth,
      this.taunt,
      this.windfury,
      this.reborn
    }).Count<bool>((Func<bool, bool>) (keyword => keyword));
  }

  public void AddRandomBonusKeyword()
  {
    KeyValuePair<string, Action> keyValuePair;
    if (!new Dictionary<string, Action>()
    {
      {
        "div",
        (Action) (() => this.div = 1)
      },
      {
        "venomous",
        (Action) (() => this.venomous = true)
      },
      {
        "stealth",
        (Action) (() => this.stealth = true)
      },
      {
        "taunt",
        (Action) (() => this.taunt = true)
      },
      {
        "windfury",
        (Action) (() => this.windfury = true)
      },
      {
        "reborn",
        (Action) (() => this.reborn = true)
      }
    }.Where<KeyValuePair<string, Action>>((Func<KeyValuePair<string, Action>, bool>) (kv => !this.HasBonusKeyword(kv.Key))).ToList<KeyValuePair<string, Action>>().TryGetRandom<KeyValuePair<string, Action>>(out keyValuePair))
      return;
    keyValuePair.Value();
  }

  public int attack() => this.baseAttack + this.attackBonus();

  public int attackBonus()
  {
    int num = this.SelfAttackPassive();
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IPassiveAttackBonus passiveAttackBonus)
        num += passiveAttackBonus.PassiveAttackBonusFor(this);
    }
    foreach (Enchantment enchantment in (IEnumerable<Enchantment>) this.Enchantments)
    {
      if (enchantment is IPassiveAttackBonus passiveAttackBonus)
        num += passiveAttackBonus.PassiveAttackBonusFor(this);
    }
    return num;
  }

  public int health() => this.baseHealth + this.healthBonus();

  public int healthBonus()
  {
    int num = this.SelfHealthPassive();
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IPassiveHealthBonus passiveHealthBonus)
        num += passiveHealthBonus.PassiveHealthBonusFor(this);
    }
    foreach (Enchantment enchantment in (IEnumerable<Enchantment>) this.Enchantments)
    {
      if (enchantment is IPassiveHealthBonus passiveHealthBonus)
        num += passiveHealthBonus.PassiveHealthBonusFor(this);
    }
    return num;
  }

  public virtual int SelfAttackPassive() => 0;

  public virtual int SelfHealthPassive() => 0;

  public bool HasDeathrattle()
  {
    return this.AdditionalDeathrattles.Count > 0 || this.GetTriggers<IDeathrattle>().Count<IDeathrattle>((Func<IDeathrattle, bool>) (x => x.GetDeathrattle() != null)) > 0;
  }

  public bool HasRally()
  {
    return this.AdditionalRallies.Count > 0 || this.GetTriggers<IOnRally>().Any<IOnRally>();
  }

  public bool IsValidAttacker() => this.IsAlive() && this.attack() > 0 && !this.cannotAttack;

  public bool HasRace(Race race)
  {
    return race == this.PrimaryRace || race == this.SecondaryRace || race != 25 && (this.PrimaryRace == 26 || this.SecondaryRace == 26);
  }

  public bool IsMech() => this.HasRace((Race) 17);

  public bool IsDemon() => this.HasRace((Race) 15);

  public bool IsBeast() => this.HasRace((Race) 20);

  public bool IsMurloc() => this.HasRace((Race) 14);

  public bool IsDragon() => this.HasRace((Race) 24);

  public bool IsPirate() => this.HasRace((Race) 23);

  public bool IsElemental() => this.HasRace((Race) 18);

  public bool IsQuilboar() => this.HasRace((Race) 43);

  public bool IsUndead() => this.HasRace((Race) 11);

  public bool IsNaga() => this.HasRace((Race) 92);

  public bool IsNoType() => this.PrimaryRace == null && this.SecondaryRace == 0;

  public void IncreaseStats(int by, Entity? source = null) => this.IncreaseStats(by, by, source);

  public virtual void IncreaseStats(int attackBuff, int healthBuff, Entity? source = null)
  {
    (int, int) valueTuple = (0, 0);
    foreach (IGetBonusStatsForFriendlyMinionIncreaseStats minionIncreaseStats in this.FriendlyEntities.OfType<IGetBonusStatsForFriendlyMinionIncreaseStats>())
    {
      (int num1, int num2) = minionIncreaseStats.GetBonusStatsForFriendlyMinionIncreaseStats(this, source, attackBuff, healthBuff) ?? (0, 0);
      valueTuple.Item1 += num1;
      valueTuple.Item2 += num2;
    }
    attackBuff += valueTuple.Item1;
    healthBuff += valueTuple.Item2;
    if (this.baseAttack < 1073741823 /*0x3FFFFFFF*/)
    {
      this.baseAttack += attackBuff;
      this.maxAttack += attackBuff;
    }
    if (this.baseHealth < 1073741823 /*0x3FFFFFFF*/)
    {
      this.baseHealth += healthBuff;
      this.maxHealth += healthBuff;
    }
    if (this.FriendlySide.Contains(this))
    {
      foreach (Entity friendlyEntity in this.FriendlyEntities)
      {
        if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
          this.Simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(this, attackBuff, healthBuff, source), (IEntity) friendlyEntity);
      }
    }
    if (this.FriendlyHandMinions(false).Any<MinionCardEntity>((Func<MinionCardEntity, bool>) (m => m.Data == this)))
    {
      foreach (Entity friendlyEntity in this.FriendlyEntities)
      {
        if (friendlyEntity is IOnFriendlyHandMinionBuffed handMinionBuffed)
          this.Simulator.RegisterTrigger(handMinionBuffed.OnFriendlyHandMinionBuffed(this, attackBuff, healthBuff, source), (IEntity) friendlyEntity);
      }
    }
    foreach (MinionCardEntity friendlyHandMinion in this.FriendlyHandMinions(false))
    {
      if (friendlyHandMinion.Data is IOnFriendlyMinionBuffedWhileInHand data)
        this.Simulator.RegisterTrigger(data.OnFriendlyMinionBuffedWhileInHand(this, attackBuff, healthBuff, source), (IEntity) friendlyHandMinion.Data);
    }
  }

  public virtual void SetStats(int? attack, int? health)
  {
    if (attack.HasValue)
    {
      int valueOrDefault = attack.GetValueOrDefault();
      this.baseAttack = valueOrDefault;
      this.maxAttack = valueOrDefault;
    }
    if (!health.HasValue)
      return;
    int valueOrDefault1 = health.GetValueOrDefault();
    this.baseHealth = valueOrDefault1;
    this.maxHealth = valueOrDefault1;
  }

  public int BoardPosition() => this.FriendlySide.IndexOf(this);

  public Func<List<Minion>>? OnReborn()
  {
    return !this.reborn ? (Func<List<Minion>>) null : (Func<List<Minion>>) (() =>
    {
      int num1 = (int) this.GetTriggers<IRebornBehavior>().Aggregate<IRebornBehavior, RebornBehavior>(RebornBehavior.None, (Func<RebornBehavior, IRebornBehavior, RebornBehavior>) ((c, n) => c | n.RebornBehavior));
      Minion minion = (num1 & 1) != 0 ? this.Clone() : this.CloneAsVanilla();
      minion.reborn = false;
      minion.ScriptDataNum1 = this.ScriptDataNum1;
      minion.ScriptDataNum2 = this.ScriptDataNum2;
      minion.ScriptDataNum3 = this.ScriptDataNum3;
      minion.ScriptDataNum4 = this.ScriptDataNum4;
      minion.baseHealth = 1;
      if ((num1 & 2) != 0)
      {
        GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
        // ISSUE: explicit non-virtual call
        int num2 = globalModifier != null ? __nonvirtual (globalModifier.PassiveHealthBonusFor(this)) : 0;
        minion.baseHealth = this.maxHealth + num2;
      }
      if ((num1 & 4) != 0)
      {
        GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
        // ISSUE: explicit non-virtual call
        int num3 = globalModifier != null ? __nonvirtual (globalModifier.PassiveAttackBonusFor(this)) : 0;
        minion.baseAttack = this.maxAttack + num3;
      }
      if (minion is IOnMinionRebornResetCounters rebornResetCounters2)
      {
        Action action = rebornResetCounters2.OnMinionRebornResetCounters();
        if (action != null)
          action();
      }
      return this.TrySummonMinion((Summon) minion, this.reborn);
    });
  }

  public List<Minion> OnRedemption()
  {
    Minion minion = this.CloneAsVanilla();
    minion.baseHealth = 1;
    return this.TrySummonMinion((Summon) minion);
  }

  public Minion? GetRightNeighbor()
  {
    return this.FriendlySide.ElementAtOrDefault<Minion>(this.BoardPosition() + 1);
  }

  public List<Minion> GetRightNeighbors(int count)
  {
    int pos = this.BoardPosition();
    return this.FriendlySide.Where<Minion>((Func<Minion, int, bool>) ((_, i) => i > pos && i <= pos + count)).ToList<Minion>();
  }

  public Minion? GetLeftNeighbor()
  {
    return this.FriendlySide.ElementAtOrDefault<Minion>(this.BoardPosition() - 1);
  }

  public List<Minion> GetLeftNeighbors(int count)
  {
    int pos = this.BoardPosition();
    return this.FriendlySide.Where<Minion>((Func<Minion, int, bool>) ((_, i) => i >= pos - count && i < pos)).ToList<Minion>();
  }

  public (Minion?, Minion?) GetLastKnownLivingNeighbors()
  {
    (Minion minion1, Minion minion2) = this.LastKnownNeighbors;
    while (minion1 != null && minion1.IsDead())
      minion1 = minion1.LastKnownNeighbors.Item1;
    while (minion2 != null && minion2.IsDead())
      minion2 = minion2.LastKnownNeighbors.Item2;
    return (minion1, minion2);
  }

  public virtual Action? OnOverkill(Minion killed, int overkillDamage) => (Action) null;

  public virtual Action? OnKilled(Entity source) => (Action) (() => this.KilledBy = source);

  public virtual Action? OnFirstTimeTakenDamage() => (Action) null;

  public virtual Action? OnSecondTimeTakenDamage() => (Action) null;

  public virtual Minion? ChooseAttackTarget()
  {
    List<Minion> list1 = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
    if (list1.Count == 0)
      return (Minion) null;
    List<Minion> list2 = list1.Where<Minion>((Func<Minion, bool>) (x => !x.stealth)).ToList<Minion>();
    if (list2.Count == 0)
      return (Minion) null;
    Minion minion;
    return !list2.Where<Minion>((Func<Minion, bool>) (x => x.taunt)).ToList<Minion>().TryGetRandom<Minion>(out minion) ? list2.GetRandom<Minion>() : minion;
  }

  public virtual Action? OnIsAttacked(Minion attacker) => (Action) null;

  public virtual Action? OnWasAttacked() => (Action) null;

  public virtual Action? OnAfterHeroPowerDamage(string heroPowerCardId, int damage)
  {
    return (Action) null;
  }

  public List<Minion> TrySummonRandomMinions(List<Summon> options, int summonCount)
  {
    int insertIndex = this.IsDead() ? this.LastKnownPosition : this.BoardPosition() + 1;
    return this.Simulator.TrySummonRandomMinions(options, summonCount, this.FriendlySide, insertIndex, (Entity) this);
  }

  public List<Minion> TrySummonMinions(params Summon[] summons)
  {
    return this.TrySummonMinions(((IEnumerable<Summon>) summons).ToList<Summon>());
  }

  public List<Minion> TrySummonMinions(List<Summon> summons)
  {
    int insertIndex = this.IsDead() ? this.LastKnownPosition : this.BoardPosition() + 1;
    return this.Simulator.TrySummonMinions((IEnumerable<Summon>) summons, this.FriendlySide, insertIndex, (Entity) this);
  }

  public List<Minion> TrySummonMinion(Summon summon, bool wasReborn = false)
  {
    int insertIndex = this.IsDead() ? this.LastKnownPosition : this.BoardPosition() + 1;
    return this.Simulator.TrySummonMinion(summon, this.FriendlySide, insertIndex, (Entity) this, wasReborn);
  }

  protected int DoubleIfGolden(int i) => !this.golden ? i : i * 2;

  public bool IsAlive() => !this.IsDead();

  public bool IsDead() => this.baseHealth <= 0 && this.health() <= 0;

  public Minion Clone(Simulator? simulator = null)
  {
    Minion minion = (simulator ?? this.Simulator).MinionFactory.Create(this.CardID, this.ControlledByPlayer);
    minion.CloneFromBaseEntity((Entity) this);
    minion.OriginalMinion = this.OriginalMinion;
    minion.minionName = this.minionName;
    minion.PrimaryRace = this.PrimaryRace;
    minion.SecondaryRace = this.SecondaryRace;
    minion.baseAttack = this.baseAttack;
    minion.baseHealth = this.baseHealth;
    minion.vanillaHealth = this.vanillaHealth;
    minion.vanillaAttack = this.vanillaAttack;
    minion.taunt = this.taunt;
    minion.maxAttack = this.maxAttack;
    minion.maxHealth = this.maxHealth;
    minion.div = this.div;
    minion.cleave = this.cleave;
    minion.poisonous = this.poisonous;
    minion.venomous = this.venomous;
    minion.windfury = this.windfury;
    minion.megaWindfury = this.megaWindfury;
    minion.stealth = this.stealth;
    minion.cannotAttack = this.cannotAttack;
    minion.golden = this.golden;
    minion.tier = this.tier;
    minion.reborn = this.reborn;
    minion.TimesTakenDamage = this.TimesTakenDamage;
    minion.HasWingmen = this.HasWingmen;
    minion.IsBuddy = this.IsBuddy;
    minion.IsWhelp = this.IsWhelp;
    minion.AdditionalDeathrattles = this.AdditionalDeathrattles.ToList<Action<Minion>>();
    minion.AdditionalRallies = this.AdditionalRallies.ToList<Action<Minion>>();
    minion.game_id = this.game_id;
    minion.ScriptDataNum1 = this.ScriptDataNum1;
    minion.ScriptDataNum2 = this.ScriptDataNum2;
    minion.ScriptDataNum3 = this.ScriptDataNum3;
    minion.ScriptDataNum4 = this.ScriptDataNum4;
    if (this.AttachedModularEntity != null)
      minion.AttachModularEntity(this.AttachedModularEntity.Clone(), false, false);
    minion.StatsFromBloodGems = this.StatsFromBloodGems;
    return minion;
  }

  public Minion CloneAsShallowExactCopy()
  {
    Minion minion = this.Clone();
    List<FieldInfo> fieldInfoList;
    if (MinionFactory.InternalFields.TryGetValue(this.GetType(), out fieldInfoList))
    {
      foreach (FieldInfo fieldInfo in fieldInfoList)
        fieldInfo.SetValue((object) minion, fieldInfo.GetValue((object) this));
    }
    return minion;
  }

  [IgnoreDataMember]
  public Minion? AttachedTo { get; set; }

  [IgnoreDataMember]
  public Minion AttachedOrThis => this.AttachedTo ?? this;

  public void AttachModularEntity(string cardId)
  {
    this.AttachModularEntity(this.Simulator.MinionFactory.CreateFromCardId(cardId, this.ControlledByPlayer), false, true);
  }

  [IgnoreDataMember]
  public Minion? AttachedModularEntity { get; set; }

  public void AttachModularEntity(Minion minion, bool addStats, bool addDeathrattles)
  {
    this.AttachedModularEntity = minion;
    minion.AttachedTo = this;
    if (addStats)
    {
      this.baseAttack += minion.baseAttack;
      this.baseHealth += minion.baseHealth;
      this.vanillaHealth += minion.vanillaHealth;
      this.vanillaAttack += minion.vanillaAttack;
      this.maxAttack += minion.maxAttack;
      this.maxHealth += minion.maxHealth;
    }
    this.taunt |= minion.taunt;
    this.div |= minion.div;
    this.cleave |= minion.cleave;
    this.poisonous |= minion.poisonous;
    this.venomous |= minion.venomous;
    this.windfury |= minion.windfury;
    this.megaWindfury |= minion.megaWindfury;
    this.stealth |= minion.stealth;
    this.cannotAttack |= minion.cannotAttack;
    this.reborn |= minion.reborn;
    if (minion.PrimaryRace == 26)
      this.PrimaryRace = (Race) 26;
    if (!addDeathrattles)
      return;
    foreach (IDeathrattle trigger in minion.GetTriggers<IDeathrattle>())
    {
      Action<Minion> deathrattle = trigger.GetDeathrattle();
      if (deathrattle != null)
        this.AdditionalDeathrattles.Add(deathrattle);
    }
    this.AdditionalDeathrattles.AddRange((IEnumerable<Action<Minion>>) minion.AdditionalDeathrattles);
  }

  public Minion CloneAsOriginal()
  {
    Minion minion;
    minion.OriginalMinion = minion = this.Clone();
    return minion;
  }

  public Minion CloneAsVanilla()
  {
    Minion fromCardId = this.Simulator.MinionFactory.CreateFromCardId(this.CardID, this.ControlledByPlayer);
    if (this.AttachedModularEntity != null)
      fromCardId.AttachModularEntity(this.AttachedModularEntity.Clone(), true, true);
    if (this.golden)
    {
      fromCardId.golden = true;
      fromCardId.baseAttack *= 2;
      fromCardId.baseHealth *= 2;
    }
    return fromCardId;
  }

  public bool CanBeMadeGolden() => !(this.CardID == "BG33_890t") && !this.golden;

  public void TryMakeGolden(bool isReplacement, Entity? source)
  {
    if (!this.CanBeMadeGolden() || this.IsDead())
      return;
    if (this.CardID == "BG32_HERO_001_Buddy" && !this.ControlledByPlayer && this.Simulator.state.Opponent.ResourcesSpentThisGame == 0)
    {
      int num = Math.Min(this.attack() - this.vanillaAttack, this.health() - this.vanillaHealth);
      if (num > 0)
      {
        this.Simulator.state.Opponent.ResourcesSpentThisGame = num * 3;
        this.baseAttack -= this.SelfAttackPassive();
        this.baseHealth -= this.SelfHealthPassive();
      }
    }
    this.golden = true;
    this.baseAttack += this.vanillaAttack;
    this.baseHealth += this.vanillaHealth;
    this.maxAttack += this.vanillaAttack;
    this.maxHealth += this.vanillaHealth;
    Minion fromCardId = this.Simulator.MinionFactory.CreateFromCardId(MinionFactory.TryGetPremiumIdFromNormal(this.CardID), this.ControlledByPlayer);
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
        this.Simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(this, this.vanillaAttack, this.vanillaHealth, (Entity) null), (IEntity) this);
    }
    if (fromCardId.megaWindfury)
      this.megaWindfury = true;
    if (!isReplacement)
      return;
    if (this is IOnMinionMadeGolden minionMadeGolden)
    {
      Action action = minionMadeGolden.OnMinionMadeGolden();
      if (action != null)
        action();
    }
    this.AvengeCounter = 0;
    int num1 = this.reborn ? 1 : 0;
  }

  public void SetBloodGemStats(int attack, int health)
  {
    this.StatsFromBloodGems = (attack, health);
  }

  public void AddBloodGemStats(int attack, int health)
  {
    this.StatsFromBloodGems = (this.StatsFromBloodGems.Item1 + attack, this.StatsFromBloodGems.Item2 + health);
  }
}
