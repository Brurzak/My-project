// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.Objective
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells;

public class Objective(string cardId, Simulator simulator, bool controlledByPlayer) : Entity(cardId, simulator, controlledByPlayer)
{
  public int ScriptDataNum1 { get; set; }

  public int ScriptDataNum2 { get; set; }

  public Objective Clone(Simulator? simulator = null)
  {
    Objective objective = (simulator ?? this.Simulator).ObjectiveFactory.Create(this.CardID, this.ControlledByPlayer);
    objective.CloneFromBaseEntity((Entity) this);
    objective.ScriptDataNum1 = this.ScriptDataNum1;
    objective.ScriptDataNum2 = this.ScriptDataNum2;
    return objective;
  }
}
