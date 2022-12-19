async Task<bool> GarenInit()
{
    return
    (await GetUnitAISelf(Output: out Global.Self)) &&
    (await GetUnitPosition(Unit: Global.Self, Output: out Global.SelfPosition)) &&
    (await SetVarFloat(Input: 1200, Output: out Global.DeaggroDistance)) &&
    (
        (
            (await TestUnitAIFirstTime(ReturnSuccessIf: true)) &&
            (await SetVarFloat(Input: 0, Output: out Global.AccumulatedDamage)) &&
            (await GetUnitCurrentHealth(Unit: Global.Self, Output: out Global.PrevHealth)) &&
            (await GetGameTime(Output: out Global.PrevTime)) &&
            (await SetVarBool(Input: false, Output: out Global.LostAggro)) &&
            (await SetVarFloat(Input: 1, Output: out Global.StrengthRatioOverTime)) &&
            (await SetVarBool(Input: false, Output: out Global.AggressiveKillMode)) &&
            (await SetVarBool(Input: false, Output: out Global.LowThreatMode)) &&
            (await SetVarInt(Input: 4, Output: out Global.PotionsToBuy)) &&
            (await SetVarBool(Input: false, Output: out Global.TeleportHome))
        )
        ||
        (
            ((true,
                    (await GetGameTime(Output: out Tree.CurrentTime)) &&
                    (await SubtractFloat(LeftHandSide: Tree.CurrentTime, RightHandSide: Global.PrevTime, Output: out Tree.TimeDiff)) &&
                    (
                        (await GreaterFloat(LeftHandSide: Tree.TimeDiff, RightHandSide: 1))
                        ||
                        (await LessFloat(LeftHandSide: Tree.TimeDiff, RightHandSide: 0))
                    ) &&
                    (
                        (await MultiplyFloat(LeftHandSide: Global.AccumulatedDamage, RightHandSide: 0.8, Output: out Global.AccumulatedDamage)) &&
                        (await MultiplyFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: 0.8, Output: out Global.StrengthRatioOverTime))
                    ) &&
                    (
                        (await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: 1000, SpellFlags: AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets, Output: out Global.TargetCollection)) &&
                        (await GarenStrengthEvaluator()) &&
                        (await SetVarFloat(Input: Global.TotalUnitStrength, Output: out Tree.EnemyStrength)) &&
                        (await GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: 900, SpellFlags: AffectFriends|AffectHeroes|AffectMinions|AffectTurrets, Output: out Global.TargetCollection)) &&
                        (await GarenStrengthEvaluator()) &&
                        (await SetVarFloat(Input: Global.TotalUnitStrength, Output: out Tree.FriendStrength)) &&
                        (await DivideFloat(LeftHandSide: Tree.EnemyStrength, RightHandSide: Tree.FriendStrength, Output: out Tree.StrRatio)) &&
                        (await AddFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: Tree.StrRatio, Output: out Global.StrengthRatioOverTime)) &&
                        (await GetUnitAIAttackers(Unit: Global.Self, Output: out Global.TargetCollection)) &&
                        ((true,
                            await IterateUntilSuccessDecorator(Collection: Global.TargetCollection, Output: out Tree.Unit)
                        ))
                    ) &&
                    (await GetGameTime(Output: out Global.PrevTime))
            )) &&
            ((true,
                (await GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.CurrentHealth)) &&
                (await SubtractFloat(LeftHandSide: Global.PrevHealth, RightHandSide: Tree.CurrentHealth, Output: out Tree.NewDamage)) &&
                (await GreaterFloat(LeftHandSide: Tree.NewDamage, RightHandSide: 0)) &&
                (await AddFloat(LeftHandSide: Global.AccumulatedDamage, RightHandSide: Tree.NewDamage, Output: out Global.AccumulatedDamage))
            )) &&
            (await GetUnitCurrentHealth(Unit: Global.Self, Output: out Global.PrevHealth))
        )
    );
}