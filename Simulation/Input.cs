// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Input
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Anomalies;
using BobsBuddy.Enchantments;
using BobsBuddy.Factory;
using BobsBuddy.Spells;
using BobsBuddy.Trinkets;
using BobsBuddy.Utils;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable enable
namespace BobsBuddy.Simulation;

public class Input
{
  public Anomaly? Anomaly;
  public bool isDuos;
  public int DamageCap;
  public int turn;
  public List<Race> availableRaces = new List<Race>();

  public Player Player { get; set; }

  public Player Opponent { get; set; }

  public Player? PlayerTeammate { get; set; }

  public Player? OpponentTeammate { get; set; }

  public Input()
  {
    this.Player = new Player(this);
    this.Opponent = new Player(this);
    this.PlayerTeammate = new Player(this);
    this.OpponentTeammate = new Player(this);
  }

  public string UnitTestableVersion => this.UnitTestCopyableVersion();

  private string UnitTestCopyablePlayers()
  {
    string str1 = "";
    Dictionary<string, Player> dictionary = new Dictionary<string, Player>()
    {
      {
        "Player",
        this.Player
      },
      {
        "Opponent",
        this.Opponent
      }
    };
    if (this.isDuos)
    {
      if (this.PlayerTeammate != null)
        dictionary.Add("PlayerTeammate", this.PlayerTeammate);
      if (this.OpponentTeammate != null)
        dictionary.Add("OpponentTeammate", this.OpponentTeammate);
    }
    foreach (KeyValuePair<string, Player> keyValuePair in dictionary)
    {
      Player player = keyValuePair.Value;
      string playerString = keyValuePair.Key;
      string str2 = playerString;
      bool flag = str2 == "Player" || str2 == "PlayerTeammate";
      string friendlyString = flag.ToString().ToLower();
      if (player.Health != 0)
        str1 += $"\n\t\tinput.{playerString}.Health = {player.Health};";
      if (player.DamageTaken != 0)
        str1 += $"\n\t\tinput.{playerString}.DamageTaken = {player.DamageTaken};";
      if (player.Tier != 0)
        str1 += $"\n\t\tinput.{playerString}.Tier = {player.Tier};";
      if (player.FriendlyMinionsDeadLastCombatCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.FriendlyMinionsDeadLastCombatCounter = {player.FriendlyMinionsDeadLastCombatCounter};";
      if (player.EternalKnightCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.EternalKnightCounter = {player.EternalKnightCounter};";
      if (player.AncestralAutomatonCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.AncestralAutomatonCounter = {player.AncestralAutomatonCounter};";
      if (player.BeastsSummonCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.BeastsSummonCounter = {player.BeastsSummonCounter};";
      if (player.BeetlesAtkBuff > 0)
        str1 += $"\n\t\tinput.{playerString}.BeetlesAtkBuff = {player.BeetlesAtkBuff};";
      if (player.BeetlesHealthBuff > 0)
        str1 += $"\n\t\tinput.{playerString}.BeetlesHealthBuff = {player.BeetlesHealthBuff};";
      if (player.UndeadAttackBonus > 0)
        str1 += $"\n\t\tinput.{playerString}.UndeadAttackBonus = {player.UndeadAttackBonus};";
      if (player.ElementalPlayCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.ElementalPlayCounter = {player.ElementalPlayCounter};";
      if (player.BloodGemAtkBuff > 0)
        str1 += $"\n\t\tinput.{playerString}.BloodGemAtkBuff = {player.BloodGemAtkBuff};";
      if (player.BloodGemHealthBuff > 1)
        str1 += $"\n\t\tinput.{playerString}.BloodGemHealthBuff = {player.BloodGemHealthBuff};";
      if (player.DeepBluesCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.DeepBluesCounter = {player.DeepBluesCounter};";
      if (player.BeastAttackBonus > 0)
        str1 += $"\n\t\tinput.{playerString}.BeastAttackBonus = {player.BeastAttackBonus};";
      if (player.BeastHealthBonus > 1)
        str1 += $"\n\t\tinput.{playerString}.BeastHealthBonus = {player.BeastHealthBonus};";
      if (player.TavernSpellCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.TavernSpellCounter = {player.TavernSpellCounter};";
      if (player.AnySpellCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.AnySpellCounter = {player.AnySpellCounter};";
      if (player.TavernSpellAtkBuff > 0)
        str1 += $"\n\t\tinput.{playerString}.TavernSpellAtkBuff = {player.TavernSpellAtkBuff};";
      if (player.TavernSpellHealthBuff > 0)
        str1 += $"\n\t\tinput.{playerString}.TavernSpellHealthBuff = {player.TavernSpellHealthBuff};";
      if (player.HauntedAtkBuff > 0 && !flag)
        str1 += $"\n\t\tinput.{playerString}.HauntedAtkBuff = {player.HauntedAtkBuff};";
      if (player.HauntedHealthBuff > 0 && !flag)
        str1 += $"\n\t\tinput.{playerString}.HauntedHealthBuff = {player.HauntedHealthBuff};";
      if (player.HauntedAtkBuff > 0 && !flag)
        str1 += $"\ninput.{playerString}.HauntedAtkBuff = {player.HauntedAtkBuff};";
      if (player.HauntedHealthBuff > 0 && !flag)
        str1 += $"\ninput.{playerString}.HauntedHealthBuff = {player.HauntedHealthBuff};";
      if (player.PiratesSummonCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.PiratesSummonCounter = {player.PiratesSummonCounter};";
      if (player.WhelpAttackBonus > 0)
        str1 += $"\n\t\tinput.{playerString}.WhelpAttackBonus = {player.WhelpAttackBonus};";
      if (player.WhelpHealthBonus > 0)
        str1 += $"\n\t\tinput.{playerString}.WhelpHealthBonus = {player.WhelpHealthBonus};";
      if (player.BattlecryCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.BattlecryCounter = {player.BattlecryCounter};";
      if (player.DeathrattleCounter > 0)
        str1 += $"\n\t\tinput.{playerString}.DeathrattleCounter = {player.DeathrattleCounter};";
      if (player.VolumizerAtkBuff > 0)
        str1 += $"\n\t\tinput.{playerString}.VolumizerAtkBuff = {player.VolumizerAtkBuff};";
      if (player.VolumizerHealthBuff > 0)
        str1 += $"\n\t\tinput.{playerString}.VolumizerHealthBuff = {player.VolumizerHealthBuff};";
      foreach (QuestData quest in player.Quests)
      {
        string str3 = $"new QuestData() {{ QuestCardId=\"{quest.QuestCardId}\", RewardCardId=\"{quest.RewardCardId}\", QuestProgress={quest.QuestProgress}, QuestProgressTotal={quest.QuestProgressTotal}";
        if (quest.RewardScriptDataNum1 > 0)
          str3 += $", RewardScriptDataNum1={quest.RewardScriptDataNum1}";
        if (quest.RewardScriptDataNum2 > 0)
          str3 += $", RewardScriptDataNum2={quest.RewardScriptDataNum2}";
        string str4 = str3 + " }";
        str1 = $"{str1}\n\t\tinput.{playerString}.Quests.Add({str4});";
      }
      foreach (HeroPowerData heroPower1 in player.HeroPowers)
      {
        HeroPowerData heroPower = heroPower1;
        if (heroPower.AttachedMinion != null)
          str1 += this.CreateMinionStmt("CreateMinion", heroPower.AttachedMinion, (Func<string, string>) (m => $"input.{playerString}.AddHeroPower(\"{heroPower.CardId}\", {friendlyString}, {heroPower.IsActivated.ToString().ToLower()}, {heroPower.Data}, {heroPower.Data2}, {heroPower.Data3}, {m}); // {HearthdbUtil.CardNameFromID(heroPower.CardId)}"));
        else
          str1 += $"\n\t\tinput.{playerString}.AddHeroPower(\"{heroPower.CardId}\", {friendlyString}, {heroPower.IsActivated.ToString().ToLower()}, {heroPower.Data}, {heroPower.Data2}, {heroPower.Data3}); // {HearthdbUtil.CardNameFromID(heroPower.CardId)}";
      }
    }
    foreach (KeyValuePair<string, Player> keyValuePair in dictionary)
    {
      Player player = keyValuePair.Value;
      string key = keyValuePair.Key;
      if (player.Hand.Any<CardEntity>())
        str1 = $"{str1}\n\n\t\t// {key} Hand";
      for (int index = 0; index < player.Hand.Count; ++index)
      {
        CardEntity cardEntity = player.Hand[index];
        if (cardEntity is MinionCardEntity minionCardEntity)
        {
          str1 += this.CreateMinionStmt($"AddMinion{key}Hand", minionCardEntity.Data);
          if (!minionCardEntity.CanSummon)
            str1 += $"\n\t\t((MinionCardEntity)input.{key}.Hand[{index}]).CanSummon = false;";
        }
        else
          str1 += $"\n\t\tinput.AddCard{key}Hand(\"{cardEntity}\");";
      }
      if (player.Quests.Any<QuestData>())
        str1 = $"{str1}\n\n\t\t// {key} Quests";
      foreach (QuestData quest in player.Quests)
      {
        string str5 = $"new QuestData() {{ QuestCardId=\"{quest.QuestCardId}\", RewardCardId=\"{quest.RewardCardId}\", QuestProgress={quest.QuestProgress}, QuestProgressTotal={quest.QuestProgressTotal}";
        if (quest.RewardScriptDataNum1 > 0)
          str5 += $", RewardScriptDataNum1={quest.RewardScriptDataNum1}";
        if (quest.RewardScriptDataNum2 > 0)
          str5 += $", RewardScriptDataNum2={quest.RewardScriptDataNum2}";
        string str6 = str5 + " }";
        str1 = $"{str1}\n\t\tinput.{key}.Quests.Add({str6});";
      }
      if (player.Objectives.Any<Objective>())
        str1 = $"{str1}\n\n\t\t// {key} Objectives";
      foreach (Objective objective in player.Objectives)
        str1 += $"\n\t\tinput.Add{key}Objective(\"{objective.CardID}\", {objective.ScriptDataNum1}, {objective.ScriptDataNum2}); // {HearthdbUtil.CardNameFromID(objective.CardID)}";
      if (player.Trinkets.Any<Trinket>())
        str1 = $"{str1}\n\n\t\t// {key} Trinkets";
      foreach (Trinket trinket in player.Trinkets)
      {
        str1 += $"\n\t\tinput.Add{key}Trinket(\"{trinket.CardID}\", {trinket.ScriptDataNum1}, {trinket.ScriptDataNum2}); // {HearthdbUtil.CardNameFromID(trinket.CardID)}";
        if (((IEnumerable<string>) Trinket.NoOpTrinketCardIds).Contains<string>(trinket.CardID))
          str1 += " (NoOp)";
      }
      if (player.Secrets.Any<Simulator.Secrets>())
      {
        str1 = $"{str1}\n\n\t\t// {key} S.";
        foreach (Simulator.Secrets secret in player.Secrets)
          str1 += $"\n\t\tinput.{key}.AddSecret({secret});";
      }
    }
    foreach (KeyValuePair<string, Player> keyValuePair in dictionary)
    {
      Player player = keyValuePair.Value;
      string key = keyValuePair.Key;
      if (player.Side.Any<Minion>())
        str1 = $"{str1}\n\n\t\t// {key} Board";
      foreach (Minion m in player.Side)
        str1 += this.CreateMinionStmt("Add" + key, m);
    }
    return str1;
  }

  private string UnitTestCopyableVersion()
  {
    string str1 = "" + "\n\t[TestMethod]\n\t" + "public async Task TESTNAME()\n\t{";
    if (this.isDuos)
      str1 += "\n\t\tinput.isDuos = true;";
    string str2 = str1 + $"\n\t\tinput.turn = {this.turn};";
    if (this.DamageCap > 0)
      str2 += $"\n\t\tinput.DamageCap = {this.DamageCap};";
    if (this.Anomaly != null)
      str2 = $"{str2}\n\t\tinput.SetAnomaly(\"{this.Anomaly.CardID}\"); // {HearthdbUtil.CardNameFromID(this.Anomaly.CardID)}";
    if (this.availableRaces.Any<Race>())
    {
      List<string> list = this.availableRaces.Select<Race, string>((Func<Race, string>) (x => $"Race.{x}")).ToList<string>();
      str2 = $"{str2}\n\t\tinput.availableRaces = new() {{ {string.Join(", ", (IEnumerable<string>) list)} }};";
    }
    return str2 + this.UnitTestCopyablePlayers() + "\n\t\tvar threadCount = TestingConstants.OneThread;" + "\n\t\tvar output = await new SimulationRunner().SimulateMultiThreaded(input, 100, threadCount);" + "\n\t\tAssert.AreNotEqual(0, output.);\n\t}";
  }

  private string CreateMinionStmt(string fn, Minion m, Func<string, string>? extra = null)
  {
    string cardID = m.golden ? MinionFactory.TryGetPremiumIdFromNormal(m.CardID) : m.CardID;
    string str1 = $"input.{fn}(\"{cardID}\", {m.baseAttack}, {m.baseHealth})";
    if (m.golden)
      str1 += ".SetGolden()";
    if (m.taunt)
      str1 += ".SetTaunt()";
    if (m.hasDiv)
      str1 += ".SetDivineShield()";
    if (m.cleave)
      str1 += ".SetCleave()";
    if (m.poisonous)
      str1 += ".SetPoisonous()";
    if (m.venomous)
      str1 += ".SetVenomous()";
    if (m.windfury)
      str1 += ".SetWindfury()";
    if (m.megaWindfury)
      str1 += ".SetMegaWindfury()";
    if (m.stealth)
      str1 += ".SetStealth()";
    if (m.cannotAttack)
      str1 += ".SetCannotAttack()";
    if (m.reborn)
      str1 += ".SetReborn()";
    if (m.game_id > 0)
      str1 += $".SetEntityId({m.game_id})";
    string str2 = HearthdbUtil.CardNameFromID(cardID);
    string str3 = $"{str1}; // {str2}";
    if (m.AttachedModularEntity == null && !m.AdditionalDeathrattles.Any<Action<Minion>>() && !m.AdditionalRallies.Any<Action<Minion>>() && m.ScriptDataNum1 <= 0 && m.ScriptDataNum2 <= 0 && m.ScriptDataNum3 <= 0 && m.ScriptDataNum4 <= 0)
    {
      (int, int) statsFromBloodGems = m.StatsFromBloodGems;
      if (statsFromBloodGems.Item1 == 0 && statsFromBloodGems.Item2 == 0 && !m.Enchantments.Any<Enchantment>() && extra == null)
        goto label_58;
    }
    string str4 = "{\n\t\t\tvar m = " + str3;
    foreach (Action<Minion> additionalDeathrattle in m.AdditionalDeathrattles)
      str4 = $"{str4}\n\t\t\tm.AdditionalDeathrattles.Add({FormatUtils.GetCleanMethodIdentifier((MethodBase) additionalDeathrattle.Method)});";
    foreach (Action<Minion> additionalRally in m.AdditionalRallies)
      str4 = $"{str4}\n\t\t\tm.AdditionalRallies.Add({FormatUtils.GetCleanMethodIdentifier((MethodBase) additionalRally.Method)});";
    if (m.AttachedModularEntity != null)
      str4 = $"{str4}\n\t\t\tm.AttachModularEntity(\"{m.AttachedModularEntity.CardID}\");";
    for (int index = 0; index < m.Enchantments.Count; ++index)
    {
      Enchantment enchantment = m.Enchantments[index];
      string str5 = $"e{index + 1}";
      str4 += $"\n\t\t\tvar {str5} = input.AttachEnchantment({enchantment.GetType()}.CardId, m);";
      if (enchantment.ScriptDataNum1 > 0)
        str4 += $"\n\t\t\t{str5}.ScriptDataNum1 = {enchantment.ScriptDataNum1};";
      if (enchantment.ScriptDataNum2 > 0)
        str4 += $"\n\t\t\t{str5}.ScriptDataNum2 = {enchantment.ScriptDataNum2};";
    }
    if (m.ScriptDataNum1 > 0)
      str4 += $"\n\t\t\tm.ScriptDataNum1 = {m.ScriptDataNum1};";
    if (m.ScriptDataNum2 > 0)
      str4 += $"\n\t\t\tm.ScriptDataNum2 = {m.ScriptDataNum2};";
    if (m.ScriptDataNum3 > 0)
      str4 += $"\n\t\t\tm.ScriptDataNum3 = {m.ScriptDataNum3};";
    if (m.ScriptDataNum4 > 0)
      str4 += $"\n\t\t\tm.ScriptDataNum4 = {m.ScriptDataNum4};";
    (int, int) statsFromBloodGems1 = m.StatsFromBloodGems;
    if (statsFromBloodGems1.Item1 != 0 || statsFromBloodGems1.Item2 != 0)
    {
      (int num1, int num2) = m.StatsFromBloodGems;
      str4 += $"\n\t\t\tm.SetBloodGemStats({num1}, {num2});";
    }
    if (extra != null)
      str4 = $"{str4}\n\t\t\t{extra(nameof (m))}";
    str3 = str4 + "\n\t\t}";
label_58:
    return "\n\t\t" + str3;
  }

  public void SetHealths(int playerHealth, int opponentHealth)
  {
    this.Player.Health = playerHealth;
    this.Opponent.Health = opponentHealth;
  }

  public void SetTiers(int playerTier, int opponentTier)
  {
    this.Player.Tier = playerTier;
    this.Opponent.Tier = opponentTier;
  }

  public void SetTeammateTiers(int playerTier, int opponentTier)
  {
    if (this.PlayerTeammate != null)
      this.PlayerTeammate.Tier = playerTier;
    if (this.OpponentTeammate == null)
      return;
    this.OpponentTeammate.Tier = opponentTier;
  }

  public void SetTurn(int turn) => this.turn = turn;

  public void AddCardsToHand(int cardNumber, Player player, Simulator simulator)
  {
    for (int index = 0; index < cardNumber; ++index)
      player.Hand.Add((CardEntity) new RandomMinionCardEntity((Entity) null, simulator));
  }

  public void AddSecretFromDbfIdHstracker(int id, List<Simulator.Secrets> target)
  {
    this.AddSecretFromDbfId(id != 0 ? new int?(id) : new int?(), target);
  }

  public void AddSecretFromDbfId(int? id, List<Simulator.Secrets> target)
  {
    if (!id.HasValue)
    {
      target.Add(Simulator.Secrets.Unknown);
    }
    else
    {
      Simulator.Secrets secrets;
      if (!SecretDbfidTable.secretDbfidTable.TryGetValue(id.Value, out secrets))
        return;
      target.Add(secrets);
    }
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("Opponent side: " + string.Join(", ", this.Opponent.Side.Select<Minion, string>((Func<Minion, string>) (x => x.ToString()))));
    stringBuilder.AppendLine("Player side: " + string.Join(", ", this.Player.Side.Select<Minion, string>((Func<Minion, string>) (x => x.ToString()))));
    stringBuilder.AppendLine($"Opponent Hero Power: {HearthdbUtil.CardNameFromID(this.Opponent.HeroPowers[0].CardId)} ({this.Opponent.HeroPowers[0].CardId}), Activated={this.Opponent.HeroPowers[0].IsActivated}, Data={this.Opponent.HeroPowers[0].Data}, Data2=${this.Opponent.HeroPowers[0].Data2}");
    stringBuilder.AppendLine($"Player Hero Power: {HearthdbUtil.CardNameFromID(this.Player.HeroPowers[0].CardId)} ({this.Player.HeroPowers[0].CardId}), Activated={this.Player.HeroPowers[0].IsActivated}, Data={this.Player.HeroPowers[0].Data}, Data2={this.Player.HeroPowers[0].Data2}");
    if (this.Opponent.HeroPowers.Count > 1)
      stringBuilder.AppendLine($"Opponent Extra Hero Power: {HearthdbUtil.CardNameFromID(this.Opponent.HeroPowers[1].CardId)} ({this.Opponent.HeroPowers[1].CardId}), Activated={this.Opponent.HeroPowers[1].IsActivated}, Data={this.Opponent.HeroPowers[1].Data}, Data2=${this.Opponent.HeroPowers[1].Data2}");
    if (this.Player.HeroPowers.Count > 1)
      stringBuilder.AppendLine($"Player Extra Hero Power: {HearthdbUtil.CardNameFromID(this.Player.HeroPowers[1].CardId)} ({this.Player.HeroPowers[1].CardId}), Activated={this.Player.HeroPowers[1].IsActivated}, Data={this.Player.HeroPowers[1].Data}, Data2={this.Player.HeroPowers[1].Data2}");
    stringBuilder.AppendLine($"Opponent tier: {this.Opponent.Tier}");
    stringBuilder.AppendLine($"Player tier: {this.Player.Tier}");
    if (this.Anomaly != null)
      stringBuilder.Append("Anomaly: " + HearthdbUtil.CardNameFromID(this.Anomaly.CardID));
    if (this.Opponent.Secrets.Any<Simulator.Secrets>())
      stringBuilder.AppendLine("Opponent Secr.: " + string.Join<Simulator.Secrets>(", ", (IEnumerable<Simulator.Secrets>) this.Opponent.Secrets));
    if (this.Player.Secrets.Any<Simulator.Secrets>())
      stringBuilder.AppendLine("Player Secr.: " + string.Join<Simulator.Secrets>(", ", (IEnumerable<Simulator.Secrets>) this.Player.Secrets));
    return stringBuilder.ToString();
  }
}
