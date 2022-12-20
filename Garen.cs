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
            await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Unit)
        );
    }
    
    async Task<bool> GarenFindClosestTarget()
    {
        return
        (
            (Global.ValueChanged = false, true) &&
            await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Attacker)
        );
    }
    
    async Task<bool> GarenDeaggroChecker()
    {
        return
        ((
            (Global.LostAggro = false, true) &&
            await DistanceBetweenObjectAndPoint(Unit: Global.AggroTarget, Point: Global.AggroPosition, Output: out Tree.Distance) &&
            Tree.Distance > 800 &&
            Tree.Distance < 1200 &&
            (Global.LostAggro = true, true)
        ), true);
    }
    
    async Task<bool> GarenInit()
    {
        return
        (
            await GetUnitAISelf(Output: out Global.Self) &&
            await GetUnitPosition(Unit: Global.Self, Output: out Global.SelfPosition) &&
            (Global.DeaggroDistance = 1200, true) &&
            (
                (
                    await TestUnitAIFirstTime() &&
                    (Global.AccumulatedDamage = 0, true) &&
                    await GetUnitCurrentHealth(Unit: Global.Self, Output: out Global.PrevHealth) &&
                    await GetGameTime(Output: out Global.PrevTime) &&
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
                        await GetGameTime(Output: out Tree.CurrentTime) &&
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
                            await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "1000", SpellFlags: "AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                            await GarenStrengthEvaluator() &&
                            (Tree.EnemyStrength = Global.TotalUnitStrength, true) &&
                            await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectFriends,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                            await GarenStrengthEvaluator() &&
                            (Tree.FriendStrength = Global.TotalUnitStrength, true) &&
                            (Tree.StrRatio = Tree.EnemyStrength / Tree.FriendStrength, true) &&
                            (Global.StrengthRatioOverTime = Global.StrengthRatioOverTime + Tree.StrRatio, true) &&
                            await GetUnitAIAttackers(Unit: Global.Self, Output: out Global.TargetCollection) &&
                            (await IterateUntilSuccessDecorator(Collection: Global.TargetCollection, Output: out Tree.Unit), true)
                        ) &&
                        await GetGameTime(Output: out Global.PrevTime)
                    ), true) &&
                    ((
                        await GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.CurrentHealth) &&
                        (Tree.NewDamage = Global.PrevHealth - Tree.CurrentHealth, true) &&
                        Tree.NewDamage > 0 &&
                        (Global.AccumulatedDamage = Global.AccumulatedDamage + Tree.NewDamage, true)
                    ), true) &&
                    await GetUnitCurrentHealth(Unit: Global.Self, Output: out Global.PrevHealth)
                )
            )
        );
    }
    
    async Task<bool> GarenAtBaseHealAndBuy()
    {
        return
        (
            await GetUnitAIBasePosition(Unit: Global.Self, Output: out Tree.BaseLocation) &&
            await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.BaseLocation, Output: out Tree.Distance) &&
            Tree.Distance <= 450 &&
            (Global.TeleportHome = false, true) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Start ----- Heal -----") &&
                    await GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    await GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.CurrentHealth) &&
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
                        await GetUnitGold(Unit: Global.Self, Output: out Tree.temp) &&
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
            await GetUnitSkillPoints(Unit: Global.Self, Output: out Tree.SkillPoints) &&
            Tree.SkillPoints > 0 &&
            await GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "0", Output: out Tree.Ability0Level) &&
            await GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "1", Output: out Tree.Ability1Level) &&
            await GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "2", Output: out Tree.Ability2Level) &&
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
                await GetUnitAIAttackTarget(Output: out Global.AggroTarget) &&
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
                await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "800", SpellFlags: "AffectFriends,AffectHeroes,AlwaysSelf", Output: out Tree.FriendlyUnits) &&
                (Global.ValueChanged = false, true) &&
                await IterateOverAllDecorator(Collection: Tree.FriendlyUnits, Output: out Tree.unit) &&
                Global.ValueChanged == true &&
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Acquired Ally under attack +++")
            )
            ||
            (
                await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "??? EnableDisableAcquire New Target ???") &&
                (Global.CurrentClosestDistance = 800, true) &&
                await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectBuildings,AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                (
                    await GetCollectionCount(Collection: Global.TargetCollection, Output: out Tree.Count) &&
                    Tree.Count > 0 &&
                    (Global.ValueChanged = false, true) &&
                    await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit)
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
            await GetUnitAIAttackTarget(Output: out Tree.Target) &&
            await GetUnitTeam(Unit: Global.Self, Output: out Tree.SelfTeam) &&
            await GetUnitTeam(Unit: Tree.Target, Output: out Tree.TargetTeam) &&
            Tree.SelfTeam != Tree.TargetTeam &&
            (
                (
                    await GetUnitType(Unit: Tree.Target, Output: out Tree.UnitType) &&
                    await EqualUnitType(LeftHandSide: Tree.UnitType, RightHandSide: "MINION_UNIT") &&
                    await GetUnitCurrentHealth(Unit: Tree.Target, Output: out Tree.currentHealth) &&
                    await GetUnitMaxHealth(Unit: Tree.Target, Output: out Tree.MaxHealth) &&
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
                    await GetUnitType(Unit: Tree.Target, Output: out Tree.UnitType) &&
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
            await GetUnitAIBasePosition(Unit: Global.Self, Output: out Tree.BaseLocation) &&
            await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.BaseLocation, Output: out Tree.Distance) &&
            Tree.Distance > 300 &&
            (
                (
                    await GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    await GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.Health) &&
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
                    await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "30000", SpellFlags: "AffectFriends,AffectTurrets", Output: out Global.TargetCollection) &&
                    await GarenFindClosestTarget() &&
                    Global.ValueChanged == true &&
                    (
                        (
                            await GetDistanceBetweenUnits(SourceUnit: Global.CurrentClosestTarget, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                            Tree.Distance < 125 &&
                            (
                                (
                                    await TestUnitAISpellPositionValid() &&
                                    await GetUnitAISpellPosition(Output: out Tree.TeleportPosition) &&
                                    await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.TeleportPosition, Output: out Tree.DistanceToTeleportPosition) &&
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
                            await GetUnitAISpellPosition(Output: out Tree.TeleportPosition) &&
                            await IssueMoveToPositionOrder(Location: Tree.TeleportPosition) &&
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Yo")
                        )
                    )
                )
                ||
                (
                    await GetUnitAIBasePosition(Unit: Global.Self, Output: out Tree.BaseLocation) &&
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
                    await GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    await GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.Health) &&
                    (Tree.Health_Ratio = Tree.Health / Tree.MaxHealth, true) &&
                    Tree.Health_Ratio <= 0.25 &&
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ LowHealthUnderAttack +++") &&
                    (Tree.SuperHighThreat = true, true)
                )
                ||
                (
                    await GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
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
                    await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectEnemies,AffectHeroes", Output: out Global.TargetCollection) &&
                    (Tree.CurrentLowestHealthRatio = 0.8, true) &&
                    (Global.ValueChanged = false, true) &&
                    await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit) &&
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
                    await GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    await GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.CurrentHealth) &&
                    (Tree.MyHealthRatio = Tree.CurrentHealth / Tree.MaxHealth, true) &&
                    Tree.MyHealthRatio > 0.5 &&
                    await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "1000", SpellFlags: "AffectEnemies,AffectHeroes", Output: out Global.TargetCollection) &&
                    (Tree.CurrentLowestHealthRatio = 0.4, true) &&
                    (Global.ValueChanged = false, true) &&
                    await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit) &&
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
                await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "800", SpellFlags: "AffectEnemies,AffectMinions", Output: out Global.TargetCollection) &&
                (Tree.CurrentLowestHealthRatio = 0.3, true) &&
                (Global.ValueChanged = false, true) &&
                await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit) &&
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
                await GetUnitAISafePosition(Output: out Tree.SafePosition) &&
                (
                    (
                        await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.SafePosition, Output: out Tree.Distance) &&
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
            await GetUnitAIAttackTarget(Output: out Tree.Target) &&
            await TestUnitAIAttackTargetValid() &&
            (
                (
                    await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                    await GetUnitAttackRange(Unit: Global.Self, Output: out Tree.AttackRange) &&
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
            await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0", Output: out Tree.Cooldown) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
        );
    }
    
    async Task<bool> GarenCanCastAbility1()
    {
        return
        (
            await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1", Output: out Tree.Cooldown) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1")
        );
    }
    
    async Task<bool> GarenCanCastAbility2()
    {
        return
        (
            await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2", Output: out Tree.Cooldown) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2")
        );
    }
    
    async Task<bool> GarenCanCastAbility3()
    {
        return
        (
            await GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0", Output: out Tree.Cooldown) &&
            Tree.Cooldown <= 0 &&
            await TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3")
        );
    }
    
    async Task<bool> GarenCastAbility0()
    {
        return
        (
            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0", Output: out Tree.Range) &&
            await GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
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
            await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1", Output: out Tree.Range) &&
            await GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
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
            await GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    await TestUnitHasBuff(TargetUnit: Global.Self, CasterUnit: Tree., BuffName: "GarenBladestorm") &&
                    await IssueMoveToUnitOrder(TargetUnit: Tree.Target)
                )
                ||
                (
                    await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2", Output: out Tree.Range) &&
                    (Tree.Range = 200, true) &&
                    (
                        (
                            await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 2") &&
                            await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
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
            await GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3", Output: out Tree.Range) &&
            await GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    await DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    await GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
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
                await GetUnitAIAttackTarget(Output: out Global.PreviousTarget) &&
                await GetUnitTeam(Unit: Global.Self, Output: out Tree.SelfTeam) &&
                await GetUnitTeam(Unit: Global.PreviousTarget, Output: out Tree.UnitTeam) &&
                Tree.UnitTeam != Tree.SelfTeam &&
                await GetUnitAIAssistTarget(Output: out Tree.Assist) &&
                (
                    (
                        Tree.Assist == Global.Self &&
                        await DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Global.AssistPosition, Output: out Tree.Distance) &&
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
                        await GetUnitPosition(Unit: Tree.Assist, Output: out Tree.AssistPosition) &&
                        await DistanceBetweenObjectAndPoint(Unit: Global.PreviousTarget, Point: Global.SelfPosition, Output: out Tree.Distance) &&
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
                await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                (
                    await GetCollectionCount(Collection: Global.TargetCollection, Output: out Tree.Count) &&
                    Tree.Count > 0 &&
                    (Global.ValueChanged = false, true) &&
                    await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Attacker)
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
            await GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.Health) &&
            await GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
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
            await GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
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
            await IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Attacker)
        );
    }
}
