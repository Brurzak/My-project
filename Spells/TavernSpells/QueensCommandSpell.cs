// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.QueensCommandSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class QueensCommandSpell : ITavernSpell
{
  public const string CardId = "BG35_922";
  public const string Text = "Give your minions +{0}/+{1}. Give all your Naga another +{0}/+{1}.";

  public void Cast(Entity source, Simulator simulator, Minion? _)
  {
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    int tavernSpellAtkBuff = playerState.TavernSpellAtkBuff;
    int tavernSpellHealthBuff = playerState.TavernSpellHealthBuff;
    foreach (Minion minion in source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
    {
      minion.IncreaseStats(3 + tavernSpellAtkBuff, 3 + tavernSpellHealthBuff, source);
      if (minion.IsNaga())
        minion.IncreaseStats(3 + tavernSpellAtkBuff, 3 + tavernSpellHealthBuff, source);
    }
  }
}
