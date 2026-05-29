// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.IOnAfterFriendlyMinionSummoned
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;

#nullable enable
namespace BobsBuddy.Simulation;

public interface IOnAfterFriendlyMinionSummoned : IEntity
{
  Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source);
}
