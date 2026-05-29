// Decompiled with JetBrains decompiler
// Type: BobsBuddy.RebornBehavior
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;

#nullable disable
namespace BobsBuddy;

[Flags]
public enum RebornBehavior
{
  None = 0,
  KeepEnchantments = 1,
  KeepMaxHealth = 2,
  KeepMaxAttack = 4,
  KeepMaxStats = KeepMaxAttack | KeepMaxHealth, // 0x00000006
}
