using static SBL;
using System.Numerics;
using static AllEnumMembers;
class UnnamedBehaviourTree
{
    float TotalUnitStrength;
    IEnumerable<AttackableUnit> TargetCollection;
    AttackableUnit Self;
    bool ValueChanged;
    Vector3 SelfPosition;
    float CurrentClosestDistance;
    AttackableUnit CurrentClosestTarget;
    bool LostAggro;
    AttackableUnit AggroTarget;
    Vector3 AggroPosition;
    float DeaggroDistance;
    float AccumulatedDamage;
    float PrevHealth;
    float PrevTime;
    float StrengthRatioOverTime;
    bool AggressiveKillMode;
    bool LowThreatMode;
    int PotionsToBuy;
    bool TeleportHome;
    Vector3 AssistPosition;
    AttackableUnit PreviousTarget;
    
    bool GarenBehavior()
    {
        
        return
        (
            GarenInit() &&
            (
                GarenAtBaseHealAndBuy() ||
                GarenLevelUp() ||
                GarenGameNotStarted() ||
                ReduceDamageTaken() ||
                GarenHighThreatManagement() ||
                GarenReturnToBase() ||
                GarenKillChampion() ||
                GarenLowThreatManagement() ||
                GarenHeal() ||
                GarenAttack() ||
                GarenPushLane()
            )
        );
    }
    
