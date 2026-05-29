// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.EntityFactory`1
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace BobsBuddy.Factory;

public abstract class EntityFactory<T> where T : Entity
{
  protected static readonly List<Type> EntityTypes;
  protected static readonly Dictionary<string, EntityFactory<
  #nullable disable
  T>.Constructor> Constructors = new Dictionary<string, EntityFactory<T>.Constructor>();
  public const BindingFlags ConstField = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;
  protected readonly 
  #nullable enable
  Simulator _simulator;

  static EntityFactory()
  {
    EntityFactory<T>.EntityTypes = ((IEnumerable<Type>) Assembly.GetAssembly(typeof (T)).GetTypes()).Where<Type>((Func<Type, bool>) (t => t.IsClass && t.IsSubclassOf(typeof (T)))).ToList<Type>();
    foreach (Type entityType in EntityFactory<T>.EntityTypes)
    {
      if (((IEnumerable<FieldInfo>) entityType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)).FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (x => x.Name == "CardId"))?.GetValue((object) entityType) is string key)
        EntityFactory<T>.Constructors[key] = EntityFactory<T>.GetConstructor(((IEnumerable<ConstructorInfo>) entityType.GetConstructors()).First<ConstructorInfo>());
    }
  }

  public EntityFactory(Simulator simulator) => this._simulator = simulator;

  public static EntityFactory<
  #nullable disable
  T>.Constructor GetConstructor(
  #nullable enable
  ConstructorInfo ctor)
  {
    ParameterExpression param = Expression.Parameter(typeof (object[]), "args");
    IEnumerable<UnaryExpression> arguments = ((IEnumerable<ParameterInfo>) ctor.GetParameters()).Select<ParameterInfo, UnaryExpression>((Func<ParameterInfo, int, UnaryExpression>) ((x, i) => Expression.Convert((Expression) Expression.ArrayIndex((Expression) param, (Expression) Expression.Constant((object) i)), x.ParameterType)));
    return Expression.Lambda<EntityFactory<T>.Constructor>((Expression) Expression.New(ctor, (IEnumerable<Expression>) arguments), param).Compile();
  }

  public bool HasImplementationFor(string cardId)
  {
    return EntityFactory<T>.Constructors.ContainsKey(cardId);
  }

  public delegate T Constructor(params object[] args) where T : 
  #nullable disable
  Entity;
}
