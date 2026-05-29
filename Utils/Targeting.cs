// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Utils.Targeting
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

#nullable enable
namespace BobsBuddy.Utils;

public static class Targeting
{
  public static double GetOppositeIndex(Minion minion)
  {
    int num1 = minion.IsDead() ? minion.LastKnownPosition : minion.BoardPosition();
    int count = minion.FriendlySide.Count;
    if (minion.IsDead())
      ++count;
    double num2 = (double) (count - minion.OpposingSide.Count) / 2.0;
    return (double) num1 - num2;
  }
}
