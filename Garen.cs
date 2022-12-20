class UnnamedBehaviourTree
{
    async Task<bool> GarenBehavior()
    {
        return
        (
            await GarenInit() &&
            (
                await GarenAtBaseHealAndBuy()
                ||
                await GarenLevelUp()
                ||
                await GarenGameNotStarted()
                ||
                await ReduceDamageTaken()
                ||
                await GarenHighThreatManagement()
                ||
                await GarenReturnToBase()
                ||
                await GarenKillChampion()
                ||
                await GarenLowThreatManagement()
                ||
                await GarenHeal()
                ||
                await GarenAttack()
                ||
                await GarenPushLane()
            )
        );
    }
    
    async Task<bool> GarenStrengthEvaluator()
    {
        return
        (
            (Global.TotalUnitStrength = 1, true) &&
            (Tree.Unit = await IterateOverAllDecorator(Collection: Global.TargetCollection), true)
        );
    }
    
    async Task<bool> GarenFindClosestTarget()
    {
        return
        (
            (Global.ValueChanged = false, true) &&
            (Tree.Attacker = await IterateOverAllDecorator(Collection: Global.TargetCollection), true)
        );
    }
    
    async Task<bool> GarenDeaggroChecker()
    {
        return
        ((
            (Global.LostAggro = false, true) &&
            (Tree.Distance = await DistanceBetweenObjectAndPoint(Unit: Global.AggroTarget, Point: Global.AggroPosition), true) &&
            Tree.Distance > 800 &&
            Tree.Distance < 1200 &&
            (Global.LostAggro = true, true)
        ), true);
    }
    
    async Task<bool> GarenInit()
    {
        return
        (
            (Global.Self = await GetUnitAISelf(), true) &&
            (Global.SelfPosition = await GetUnitPosition(Unit: Global.Self), true) &&
            (Global.DeaggroDistance = 1200, true) &&
            (
                (
                    await TestUnitAIFirstTime() &&
                    (Global.AccumulatedDamage = 0, true) &&
                    (Global.PrevHealth = await GetUnitCurrentHealth(Unit: Global.Self), true) &&
                    (Global.PrevTime = await GetGameTime(), true) &&
                    (Global.LostAggro = false, true) &&
                    (Global.StrengthRatioOverTime = 1, true) &&
                    (Global.AggressiveKillMode = false, true) &&
                    (Global.LowThreatMode = false, true) &&
                    (Global.PotionsToBuy = 4, true) &&
                    (Global.TeleportHome = false, true)
                )
                ||
                (
                    ((
                        (Tree.CurrentTime = await GetGameTime(), true) &&
                        (Tree.TimeDiff = Tree.CurrentTime - Global.PrevTime, true) &&
                        (
                            Tree.TimeDiff > 1
                            ||
                            Tree.TimeDiff < 0
                        ) &&
                        (
                            (Global.AccumulatedDamage = Global.AccumulatedDamage * 0.8, true) &&
                            (Global.StrengthRatioOverTime = Global.StrengthRatioOverTime * 0.8, true)
                        ) &&
                        (
                            (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "1000", SpellFlags: "AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets"), true) &&
                            await GarenStrengthEvaluator() &&
                            (Tree.EnemyStrength = Global.TotalUnitStrength, true) &&
                            (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectFriends,AffectHeroes,AffectMinions,AffectTurrets"), true) &&
                            await GarenStrengthEvaluator() &&
                            (Tree.FriendStrength = Global.TotalUnitStrength, true) &&
                            (Tree.StrRatio = Tree.EnemyStrength / Tree.FriendStrength, true) &&
                            (Global.StrengthRatioOverTime = Global.StrengthRatioOverTime + Tree.StrRatio, true) &&
                            (Global.TargetCollection = await GetUnitAIAttackers(Unit: Global.Self), true) &&
                            ((Tree.Unit = await IterateUntilSuccessDecorator(Collection: Global.TargetCollection), true), true)
                        ) &&
                        (Global.PrevTime = await GetGameTime(), true)
                    ), true) &&
                    ((
                        (Tree.CurrentHealth = await GetUnitCurrentHealth(Unit: Global.Self), true) &&
                        (Tree.NewDamage = Global.PrevHealth - Tree.CurrentHealth, true) &&
                        Tree.NewDamage > 0 &&
                        (Global.AccumulatedDamage = Global.AccumulatedDamage + Tree.NewDamage, true)
                    ), true) &&
                    (Global.PrevHealth = await GetUnitCurrentHealth(Unit: Global.Self), true)
                )
            )
        );
    }
    
    async Task<bool> GarenAtBaseHealAndBuy()
    {
        return
        (
            (Tree.BaseLocation = await GetUnitAIBasePosition(Unit: Global.Self), true) &&
            (Tree.Distance = await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.BaseLocation), true) &&
            Tree.Distance <= 450 &&
            (Global.TeleportHome = false, true) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Start ----- Heal -----") &&
                    (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Global.Self), true) &&
                    (Tree.CurrentHealth = await GetUnitCurrentHealth(Unit: Global.Self), true) &&
                    (Tree.Health_Ratio = Tree.CurrentHealth / Tree.MaxHealth, true) &&
                    Tree.Health_Ratio < 0.95 &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Success ----- Heal -----")
                )
                ||
                (
                    (
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "1054") &&
                        await TestUnitAICanBuyItem(ItemID: "1054") &&
                        await UnitAIBuyItem(ItemID: "1054")
                    )
                    ||
                    (
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "1001") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3009") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3117") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3020") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3006") &&
                        await TestUnitAICanBuyItem(ItemID: "3111") &&
                        await UnitAIBuyItem(ItemID: "1001")
                    )
                    ||
                    (
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3105") &&
                        (
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1028") &&
                                await TestUnitAICanBuyItem(ItemID: "1028") &&
                                await UnitAIBuyItem(ItemID: "1028")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1029") &&
                                await TestUnitAICanBuyItem(ItemID: "1029") &&
                                await UnitAIBuyItem(ItemID: "1029")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1033") &&
                                await TestUnitAICanBuyItem(ItemID: "1033") &&
                                await UnitAIBuyItem(ItemID: "1033")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3105") &&
                                await TestUnitAICanBuyItem(ItemID: "3105") &&
                                await UnitAIBuyItem(ItemID: "3105")
                            )
                        )
                    )
                    ||
                    (
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3105") &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "1001") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3009") &&
                        await TestUnitAICanBuyItem(ItemID: "3009") &&
                        await UnitAIBuyItem(ItemID: "3009")
                    )
                    ||
                    (
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3105") &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3009") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3068") &&
                        (
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1011") &&
                                await TestUnitAICanBuyItem(ItemID: "1011") &&
                                await UnitAIBuyItem(ItemID: "1011")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1031") &&
                                await TestUnitAICanBuyItem(ItemID: "1031") &&
                                await UnitAIBuyItem(ItemID: "1031")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3068") &&
                                await TestUnitAICanBuyItem(ItemID: "3068") &&
                                await UnitAIBuyItem(ItemID: "3068")
                            )
                        )
                    )
                    ||
                    (
                        (Tree.temp = await GetUnitGold(Unit: Global.Self), true) &&
                        Tree.temp > 0 &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3105") &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3009") &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3068") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3026") &&
                        (
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1029") &&
                                await TestUnitAICanBuyItem(ItemID: "1029") &&
                                await UnitAIBuyItem(ItemID: "1029")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1033") &&
                                await TestUnitAICanBuyItem(ItemID: "1033") &&
                                await UnitAIBuyItem(ItemID: "1033")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1031") &&
                                await TestUnitAICanBuyItem(ItemID: "1031") &&
                                await UnitAIBuyItem(ItemID: "1031")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3026") &&
                                await TestUnitAICanBuyItem(ItemID: "3026") &&
                                await UnitAIBuyItem(ItemID: "3026")
                            )
                        )
                    )
                    ||
                    (
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3105") &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3009") &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3068") &&
                        await TestChampionHasItem(Unit: Global.Self, ItemID: "3026") &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "3142") &&
                        (
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "1036") &&
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3134") &&
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3142") &&
                                await TestUnitAICanBuyItem(ItemID: "1036") &&
                                await UnitAIBuyItem(ItemID: "1036")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3134") &&
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3142") &&
                                await TestUnitAICanBuyItem(ItemID: "3134") &&
                                await UnitAIBuyItem(ItemID: "3134")
                            )
                            ||
                            (
                                !await TestChampionHasItem(Unit: Global.Self, ItemID: "3142") &&
                                await TestUnitAICanBuyItem(ItemID: "3142") &&
                                await UnitAIBuyItem(ItemID: "3142")
                            )
                        )
                    )
                    ||
                    (
                        Global.PotionsToBuy > 0 &&
                        !await TestChampionHasItem(Unit: Global.Self, ItemID: "2003") &&
                        await TestUnitAICanBuyItem(ItemID: "2003") &&
                        await UnitAIBuyItem(ItemID: "2003") &&
                        (Global.PotionsToBuy = Global.PotionsToBuy - 1, true)
                    )
                )
            ) &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ At Base Heal & Buy +++")
        );
    }
    
    async Task<bool> GarenLevelUp()
    {
        return
        (
            (Tree.SkillPoints = await GetUnitSkillPoints(Unit: Global.Self), true) &&
            Tree.SkillPoints > 0 &&
            (Tree.Ability0Level = await GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "0"), true) &&
            (Tree.Ability1Level = await GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "1"), true) &&
            (Tree.Ability2Level = await GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "2"), true) &&
            (
                (
                    await TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "3") &&
                    await LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3") &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 3")
                )
                ||
                (
                    await TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "1") &&
                    (
                        (
                            Tree.Ability0Level >= 1 &&
                            Tree.Ability2Level >= 1 &&
                            Tree.Ability1Level <= 0
                        )
                        ||
                        (
                            Tree.Ability0Level >= 3 &&
                            Tree.Ability2Level >= 3 &&
                            Tree.Ability1Level <= 1
                        )
                    ) &&
                    await LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1") &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                )
                ||
                (
                    (
                        await TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "2") &&
                        Tree.Ability2Level <= Tree.Ability0Level &&
                        await LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2") &&
                        await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                    )
                    ||
                    (
                        await TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "0") &&
                        await LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0") &&
                        await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                    )
                )
                ||
                (
                    await TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "1") &&
                    await LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1") &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                )
            ) &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Level up ++++")
        );
    }
    
    async Task<bool> GarenGameNotStarted()
    {
        return
        (
            !await TestGameStarted() &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Game Not Started +++")
        );
    }
    
    async Task<bool> GarenAttack()
    {
        return
        (
            await GarenAcquireTarget() &&
            await GarenAttackTarget() &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Attack ++++")
        );
    }
    
    async Task<bool> GarenAcquireTarget()
    {
        return
        (
            (
                (Global.LostAggro = false, true) &&
                await TestUnitAIAttackTargetValid() &&
                (Global.AggroTarget = await GetUnitAIAttackTarget(), true) &&
                (Global.AggroPosition = Global.AssistPosition, true) &&
                await TestUnitIsVisible(Unit: Global.Self, TargetUnit: Global.AggroTarget) &&
                await GarenDeaggroChecker() &&
                Global.LostAggro == false &&
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Use Previous Target +++")
            )
            ||
            (
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "EnableOrDisableAllyAggro") &&
                (Global.CurrentClosestDistance = 800, true) &&
                (Tree.FriendlyUnits = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "800", SpellFlags: "AffectFriends,AffectHeroes,AlwaysSelf"), true) &&
                (Global.ValueChanged = false, true) &&
                (Tree.unit = await IterateOverAllDecorator(Collection: Tree.FriendlyUnits), true) &&
                Global.ValueChanged == true &&
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Acquired Ally under attack +++")
            )
            ||
            (
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "??? EnableDisableAcquire New Target ???") &&
                (Global.CurrentClosestDistance = 800, true) &&
                (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectBuildings,AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets"), true) &&
                (
                    (Tree.Count = await GetCollectionCount(Collection: Global.TargetCollection), true) &&
                    Tree.Count > 0 &&
                    (Global.ValueChanged = false, true) &&
                    (Tree.unit = await IterateOverAllDecorator(Collection: Global.TargetCollection), true)
                ) &&
                Global.ValueChanged == true &&
                await SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                await SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                (Global.AssistPosition = Global.SelfPosition, true) &&
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ AcquiredNewTarget +++")
            )
        );
    }
    
    async Task<bool> GarenAttackTarget()
    {
        return
        (
            (Tree.Target = await GetUnitAIAttackTarget(), true) &&
            (Tree.SelfTeam = await GetUnitTeam(Unit: Global.Self), true) &&
            (Tree.TargetTeam = await GetUnitTeam(Unit: Tree.Target), true) &&
            Tree.SelfTeam != Tree.TargetTeam &&
            (
                (
                    (Tree.UnitType = await GetUnitType(Unit: Tree.Target), true) &&
                    await EqualUnitType(LeftHandSide: Tree.UnitType, RightHandSide: "MINION_UNIT") &&
                    (Tree.currentHealth = await GetUnitCurrentHealth(Unit: Tree.Target), true) &&
                    (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Tree.Target), true) &&
                    (Tree.HP_Ratio = Tree.currentHealth / Tree.MaxHealth, true) &&
                    Tree.HP_Ratio < 0.2 &&
                    (
                        (
                            Global.StrengthRatioOverTime > 2 &&
                            await GarenCanCastAbility2() &&
                            await SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                            await GarenCastAbility2()
                        )
                        ||
                        (
                            await GarenCanCastAbility0() &&
                            await SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                            await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                        )
                    )
                )
                ||
                (
                    (Tree.UnitType = await GetUnitType(Unit: Tree.Target), true) &&
                    await EqualUnitType(LeftHandSide: Tree.UnitType, RightHandSide: "HERO_UNIT") &&
                    (
                        (
                            await GarenCanCastAbility0() &&
                            await SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                            await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                        )
                        ||
                        (
                            await GarenCanCastAbility2() &&
                            await GarenCastAbility2()
                        )
                    )
                )
                ||
                await GarenAutoAttackTarget()
            ) &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++ Attack Success ++")
        );
    }
    
    async Task<bool> GarenReturnToBase()
    {
        return
        (
            (Tree.BaseLocation = await GetUnitAIBasePosition(Unit: Global.Self), true) &&
            (Tree.Distance = await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.BaseLocation), true) &&
            Tree.Distance > 300 &&
            (
                (
                    (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Global.Self), true) &&
                    (Tree.Health = await GetUnitCurrentHealth(Unit: Global.Self), true) &&
                    (Tree.Health_Ratio = Tree.Health / Tree.MaxHealth, true) &&
                    (
                        (
                            Global.TeleportHome == true &&
                            Tree.Health_Ratio <= 0.35
                        )
                        ||
                        (
                            Global.TeleportHome == false &&
                            Tree.Health_Ratio <= 0.25 &&
                            (Global.TeleportHome = true, true)
                        )
                    )
                )
                ||
                (
                    await DebugAction(RunningLimit: "0", Result: "FAILURE", String: "EmptyNode: HighGold")
                )
            ) &&
            (
                (
                    (Global.CurrentClosestDistance = 30000, true) &&
                    (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "30000", SpellFlags: "AffectFriends,AffectTurrets"), true) &&
                    await GarenFindClosestTarget() &&
                    Global.ValueChanged == true &&
                    (
                        (
                            (Tree.Distance = await GetDistanceBetweenUnits(SourceUnit: Global.CurrentClosestTarget, DestinationUnit: Global.Self), true) &&
                            Tree.Distance < 125 &&
                            (
                                (
                                    await TestUnitAISpellPositionValid() &&
                                    (Tree.TeleportPosition = await GetUnitAISpellPosition(), true) &&
                                    (Tree.DistanceToTeleportPosition = await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.TeleportPosition), true) &&
                                    Tree.DistanceToTeleportPosition < 50
                                )
                                ||
                                !await TestUnitAISpellPositionValid()
                            ) &&
                            await IssueTeleportToBaseOrder() &&
                            await ClearUnitAISpellPosition() &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Yo")
                        )
                        ||
                        (
                            (
                                (
                                    !await TestUnitAISpellPositionValid() &&
                                    await ComputeUnitAISpellPosition(TargetUnit: Global.CurrentClosestTarget, ReferenceUnit: Global.Self, Range: "150", UnitSide: "false")
                                )
                                ||
                                await TestUnitAISpellPositionValid()
                            ) &&
                            (Tree.TeleportPosition = await GetUnitAISpellPosition(), true) &&
                            await IssueMoveToPositionOrder(Location: Tree.TeleportPosition) &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Yo")
                        )
                    )
                )
                ||
                (
                    (Tree.BaseLocation = await GetUnitAIBasePosition(Unit: Global.Self), true) &&
                    await IssueMoveToPositionOrder(Location: Tree.BaseLocation)
                )
            ) &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Teleport Home +++")
        );
    }
    
    async Task<bool> GarenHighThreatManagement()
    {
        return
        (
            (
                (
                    (Tree.SuperHighThreat = false, true) &&
                    await TestUnitUnderAttack(Unit: Global.Self) &&
                    (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Global.Self), true) &&
                    (Tree.Health = await GetUnitCurrentHealth(Unit: Global.Self), true) &&
                    (Tree.Health_Ratio = Tree.Health / Tree.MaxHealth, true) &&
                    Tree.Health_Ratio <= 0.25 &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ LowHealthUnderAttack +++") &&
                    (Tree.SuperHighThreat = true, true)
                )
                ||
                (
                    (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Global.Self), true) &&
                    (Tree.Damage_Ratio = Global.AccumulatedDamage / Tree.MaxHealth, true) &&
                    (
                        (
                            Global.AggressiveKillMode == true &&
                            Tree.Damage_Ratio > 0.15
                        )
                        ||
                        (
                            Global.AggressiveKillMode == false &&
                            Tree.Damage_Ratio > 0.02
                        )
                    ) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ BurstDamage +++")
                )
            ) &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ High Threat +++") &&
            await ClearUnitAIAttackTarget() &&
            (
                (
                    Tree.SuperHighThreat == true &&
                    await GarenCanCastAbility1() &&
                    await SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1")
                )
                ||
                (
                    Tree.SuperHighThreat == true &&
                    await GarenCanCastAbility0() &&
                    await SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                )
                ||
                await GarenMicroRetreat()
            ) &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ High Threat +++")
        );
    }
    
    async Task<bool> GarenLowThreatManagement()
    {
        return
        (
            (
                (
                    Global.StrengthRatioOverTime > 6 &&
                    await ClearUnitAIAttackTarget() &&
                    (Global.LowThreatMode = true, true)
                )
                ||
                (
                    Global.LowThreatMode == true &&
                    (Global.LowThreatMode = false, true) &&
                    Global.StrengthRatioOverTime > 4 &&
                    await ClearUnitAIAttackTarget() &&
                    (Global.LowThreatMode = true, true)
                )
                ||
                (
                    await ClearUnitAISafePosition() &&
                    await DebugAction(RunningLimit: "0", Result: "FAILURE", String: "DoNotRemoveForcedFail")
                )
            ) &&
            await GarenMicroRetreat() &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Low Threat +++")
        );
    }
    
    async Task<bool> GarenKillChampion()
    {
        return
        (
            (Global.AggressiveKillMode = false, true) &&
            (
                (
                    Global.StrengthRatioOverTime < 3 &&
                    (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectEnemies,AffectHeroes"), true) &&
                    (Tree.CurrentLowestHealthRatio = 0.8, true) &&
                    (Global.ValueChanged = false, true) &&
                    (Tree.unit = await IterateOverAllDecorator(Collection: Global.TargetCollection), true) &&
                    Global.ValueChanged == true &&
                    await SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                    await SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                    (Global.AssistPosition = Global.SelfPosition, true) &&
                    (Tree.Aggressive = false, true) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "PassiveKillChampion")
                )
                ||
                (
                    Global.StrengthRatioOverTime < 5.1 &&
                    (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Global.Self), true) &&
                    (Tree.CurrentHealth = await GetUnitCurrentHealth(Unit: Global.Self), true) &&
                    (Tree.MyHealthRatio = Tree.CurrentHealth / Tree.MaxHealth, true) &&
                    Tree.MyHealthRatio > 0.5 &&
                    (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "1000", SpellFlags: "AffectEnemies,AffectHeroes"), true) &&
                    (Tree.CurrentLowestHealthRatio = 0.4, true) &&
                    (Global.ValueChanged = false, true) &&
                    (Tree.unit = await IterateOverAllDecorator(Collection: Global.TargetCollection), true) &&
                    Global.ValueChanged == true &&
                    await SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                    await SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                    (Global.AssistPosition = Global.SelfPosition, true) &&
                    (Tree.Aggressive = true, true) &&
                    (Global.AggressiveKillMode = true, true) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ AggressiveMode +++")
                )
            ) &&
            (
                (
                    await GarenCanCastAbility0() &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                )
                ||
                (
                    Tree.Aggressive == true &&
                    await GarenCanCastAbility3() &&
                    await GarenCastAbility3() &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Use Ultiamte +++")
                )
                ||
                (
                    !await TestUnitHasBuff(TargetUnit: Global.Self, CasterUnit: Tree., BuffName: "GarenBladestorm") &&
                    await GarenCanCastAbility2() &&
                    await GarenCastAbility2()
                )
                ||
                await GarenAutoAttackTarget()
                ||
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Attack Champion+++")
            ) &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Success: Kill  +++")
        );
    }
    
    async Task<bool> GarenLastHitMinion()
    {
        return
        (
            (
                (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "800", SpellFlags: "AffectEnemies,AffectMinions"), true) &&
                (Tree.CurrentLowestHealthRatio = 0.3, true) &&
                (Global.ValueChanged = false, true) &&
                (Tree.unit = await IterateOverAllDecorator(Collection: Global.TargetCollection), true) &&
                Global.ValueChanged == true &&
                await SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                await SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                (Global.AssistPosition = Global.SelfPosition, true) &&
                (Tree.Target = Global.CurrentClosestTarget, true)
            ) &&
            await GarenAutoAttackTarget() &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++++++ Last Hit ++++++++")
        );
    }
    
    async Task<bool> GarenMicroRetreat()
    {
        return
        (
            (
                await TestUnitAISafePositionValid() &&
                (Tree.SafePosition = await GetUnitAISafePosition(), true) &&
                (
                    (
                        (Tree.Distance = await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.SafePosition), true) &&
                        Tree.Distance < 50 &&
                        await ComputeUnitAISafePosition(Range: "800", UseDefender: "false", UseEnemy: "false") &&
                        await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "------- At location computed new position --------------")
                    )
                    ||
                    (
                        await IssueMoveToPositionOrder(Location: Tree.SafePosition) &&
                        await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "------------ Success: Move to safe position ----------")
                    )
                )
            )
            ||
            await ComputeUnitAISafePosition(Range: "600", UseDefender: "false", UseEnemy: "false")
        );
    }
    
    async Task<bool> GarenAutoAttackTarget()
    {
        return
        (
            (Tree.Target = await GetUnitAIAttackTarget(), true) &&
            await TestUnitAIAttackTargetValid() &&
            (
                (
                    (Tree.Distance = await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self), true) &&
                    (Tree.AttackRange = await GetUnitAttackRange(Unit: Global.Self), true) &&
                    (Tree.AttackRange = Tree.AttackRange * 0.9, true) &&
                    Tree.Distance <= Tree.AttackRange &&
                    await ClearUnitAIAttackTarget() &&
                    await SetUnitAIAttackTarget(TargetUnit: Tree.Target) &&
                    await IssueAttackOrder()
                )
                ||
                await IssueMoveToUnitOrder(TargetUnit: Tree.Target)
            )
        );
    }
    
    async Task<bool> GarenCanCastAbility0()
    {
        return
        (
            (Tree.Cooldown = await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0"), true) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
        );
    }
    
    async Task<bool> GarenCanCastAbility1()
    {
        return
        (
            (Tree.Cooldown = await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1"), true) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1")
        );
    }
    
    async Task<bool> GarenCanCastAbility2()
    {
        return
        (
            (Tree.Cooldown = await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2"), true) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2")
        );
    }
    
    async Task<bool> GarenCanCastAbility3()
    {
        return
        (
            (Tree.Cooldown = await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0"), true) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3")
        );
    }
    
    async Task<bool> GarenCastAbility0()
    {
        return
        (
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            (Tree.Range = await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0"), true) &&
            (Tree.Target = await GetUnitAIAttackTarget(), true) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    (Tree.Distance = await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self), true) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                    Tree.Distance <= Tree.Range &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0") &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 1 Success ----------------")
                )
                ||
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                    await IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
                )
            )
        );
    }
    
    async Task<bool> GarenCastAbility1()
    {
        return
        (
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            (Tree.Range = await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1"), true) &&
            (Tree.Target = await GetUnitAIAttackTarget(), true) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    (Tree.Distance = await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self), true) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                    Tree.Distance <= Tree.Range &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1") &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 1 Success ----------------")
                )
                ||
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                    await IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
                )
            )
        );
    }
    
    async Task<bool> GarenCastAbility2()
    {
        return
        (
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            (Tree.Target = await GetUnitAIAttackTarget(), true) &&
            (
                (
                    await TestUnitHasBuff(TargetUnit: Global.Self, CasterUnit: Tree., BuffName: "GarenBladestorm") &&
                    await IssueMoveToUnitOrder(TargetUnit: Tree.Target)
                )
                ||
                (
                    (Tree.Range = await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2"), true) &&
                    (Tree.Range = 200, true) &&
                    (
                        (
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 2") &&
                            (Tree.Distance = await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self), true) &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                            Tree.Distance <= Tree.Range &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                            await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2") &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 2 Success ----------------")
                        )
                        ||
                        (
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                            await IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
                        )
                    )
                )
            )
        );
    }
    
    async Task<bool> GarenCastAbility3()
    {
        return
        (
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            (Tree.Range = await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3"), true) &&
            (Tree.Target = await GetUnitAIAttackTarget(), true) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    (Tree.Distance = await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self), true) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                    Tree.Distance <= Tree.Range &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3") &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 1 Success ----------------")
                )
                ||
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                    await IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
                )
            )
        );
    }
    
    async Task<bool> GarenPushLane()
    {
        return
        (
            await ClearUnitAIAttackTarget() &&
            await IssueMoveOrder() &&
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Move To Lane +++")
        );
    }
    
    async Task<bool> GarenMisc()
    {
        return
        (
            (
                await DebugAction(RunningLimit: "0", Result: "FAILURE", String: "??? EnableOrDisablePreviousTarget ???") &&
                await TestUnitAIAttackTargetValid() &&
                (Global.LostAggro = false, true) &&
                (Global.PreviousTarget = await GetUnitAIAttackTarget(), true) &&
                (Tree.SelfTeam = await GetUnitTeam(Unit: Global.Self), true) &&
                (Tree.UnitTeam = await GetUnitTeam(Unit: Global.PreviousTarget), true) &&
                Tree.UnitTeam != Tree.SelfTeam &&
                (Tree.Assist = await GetUnitAIAssistTarget(), true) &&
                (
                    (
                        Tree.Assist == Global.Self &&
                        (Tree.Distance = await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Global.AssistPosition), true) &&
                        ((
                            Tree.Distance >= Global.DeaggroDistance &&
                            await ClearUnitAIAttackTarget() &&
                            (Global.LostAggro = true, true) &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Lost Aggro +++")
                        ), true) &&
                        Tree.Distance < Global.DeaggroDistance &&
                        await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ In Aggro Range, Use Previous")
                    )
                    ||
                    (
                        Global.Self != Tree.Assist &&
                        (Tree.AssistPosition = await GetUnitPosition(Unit: Tree.Assist), true) &&
                        (Tree.Distance = await DistanceBetweenObjectAndPoint(Unit: Global.PreviousTarget, Point: Global.SelfPosition), true) &&
                        ((
                            Tree.Distance >= 1000 &&
                            await ClearUnitAIAttackTarget() &&
                            (Global.LostAggro = true, true) &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "------- Losing aggro from assist ----------")
                        ), true) &&
                        Tree.Distance < 1000 &&
                        await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "============= Use Previous Target: Still close to assist -----------")
                    )
                ) &&
                (Global.LostAggro = false, true) &&
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++ Use Previous Target ++")
            ) &&
            (
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "??? EnableDisableAcquire New Target ???") &&
                (Global.CurrentClosestDistance = 800, true) &&
                (Global.TargetCollection = await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets"), true) &&
                (
                    (Tree.Count = await GetCollectionCount(Collection: Global.TargetCollection), true) &&
                    Tree.Count > 0 &&
                    (Global.ValueChanged = false, true) &&
                    (Tree.Attacker = await IterateOverAllDecorator(Collection: Global.TargetCollection), true)
                ) &&
                Global.ValueChanged == true &&
                await SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                await SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                (Global.AssistPosition = Global.SelfPosition, true) &&
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ AcquiredNewTarget +++")
            )
        );
    }
    
    async Task<bool> GarenHeal()
    {
        return
        (
            (Tree.Health = await GetUnitCurrentHealth(Unit: Global.Self), true) &&
            (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Global.Self), true) &&
            (Tree.HP_Ratio = Tree.Health / Tree.MaxHealth, true) &&
            (
                Tree.HP_Ratio < 0.5 &&
                await TestUnitAICanUseItem(ItemID: "2003") &&
                await IssueUseItemOrder(ItemID: "2003")
            )
        );
    }
    
    async Task<bool> ReduceDamageTaken()
    {
        return
        (
            (Tree.MaxHealth = await GetUnitMaxHealth(Unit: Global.Self), true) &&
            (Tree.Damage_Ratio = Global.AccumulatedDamage / Tree.MaxHealth, true) &&
            Tree.Damage_Ratio >= 0.1 &&
            (
                (
                    await GarenCanCastAbility1() &&
                    await SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1")
                )
                ||
                (
                    await GarenCanCastAbility0() &&
                    await SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    await CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                )
            )
        );
    }
    
    async Task<bool> GarenFindClosestVisibleTarget()
    {
        return
        (
            (Global.ValueChanged = false, true) &&
            (Tree.Attacker = await IterateOverAllDecorator(Collection: Global.TargetCollection), true)
        );
    }
}
