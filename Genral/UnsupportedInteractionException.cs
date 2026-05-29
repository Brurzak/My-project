// Decompiled with JetBrains decompiler
// Type: BobsBuddy.UnsupportedInteractionException
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy;

public class UnsupportedInteractionException : Exception
{
  public Entity? Entity { get; }

  public UnsupportedInteractionException(string message, Entity? entity)
    : base(message)
  {
    this.Entity = entity;
  }
}
