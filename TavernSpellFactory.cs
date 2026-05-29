// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.TavernSpellFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Spells.TavernSpells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace BobsBuddy.Factory;

public class TavernSpellFactory
{
  private static readonly Dictionary<string, Type> TavernSpellByCardId;

  static TavernSpellFactory()
  {
    IEnumerable<Type> types = ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).Where<Type>((Func<Type, bool>) (t => t.IsClass && !t.IsAbstract && typeof (ITavernSpell).IsAssignableFrom(t)));
    TavernSpellFactory.TavernSpellByCardId = new Dictionary<string, Type>();
    foreach (Type type in types)
    {
      FieldInfo field = type.GetField("CardId", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
      if (field != (FieldInfo) null)
      {
        string key = field.GetValue((object) null) as string;
        if (!string.IsNullOrEmpty(key))
          TavernSpellFactory.TavernSpellByCardId[key] = type;
      }
    }
  }

  public static ITavernSpell? CreateByCardId(string cardId)
  {
    if (string.IsNullOrEmpty(cardId))
      return (ITavernSpell) null;
    Type type;
    return TavernSpellFactory.TavernSpellByCardId.TryGetValue(cardId, out type) ? Activator.CreateInstance(type) as ITavernSpell : (ITavernSpell) new NullTavernSpell();
  }

  public static IEnumerable<string> GetImplementedCardIds()
  {
    return (IEnumerable<string>) TavernSpellFactory.TavernSpellByCardId.Keys;
  }
}
