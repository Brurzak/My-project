// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.BluesySiren
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells.SpellcraftSpells;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class BluesySiren(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnSetupPlayerStateCounters,
  IEntity,
  IOnMinionMadeGolden,
  IOnFriendlyMinionIsAttacking
{
  public const string CardId = "BG34_931";
  public const string Text = "Whenever a friendly Naga attacks, this casts Deep Blues for +{1}/+{2} on it. <i>({0} times per combat.)</i>3[x]Whenever a friendly Naga attacks, this casts Deep Blues on it for +{1}/+{2}. <i>({0} left!)</i>";
  public const string GoldenText = "Whenever a friendly Naga attacks, this casts Deep Blues for +{1}/+{2} on it twice. <i>({0} times per combat.)</i>3[x]Whenever a friendly Naga attacks, this casts Deep Blues on it for +{1}/+{2} twice. <i>({0} left!)</i>";
  private static ISpellcraftSpell _deepBluesSpell = (ISpellcraftSpell) new DeepBlueSpell();
  private int _castCounter;

  public void OnSetupPlayerStateCounters(GameState.PlayerState playerState)
  {
    playerState.DeepBluesCounter = this.ScriptDataNum2;
  }

  private static int CastLimit => 3;

  public Action? OnMinionMadeGolden() => (Action) (() => this._castCounter = 0);

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion _)
  {
    return (Action) (() =>
    {
      if (!attacker.IsNaga() || this._castCounter >= BluesySiren.CastLimit)
        return;
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
        this.Simulator.CastSpellcraftSpell(BluesySiren._deepBluesSpell, (Entity) this, this.golden, attacker);
      ++this._castCounter;
    });
  }
}