    bool GarenStrengthEvaluator()
    {
        AttackableUnit Unit;
        UnitType UnitType;
        return
        (
            SetVarFloat(out this.TotalUnitStrength, 1) &&
            this.TargetCollection.ForEach(Unit => (
                TestUnitIsVisible(this.Self, Unit) &&
                (
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == MINION_UNIT &&
                        AddFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 20)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == HERO_UNIT &&
                        AddFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 30)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == TURRET_UNIT &&
                        AddFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 90)
                    )
                )
            ))
        );
    }
    
    bool GarenFindClosestTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return
        (
            SetVarBool(out this.ValueChanged, false) &&
            this.TargetCollection.ForEach(Attacker => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                Distance < this.CurrentClosestDistance &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }
    
    bool GarenDeaggroChecker()
    {
        float Distance;
        return
        ((
            SetVarBool(out this.LostAggro, false) &&
            DistanceBetweenObjectAndPoint(out Distance, this.AggroTarget, this.AggroPosition) &&
            Distance > 800 &&
            Distance < 1200 &&
            SetVarBool(out this.LostAggro, true)
        ) || true);
    }
    
    bool GarenInit()
    {
        float CurrentTime;
        float TimeDiff;
        float EnemyStrength;
        float FriendStrength;
        float StrRatio;
        AttackableUnit Unit;
        UnitType UnitType;
        float CurrentHealth;
        float NewDamage;
        return
        (
            GetUnitAISelf(out this.Self) &&
            GetUnitPosition(out this.SelfPosition, this.Self) &&
            SetVarFloat(out this.DeaggroDistance, 1200) &&
            (
                (
                    TestUnitAIFirstTime() &&
                    SetVarFloat(out this.AccumulatedDamage, 0) &&
                    GetUnitCurrentHealth(out this.PrevHealth, this.Self) &&
                    GetGameTime(out this.PrevTime) &&
                    SetVarBool(out this.LostAggro, false) &&
                    SetVarFloat(out this.StrengthRatioOverTime, 1) &&
                    SetVarBool(out this.AggressiveKillMode, false) &&
                    SetVarBool(out this.LowThreatMode, false) &&
                    SetVarInt(out this.PotionsToBuy, 4) &&
                    SetVarBool(out this.TeleportHome, false)
                ) ||
                (
                    ((
                        GetGameTime(out CurrentTime) &&
                        SubtractFloat(out TimeDiff, CurrentTime, this.PrevTime) &&
                        (
                            TimeDiff > 1 ||
                            TimeDiff < 0
                        ) &&
                        (
                            MultiplyFloat(out this.AccumulatedDamage, this.AccumulatedDamage, 0.8f) &&
                            MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 0.8f)
                        ) &&
                        (
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                            GarenStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectFriends|AffectHeroes|AffectMinions|AffectTurrets) &&
                            GarenStrengthEvaluator() &&
                            SetVarFloat(out FriendStrength, this.TotalUnitStrength) &&
                            DivideFloat(out StrRatio, EnemyStrength, FriendStrength) &&
                            AddFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, StrRatio) &&
                            GetUnitAIAttackers(out this.TargetCollection, this.Self) &&
                            (this.TargetCollection.Any(Unit => (
                                GetUnitType(out UnitType, Unit) &&
                                UnitType == TURRET_UNIT &&
                                AddFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 8)
                            )) || true)
                        ) &&
                        GetGameTime(out this.PrevTime)
                    ) || true) &&
                    ((
                        GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                        SubtractFloat(out NewDamage, this.PrevHealth, CurrentHealth) &&
                        NewDamage > 0 &&
                        AddFloat(out this.AccumulatedDamage, this.AccumulatedDamage, NewDamage)
                    ) || true) &&
                    GetUnitCurrentHealth(out this.PrevHealth, this.Self)
                )
            )
        );
    }
    
    bool GarenAtBaseHealAndBuy()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float CurrentHealth;
        float Health_Ratio;
        float temp;
        return
        (
            GetUnitAIBasePosition(out BaseLocation, this.Self) &&
            DistanceBetweenObjectAndPoint(out Distance, this.Self, BaseLocation) &&
            Distance <= 450 &&
            SetVarBool(out this.TeleportHome, false) &&
            (
                (
                    DebugAction("Start ----- Heal -----") &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    Health_Ratio < 0.95f &&
                    DebugAction("Success ----- Heal -----")
                ) ||
                (
                    (
                        !TestChampionHasItem(this.Self, 1054) &&
                        TestUnitAICanBuyItem(1054) &&
                        UnitAIBuyItem(1054)
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 1001) &&
                        !TestChampionHasItem(this.Self, 3009) &&
                        !TestChampionHasItem(this.Self, 3117) &&
                        !TestChampionHasItem(this.Self, 3020) &&
                        !TestChampionHasItem(this.Self, 3006) &&
                        TestUnitAICanBuyItem(3111) &&
                        UnitAIBuyItem(1001)
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 3105) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1028) &&
                                TestUnitAICanBuyItem(1028) &&
                                UnitAIBuyItem(1028)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1029) &&
                                TestUnitAICanBuyItem(1029) &&
                                UnitAIBuyItem(1029)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1033) &&
                                TestUnitAICanBuyItem(1033) &&
                                UnitAIBuyItem(1033)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3105) &&
                                TestUnitAICanBuyItem(3105) &&
                                UnitAIBuyItem(3105)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 1001) &&
                        !TestChampionHasItem(this.Self, 3009) &&
                        TestUnitAICanBuyItem(3009) &&
                        UnitAIBuyItem(3009)
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        !TestChampionHasItem(this.Self, 3068) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1011) &&
                                TestUnitAICanBuyItem(1011) &&
                                UnitAIBuyItem(1011)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1031) &&
                                TestUnitAICanBuyItem(1031) &&
                                UnitAIBuyItem(1031)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3068) &&
                                TestUnitAICanBuyItem(3068) &&
                                UnitAIBuyItem(3068)
                            )
                        )
                    ) ||
                    (
                        GetUnitGold(out temp, this.Self) &&
                        temp > 0 &&
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        TestChampionHasItem(this.Self, 3068) &&
                        !TestChampionHasItem(this.Self, 3026) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1029) &&
                                TestUnitAICanBuyItem(1029) &&
                                UnitAIBuyItem(1029)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1033) &&
                                TestUnitAICanBuyItem(1033) &&
                                UnitAIBuyItem(1033)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1031) &&
                                TestUnitAICanBuyItem(1031) &&
                                UnitAIBuyItem(1031)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3026) &&
                                TestUnitAICanBuyItem(3026) &&
                                UnitAIBuyItem(3026)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        TestChampionHasItem(this.Self, 3068) &&
                        TestChampionHasItem(this.Self, 3026) &&
                        !TestChampionHasItem(this.Self, 3142) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1036) &&
                                !TestChampionHasItem(this.Self, 3134) &&
                                !TestChampionHasItem(this.Self, 3142) &&
                                TestUnitAICanBuyItem(1036) &&
                                UnitAIBuyItem(1036)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3134) &&
                                !TestChampionHasItem(this.Self, 3142) &&
                                TestUnitAICanBuyItem(3134) &&
                                UnitAIBuyItem(3134)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3142) &&
                                TestUnitAICanBuyItem(3142) &&
                                UnitAIBuyItem(3142)
                            )
                        )
                    ) ||
                    (
                        this.PotionsToBuy > 0 &&
                        !TestChampionHasItem(this.Self, 2003) &&
                        TestUnitAICanBuyItem(2003) &&
                        UnitAIBuyItem(2003) &&
                        SubtractInt(out this.PotionsToBuy, this.PotionsToBuy, 1)
                    )
                )
            ) &&
            DebugAction("++++ At Base Heal & Buy +++")
        );
    }
    
    bool GarenLevelUp()
    {
        int SkillPoints;
        int Ability0Level;
        int Ability1Level;
        int Ability2Level;
        return
        (
            GetUnitSkillPoints(out SkillPoints, this.Self) &&
            SkillPoints > 0 &&
            GetUnitSpellLevel(out Ability0Level, this.Self, SPELLBOOK_UNKNOWN, 0) &&
            GetUnitSpellLevel(out Ability1Level, this.Self, SPELLBOOK_UNKNOWN, 1) &&
            GetUnitSpellLevel(out Ability2Level, this.Self, SPELLBOOK_UNKNOWN, 2) &&
            (
                (
                    TestUnitCanLevelUpSpell(this.Self, 3) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                    DebugAction("levelup 3")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    (
                        (
                            Ability0Level >= 1 &&
                            Ability2Level >= 1 &&
                            Ability1Level <= 0
                        ) ||
                        (
                            Ability0Level >= 3 &&
                            Ability2Level >= 3 &&
                            Ability1Level <= 1
                        )
                    ) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("levelup 0")
                ) ||
                (
                    (
                        TestUnitCanLevelUpSpell(this.Self, 2) &&
                        Ability2Level <= Ability0Level &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                        DebugAction("levelup 0")
                    ) ||
                    (
                        TestUnitCanLevelUpSpell(this.Self, 0) &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                        DebugAction("levelup 0")
                    )
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("levelup 0")
                )
            ) &&
            DebugAction("++++ Level up ++++")
        );
    }
    
    bool GarenGameNotStarted()
    {
        
        return
        (
            !TestGameStarted() &&
            DebugAction("+++ Game Not Started +++")
        );
    }
    
    bool GarenAttack()
    {
        
        return
        (
            GarenAcquireTarget() &&
            GarenAttackTarget() &&
            DebugAction("++++ Attack ++++")
        );
    }
    
    bool GarenAcquireTarget()
    {
        IEnumerable<AttackableUnit> FriendlyUnits;
        AttackableUnit unit;
        int Count;
        float Distance;
        return
        (
            (
                SetVarBool(out this.LostAggro, false) &&
                TestUnitAIAttackTargetValid() &&
                GetUnitAIAttackTarget(out this.AggroTarget) &&
                SetVarVector(out this.AggroPosition, this.AssistPosition) &&
                TestUnitIsVisible(this.Self, this.AggroTarget) &&
                GarenDeaggroChecker() &&
                this.LostAggro == false &&
                DebugAction("+++ Use Previous Target +++")
            ) ||
            (
                DebugAction("EnableOrDisableAllyAggro") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends|AffectHeroes|AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                FriendlyUnits.ForEach(unit => (
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    GarenFindClosestVisibleTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    unit == this.Self &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                )) &&
                this.ValueChanged == true &&
                DebugAction("+++ Acquired Ally under attack +++")
            ) ||
            (
                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectBuildings|AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                (
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&
                    this.TargetCollection.ForEach(unit => (
                        DistanceBetweenObjectAndPoint(out Distance, unit, this.SelfPosition) &&
                        Distance < this.CurrentClosestDistance &&
                        TestUnitIsVisible(this.Self, unit) &&
                        (
                            (
                                this.LostAggro == true &&
                                GetUnitAIAttackTarget(out this.AggroTarget) &&
                                this.AggroTarget != unit
                            ) ||
                            this.LostAggro == false
                        ) &&
                        SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    ))
                ) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")
            )
        );
    }
    
    bool GarenAttackTarget()
    {
        AttackableUnit Target;
        TeamID SelfTeam;
        TeamID TargetTeam;
        UnitType UnitType;
        float currentHealth;
        float MaxHealth;
        float HP_Ratio;
        return
        (
            GetUnitAIAttackTarget(out Target) &&
            GetUnitTeam(out SelfTeam, this.Self) &&
            GetUnitTeam(out TargetTeam, Target) &&
            SelfTeam != TargetTeam &&
            (
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == MINION_UNIT &&
                    GetUnitCurrentHealth(out currentHealth, Target) &&
                    GetUnitMaxHealth(out MaxHealth, Target) &&
                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                    HP_Ratio < 0.2f &&
                    (
                        (
                            this.StrengthRatioOverTime > 2 &&
                            GarenCanCastAbility2() &&
                            SetUnitAIAttackTarget(this.Self) &&
                            GarenCastAbility2()
                        ) ||
                        (
                            GarenCanCastAbility0() &&
                            SetUnitAIAttackTarget(this.Self) &&
                            CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                        )
                    )
                ) ||
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == HERO_UNIT &&
                    (
                        (
                            GarenCanCastAbility0() &&
                            SetUnitAIAttackTarget(this.Self) &&
                            CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                        ) ||
                        (
                            GarenCanCastAbility2() &&
                            GarenCastAbility2()
                        )
                    )
                ) ||
                GarenAutoAttackTarget()
            ) &&
            DebugAction("++ Attack Success ++")
        );
    }
    
    bool GarenReturnToBase()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        Vector3 TeleportPosition;
        float DistanceToTeleportPosition;
        return
        (
            GetUnitAIBasePosition(out BaseLocation, this.Self) &&
            DistanceBetweenObjectAndPoint(out Distance, this.Self, BaseLocation) &&
            Distance > 300 &&
            (
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    (
                        (
                            this.TeleportHome == true &&
                            Health_Ratio <= 0.35f
                        ) ||
                        (
                            this.TeleportHome == false &&
                            Health_Ratio <= 0.25f &&
                            SetVarBool(out this.TeleportHome, true)
                        )
                    )
                ) ||
                (
                    !DebugAction("EmptyNode: HighGold")
                )
            ) &&
            (
                (
                    SetVarFloat(out this.CurrentClosestDistance, 30000) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends|AffectTurrets) &&
                    GarenFindClosestTarget() &&
                    this.ValueChanged == true &&
                    (
                        (
                            GetDistanceBetweenUnits(out Distance, this.CurrentClosestTarget, this.Self) &&
                            Distance < 125 &&
                            (
                                (
                                    TestUnitAISpellPositionValid() &&
                                    GetUnitAISpellPosition(out TeleportPosition) &&
                                    DistanceBetweenObjectAndPoint(out DistanceToTeleportPosition, this.Self, TeleportPosition) &&
                                    DistanceToTeleportPosition < 50
                                ) ||
                                !TestUnitAISpellPositionValid()
                            ) &&
                            IssueTeleportToBaseOrder() &&
                            ClearUnitAISpellPosition() &&
                            DebugAction("Yo")
                        ) ||
                        (
                            (
                                (
                                    !TestUnitAISpellPositionValid() &&
                                    ComputeUnitAISpellPosition(this.CurrentClosestTarget, this.Self, 150, false)
                                ) ||
                                TestUnitAISpellPositionValid()
                            ) &&
                            GetUnitAISpellPosition(out TeleportPosition) &&
                            IssueMoveToPositionOrder(TeleportPosition) &&
                            DebugAction("Yo")
                        )
                    )
                ) ||
                (
                    GetUnitAIBasePosition(out BaseLocation, this.Self) &&
                    IssueMoveToPositionOrder(BaseLocation)
                )
            ) &&
            DebugAction("+++ Teleport Home +++")
        );
    }
    
    bool GarenHighThreatManagement()
    {
        bool SuperHighThreat;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        float Damage_Ratio;
        return
        (
            (
                (
                    SetVarBool(out SuperHighThreat, false) &&
                    TestUnitUnderAttack(this.Self) &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    Health_Ratio <= 0.25f &&
                    DebugAction("+++ LowHealthUnderAttack +++") &&
                    SetVarBool(out SuperHighThreat, true)
                ) ||
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    DivideFloat(out Damage_Ratio, this.AccumulatedDamage, MaxHealth) &&
                    (
                        (
                            this.AggressiveKillMode == true &&
                            Damage_Ratio > 0.15f
                        ) ||
                        (
                            this.AggressiveKillMode == false &&
                            Damage_Ratio > 0.02f
                        )
                    ) &&
                    DebugAction("+++ BurstDamage +++")
                )
            ) &&
            DebugAction("+++ High Threat +++") &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    SuperHighThreat == true &&
                    GarenCanCastAbility1() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    SuperHighThreat == true &&
                    GarenCanCastAbility0() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                GarenMicroRetreat()
            ) &&
            DebugAction("+++ High Threat +++")
        );
    }
    
    bool GarenLowThreatManagement()
    {
        
        return
        (
            (
                (
                    this.StrengthRatioOverTime > 6 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    this.LowThreatMode == true &&
                    SetVarBool(out this.LowThreatMode, false) &&
                    this.StrengthRatioOverTime > 4 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    ClearUnitAISafePosition() &&
                    !DebugAction("DoNotRemoveForcedFail")
                )
            ) &&
            GarenMicroRetreat() &&
            DebugAction("++++ Low Threat +++")
        );
    }
    
    bool GarenKillChampion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        bool Aggressive;
        float MyHealthRatio;
        return
        (
            SetVarBool(out this.AggressiveKillMode, false) &&
            (
                (
                    this.StrengthRatioOverTime < 3 &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.8f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    this.TargetCollection.ForEach(unit => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        TestUnitIsVisible(this.Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    )) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, false) &&
                    DebugAction("PassiveKillChampion")
                ) ||
                (
                    this.StrengthRatioOverTime < 5.1f &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.4f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    this.TargetCollection.ForEach(unit => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        TestUnitIsVisible(this.Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    )) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, true) &&
                    SetVarBool(out this.AggressiveKillMode, true) &&
                    DebugAction("+++ AggressiveMode +++")
                )
            ) &&
            (
                (
                    GarenCanCastAbility0() &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                (
                    Aggressive == true &&
                    GarenCanCastAbility3() &&
                    GarenCastAbility3() &&
                    DebugAction("+++ Use Ultiamte +++")
                ) ||
                (
                    !TestUnitHasBuff(this.Self, null, "GarenBladestorm") &&
                    GarenCanCastAbility2() &&
                    GarenCastAbility2()
                ) ||
                GarenAutoAttackTarget() ||
                DebugAction("+++ Attack Champion+++")
            ) &&
            DebugAction("++++ Success: Kill  +++")
        );
    }
    
    bool GarenLastHitMinion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        AttackableUnit Target;
        return
        (
            (
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 800, AffectEnemies|AffectMinions) &&
                SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
                SetVarBool(out this.ValueChanged, false) &&
                this.TargetCollection.ForEach(unit => (
                    GetUnitCurrentHealth(out CurrentHealth, unit) &&
                    GetUnitMaxHealth(out MaxHealth, unit) &&
                    DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                    HP_Ratio < CurrentLowestHealthRatio &&
                    SetVarBool(out this.ValueChanged, true) &&
                    SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                    SetVarAttackableUnit(out this.CurrentClosestTarget, unit)
                )) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                SetVarAttackableUnit(out Target, this.CurrentClosestTarget)
            ) &&
            GarenAutoAttackTarget() &&
            DebugAction("+++++++ Last Hit ++++++++")
        );
    }
    
    bool GarenMicroRetreat()
    {
        Vector3 SafePosition;
        float Distance;
        return
        (
            (
                TestUnitAISafePositionValid() &&
                GetUnitAISafePosition(out SafePosition) &&
                (
                    (
                        DistanceBetweenObjectAndPoint(out Distance, this.Self, SafePosition) &&
                        Distance < 50 &&
                        ComputeUnitAISafePosition(800, false, false) &&
                        DebugAction("------- At location computed new position --------------")
                    ) ||
                    (
                        IssueMoveToPositionOrder(SafePosition) &&
                        DebugAction("------------ Success: Move to safe position ----------")
                    )
                )
            ) ||
            ComputeUnitAISafePosition(600, false, false)
        );
    }
    
    bool GarenAutoAttackTarget()
    {
        AttackableUnit Target;
        float Distance;
        float AttackRange;
        return
        (
            GetUnitAIAttackTarget(out Target) &&
            TestUnitAIAttackTargetValid() &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    GetUnitAttackRange(out AttackRange, this.Self) &&
                    MultiplyFloat(out AttackRange, AttackRange, 0.9f) &&
                    Distance <= AttackRange &&
                    ClearUnitAIAttackTarget() &&
                    SetUnitAIAttackTarget(Target) &&
                    IssueAttackOrder()
                ) ||
                IssueMoveToUnitOrder(Target)
            )
        );
    }
    
    bool GarenCanCastAbility0()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        );
    }
    
    bool GarenCanCastAbility1()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 1) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 1)
        );
    }
    
    bool GarenCanCastAbility2()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 2) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 2)
        );
    }
    
    bool GarenCanCastAbility3()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 3)
        );
    }
    
    bool GarenCastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        );
    }
    
    bool GarenCastAbility1()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        );
    }
    
    bool GarenCastAbility2()
    {
        AttackableUnit Target;
        float Range;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    TestUnitHasBuff(this.Self, null, "GarenBladestorm") &&
                    IssueMoveToUnitOrder(Target)
                ) ||
                (
                    GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
                    SetVarFloat(out Range, 200) &&
                    (
                        (
                            DebugAction("Pareparing to cast ability 2") &&
                            GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                            DebugAction("GoingToRangeCheck") &&
                            Distance <= Range &&
                            DebugAction("Range Check Succses") &&
                            CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                            DebugAction("Ability 2 Success ----------------")
                        ) ||
                        (
                            DebugAction("MoveIntoRangeSequence------------------") &&
                            IssueMoveToUnitOrder(Target) &&
                            DebugAction("Moving To Cast")
                        )
                    )
                )
            )
        );
    }
    
    bool GarenCastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 3) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        );
    }
    
    bool GarenPushLane()
    {
        
        return
        (
            ClearUnitAIAttackTarget() &&
            IssueMoveOrder() &&
            DebugAction("+++ Move To Lane +++")
        );
    }
    
    bool GarenMisc()
    {
        TeamID SelfTeam;
        TeamID UnitTeam;
        AttackableUnit Assist;
        float Distance;
        Vector3 AssistPosition;
        int Count;
        AttackableUnit Attacker;
        return
        (
            (
                !DebugAction("??? EnableOrDisablePreviousTarget ???") &&
                TestUnitAIAttackTargetValid() &&
                SetVarBool(out this.LostAggro, false) &&
                GetUnitAIAttackTarget(out this.PreviousTarget) &&
                GetUnitTeam(out SelfTeam, this.Self) &&
                GetUnitTeam(out UnitTeam, this.PreviousTarget) &&
                UnitTeam != SelfTeam &&
                GetUnitAIAssistTarget(out Assist) &&
                (
                    (
                        Assist == this.Self &&
                        DistanceBetweenObjectAndPoint(out Distance, this.Self, this.AssistPosition) &&
                        ((
                            Distance >= this.DeaggroDistance &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out this.LostAggro, true) &&
                            DebugAction("+++ Lost Aggro +++")
                        ) || true) &&
                        Distance < this.DeaggroDistance &&
                        DebugAction("+++ In Aggro Range, Use Previous")
                    ) ||
                    (
                        this.Self != Assist &&
                        GetUnitPosition(out AssistPosition, Assist) &&
                        DistanceBetweenObjectAndPoint(out Distance, this.PreviousTarget, this.SelfPosition) &&
                        ((
                            Distance >= 1000 &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out this.LostAggro, true) &&
                            DebugAction("------- Losing aggro from assist ----------")
                        ) || true) &&
                        Distance < 1000 &&
                        DebugAction("============= Use Previous Target: Still close to assist -----------")
                    )
                ) &&
                SetVarBool(out this.LostAggro, false) &&
                DebugAction("++ Use Previous Target ++")
            ) &&
            (
                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                (
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&
                    this.TargetCollection.ForEach(Attacker => (
                        (
                            (
                                this.LostAggro == true &&
                                Attacker != this.PreviousTarget
                            ) ||
                            this.LostAggro == false
                        ) &&
                        DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                        Distance < this.CurrentClosestDistance &&
                        SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                        SetVarBool(out this.ValueChanged, true)
                    ))
                ) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")
            )
        );
    }
    
    bool GarenHeal()
    {
        float Health;
        float MaxHealth;
        float HP_Ratio;
        return
        (
            GetUnitCurrentHealth(out Health, this.Self) &&
            GetUnitMaxHealth(out MaxHealth, this.Self) &&
            DivideFloat(out HP_Ratio, Health, MaxHealth) &&
            (
                HP_Ratio < 0.5f &&
                TestUnitAICanUseItem(2003) &&
                IssueUseItemOrder(2003)
            )
        );
    }
    
    bool ReduceDamageTaken()
    {
        float MaxHealth;
        float Damage_Ratio;
        return
        (
            GetUnitMaxHealth(out MaxHealth, this.Self) &&
            DivideFloat(out Damage_Ratio, this.AccumulatedDamage, MaxHealth) &&
            Damage_Ratio >= 0.1f &&
            (
                (
                    GarenCanCastAbility1() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    GarenCanCastAbility0() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                )
            )
        );
    }
    
    bool GarenFindClosestVisibleTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return
        (
            SetVarBool(out this.ValueChanged, false) &&
            this.TargetCollection.ForEach(Attacker => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                Distance < this.CurrentClosestDistance &&
                TestUnitIsVisible(this.Self, Attacker) &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }
}