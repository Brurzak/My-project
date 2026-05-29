// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Damage
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

#nullable enable
namespace BobsBuddy.Simulation;

public class Damage
{
  public int Amount { get; }

  public Minion Target { get; }

  public Entity? Source { get; }

  public int ActualDamageDealt { get; set; }

  public Damage(int amount, Minion target, Entity? source = null)
  {
    this.Amount = amount * target.DamageMultiplier;
    this.Target = target;
    this.Source = source;
  }
}
