// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.IOnFriendlyMinionLostBonusKeyword
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

#nullable disable
namespace BobsBuddy.Simulation;

public interface IOnFriendlyMinionLostBonusKeyword : 
  IOnFriendlyMinionLostDiv,
  IEntity,
  IOnFriendlyMinionLostVenomous,
  IOnFriendlyMinionLostStealth,
  IOnFriendlyMinionLostTaunt,
  IOnFriendlyMinionLostReborn,
  IOnFriendlyMinionLostWindfury
{
}
