// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.HeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class HeroPower : Entity
{
  public HeroPower(
    string cardId,
    Simulator simulator,
    bool controlledByPlayer,
    HeroPowerData data)
    : base(cardId, simulator, controlledByPlayer)
  {
    this.Data = data;
  }

  public HeroPowerData Data { get; }
}
