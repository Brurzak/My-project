// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.WearyMageSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public class WearyMageSpell : ISpellcraftSpell
{
  public const string CardId = "BG31_830t";

  public bool CanTargetMinion => true;

  public void Cast(Entity source, bool golden, Simulator simulator, Minion? target)
  {
    if (target == null || !target.IsAlive())
      return;
    int by = golden ? 4 : 2;
    target.IncreaseStats(by);
    if (!target.IsNaga())
      return;
    target.reborn = true;
  }
}
