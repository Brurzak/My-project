// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.PerfectVisionSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class PerfectVisionSpell : ITavernSpell
{
  public const string CardId = "BG28_838";
  public const string Text = "Set a minion's stats to {0}/{1}.";

  public void Cast(Entity source, Simulator simulator, Minion? target)
  {
    Minion minion;
    if (target == null && source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).Concat<Minion>(source.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive()))).ToList<Minion>().TryGetRandom<Minion>(out minion))
      target = minion;
    if (target == null)
      return;
    int attackChange = Math.Max(0, 20 - target.baseAttack);
    int healthChange = Math.Max(0, 20 - target.baseHealth);
    target.SetStats(new int?(20), new int?(20));
    if (attackChange <= 0 && healthChange <= 0)
      return;
    foreach (Entity friendlyEntity in target.FriendlyEntities)
    {
      if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
        simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(target, attackChange, healthChange, source), (IEntity) source);
    }
  }
}
