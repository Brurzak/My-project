// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.TavishLockAndLoadHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class TavishLockAndLoadHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnStartOfCombat, IEntity, IOnAfterAttackStep
{
  private bool _tokenSummoned;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat() => new Action(this.CheckForSummon);

  public void OnAfterAttackStep() => this.CheckForSummon();

  private void CheckForSummon()
  {
    if (this.FriendlySide.Count >= 7 || this._tokenSummoned)
      return;
    Minion minion = this.Data.AttachedMinion?.Clone();
    if (minion == null)
      return;
    Minion attacker = this.Simulator.TrySummonMinion((Summon) minion, this.FriendlySide, this.FriendlySide.Count, (Entity) this).FirstOrDefault<Minion>();
    if (attacker == null)
      return;
    this._tokenSummoned = true;
    Minion target;
    if (!this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => !x.stealth)).ToList<Minion>().TryGetRandom<Minion>(out target))
      return;
    this.Simulator.AttackWithMinion(attacker, target);
  }
}
