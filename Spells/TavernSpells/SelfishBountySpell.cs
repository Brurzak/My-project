// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.SelfishBountySpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class SelfishBountySpell : ITavernSpell
{
  public const string CardId = "BG33_813";
  public const string Text = "Give your left-most minion +{0}/+{1}.";

  public void Cast(Entity source, Simulator simulator, Minion? _)
  {
    List<Minion> list = source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
    if (!list.Any<Minion>())
      return;
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    int tavernSpellAtkBuff = playerState.TavernSpellAtkBuff;
    int tavernSpellHealthBuff = playerState.TavernSpellHealthBuff;
    list[0].IncreaseStats(6 + tavernSpellAtkBuff, 6 + tavernSpellHealthBuff, source);
  }
}
