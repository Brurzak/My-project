// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.WaveOfGoldSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class WaveOfGoldSpell : ITavernSpell
{
  public const string CardId = "BG34_990";
  public const string Text = "Give your minions +{0}/+{1}. Give Golden ones another +{0}/+{1}.";

  public void Cast(Entity source, Simulator simulator, Minion? _)
  {
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    int tavernSpellAtkBuff = playerState.TavernSpellAtkBuff;
    int tavernSpellHealthBuff = playerState.TavernSpellHealthBuff;
    foreach (Minion minion in source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
      minion.IncreaseStats(3 + tavernSpellAtkBuff, 2 + tavernSpellHealthBuff, source);
    foreach (Minion minion in source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.golden && x.IsAlive())))
      minion.IncreaseStats(3 + tavernSpellAtkBuff, 2 + tavernSpellHealthBuff, source);
  }
}
