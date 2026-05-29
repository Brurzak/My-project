// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.DeepBlueSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class DeepBlueSpell : ISpellcraftSpell
{
  public const string CardId = "BG26_502t";

  public bool CanTargetMinion => true;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    if (target == null || !target.IsAlive())
      return;
    int num1 = golden ? 2 : 1;
    int num2 = golden ? 4 : 2;
    GameState.PlayerState playerState = source.ControlledByPlayer ? simulator.state.Player : simulator.state.Opponent;
    target.IncreaseStats(playerState.DeepBluesCounter * num1, playerState.DeepBluesCounter * num2);
    ++playerState.DeepBluesCounter;
  }
}
