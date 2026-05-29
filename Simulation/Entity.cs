// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Entity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Enchantments;
using BobsBuddy.HeroPowers;
using BobsBuddy.Quests.Rewards;
using BobsBuddy.Spells;
using BobsBuddy.Trinkets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable enable
namespace BobsBuddy.Simulation;

public class Entity : IEntity
{
  private readonly List<Enchantment> _enchantments = new List<Enchantment>();

  [IgnoreDataMember]
  public Simulator Simulator { get; }

  [IgnoreDataMember]
  public List<Minion> FriendlySide { get; }

  [IgnoreDataMember]
  public List<Minion>? TeammateSide { get; }

  [IgnoreDataMember]
  public List<CardEntity> FriendlyHand { get; }

  [IgnoreDataMember]
  public List<QuestReward> FriendlyQuestRewards
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.QuestRewards : this.Simulator.state.Player.QuestRewards;
    }
  }

  [IgnoreDataMember]
  public List<Objective> FriendlyObjectives
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.Objectives : this.Simulator.state.Player.Objectives;
    }
  }

  [IgnoreDataMember]
  public List<Trinket> FriendlyTrinkets
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.Trinkets : this.Simulator.state.Player.Trinkets;
    }
  }

  [IgnoreDataMember]
  public List<HeroPower> FriendlyHeroPowers
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.HeroPowers : this.Simulator.state.Player.HeroPowers;
    }
  }

  [IgnoreDataMember]
  public IEnumerable<Entity> FriendlyEntities
  {
    get => this.Simulator.GetAllEntities(this.ControlledByPlayer);
  }

  [IgnoreDataMember]
  public List<Minion> OpposingSide { get; }

  [IgnoreDataMember]
  public List<QuestReward> OpposingQuestRewards
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Player.QuestRewards : this.Simulator.state.Opponent.QuestRewards;
    }
  }

  [IgnoreDataMember]
  public List<Objective> OpposingObjectives
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Player.Objectives : this.Simulator.state.Opponent.Objectives;
    }
  }

  [IgnoreDataMember]
  public List<HeroPower> OpposingHeroPowers
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Player.HeroPowers : this.Simulator.state.Opponent.HeroPowers;
    }
  }

  [IgnoreDataMember]
  public IEnumerable<Entity> OpposingEntities
  {
    get => this.Simulator.GetAllEntities(!this.ControlledByPlayer);
  }

  public string CardID { get; }

  public bool ControlledByPlayer { get; }

  public List<MinionCardEntity> FriendlyHandMinions(bool throwOnUnsupportedEntity = false)
  {
    List<MinionCardEntity> minionCardEntityList = new List<MinionCardEntity>();
    foreach (CardEntity cardEntity in this.FriendlyHand)
    {
      switch (cardEntity)
      {
        case MinionCardEntity minionCardEntity:
          minionCardEntityList.Add(minionCardEntity);
          continue;
        case UnknownCardEntity _:
          if (throwOnUnsupportedEntity)
            throw new UnsupportedInteractionException("Hand contains unknown card", this);
          continue;
        case RandomMinionCardEntity _:
          if (throwOnUnsupportedEntity)
            throw new UnsupportedInteractionException("Hand contains random unknown card", this);
          continue;
        default:
          continue;
      }
    }
    return minionCardEntityList;
  }

  public Entity(string cardId, Simulator simulator, bool controlledByPlayer)
  {
    this.CardID = cardId;
    this.Simulator = simulator;
    this.ControlledByPlayer = controlledByPlayer;
    if (controlledByPlayer)
    {
      this.FriendlySide = simulator.playerSide;
      this.OpposingSide = simulator.opponentSide;
      this.FriendlyHand = simulator.state.Player.Hand;
      this.TeammateSide = simulator.Input.PlayerTeammate == null ? (List<Minion>) null : (simulator.friendlyLost ? simulator.Input.Player.Side : simulator.Input.PlayerTeammate.Side);
    }
    else
    {
      this.FriendlySide = simulator.opponentSide;
      this.OpposingSide = simulator.playerSide;
      this.FriendlyHand = simulator.state.Opponent.Hand;
      this.TeammateSide = simulator.Input.OpponentTeammate == null ? (List<Minion>) null : (simulator.opponentLost ? simulator.Input.Opponent.Side : simulator.Input.OpponentTeammate.Side);
    }
  }

  public bool AddCardToFriendlyHand(CardEntity cardEntity)
  {
    return this.Simulator.AddCardToHand(this.FriendlySide, cardEntity, this);
  }

  public bool AddMinionToFriendlyHand(string? cardId = null)
  {
    return cardId == null ? this.AddCardToFriendlyHand((CardEntity) new UnknownCardEntity(this, this.Simulator)) : this.AddCardToFriendlyHand((CardEntity) new MinionCardEntity(this.Simulator.MinionFactory.CreateFromCardId(cardId, this.ControlledByPlayer), this, this.Simulator));
  }

  public bool AddSpellToFriendlyHand()
  {
    return this.AddCardToFriendlyHand((CardEntity) new SpellCardEntity(this, this.Simulator));
  }

  public bool AddBloodGemToFriendlyHand()
  {
    return this.AddCardToFriendlyHand((CardEntity) new BloodGem(this, this.Simulator));
  }

  public virtual Action? OnMinionSummonedByFriendly(Minion summoned, Entity? source, bool wasReborn)
  {
    return (Action) null;
  }

  public override string ToString() => $"[{this.CardID}]";

  public IReadOnlyList<Enchantment> Enchantments => (IReadOnlyList<Enchantment>) this._enchantments;

  public void TryAttachEnchantment(string cardId)
  {
    Enchantment enchantment = this.Simulator.EnchantmentFactory.Create(cardId, this.ControlledByPlayer);
    if (enchantment == null)
      return;
    this.AttachEnchantment(enchantment);
  }

  public void AttachEnchantment(Enchantment enchantment)
  {
    this._enchantments.Add(enchantment);
    enchantment.AttachedTo = this;
  }

  public virtual IEnumerable<T> GetTriggers<T>() where T : class
  {
    Entity entity = this;
    if (entity is T trigger1)
      yield return trigger1;
    foreach (Enchantment enchantment in entity._enchantments.ToList<Enchantment>())
    {
      if (enchantment is T trigger2)
        yield return trigger2;
    }
  }

  protected void CloneFromBaseEntity(Entity original)
  {
    foreach (Enchantment enchantment in original._enchantments)
      this.AttachEnchantment(enchantment.Clone(this.Simulator));
  }
}
