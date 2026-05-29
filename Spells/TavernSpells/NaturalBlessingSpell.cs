// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.NaturalBlessingSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class NaturalBlessingSpell : ITavernSpell
{
  public const string CardId = "BG28_845";
  public const string Text = "Choose a minion. Give all minions that share a type with it +{0}/+{1}.";

  public void Cast(Entity source, Simulator simulator, Minion? _)
  {
    List<Minion> list = source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && !x.IsNoType())).Concat<Minion>(source.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && !x.IsNoType()))).ToList<Minion>();
    Minion typeTarget;
    if (!list.TryGetRandom<Minion>(out typeTarget))
      return;
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    int tavernSpellAtkBuff = playerState.TavernSpellAtkBuff;
    int tavernSpellHealthBuff = playerState.TavernSpellHealthBuff;
    foreach (Minion minion in list.Where<Minion>((Func<Minion, bool>) (x =>
    {
      if (x.HasRace(typeTarget.PrimaryRace))
        return true;
      return typeTarget.SecondaryRace != null && x.HasRace(typeTarget.SecondaryRace);
    })).ToList<Minion>())
      minion.IncreaseStats(3 + tavernSpellAtkBuff, 3 + tavernSpellHealthBuff, source);
    if (!source.FriendlyEntities.Any<Entity>((Func<Entity, bool>) (x => x is BlessingPortrait)))
      return;
    foreach (MinionCardEntity minionCardEntity in source.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (x =>
    {
      if (x.Data.HasRace(typeTarget.PrimaryRace))
        return true;
      return typeTarget.SecondaryRace != null && x.Data.HasRace(typeTarget.SecondaryRace);
    })).ToList<MinionCardEntity>())
      minionCardEntity.Data.IncreaseStats(3 + tavernSpellAtkBuff, 3 + tavernSpellHealthBuff, source);
  }
}
