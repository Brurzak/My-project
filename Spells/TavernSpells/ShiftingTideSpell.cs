// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.ShiftingTideSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class ShiftingTideSpell : ITavernSpell
{
  public const string CardId = "BG32_815";
  public const string Text = "Give a minion +{0}/+{1} twice. If it's a Naga, repeat this.";

  public void Cast(Entity source, Simulator simulator, Minion? target)
  {
    Minion minion;
    if (target == null && source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).Concat<Minion>(source.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive()))).ToList<Minion>().TryGetRandom<Minion>(out minion))
      target = minion;
    if (target == null)
      return;
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    int tavernSpellAtkBuff = playerState.TavernSpellAtkBuff;
    int tavernSpellHealthBuff = playerState.TavernSpellHealthBuff;
    int num = target.IsNaga() ? 4 : 2;
    target.IncreaseStats(num + num * tavernSpellAtkBuff, num + num * tavernSpellHealthBuff);
  }
}
