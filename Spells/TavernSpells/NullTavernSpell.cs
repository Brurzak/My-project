// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.NullTavernSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class NullTavernSpell : ITavernSpell
{
  public void Cast(Entity source, Simulator simulator, Minion? target = null)
  {
  }
}
