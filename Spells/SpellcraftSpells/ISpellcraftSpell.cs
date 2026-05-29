// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.SpellcraftSpells.ISpellcraftSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells.SpellcraftSpells;

public interface ISpellcraftSpell
{
  void Cast(Entity source, bool golden, Simulator simulator, Minion? target);

  bool CanTargetMinion { get; }
}
