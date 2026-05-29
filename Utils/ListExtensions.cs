// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Utils.ListExtensions
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Utils;

public static class ListExtensions
{
  public static void TryAdd<T>(this ICollection<T> list, T? item)
  {
    if ((object) item == null)
      return;
    list.Add(item);
  }

  public static bool TryGetFirstAndLast<T>(this List<T> list, out T first, out T last)
  {
    if (list.Count == 0)
    {
      first = default (T);
      last = default (T);
      return false;
    }
    first = list[0];
    last = list[list.Count - 1];
    return true;
  }
}
