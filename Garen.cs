using static SBL;
public static class SBL
{
    /// <summary>
    /// Sequence blocks will tick their children in order until one returns a FAILURE, at which point the node will return FAILURE.  If a child return RUNNING then the node will return RUNNING and execute that child first next tick.  If all children return SUCCESS the node will return SUCCESS.
    /// </summary>
    /// <remarks>
    /// Generally alternate these with selector nodes
    /// </remarks>
    public extern static Task<bool> Sequence();
    /// <summary>
    /// Selector blocks will tick their children in order until one returns a SUCCESS, at which point the node will return SUCCESS.  If a child return RUNNING then the node will return RUNNING and execute that child first next tick.  If all children return FAILURE the node will return FAILURE.
    /// </summary>
    /// <remarks>
    /// Generally alternate these with sequence nodes
    /// </remarks>
    public extern static Task<bool> Selector();
    /// <summary>
    /// Creates a dynamic BTInstance based on the name of the provided tree.
    /// </summary>
    /// <remarks>
    /// For now this should generally be used for one-off tress that delete themselves since references outside of this function are difficult
    /// </remarks>
    /// <param name="TreeName">Name of the tree to instantiate</param>
    /// <param name="Type">The type of the instance.  For now I strongly recommend only using DELETE_SELF.</param>
    public extern static Task<bool> CreateDynamicBTInstance(string TreeName = "", BTInstanceType Type = DELETE_SELF);
    /// <summary>
    /// Debug node used to return an explicit value and write a string to log.  In the case that RUNNING is selected the node will return RUNNING a number of times equal to runningLimit.
    /// </summary>
    /// <remarks>
    /// Running limit is unused in non RUNNING types, and after the running limit has been reached the block will return SUCCESS
    /// </remarks>
    /// <param name="RunningLimit">How many times should we return RUNNING before returning SUCCESS?</param>
    /// <param name="Result">What the node should return when ticked</param>
    /// <param name="String">The string that should be outputted to log</param>
    public extern static Task<bool> DebugAction(int RunningLimit = 0, BehaveResult Result = SUCCESS, string String = "Yo");
    /// <summary>
    /// Decorator that masks FAILURE.
    /// </summary>
    /// <remarks>
    /// Running still returns running
    /// </remarks>
    public extern static Task<bool> MaskFailure();
    /// <summary>
    /// Decorator that allows user to specify the number of times the subtree will run.
    /// </summary>
    /// <remarks>
    /// Need comment
    /// </remarks>
    /// <param name="RunningLimit">The number of times the sub tree is to be executed</param>
    public extern static Task<bool> LoopNTimes(int RunningLimit = 1);
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will always return SUCCESS.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    public extern static Task<bool> IterateOverAllDecorator(AttackableUnitCollection Collection = );
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns FAILURE.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will return SUCCESS if all children return SUCCESS and FAILURE if one child returns FAILURE.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    public extern static Task<bool> IterateUntilFailureDecorator(AttackableUnitCollection Collection = );
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns SUCCESS.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will return SUCCESS if a child returns SUCCESS and FAILURE if all children return FAILURE.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    public extern static Task<bool> IterateUntilSuccessDecorator(AttackableUnitCollection Collection = );
    /// <summary>
    /// Return RUNNING for X seconds after first tick.
    /// </summary>
    /// <remarks>
    /// This is a blocking delay and it uses the real timer not the game timer, so it is unaffected by pause.
    /// </remarks>
    /// <param name="DelayAmount">The amount of time to delay after first tick.</param>
    public extern static Task<bool> DelayNSecondsBlocking(float DelayAmount = 0);
    /// <summary>
    /// Enable/Disable a quest by name
    /// </summary>
    /// <remarks>
    /// This will fail if the quest does not exist
    /// </remarks>
    /// <param name="Enabled">Should the quest be enabled or disabled?</param>
    /// <param name="Name">The name of the quest to adjust</param>
    public extern static Task<bool> SetBTInstanceStatus(bool Enabled = true, string Name = "");
    /// <summary>
    /// Set all map barracks active/inactive
    /// </summary>
    /// <remarks>
    /// This functionally is the same as the kill minions cheat
    /// </remarks>
    /// <param name="Enable">The status of the barracks</param>
    public extern static Task<bool> SetBarrackStatus(bool Enable = true);
    /// <summary>
    /// Display objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    public extern static Task<bool> ShowObjectiveText(string String = "EMPTY STRING");
    /// <summary>
    /// Hide objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no objective text is displayed
    /// </remarks>
    public extern static Task<bool> HideObjectiveText();
    /// <summary>
    /// Display auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    public extern static Task<bool> ShowAuxiliaryText(string String = "EMPTY STRING");
    /// <summary>
    /// Hide auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no auxiliary text is displayed
    /// </remarks>
    public extern static Task<bool> HideAuxiliaryText();
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static Task<bool> SetVarBool(bool Input = true);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static Task<bool> SetVarAttackableUnit(AttackableUnit Input = True);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static Task<bool> SetVarInt(int Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static Task<bool> SetVarDWORD(DWORD Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static Task<bool> SetVarString(string Input = "DEFAULT STRING");
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static Task<bool> SetVarFloat(float Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for Vector References.  If you want to make a vector out of 3 floats, use MakeVector.
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static Task<bool> SetVarVector(Vector3 Input = 0;0;0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit team
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualUnitTeam(TeamEnum LeftHandSide = TEAM_UNKNOWN, TeamEnum RightHandSide = TEAM_UNKNOWN);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit team
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualUnitTeam(TeamEnum LeftHandSide = TEAM_UNKNOWN, TeamEnum RightHandSide = TEAM_UNKNOWN);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualBool(bool LeftHandSide = true, bool RightHandSide = true);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualBool(bool LeftHandSide = true, bool RightHandSide = true);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualString(string LeftHandSide = "True", string RightHandSide = "True");
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualString(string LeftHandSide = "True", string RightHandSide = "True");
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than the RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> LessInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> LessEqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> GreaterInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> GreaterEqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for flooat References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than the RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> LessFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> LessEqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> GreaterFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualUnit(AttackableUnit LeftHandSide = 0, AttackableUnit RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualUnit(AttackableUnit LeftHandSide = 0, AttackableUnit RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> GreaterEqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> AddInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> SubtractInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> MultiplyInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> DivideInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> ModulusInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> MinInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> MaxInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> AddFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> SubtractFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> MultiplyFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> DivideFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> MinFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static Task<bool> MaxFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Gets a handle to the player and puts it in OutputRef
    /// </summary>
    /// <remarks>
    /// Only works in Tutorial, or other situation where there's only one player.  Works by getting the first player in the roster that has a legal client ID.
    /// </remarks>
    public extern static Task<bool> GetTutorialPlayer();
    /// <summary>
    /// Returns a handle to a collection containing all champions in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains champions who have disconnected or are played by bots.
    /// </remarks>
    public extern static Task<bool> GetChampionCollection();
    /// <summary>
    /// Returns a handle to a collection containing all turrets alive in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains turrets on both teams.
    /// </remarks>
    public extern static Task<bool> GetTurretCollection();
    /// <summary>
    /// Gets a handle to the turret in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the turret is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the turrets to be checked.</param>
    /// <param name="Lane">Lane of the turret.  Check the level script for the enum.</param>
    /// <param name="Position">Position of the turret.  Check the level script for the enum.</param>
    public extern static Task<bool> GetTurret(TeamEnum Team = TEAM_ORDER, int Lane = 1, int Position = 1);
    /// <summary>
    /// Gets a handle to the inhibitor in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the inhibitor is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the inhibitor to be checked.</param>
    /// <param name="Lane">Lane of the inhibitor.  Check the level script for the enum.</param>
    public extern static Task<bool> GetInhibitor(TeamEnum Team = TEAM_ORDER, int Lane = 1);
    /// <summary>
    /// Gets a handle to the nexus on a specific teamin a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the Nexus is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the nexus to return.</param>
    public extern static Task<bool> GetNexus(TeamEnum Team = TEAM_ORDER);
    /// <summary>
    /// Returns the current position of a specific unit
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitPosition(AttackableUnit Unit = );
    /// <summary>
    /// Returns the current elapsed game time.  This will be affected by pausing, cheats, or other things.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    public extern static Task<bool> GetGameTime();
    /// <summary>
    /// Returns the lane position of a turret.
    /// </summary>
    /// <remarks>
    /// This position is defined in the level script and is map specific.
    /// </remarks>
    /// <param name="Turret">Turret to poll.</param>
    public extern static Task<bool> GetTurretPosition(AttackableUnit Turret = );
    /// <summary>
    /// Returns the max health of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitMaxHealth(AttackableUnit Unit = );
    /// <summary>
    /// Returns the current health of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitCurrentHealth(AttackableUnit Unit = );
    /// <summary>
    /// Returns the current Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    public extern static Task<bool> GetUnitCurrentPAR(AttackableUnit Unit = , PrimaryAbilityResourceType PrimaryAbilityResourceType = PAR_MANA);
    /// <summary>
    /// Returns the maximum Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX PAR
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    public extern static Task<bool> GetUnitMaxPAR(AttackableUnit Unit = , PrimaryAbilityResourceType PrimaryAbilityResourceType = PAR_MANA);
    /// <summary>
    /// Returns the current armor of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT armor
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitArmor(AttackableUnit Unit = );
    /// <summary>
    /// Returns the number of discrete elements contained within the collection.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Collection">Collection to count.</param>
    public extern static Task<bool> GetCollectionCount(AttackableUnitCollection Collection = );
    /// <summary>
    /// Returns the current skin of a specific unit
    /// </summary>
    /// <remarks>
    /// Since buildings don't hame skins, it will return the name of the building.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitSkinName(AttackableUnit Unit = );
    /// <summary>
    /// Turns on or off the highlight of a unit.
    /// </summary>
    /// <remarks>
    /// Creates a unit highlight akin to what is used in the tutorial.  This highlight is by default blue.  Always returns SUCCESS.
    /// </remarks>
    /// <param name="Enable">Should the Highlight be turned on or turned off?</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    public extern static Task<bool> ToggleUnitHighlight(bool Enable = , AttackableUnit TargetUnit = );
    /// <summary>
    /// Pings a unit on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.  Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.  Important for team coloration and chat info.</param>
    /// <param name="TargetUnit">Unit to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    public extern static Task<bool> PingMinimapUnit(AttackableUnit PingingUnit = , AttackableUnit TargetUnit = , bool PlayAudio = true);
    /// <summary>
    /// Pings a location on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.  Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.  Important for team coloration and chat info.</param>
    /// <param name="TargetPosition">Location to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    public extern static Task<bool> PingMinimapLocation(AttackableUnit PingingUnit = , Vector3 TargetPosition = 0;0;0;, bool PlayAudio = true);
    /// <summary>
    /// Create a new quest and display it in the HUD
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    /// <param name="Player">The player whose quest you want to activate</param>
    /// <param name="QuestType">Quest type; which quest tracker you want the quest to be added to</param>
    /// <param name="HandleRollOver">OPTIONAL. Should we handle the mousing rolling over and rolling out from this quest?</param>
    /// <param name="Tooltip">Optional: The tooltip to display on rollover of the quest.</param>
    public extern static Task<bool> ActivateQuest(string String = "EMPTY STRING", AttackableUnit Player = , QuestType QuestType = PRIMARY_QUEST, bool HandleRollOver = false, string Tooltip = "");
    /// <summary>
    /// Plays a quest completion animation and then removes it from the HUD
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    public extern static Task<bool> CompleteQuest(int QuestId = );
    /// <summary>
    /// Removes quest from the HUD immediately
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node; there is no ceremony involved in quest removal
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    public extern static Task<bool> RemoveQuest(int QuestId = );
    /// <summary>
    /// Test to see if the quest has the mouse rolled over it
    /// </summary>
    /// <remarks>
    /// This quest must have been activated with HandleRollOver=true in ActivateQuest
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the quest has the mouse over it; if False, returns SUCCESS if unit does not have the mouse over it, or if the quest doesnt exist, or if the quest doesnt have HandleRollOver=true</param>
    public extern static Task<bool> TestQuestRolledOver(int QuestId = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if the quest is being clicked right now with the mouse down over it.
    /// </summary>
    /// <remarks>
    /// Tests to see if the quest is being clicked right now, or if the mouse is not clicking it right now.
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the quest has been clicked by the mouse right now; if False, returns SUCCESS if the quest is not being clicked, or if the quest doesnt exist.</param>
    public extern static Task<bool> TestQuestClicked(int QuestId = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Create a new Tip and display it in the TipTracker
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipCategory">The localized string for the Tip Category.</param>
    public extern static Task<bool> ActivateTip(AttackableUnit Player = , string TipName = "EMPTY STRING", string TipCategory = "EMPTY STRING");
    /// <summary>
    /// Removes Tip from the Tip Tracker immediately
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip node; there is no ceremony involved in Tip removal
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip</param>
    public extern static Task<bool> RemoveTip(int TipId = );
    /// <summary>
    /// Enables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to enable</param>
    public extern static Task<bool> EnableTipEvents(AttackableUnit Player = );
    /// <summary>
    /// Disables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to disable</param>
    public extern static Task<bool> DisableTipEvents(AttackableUnit Player = );
    /// <summary>
    /// Tests to see if a Tip in the Tip Tracker or a Tip Dialogue has been clicked by the user
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip and ActivateTipDialogue nodes. Use ReturnSuccessIf to control the output.  This will return as if the Tip has NOT been clicked if the Tip Id is invalid.
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip or ActivateTipDialogue</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the Tip has been clicked; if False, returns SUCCESS if the Tip has NOT been clicked.</param>
    public extern static Task<bool> TestTipClicked(int TipId = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Create a new Tip Dialogue and display it in the HUD
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipBody">The localized string for the Tip Body.</param>
    /// <param name="TipImage">Optional. The path+filename of the image to display in the tap dialog.</param>
    public extern static Task<bool> ActivateTipDialogue(AttackableUnit Player = , string TipName = "EMPTY STRING", string TipBody = "EMPTY STRING", string TipImage = "");
    /// <summary>
    /// Enables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to enable</param>
    public extern static Task<bool> EnableTipDialogueEvents(AttackableUnit Player = );
    /// <summary>
    /// Disables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to disable</param>
    public extern static Task<bool> DisableTipDialogueEvents(AttackableUnit Player = );
    /// <summary>
    /// Creates a vector from three static components
    /// </summary>
    /// <remarks>
    /// If you want to copy a Vector, use SetVarVector.
    /// </remarks>
    /// <param name="X">X component</param>
    /// <param name="Y">Y component</param>
    /// <param name="Z">Z component</param>
    public extern static Task<bool> MakeVector(float X = 0, float Y = 0, float Z = 0);
    /// <summary>
    /// Turn on or off a UI highlight for a specific UI Element
    /// </summary>
    /// <remarks>
    /// Set the enabled flag to control whether this node turns the element on or off
    /// </remarks>
    /// <param name="UIElement">UIElement; which element on the minimap do you want to highlight</param>
    /// <param name="Enabled">If true, turns on the UI Highlight, if false then turns off the UI Highlight</param>
    public extern static Task<bool> ToggleUIHighlight(UIElement UIElement = UI_MINIMAP, bool Enabled = true);
    /// <summary>
    /// Keeps track whether a player has opened his scoreboard.
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Unit">Handle of the attacking unit</param>
    public extern static Task<bool> RegisterScoreboardOpened(AttackableUnit Unit = );
    /// <summary>
    /// Keeps track of the number of minions (not neutrals) not on the attacker's team killed by an attacker
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Unit">Handle of the attacking unit</param>
    public extern static Task<bool> RegisterMinionKillCounter(AttackableUnit Unit = );
    /// <summary>
    /// Returns an int containing the number of kills the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    public extern static Task<bool> GetChampionKills(AttackableUnit Unit = );
    /// <summary>
    /// Returns an int containing the number of deaths the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    public extern static Task<bool> GetChampionDeaths(AttackableUnit Unit = );
    /// <summary>
    /// Returns an int containing the number of assists the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    public extern static Task<bool> GetChampionAssists(AttackableUnit Unit = );
    /// <summary>
    /// Gives target champion a variable amount of gold.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to give gold to.</param>
    /// <param name="GoldAmount">Amount of gold to give the champion.</param>
    public extern static Task<bool> GiveChampionGold(AttackableUnit Unit = , float GoldAmount = );
    /// <summary>
    /// Orders a unit to stop its movement.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  How this block will interract with forced move orders (say from a skill) is currently untested.
    /// </remarks>
    /// <param name="Unit">Handle of the champion to order.</param>
    public extern static Task<bool> StopUnitMovement(AttackableUnit Unit = );
    /// <summary>
    /// Test if a hero has a specific item.
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.  This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    /// <param name="ItemID">Numerical ID of the item to look for.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has the item; if False, returns SUCCESS if unit does not.</param>
    public extern static Task<bool> TestChampionHasItem(AttackableUnit Unit = , int ItemID = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Pause or unpause the game.
    /// </summary>
    /// <remarks>
    /// Be careful using this!  It is not fully protected for use in a production environment!
    /// </remarks>
    /// <param name="Pause">Pause or unpause the game.</param>
    public extern static Task<bool> SetGamePauseState(bool Pause = true);
    /// <summary>
    /// Pan the camera from its current position to a target point.
    /// </summary>
    /// <remarks>
    /// Once the pan starts this node will return RUNNING until the pan completes.  After the pan completes the node will always return SUCCESS. This node locks camera movement while panning, and returns camera movement state to what it was before the pan started.  Be careful if you change camera movement locking state while panning, because it will not stick.
    /// </remarks>
    /// <param name="Unit">The unit whose camera is being manipulated.</param>
    /// <param name="TargetPosition">3D Point containing the target camera position.</param>
    /// <param name="Time">The amount of time the pan should take; this will scale the pan speed. </param>
    public extern static Task<bool> PanCameraFromCurrentPositionToPoint(AttackableUnit Unit = , Vector3 TargetPosition = 0;0;0, float Time = 1);
    /// <summary>
    /// Returns the number of item slots filled for a particular champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    public extern static Task<bool> GetNumberOfInventorySlotsFilled(AttackableUnit Unit = );
    /// <summary>
    /// Returns the level of the target unit.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose level you want to check.</param>
    public extern static Task<bool> GetUnitLevel(AttackableUnit Unit = );
    /// <summary>
    /// Returns the current XP total of the target champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  Returns 0 if unit is not champion.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose XP total you want to get.</param>
    public extern static Task<bool> GetUnitXP(AttackableUnit Unit = );
    /// <summary>
    /// Returns the distance between the Unit and the Point
    /// </summary>
    /// <remarks>
    /// Distance is measured from the edge of the unit's bounding box
    /// </remarks>
    /// <param name="Unit">Handle of the unit</param>
    /// <param name="Point">Point</param>
    public extern static Task<bool> DistanceBetweenObjectAndPoint(AttackableUnit Unit = , Vector3 Point = 0;0;0;);
    /// <summary>
    /// Returns a collection of units in the target area.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  Uses the reference unit for enemy/ally checks; must be present!
    /// </remarks>
    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    public extern static Task<bool> GetUnitsInTargetArea(AttackableUnit Unit = , Vector3 TargetLocation = 0;0;0;, float Radius = 0, SpellFlags SpellFlags = AlwaysSelf);
    /// <summary>
    /// Test to see if unit is alive
    /// </summary>
    /// <remarks>
    /// Unit is not alive if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is alive; if False, returns SUCCESS if unit is dead or does not exist</param>
    public extern static Task<bool> TestUnitCondition(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if unit is invulnerable
    /// </summary>
    /// <remarks>
    /// Unit is not invulnerable if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is invulnerable; if False, returns SUCCESS if unit is not invulnerable or does not exist</param>
    public extern static Task<bool> TestUnitIsInvulnerable(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if unit is in brush
    /// </summary>
    /// <remarks>
    /// Unit is not in brush if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is in brush; if False, returns SUCCESS if unit is not in brush or does not exist</param>
    public extern static Task<bool> TestUnitInBrush(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if unit has a specific buff
    /// </summary>
    /// <remarks>
    /// Unit does not have buff if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="TargetUnit">Unit to be tested</param>
    /// <param name="CasterUnit">OPTIONAL.  Additional filter to check if buff was cast by a specific unit</param>
    /// <param name="BuffName">Name of buff to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has buff; if False, returns SUCCESS if unit does not have buff or does not exist</param>
    public extern static Task<bool> TestUnitHasBuff(AttackableUnit TargetUnit = , AttackableUnit CasterUnit = , string BuffName = "", bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if a one unit has visibility of another unit
    /// </summary>
    /// <remarks>
    /// If either unit does not exist, then they are not visible; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Viewer">Can this unit see the other?</param>
    /// <param name="TargetUnit">Is this unit visible to the viewer unit?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the target is visible to the viewer; if False, returns SUCCESS if unit is not visible to the viewer or does not exist.</param>
    public extern static Task<bool> TestUnitVisibility(AttackableUnit Viewer = , AttackableUnit TargetUnit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Disabled or Enables all user input
    /// </summary>
    /// <remarks>
    /// Disables or Enables all user input, for all users.
    /// </remarks>
    /// <param name="Enabled">If False disables all input for all users. If True, enables it.</param>
    public extern static Task<bool> ToggleUserInput(bool Enabled = true);
    /// <summary>
    /// Disabled or Enables the texture for fog of war for all users.
    /// </summary>
    /// <remarks>
    /// This will not reveal any units in the fog of war; perception bubbles are necessary for that.
    /// </remarks>
    /// <param name="Enabled">If False disables the texture for all users for all users. If True, enables it.</param>
    public extern static Task<bool> ToggleFogOfWarTexture(bool Enabled = true);
    /// <summary>
    /// Plays a localized VO event
    /// </summary>
    /// <remarks>
    /// Event is a 2D one-shot audio event.  A bad event name fails without complaint.  This node always returns SUCCESS.
    /// </remarks>
    /// <param name="EventID">FMOD event ID</param>
    /// <param name="FolderName">Folder the FMOD event is in in the Dialogue folder of the VO sound bank</param>
    /// <param name="FireAndForget">If true, plays sound as fire-and-forget and the node will return SUCCESS immediately.  If false, node will return RUNNING until the client tells the server that the VO is finished.</param>
    public extern static Task<bool> PlayVOAudioEvent(string EventID = "", string FolderName = "", bool FireAndForget = true);
    /// <summary>
    /// Returns the attack range for unit
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitAttackRange(AttackableUnit Unit = );
    /// <summary>
    /// Disables or Enables initial neutral minion spawn.
    /// </summary>
    /// <remarks>
    /// Once neutral minion spawning has begun, this node no longer has any effect.
    /// </remarks>
    /// <param name="Enabled">If True, enables neutral minion spawning; if False, delays neutral minion spawning.</param>
    public extern static Task<bool> SetNeutralSpawnEnabled(bool Enabled = true);
    /// <summary>
    /// Returns the amount of gold the unit has
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitGold(AttackableUnit Unit = );
    /// <summary>
    /// Returns unit unspent skill points
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitSkillPoints(AttackableUnit Unit = );
    /// <summary>
    /// Adds a unit Perception Bubble
    /// </summary>
    /// <remarks>
    /// Returns a BubbleID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="TargetUnit">Unit to attach the Perception Bubble to.</param>
    /// <param name="Radius">Radius of Perception Bubble. If set to 0, the bubble visibility radius matches the visibility radius of the target unit.</param>
    /// <param name="Duration">Duration of Perception Bubble in seconds.  Bubbles can be removed earlier by using the RemovePerceptionBubble node.</param>
    /// <param name="Team">Team ID that has visibility of this bubble.</param>
    /// <param name="RevealStealth">If this is true then the bubble will reveal stealth for anything inside of that bubble.</param>
    /// <param name="SpecificUnitsClientOnly">OPTIONAL. If specified a client specific message will be sent only to this client about this bubble.  Only that client will have that visiblity.</param>
    /// <param name="RevealSpecificUnitOnly">OPTIONAL. If set then only a units that have the RevealSpecificUnit state on are seeable by this bubble.</param>
    public extern static Task<bool> AddUnitPerceptionBubble(AttackableUnit TargetUnit = , float Radius = 0.0, float Duration = 0.0, TeamEnum Team = TEAM_ORDER, bool RevealStealth = false, AttackableUnit SpecificUnitsClientOnly = , AttackableUnit RevealSpecificUnitOnly = );
    /// <summary>
    /// Adds a position Perception Bubble
    /// </summary>
    /// <remarks>
    /// Returns a BubbleID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="Position">Position of the Perception Bubble.</param>
    /// <param name="Radius">Radius of Perception Bubble. If set to 0, the bubble visibility radius matches the visibility radius of the target unit.</param>
    /// <param name="Duration">Duration of Perception Bubble in seconds.  Bubbles can be removed earlier by using the RemovePerceptionBubble node.</param>
    /// <param name="Team">Team ID that has visibility of this bubble.</param>
    /// <param name="RevealStealth">If this is true then the bubble will reveal stealth for anything inside of that bubble.</param>
    /// <param name="SpecificUnitsClientOnly">OPTIONAL. If specified a client specific message will be sent only to this client about this bubble.  Only that client will have that visiblity.</param>
    /// <param name="RevealSpecificUnitOnly">OPTIONAL. If set then only a units that have the RevealSpecificUnit state on are seeable by this bubble.</param>
    public extern static Task<bool> AddPositionPerceptionBubble(Vector3 Position = 0;0;0, float Radius = 0.0, float Duration = 0.0, TeamEnum Team = TEAM_ORDER, bool RevealStealth = false, AttackableUnit SpecificUnitsClientOnly = , AttackableUnit RevealSpecificUnitOnly = );
    /// <summary>
    /// Removes Perception Bubble
    /// </summary>
    /// <remarks>
    /// Used on Bubble IDs returned by the AddUnitPerceptionBubble and AddPositionPerceptionBubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble; returned by AddPerceptionBubble nodes</param>
    public extern static Task<bool> RemovePerceptionBubble(DWORD BubbleID = );
    /// <summary>
    /// Adds a unit particle effect
    /// </summary>
    /// <remarks>
    /// Returns an EffectID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="BindObject">Unit to attach the particle effect to.</param>
    /// <param name="BoneName">OPTIONAL. Name of the bone to attach the particle effect to.</param>
    /// <param name="EffectName">File name of the particle effect file to use.</param>
    /// <param name="TargetObject">OPTIONAL. Unit to attach the far end of a beam particle to.  Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="TargetBoneName">OPTIONAL. Name of the bone to attach the far end of a beam particle to.  Used in conjunction with TargetObject.</param>
    /// <param name="TargetPosition">OPTIONAL. A fixed position for the far end of a beam particle.  Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="OrientTowards">OPTIONAL. Particle effect will orient to face this point.</param>
    /// <param name="SpecificUnitOnly">OPTIONAL. If used, only sends this particle to this unit.  Otherwise, all units will see the particle.</param>
    /// <param name="SpecificTeamOnly">OPTIONAL.  If used, only this team will see the particle.  Otherwise, all teams will see the particle.</param>
    /// <param name="FOWVisibilityRadius">Used with FOWTeam to determine particle visibility in the FoW.  The particle will be visible if a unit has visibility into the area defined by this radius and the center of the particle.</param>
    /// <param name="FOWTeam">OPTIONAL.  If the viewing unit is on the same team as set by this variable, that unit will see this particle even if it's in the Fog of War.  Only used if FOWVisibilityRadius is non-zero.</param>
    /// <param name="SendIfOnScreenOrDiscard">If true, will only try to send the particle if a unit can see it when the particle spawns.  Use for one-shot particles; saves a lot of bandwidth, so use as often as possible.</param>
    public extern static Task<bool> CreateUnitParticle(AttackableUnit BindObject = , string BoneName = "", string EffectName = "", AttackableUnit TargetObject = , string TargetBoneName = "", Vector3 TargetPosition = 0;0;0, Vector3 OrientTowards = 0;0;0, AttackableUnit SpecificUnitOnly = , TeamEnum SpecificTeamOnly = TEAM_UNKNOWN, float FOWVisibilityRadius = 0.0, TeamEnum FOWTeam = TEAM_UNKNOWN, bool SendIfOnScreenOrDiscard = false);
    /// <summary>
    /// Adds a unit particle effect
    /// </summary>
    /// <remarks>
    /// Returns an EffectID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="Position">Position of the particle effect.</param>
    /// <param name="EffectName">File name of the particle effect file to use.</param>
    /// <param name="TargetObject">OPTIONAL. Unit to attach the far end of a beam particle to.  Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="TargetBoneName">OPTIONAL. Name of the bone to attach the far end of a beam particle to.  Used in conjunction with TargetObject.</param>
    /// <param name="TargetPosition">OPTIONAL. A fixed position for the far end of a beam particle.  Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="OrientTowards">OPTIONAL. Particle effect will orient to face this point.</param>
    /// <param name="SpecificUnitOnly">OPTIONAL. If used, only sends this particle to this unit.  Otherwise, all units will see the particle.</param>
    /// <param name="SpecificTeamOnly">OPTIONAL.  If used, only this team will see the particle.  Otherwise, all teams will see the particle.</param>
    /// <param name="FOWVisibilityRadius">Used with FOWTeam to determine particle visibility in the FoW.  The particle will be visible if a unit has visibility into the area defined by this radius and the center of the particle.</param>
    /// <param name="FOWTeam">OPTIONAL.  If the viewing unit is on the same team as set by this variable, that unit will see this particle even if it's in the Fog of War.  Only used if FOWVisibilityRadius is non-zero.</param>
    /// <param name="SendIfOnScreenOrDiscard">If true, will only try to send the particle if a unit can see it when the particle spawns.  Use for one-shot particles; saves a lot of bandwidth, so use as often as possible.</param>
    public extern static Task<bool> CreatePositionParticle(Vector3 Position = 0;0;0, string EffectName = "", AttackableUnit TargetObject = , string TargetBoneName = "", Vector3 TargetPosition = 0;0;0, Vector3 OrientTowards = 0;0;0, AttackableUnit SpecificUnitOnly = , TeamEnum SpecificTeamOnly = TEAM_UNKNOWN, float FOWVisibilityRadius = 0.0, TeamEnum FOWTeam = TEAM_UNKNOWN, bool SendIfOnScreenOrDiscard = false);
    /// <summary>
    /// Removes Particle
    /// </summary>
    /// <remarks>
    /// Used on Effect IDs returned by the CreateUnitParticle and CreatePositionParticle
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; returned by CreateParticle nodes</param>
    public extern static Task<bool> RemoveParticle(DWORD EffectID = );
    /// <summary>
    /// Returns unit Team ID
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitTeam(AttackableUnit Unit = );
    /// <summary>
    /// Sets unit state DisableAmbientGold.  If disabled, unit does not get ambient gold gain (but still gets gold/5 from runes).
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.
    /// </remarks>
    /// <param name="Unit">Sets state of this unit.</param>
    /// <param name="Disabled">If true, ambient gold gain is disabled.</param>
    public extern static Task<bool> SetStateDisableAmbientGold(AttackableUnit Unit = , bool Disabled = false);
    /// <summary>
    /// Sets unit level cap.  Level cap 0 means no cap.  Otherwise unit will earn experience up to one XP less than the level cap.
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.  If unit is already higher than the cap, it will earn 0 XP.
    /// </remarks>
    /// <param name="Unit">Sets level cap of this unit.</param>
    /// <param name="LevelCap">If 0, no level cap; otherwise unit cannot get higher than this level.</param>
    public extern static Task<bool> SetUnitLevelCap(AttackableUnit Unit = , int LevelCap = 0);
    /// <summary>
    /// Locks all player cameras to their champions.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Lock">If true, locks all player cameras to their champions.  If false, unlocks all player cameras from their champions.</param>
    public extern static Task<bool> LockAllPlayerCameras(bool Lock = true);
    /// <summary>
    /// Test to see if Player has camera locking enabled (camera locked to hero).
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.  This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Player">Player to test.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the the player has camera locking enabled; if False, returns SUCCESS if player does not have camera locking enabled, or if the player does not exist.</param>
    public extern static Task<bool> TestPlayerCameraLocked(AttackableUnit Player = , bool ReturnSuccessIf = true);
    /// <summary>
    /// A subtree. collapse
    /// </summary>
    /// <remarks>
    /// Subtree
    /// </remarks>
    /// <param name="TreeName"> can not be empty </param>
    public extern static Task<bool> SubTree(string TreeName = "");
    /// <summary>
    /// A Procedure call
    /// </summary>
    /// <remarks>
    /// Procedure
    /// </remarks>
    /// <param name="PocedureName"> can not be empty </param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ChatMessage">Chat string</param>
    public extern static Task<bool> Procedure2To2(string PocedureName = "", AttackableUnit Unit = , string ChatMessage = "Bot Talking Here");
    /// <summary>
    /// Test if game started
    /// </summary>
    /// <remarks>
    /// Tests if game started. True if game started. False if not
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>
    public extern static Task<bool> TestGameStarted(bool ReturnSuccessIf = true);
    /// <summary>
    /// Tests if the specified unit is under attack
    /// </summary>
    /// <remarks>
    /// Tests if the specified unit is under attack. May gather enemies of given unit to figure out if under attack
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is under attack; if False, returns SUCCESS if unit is not under attack</param>
    public extern static Task<bool> TestUnitUnderAttack(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns the type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitType(AttackableUnit Unit = );
    /// <summary>
    /// Returns the creature type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit creature type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitCreatureType(AttackableUnit Unit = );
    /// <summary>
    /// Tests if the specified unit can use the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can cast spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use spell</param>
    public extern static Task<bool> TestCanCastSpell(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Cast specified Spell
    /// </summary>
    /// <remarks>
    /// Spell cast
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> CastUnitSpell(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Set ignore visibility for a specific spell
    /// </summary>
    /// <remarks>
    /// Set ignore visibility for a specific spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="IgnoreVisibility">Ignore visibility ?</param>
    public extern static Task<bool> SetUnitSpellIgnoreVisibity(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = , bool IgnoreVisibility = false);
    /// <summary>
    /// Set specified Spell target position
    /// </summary>
    /// <remarks>
    /// Set specified Spell target position
    /// </remarks>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> SetUnitAISpellTargetLocation(Vector3 TargetLocation = 0;0;0;, int SlotIndex = );
    /// <summary>
    /// Set specified Spell target
    /// </summary>
    /// <remarks>
    /// Set specified Spell target
    /// </remarks>
    /// <param name="TargetUnit">Target Input.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> SetUnitAISpellTarget(AttackableUnit TargetUnit = , int SlotIndex = );
    /// <summary>
    /// Clears specified Spell target
    /// </summary>
    /// <remarks>
    /// Clears specified Spell target
    /// </remarks>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> ClearUnitAISpellTarget(int SlotIndex = );
    /// <summary>
    /// Test validity of specified Spell target
    /// </summary>
    /// <remarks>
    /// Test validity of specified Spell target
    /// </remarks>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit spell target is valid</param>
    public extern static Task<bool> TestUnitAISpellTargetValid(int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> GetSpellSlotCooldown(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Cooldown">Slot cooldown</param>
    public extern static Task<bool> SetSpellSlotCooldown(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = , float Cooldown = 1);
    /// <summary>
    /// Returns the PAR type for specified unit
    /// </summary>
    /// <remarks>
    /// PAR Type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitPARType(AttackableUnit Unit = );
    /// <summary>
    /// Returns the cost for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cost
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> GetUnitSpellCost(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Returns the cast range for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cast range
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> GetUnitSpellCastRange(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Returns the level for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell level
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> GetUnitSpellLevel(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Levels up a specified spell
    /// </summary>
    /// <remarks>
    /// Levels up a specified spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> LevelUpUnitSpell(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Tests if the specified unit can level up the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can level up spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use spell</param>
    public extern static Task<bool> TestUnitCanLevelUpSpell(AttackableUnit Unit = , int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Gets a handle to the the unit running the behavior tree in OutputRef
    /// </summary>
    /// <remarks>
    /// Gets a handle to the the unit running the behavior tree
    /// </remarks>
    public extern static Task<bool> GetUnitAISelf();
    /// <summary>
    /// Unit run logic for first time
    /// </summary>
    /// <remarks>
    /// Unit run logic for first time
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if first time</param>
    public extern static Task<bool> TestUnitAIFirstTime(bool ReturnSuccessIf = true);
    /// <summary>
    /// Sets unit to assist
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target unit</param>
    public extern static Task<bool> SetUnitAIAssistTarget(AttackableUnit TargetUnit = );
    /// <summary>
    /// Sets unit to target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Source Reference</param>
    public extern static Task<bool> SetUnitAIAttackTarget(AttackableUnit TargetUnit = );
    /// <summary>
    /// Gets unit being assisted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    public extern static Task<bool> GetUnitAIAssistTarget();
    /// <summary>
    /// Gets unit being targeted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    public extern static Task<bool> GetUnitAIAttackTarget();
    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    public extern static Task<bool> IssueMoveOrder();
    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="TargetUnit">Target Unit.</param>
    public extern static Task<bool> IssueMoveToUnitOrder(AttackableUnit TargetUnit = );
    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="Location">Position to move to</param>
    public extern static Task<bool> IssueMoveToPositionOrder(Vector3 Location = 0;0;0);
    /// <summary>
    /// Issue Chase Order
    /// </summary>
    /// <remarks>
    /// Chase
    /// </remarks>
    public extern static Task<bool> IssueChaseOrder();
    /// <summary>
    /// Issue Attack Order
    /// </summary>
    /// <remarks>
    /// Attack
    /// </remarks>
    public extern static Task<bool> IssueAttackOrder();
    /// <summary>
    /// Issue Wander order
    /// </summary>
    /// <remarks>
    /// Wander
    /// </remarks>
    public extern static Task<bool> IssueWanderOrder();
    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="EmoteIndex">Emote ID</param>
    public extern static Task<bool> IssueAIEmoteOrder(UnsignedInt EmoteIndex = 0);
    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="EmoteIndex">Emote ID</param>
    public extern static Task<bool> IssueGloabalEmoteOrder(AttackableUnit Unit = , UnsignedInt EmoteIndex = 0);
    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    public extern static Task<bool> IssueAIChatOrder(string ChatMessage = "Bot Chat", string ChatRcvr = "/all");
    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    public extern static Task<bool> IssueImmediateChatOrder(string ChatMessage = "Bot Chat", string ChatRcvr = "/all");
    /// <summary>
    /// Issue disable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    public extern static Task<bool> IssueAIDisableTaskOrder();
    /// <summary>
    /// Issue enable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    public extern static Task<bool> IssueAIEnableTaskOrder();
    /// <summary>
    /// Clear AI Attack target
    /// </summary>
    /// <remarks>
    /// Clear AI attack target
    /// </remarks>
    public extern static Task<bool> ClearUnitAIAttackTarget();
    /// <summary>
    /// Clear AI assist target
    /// </summary>
    /// <remarks>
    /// Clear AI assist target
    /// </remarks>
    public extern static Task<bool> ClearUnitAIAssistTarget();
    /// <summary>
    /// Teleport To base
    /// </summary>
    /// <remarks>
    /// Used for Teleporting home
    /// </remarks>
    public extern static Task<bool> IssueTeleportToBaseOrder();
    /// <summary>
    /// Returns the number of discrete attackers.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitAIAttackers(AttackableUnit Unit = );
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for PAR References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualPARType(PrimaryAbilityResourceType LeftHandSide = PAR_MANA, PrimaryAbilityResourceType RightHandSide = PAR_MANA);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for PAR References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualPARType(PrimaryAbilityResourceType LeftHandSide = PAR_MANA, PrimaryAbilityResourceType RightHandSide = PAR_MANA);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit type References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualUnitType(UnitType LeftHandSide = UNKNOWN_UNIT, UnitType RightHandSide = UNKNOWN_UNIT);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit type References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualUnitType(UnitType LeftHandSide = UNKNOWN_UNIT, UnitType RightHandSide = UNKNOWN_UNIT);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualCreatureType(CreatureType LeftHandSide = UNKNOWN_CREATURE, CreatureType RightHandSide = UNKNOWN_CREATURE);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualCreatureType(CreatureType LeftHandSide = UNKNOWN_CREATURE, CreatureType RightHandSide = UNKNOWN_CREATURE);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> EqualSpellbookType(SpellbookTypeEnum LeftHandSide = SPELLBOOK_UNKNOWN, SpellbookTypeEnum RightHandSide = SPELLBOOK_UNKNOWN);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static Task<bool> NotEqualSpellbookType(SpellbookTypeEnum LeftHandSide = SPELLBOOK_UNKNOWN, SpellbookTypeEnum RightHandSide = SPELLBOOK_UNKNOWN);
    /// <summary>
    /// Unit can buy next recommended item
    /// </summary>
    /// <remarks>
    /// Unit can buy next recommended item
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit at spell location</param>
    public extern static Task<bool> TestUnitAICanBuyRecommendedItem(bool ReturnSuccessIf = true);
    /// <summary>
    /// Buy next recommended item
    /// </summary>
    /// <remarks>
    /// Buy next recommended item
    /// </remarks>
    public extern static Task<bool> UnitAIBuyRecommendedItem();
    /// <summary>
    /// Unit can buy item
    /// </summary>
    /// <remarks>
    /// Unit can buy  item
    /// </remarks>
    /// <param name="ItemID">Item to buy.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can buy item</param>
    public extern static Task<bool> TestUnitAICanBuyItem(UnsignedInt ItemID = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Buy item
    /// </summary>
    /// <remarks>
    /// Buy item
    /// </remarks>
    /// <param name="ItemID">Item to buy.</param>
    public extern static Task<bool> UnitAIBuyItem(UnsignedInt ItemID = 0);
    /// <summary>
    /// Computes a position for spell cast
    /// </summary>
    /// <remarks>
    /// Computes a position for spell cast
    /// </remarks>
    /// <param name="TargetUnit">target unit</param>
    /// <param name="ReferenceUnit">Reference unit</param>
    /// <param name="Range">Spell range</param>
    /// <param name="UnitSide">Which side of target are we going to (in between our out)</param>
    public extern static Task<bool> ComputeUnitAISpellPosition(AttackableUnit TargetUnit = , AttackableUnit ReferenceUnit = , float Range = , bool UnitSide = true);
    /// <summary>
    /// Retrieves a position for spell cast
    /// </summary>
    /// <remarks>
    /// Retrieves a position for spell cast
    /// </remarks>
    public extern static Task<bool> GetUnitAISpellPosition();
    /// <summary>
    /// Clears position for spell cast
    /// </summary>
    /// <remarks>
    /// Clears position for spell cast
    /// </remarks>
    public extern static Task<bool> ClearUnitAISpellPosition();
    /// <summary>
    /// Unit precomputed cast location valid 
    /// </summary>
    /// <remarks>
    /// Unit precomputed cast location valid
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit cast location is valid</param>
    public extern static Task<bool> TestUnitAISpellPositionValid(bool ReturnSuccessIf = true);
    /// <summary>
    /// Unit at precomputed spell cast location
    /// </summary>
    /// <remarks>
    /// Unit at precomputed spell location
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Location">Source Reference</param>
    /// <param name="Error">Accepted error</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit at spell location</param>
    public extern static Task<bool> TestUnitAtLocation(AttackableUnit Unit = , Vector3 Location = 0;0;0, float Error = 200, bool ReturnSuccessIf = true);
    /// <summary>
    /// Unit in safe range
    /// </summary>
    /// <remarks>
    /// Unit in safe Range
    /// </remarks>
    /// <param name="Range">Unit in safe Range</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use spell</param>
    public extern static Task<bool> TestUnitAIIsInSafeRange(float Range = 600, bool ReturnSuccessIf = true);
    /// <summary>
    /// Computes a safe position for AI unit
    /// </summary>
    /// <remarks>
    /// Computes a safe position for AI unit
    /// </remarks>
    /// <param name="Range">safe range</param>
    /// <param name="UseDefender">If True, use defenders in search</param>
    /// <param name="UseEnemy">If True, use enemies to guide in search</param>
    public extern static Task<bool> ComputeUnitAISafePosition(float Range = , bool UseDefender = true, bool UseEnemy = true);
    /// <summary>
    /// Retrieves a safe position for AI unit
    /// </summary>
    /// <remarks>
    /// Retrieves a safe position for AI unit
    /// </remarks>
    public extern static Task<bool> GetUnitAISafePosition();
    /// <summary>
    /// Clears position for safe
    /// </summary>
    /// <remarks>
    /// Clears position for safe
    /// </remarks>
    public extern static Task<bool> ClearUnitAISafePosition();
    /// <summary>
    /// Unit precomputed safe location valid 
    /// </summary>
    /// <remarks>
    /// Unit precomputed safelocation valid
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit cast location is valid</param>
    public extern static Task<bool> TestUnitAISafePositionValid(bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns the base location of a given unit
    /// </summary>
    /// <remarks>
    /// Return SUCCES if we can find the base
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static Task<bool> GetUnitAIBasePosition(AttackableUnit Unit = );
    /// <summary>
    /// Returns the radius AOE of spell in a given slot
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static Task<bool> GetUnitSpellRadius(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Returns distance between 2 units
    /// </summary>
    /// <remarks>
    /// takes into account their BB
    /// </remarks>
    /// <param name="SourceUnit">Source unit</param>
    /// <param name="DestinationUnit">Destination unit</param>
    public extern static Task<bool> GetDistanceBetweenUnits(AttackableUnit SourceUnit = , AttackableUnit DestinationUnit = );
    /// <summary>
    /// Unit target is in range
    /// </summary>
    /// <remarks>
    /// Unit target is in range
    /// </remarks>
    /// <param name="Error">Accepted error for unit location</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has target in range</param>
    public extern static Task<bool> TestUnitAIAttackTargetInRange(float Error = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Unit has valid target
    /// </summary>
    /// <remarks>
    /// Unit has valid target, use before getting attack target.
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has valid target</param>
    public extern static Task<bool> TestUnitAIAttackTargetValid(bool ReturnSuccessIf = true);
    /// <summary>
    /// Unit can see target
    /// </summary>
    /// <remarks>
    /// Unit can see target
    /// </remarks>
    /// <param name="Unit">Viewer Unit</param>
    /// <param name="TargetUnit">Target  Unit</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can see target</param>
    public extern static Task<bool> TestUnitIsVisible(AttackableUnit Unit = , AttackableUnit TargetUnit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Sets item target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target</param>
    /// <param name="ItemID">Item ID</param>
    public extern static Task<bool> SetUnitAIItemTarget(AttackableUnit TargetUnit = , int ItemID = 0);
    /// <summary>
    /// Clears item target
    /// </summary>
    /// <remarks>
    /// Clears item target
    /// </remarks>
    public extern static Task<bool> ClearUnitAIItemTarget();
    /// <summary>
    /// Unit can use item
    /// </summary>
    /// <remarks>
    /// Unit can use item
    /// </remarks>
    /// <param name="ItemID">Item ID</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use item</param>
    public extern static Task<bool> TestUnitAICanUseItem(int ItemID = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Issue Use item Order
    /// </summary>
    /// <remarks>
    /// Use item
    /// </remarks>
    /// <param name="ItemID">Item ID</param>
    public extern static Task<bool> IssueUseItemOrder(int ItemID = 0);
    /// <summary>
    /// Tests if specified slot has spell toggled ON
    /// </summary>
    /// <remarks>
    /// Tests if specified slot has spell toggled ON
    /// </remarks>
    /// <param name="Unit">Unit to poll</param>
    /// <param name="SlotIndex">spell slot ID</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the spell is toggled ON</param>
    public extern static Task<bool> TestUnitSpellToggledOn(AttackableUnit Unit = , int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Tests if unit is channeling
    /// </summary>
    /// <remarks>
    /// Tests if unit is channeling
    /// </remarks>
    /// <param name="Unit">Unit to poll</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if unit is channeling</param>
    public extern static Task<bool> TestUnitIsChanneling(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns unit that casted a buff on input unit
    /// </summary>
    /// <remarks>
    /// Returns unit that casted a buff on input unit
    /// </remarks>
    /// <param name="Unit">Source unit</param>
    /// <param name="BuffName">Buff name</param>
    public extern static Task<bool> GetUnitBuffCaster(AttackableUnit Unit = , string BuffName = "");
    /// <summary>
    /// AI Unit has an assigned task
    /// </summary>
    /// <remarks>
    /// AI Unit has an assigned task
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is assigned a task</param>
    public extern static Task<bool> TestUnitAIHasTask(bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns position computed by a task assigned to the unit
    /// </summary>
    /// <remarks>
    /// Returns position computed by a task assigned to the unit
    /// </remarks>
    public extern static Task<bool> GetUnitAITaskPosition();
    /// <summary>
    /// Permanently modifies a target unit's armor.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static Task<bool> IncPermanentFlatArmorMod(AttackableUnit Unit = , float Delta = );
    /// <summary>
    /// Permanently modifies a target unit's magic resistance.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static Task<bool> IncPermanentFlatMagicResistanceMod(AttackableUnit Unit = , float Delta = );
    /// <summary>
    /// Permanently modifies a target unit's max health.  This will heal the target.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.  Further, this later needs to be converted to a non-healing implementation; it is using the healing approach until Kuo fixes a bug.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static Task<bool> IncPermanentFlatMaxHealthMod(AttackableUnit Unit = , float Delta = );
    /// <summary>
    /// Permanently modifies a target unit's attack damage.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static Task<bool> IncPermanentFlatAttackDamageMod(AttackableUnit Unit = , float Delta = );
    
}
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
