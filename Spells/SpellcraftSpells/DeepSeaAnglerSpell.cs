// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.DeepSeaAnglerSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class DeepSeaAnglerSpell : ISpellcraftSpell
{
  public const string CardId = "BG23_004t";

  public bool CanTargetMinion => true;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    if (target == null || !target.IsAlive())
      return;
    int attackBuff = golden ? 4 : 2;
    int healthBuff = golden ? 12 : 6;
    target.IncreaseStats(attackBuff, healthBuff);
    target.taunt = true;
  }
}
