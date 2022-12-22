public class AttackableUnit {}
public class Champion {}
public class Turret {}
public enum TeamID
{
    TEAM_UNKNOWN,
    TEAM_ORDER
}
public enum PrimaryAbilityResourceType
{
    PAR_MANA
}

public enum BTInstanceType
{
    DELETE_SELF
}
public enum BehaveResult
{
    SUCCESS,
    FAILURE
}
public enum QuestType
{
    PRIMARY_QUEST
}
public enum SpellbookTypeEnum
{
    SPELLBOOK_UNKNOWN,
    SPELLBOOK_CHAMPION
}
public enum SpellFlags
{
    AffectEnemies, AffectHeroes, AffectMinions, AffectTurrets,
    AffectFriends, AlwaysSelf, AffectBuildings
}
public enum UnitType
{
    UNKNOWN_UNIT,
    UNKNOWN_CREATURE,
    MINION_UNIT,
    HERO_UNIT
}
public enum UIElement
{
    UI_MINIMAP
}

public static class AllEnumMembers
{
    public const UIElement UI_MINIMAP = UIElement.UI_MINIMAP;

    public const BTInstanceType DELETE_SELF = BTInstanceType.DELETE_SELF;
    
    public const BehaveResult SUCCESS = BehaveResult.SUCCESS;
    public const BehaveResult FAILURE = BehaveResult.FAILURE;
    
    public const TeamID TEAM_UNKNOWN = TeamID.TEAM_UNKNOWN;
    public const TeamID TEAM_ORDER = TeamID.TEAM_ORDER;
    
    public const PrimaryAbilityResourceType PAR_MANA = PrimaryAbilityResourceType.PAR_MANA;
    
    public const QuestType PRIMARY_QUEST = QuestType.PRIMARY_QUEST;

    public const SpellbookTypeEnum SPELLBOOK_UNKNOWN = SpellbookTypeEnum.SPELLBOOK_UNKNOWN;
    public const SpellbookTypeEnum SPELLBOOK_CHAMPION = SpellbookTypeEnum.SPELLBOOK_CHAMPION;

    public const SpellFlags AffectEnemies = SpellFlags.AffectEnemies;
    public const SpellFlags AffectHeroes = SpellFlags.AffectHeroes;
    public const SpellFlags AffectMinions = SpellFlags.AffectMinions;
    public const SpellFlags AffectTurrets = SpellFlags.AffectTurrets;
    public const SpellFlags AffectFriends = SpellFlags.AffectFriends;
    public const SpellFlags AlwaysSelf = SpellFlags.AlwaysSelf;
    public const SpellFlags AffectBuildings = SpellFlags.AffectBuildings;

    public const UnitType UNKNOWN_UNIT = UnitType.UNKNOWN_UNIT;
    public const UnitType UNKNOWN_CREATURE = UnitType.UNKNOWN_CREATURE;
    public const UnitType MINION_UNIT = UnitType.MINION_UNIT;
    public const UnitType HERO_UNIT = UnitType.HERO_UNIT;
}