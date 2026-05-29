// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.DeepwaterClanSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class DeepwaterClanSpell : ITavernSpell
{
  public const string CardId = "BG35_149";
  public const string Text = "Give a minion +{0}/+{1}. Give your Murlocs +{0}/+{1}.";

  public void Cast(Entity source, Simulator simulator, Minion? _)
  {
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    int tavernSpellAtkBuff = playerState.TavernSpellAtkBuff;
    int tavernSpellHealthBuff = playerState.TavernSpellHealthBuff;
    Minion minion1;
    source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).Concat<Minion>(source.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive()))).ToList<Minion>().TryGetRandom<Minion>(out minion1);
    minion1?.IncreaseStats(2 + tavernSpellAtkBuff, 2 + tavernSpellHealthBuff, source);
    foreach (Minion minion2 in source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsMurloc())))
      minion2.IncreaseStats(2 + tavernSpellAtkBuff, 2 + tavernSpellHealthBuff, source);
  }
}
