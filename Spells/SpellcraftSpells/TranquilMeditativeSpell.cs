// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.TranquilMeditativeSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class TranquilMeditativeSpell : ISpellcraftSpell
{
  public const string CardId = "BG32_835t";

  public bool CanTargetMinion => false;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    playerState.TavernSpellAtkBuff += golden ? 2 : 1;
    playerState.TavernSpellHealthBuff += golden ? 2 : 1;
  }
}
