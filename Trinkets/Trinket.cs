// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.Trinket
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Trinkets;

public class Trinket(string cardId, Simulator simulator, bool controlledByPlayer) : Entity(cardId, simulator, controlledByPlayer)
{
  public static readonly string[] NoOpTrinketCardIds = new string[128 /*0x80*/]
  {
    "BG35_MagicItem_309",
    "BG35_MagicItem_434",
    "BG35_MagicItem_850",
    "BG35_MagicItem_872",
    "BG35_MagicItem_301",
    "BG35_MagicItem_305",
    "BG35_MagicItem_890",
    "BG35_MagicItem_851",
    "BG35_MagicItem_817",
    "BG35_MagicItem_152",
    "BG35_MagicItem_150",
    "BG35_MagicItem_931",
    "BG35_MagicItem_306",
    "BG35_MagicItem_840",
    "BG35_MagicItem_870",
    "BG35_MagicItem_151",
    "BG35_MagicItem_303",
    "BG35_MagicItem_852",
    "BG35_MagicItem_921",
    "BG35_MagicItem_815",
    "BG32_MagicItem_931",
    "BG30_MagicItem_914",
    "BG32_MagicItem_361",
    "BG30_MagicItem_426",
    "BG35_MagicItem_821",
    "BG30_MagicItem_706",
    "BG30_MagicItem_420",
    "BG30_MagicItem_847",
    "BG30_MagicItem_430",
    "BG30_MagicItem_888",
    "BG30_MagicItem_891",
    "BG32_MagicItem_362",
    "BG30_MagicItem_425",
    "BG30_MagicItem_435",
    "BG30_MagicItem_705",
    "BG30_MagicItem_423",
    "BG30_MagicItem_703",
    "BG32_MagicItem_858",
    "BG32_MagicItem_350",
    "BG30_MagicItem_924",
    "BG30_MagicItem_416",
    "BG32_MagicItem_428",
    "BG30_MagicItem_707",
    "BG35_MagicItem_816",
    "BG30_MagicItem_841",
    "BG32_MagicItem_957",
    "BG35_MagicItem_842",
    "BG32_MagicItem_817",
    "BG32_MagicItem_271",
    "BG30_MagicItem_900",
    "BG35_MagicItem_870",
    "BG30_MagicItem_994",
    "BG32_MagicItem_806",
    "BG32_MagicItem_931",
    "BG32_MagicItem_951",
    "BG32_MagicItem_821",
    "BG30_MagicItem_821",
    "BG32_MagicItem_831",
    "BG32_MagicItem_822",
    "BG32_MagicItem_231",
    "BG32_MagicItem_417",
    "BG32_MagicItem_300",
    "BG32_MagicItem_276",
    "BG32_MagicItem_844",
    "BG32_MagicItem_954",
    "BG32_MagicItem_400",
    "BG32_MagicItem_830",
    "BG30_MagicItem_998",
    "BG35_MagicItem_850t",
    "BG35_MagicItem_862",
    "BG35_MagicItem_743",
    "BG35_MagicItem_861",
    "BG35_MagicItem_741",
    "BG35_MagicItem_712",
    "BG35_MagicItem_742",
    "BG35_MagicItem_863",
    "BG35_MagicItem_753",
    "BG35_MagicItem_733",
    "BG35_MagicItem_931t",
    "BG35_MagicItem_812",
    "BG35_MagicItem_930",
    "BG31_MagicItem_903",
    "BG35_MagicItem_840t",
    "BG30_MagicItem_914t",
    "BG32_MagicItem_361t",
    "BG30_MagicItem_426t",
    "BG32_MagicItem_901",
    "BG35_MagicItem_821t",
    "BG30_MagicItem_993",
    "BG32_MagicItem_366",
    "BG30_MagicItem_879",
    "BG32_MagicItem_231t",
    "BG30_MagicItem_943",
    "BG35_MagicItem_154",
    "BG32_MagicItem_362t",
    "BG30_MagicItem_876",
    "BG30_MagicItem_996",
    "BG30_MagicItem_544",
    "BG32_MagicItem_926",
    "BG30_MagicItem_924t",
    "BG30_MagicItem_541",
    "BG30_MagicItem_986",
    "BG35_MagicItem_750",
    "BG30_MagicItem_439",
    "BG35_MagicItem_310",
    "BG32_MagicItem_367",
    "BG32_MagicItem_807",
    "BG35_MagicItem_848t",
    "BG35_MagicItem_924",
    "BG35_MagicItem_752",
    "BG32_MagicItem_232",
    "BG30_MagicItem_900t",
    "BG32_MagicItem_364",
    "BG30_MagicItem_701",
    "BG32_MagicItem_998",
    "BG30_MagicItem_951",
    "BG32_MagicItem_172",
    "BG30_MagicItem_555",
    "BG30_MagicItem_406",
    "BG32_MagicItem_888",
    "BG32_MagicItem_282",
    "BG32_MagicItem_286",
    "BG30_MagicItem_999",
    "BG30_MagicItem_919",
    "BG32_MagicItem_922",
    "BG32_MagicItem_284",
    "BG35_MagicItem_151t",
    "BG35_MagicItem_156"
  };

  public int ScriptDataNum1 { get; set; }

  public int ScriptDataNum2 { get; set; }

  public Trinket Clone(Simulator? simulator = null)
  {
    Trinket trinket = (simulator ?? this.Simulator).TrinketFactory.Create(this.CardID, this.ControlledByPlayer);
    trinket.CloneFromBaseEntity((Entity) this);
    trinket.ScriptDataNum1 = this.ScriptDataNum1;
    trinket.ScriptDataNum2 = this.ScriptDataNum2;
    return trinket;
  }
}
