// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Utils.FormatUtils
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable enable
namespace BobsBuddy.Utils;

public static class FormatUtils
{
  public static string GetCleanMethodIdentifier(MethodBase methodInfo)
  {
    Type declaringType = methodInfo.DeclaringType.DeclaringType;
    if ((object) declaringType == null)
      declaringType = methodInfo.DeclaringType;
    string name1 = declaringType.Name;
    string name2 = methodInfo.Name;
    if (name2.StartsWith("<"))
      name2 = Regex.Match(name2, "<(.+)>").Groups[1].Value;
    string str = name2;
    return $"{name1}.{str}";
  }
}
