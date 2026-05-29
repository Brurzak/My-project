// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.IOnFriendlyMinionBuffed
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;

#nullable enable
namespace BobsBuddy.Simulation;

public interface IOnFriendlyMinionBuffed : IEntity
{
  Action? OnFriendlyMinionBuffed(Minion buffed, int attackChange, int healthChange, Entity? source);
}
