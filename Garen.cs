using static SBL;
public static class SBL
{
    /// <summary>
    /// Sequence blocks will tick their children in order until one returns a FAILURE, at which point the node will return FAILURE.  If a child return RUNNING then the node will return RUNNING and execute that child first next tick.  If all children return SUCCESS the node will return SUCCESS.
    /// </summary>
    /// <remarks>
    /// Generally alternate these with selector nodes
    /// </remarks>
    public extern static bool Sequence();
    /// <summary>
    /// Selector blocks will tick their children in order until one returns a SUCCESS, at which point the node will return SUCCESS.  If a child return RUNNING then the node will return RUNNING and execute that child first next tick.  If all children return FAILURE the node will return FAILURE.
    /// </summary>
    /// <remarks>
    /// Generally alternate these with sequence nodes
    /// </remarks>
    public extern static bool Selector();
    /// <summary>
    /// Creates a dynamic BTInstance based on the name of the provided tree.
    /// </summary>
    /// <remarks>
    /// For now this should generally be used for one-off tress that delete themselves since references outside of this function are difficult
    /// </remarks>
    /// <param name="TreeName">Name of the tree to instantiate</param>
    /// <param name="Type">The type of the instance.  For now I strongly recommend only using DELETE_SELF.</param>
    public extern static bool CreateDynamicBTInstance(string TreeName = "", BTInstanceType Type = DELETE_SELF);
    /// <summary>
    /// Debug node used to return an explicit value and write a string to log.  In the case that RUNNING is selected the node will return RUNNING a number of times equal to runningLimit.
    /// </summary>
    /// <remarks>
    /// Running limit is unused in non RUNNING types, and after the running limit has been reached the block will return SUCCESS
    /// </remarks>
    /// <param name="RunningLimit">How many times should we return RUNNING before returning SUCCESS?</param>
    /// <param name="Result">What the node should return when ticked</param>
    /// <param name="String">The string that should be outputted to log</param>
    public extern static bool DebugAction(int RunningLimit = 0, BehaveResult Result = SUCCESS, string String = "Yo");
    /// <summary>
    /// Decorator that masks FAILURE.
    /// </summary>
    /// <remarks>
    /// Running still returns running
    /// </remarks>
    public extern static bool MaskFailure();
    /// <summary>
    /// Decorator that allows user to specify the number of times the subtree will run.
    /// </summary>
    /// <remarks>
    /// Need comment
    /// </remarks>
    /// <param name="RunningLimit">The number of times the sub tree is to be executed</param>
    public extern static bool LoopNTimes(int RunningLimit = 1);
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will always return SUCCESS.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    /// <param name="Output">Output reference for each individual iteration of the node.  This should only be referenced by children!</param>
    public extern static bool IterateOverAllDecorator(out AttackableUnit Output, AttackableUnitCollection Collection = );
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns FAILURE.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will return SUCCESS if all children return SUCCESS and FAILURE if one child returns FAILURE.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    /// <param name="Output">Output reference for each individual iteration of the node.  This should only be referenced by children!</param>
    public extern static bool IterateUntilFailureDecorator(out AttackableUnit Output, AttackableUnitCollection Collection = );
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns SUCCESS.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will return SUCCESS if a child returns SUCCESS and FAILURE if all children return FAILURE.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    /// <param name="Output">Output reference for each individual iteration of the node.  This should only be referenced by children!</param>
    public extern static bool IterateUntilSuccessDecorator(out AttackableUnit Output, AttackableUnitCollection Collection = );
    /// <summary>
    /// Return RUNNING for X seconds after first tick.
    /// </summary>
    /// <remarks>
    /// This is a blocking delay and it uses the real timer not the game timer, so it is unaffected by pause.
    /// </remarks>
    /// <param name="DelayAmount">The amount of time to delay after first tick.</param>
    public extern static bool DelayNSecondsBlocking(float DelayAmount = 0);
    /// <summary>
    /// Enable/Disable a quest by name
    /// </summary>
    /// <remarks>
    /// This will fail if the quest does not exist
    /// </remarks>
    /// <param name="Enabled">Should the quest be enabled or disabled?</param>
    /// <param name="Name">The name of the quest to adjust</param>
    public extern static bool SetBTInstanceStatus(bool Enabled = true, string Name = "");
    /// <summary>
    /// Set all map barracks active/inactive
    /// </summary>
    /// <remarks>
    /// This functionally is the same as the kill minions cheat
    /// </remarks>
    /// <param name="Enable">The status of the barracks</param>
    public extern static bool SetBarrackStatus(bool Enable = true);
    /// <summary>
    /// Display objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    public extern static bool ShowObjectiveText(string String = "EMPTY STRING");
    /// <summary>
    /// Hide objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no objective text is displayed
    /// </remarks>
    public extern static bool HideObjectiveText();
    /// <summary>
    /// Display auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    public extern static bool ShowAuxiliaryText(string String = "EMPTY STRING");
    /// <summary>
    /// Hide auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no auxiliary text is displayed
    /// </remarks>
    public extern static bool HideAuxiliaryText();
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>
    public extern static bool SetVarBool(out bool Output, bool Input = true);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>
    public extern static bool SetVarAttackableUnit(out AttackableUnit Output, AttackableUnit Input = True);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>
    public extern static bool SetVarInt(out int Output, int Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>
    public extern static bool SetVarDWORD(out DWORD Output, DWORD Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>
    public extern static bool SetVarString(out string Output, string Input = "DEFAULT STRING");
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>
    public extern static bool SetVarFloat(out float Output, float Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for Vector References.  If you want to make a vector out of 3 floats, use MakeVector.
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>
    public extern static bool SetVarVector(out Vector3 Output, Vector3 Input = 0;0;0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit team
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualUnitTeam(TeamEnum LeftHandSide = TEAM_UNKNOWN, TeamEnum RightHandSide = TEAM_UNKNOWN);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit team
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualUnitTeam(TeamEnum LeftHandSide = TEAM_UNKNOWN, TeamEnum RightHandSide = TEAM_UNKNOWN);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualBool(bool LeftHandSide = true, bool RightHandSide = true);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualBool(bool LeftHandSide = true, bool RightHandSide = true);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualString(string LeftHandSide = "True", string RightHandSide = "True");
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualString(string LeftHandSide = "True", string RightHandSide = "True");
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than the RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool LessInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool LessEqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool GreaterInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool GreaterEqualInt(int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for flooat References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than the RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool LessFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool LessEqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool GreaterFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualUnit(AttackableUnit LeftHandSide = 0, AttackableUnit RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualUnit(AttackableUnit LeftHandSide = 0, AttackableUnit RightHandSide = 0);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is greater than or equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool GreaterEqualFloat(float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool AddInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool SubtractInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool MultiplyInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool DivideInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool ModulusInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool MinInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool MaxInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool AddFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool SubtractFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool MultiplyFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool DivideFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool MinFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>
    public extern static bool MaxFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Gets a handle to the player and puts it in OutputRef
    /// </summary>
    /// <remarks>
    /// Only works in Tutorial, or other situation where there's only one player.  Works by getting the first player in the roster that has a legal client ID.
    /// </remarks>
    /// <param name="Output">Destination reference; holds a hero object handle</param>
    public extern static bool GetTutorialPlayer(out AttackableUnit Output);
    /// <summary>
    /// Returns a handle to a collection containing all champions in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains champions who have disconnected or are played by bots.
    /// </remarks>
    /// <param name="Output">Destination reference; holds the collection of all champions in the game.</param>
    public extern static bool GetChampionCollection(out Obj_AI_HeroCollection Output);
    /// <summary>
    /// Returns a handle to a collection containing all turrets alive in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains turrets on both teams.
    /// </remarks>
    /// <param name="Output">Destination reference; holds the collection of all champions in the game.</param>
    public extern static bool GetTurretCollection(out Obj_AI_TurretCollection Output);
    /// <summary>
    /// Gets a handle to the turret in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the turret is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the turrets to be checked.</param>
    /// <param name="Lane">Lane of the turret.  Check the level script for the enum.</param>
    /// <param name="Position">Position of the turret.  Check the level script for the enum.</param>
    /// <param name="Turret">Destination Reference; holds a turret object handle</param>
    public extern static bool GetTurret(out AttackableUnit Turret, TeamEnum Team = TEAM_ORDER, int Lane = 1, int Position = 1);
    /// <summary>
    /// Gets a handle to the inhibitor in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the inhibitor is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the inhibitor to be checked.</param>
    /// <param name="Lane">Lane of the inhibitor.  Check the level script for the enum.</param>
    /// <param name="Inhibitor">Destination Reference; holds an inhibitor object handle</param>
    public extern static bool GetInhibitor(out AttackableUnit Inhibitor, TeamEnum Team = TEAM_ORDER, int Lane = 1);
    /// <summary>
    /// Gets a handle to the nexus on a specific teamin a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the Nexus is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the nexus to return.</param>
    /// <param name="Nexus">Destination Reference; holds a nexus object handle</param>
    public extern static bool GetNexus(out AttackableUnit Nexus, TeamEnum Team = TEAM_ORDER);
    /// <summary>
    /// Returns the current position of a specific unit
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current position of the unit.</param>
    public extern static bool GetUnitPosition(out Vector3 Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the current elapsed game time.  This will be affected by pausing, cheats, or other things.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the currently elapsed game time.</param>
    public extern static bool GetGameTime(out float Output);
    /// <summary>
    /// Returns the lane position of a turret.
    /// </summary>
    /// <remarks>
    /// This position is defined in the level script and is map specific.
    /// </remarks>
    /// <param name="Turret">Turret to poll.</param>
    /// <param name="Output">Destination reference; contains the integer position of the turret.  This is defined in the level script.</param>
    public extern static bool GetTurretPosition(out int Output, AttackableUnit Turret = );
    /// <summary>
    /// Returns the max health of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the max health of the unit.</param>
    public extern static bool GetUnitMaxHealth(out float Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the current health of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current health of the unit.</param>
    public extern static bool GetUnitCurrentHealth(out float Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the current Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    /// <param name="Output">Destination reference; contains the current Primary Ability Resource value of the unit.</param>
    public extern static bool GetUnitCurrentPAR(out float Output, AttackableUnit Unit = , PrimaryAbilityResourceType PrimaryAbilityResourceType = PAR_MANA);
    /// <summary>
    /// Returns the maximum Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX PAR
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    /// <param name="Output">Destination reference; contains the maximum Primary Ability Resource value of the unit.</param>
    public extern static bool GetUnitMaxPAR(out float Output, AttackableUnit Unit = , PrimaryAbilityResourceType PrimaryAbilityResourceType = PAR_MANA);
    /// <summary>
    /// Returns the current armor of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT armor
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current armor of the unit.</param>
    public extern static bool GetUnitArmor(out float Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the number of discrete elements contained within the collection.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Collection">Collection to count.</param>
    /// <param name="Output">Destination reference; contains the number of elements in the collection.</param>
    public extern static bool GetCollectionCount(out int Output, AttackableUnitCollection Collection = );
    /// <summary>
    /// Returns the current skin of a specific unit
    /// </summary>
    /// <remarks>
    /// Since buildings don't hame skins, it will return the name of the building.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the skin name of the unit.</param>
    public extern static bool GetUnitSkinName(out string Output, AttackableUnit Unit = );
    /// <summary>
    /// Turns on or off the highlight of a unit.
    /// </summary>
    /// <remarks>
    /// Creates a unit highlight akin to what is used in the tutorial.  This highlight is by default blue.  Always returns SUCCESS.
    /// </remarks>
    /// <param name="Enable">Should the Highlight be turned on or turned off?</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    public extern static bool ToggleUnitHighlight(bool Enable = , AttackableUnit TargetUnit = );
    /// <summary>
    /// Pings a unit on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.  Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.  Important for team coloration and chat info.</param>
    /// <param name="TargetUnit">Unit to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    public extern static bool PingMinimapUnit(AttackableUnit PingingUnit = , AttackableUnit TargetUnit = , bool PlayAudio = true);
    /// <summary>
    /// Pings a location on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.  Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.  Important for team coloration and chat info.</param>
    /// <param name="TargetPosition">Location to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    public extern static bool PingMinimapLocation(AttackableUnit PingingUnit = , Vector3 TargetPosition = 0;0;0;, bool PlayAudio = true);
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
    /// <param name="QuestId">Gives a unique identifier to refer back to this quest</param>
    public extern static bool ActivateQuest(out int QuestId, string String = "EMPTY STRING", AttackableUnit Player = , QuestType QuestType = PRIMARY_QUEST, bool HandleRollOver = false, string Tooltip = "");
    /// <summary>
    /// Plays a quest completion animation and then removes it from the HUD
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    public extern static bool CompleteQuest(int QuestId = );
    /// <summary>
    /// Removes quest from the HUD immediately
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node; there is no ceremony involved in quest removal
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    public extern static bool RemoveQuest(int QuestId = );
    /// <summary>
    /// Test to see if the quest has the mouse rolled over it
    /// </summary>
    /// <remarks>
    /// This quest must have been activated with HandleRollOver=true in ActivateQuest
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the quest has the mouse over it; if False, returns SUCCESS if unit does not have the mouse over it, or if the quest doesnt exist, or if the quest doesnt have HandleRollOver=true</param>
    public extern static bool TestQuestRolledOver(int QuestId = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if the quest is being clicked right now with the mouse down over it.
    /// </summary>
    /// <remarks>
    /// Tests to see if the quest is being clicked right now, or if the mouse is not clicking it right now.
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the quest has been clicked by the mouse right now; if False, returns SUCCESS if the quest is not being clicked, or if the quest doesnt exist.</param>
    public extern static bool TestQuestClicked(int QuestId = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Create a new Tip and display it in the TipTracker
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipCategory">The localized string for the Tip Category.</param>
    /// <param name="TipId">Gives a unique identifier to refer back to this Tip.</param>
    public extern static bool ActivateTip(out int TipId, AttackableUnit Player = , string TipName = "EMPTY STRING", string TipCategory = "EMPTY STRING");
    /// <summary>
    /// Removes Tip from the Tip Tracker immediately
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip node; there is no ceremony involved in Tip removal
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip</param>
    public extern static bool RemoveTip(int TipId = );
    /// <summary>
    /// Enables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to enable</param>
    public extern static bool EnableTipEvents(AttackableUnit Player = );
    /// <summary>
    /// Disables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to disable</param>
    public extern static bool DisableTipEvents(AttackableUnit Player = );
    /// <summary>
    /// Tests to see if a Tip in the Tip Tracker or a Tip Dialogue has been clicked by the user
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip and ActivateTipDialogue nodes. Use ReturnSuccessIf to control the output.  This will return as if the Tip has NOT been clicked if the Tip Id is invalid.
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip or ActivateTipDialogue</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the Tip has been clicked; if False, returns SUCCESS if the Tip has NOT been clicked.</param>
    public extern static bool TestTipClicked(int TipId = , bool ReturnSuccessIf = true);
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
    /// <param name="TipId">Gives a unique identifier to refer back to this Tip Dialogue.</param>
    public extern static bool ActivateTipDialogue(out int TipId, AttackableUnit Player = , string TipName = "EMPTY STRING", string TipBody = "EMPTY STRING", string TipImage = "");
    /// <summary>
    /// Enables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to enable</param>
    public extern static bool EnableTipDialogueEvents(AttackableUnit Player = );
    /// <summary>
    /// Disables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to disable</param>
    public extern static bool DisableTipDialogueEvents(AttackableUnit Player = );
    /// <summary>
    /// Creates a vector from three static components
    /// </summary>
    /// <remarks>
    /// If you want to copy a Vector, use SetVarVector.
    /// </remarks>
    /// <param name="X">X component</param>
    /// <param name="Y">Y component</param>
    /// <param name="Z">Z component</param>
    /// <param name="Vector">OutputVector</param>
    public extern static bool MakeVector(out Vector3 Vector, float X = 0, float Y = 0, float Z = 0);
    /// <summary>
    /// Turn on or off a UI highlight for a specific UI Element
    /// </summary>
    /// <remarks>
    /// Set the enabled flag to control whether this node turns the element on or off
    /// </remarks>
    /// <param name="UIElement">UIElement; which element on the minimap do you want to highlight</param>
    /// <param name="Enabled">If true, turns on the UI Highlight, if false then turns off the UI Highlight</param>
    public extern static bool ToggleUIHighlight(UIElement UIElement = UI_MINIMAP, bool Enabled = true);
    /// <summary>
    /// Keeps track whether a player has opened his scoreboard.
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Unit">Handle of the attacking unit</param>
    /// <param name="Output">Destination Reference; holds whether the scoreboard has been opened since the tree was enabled.</param>
    public extern static bool RegisterScoreboardOpened(out bool Output, AttackableUnit Unit = );
    /// <summary>
    /// Keeps track of the number of minions (not neutrals) not on the attacker's team killed by an attacker
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Unit">Handle of the attacking unit</param>
    /// <param name="Output">Destination Reference; holds the number of units killed by the attacker</param>
    public extern static bool RegisterMinionKillCounter(out int Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns an int containing the number of kills the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    /// <param name="Output">Destination Reference; holds the number of champions killed by the attacker</param>
    public extern static bool GetChampionKills(out int Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns an int containing the number of deaths the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    /// <param name="Output">Destination Reference; holds the number of times the champion has been killed.</param>
    public extern static bool GetChampionDeaths(out int Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns an int containing the number of assists the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    /// <param name="Output">Destination Reference; holds the number of assists the champion has earned.</param>
    public extern static bool GetChampionAssists(out int Output, AttackableUnit Unit = );
    /// <summary>
    /// Gives target champion a variable amount of gold.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to give gold to.</param>
    /// <param name="GoldAmount">Amount of gold to give the champion.</param>
    public extern static bool GiveChampionGold(AttackableUnit Unit = , float GoldAmount = );
    /// <summary>
    /// Orders a unit to stop its movement.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  How this block will interract with forced move orders (say from a skill) is currently untested.
    /// </remarks>
    /// <param name="Unit">Handle of the champion to order.</param>
    public extern static bool StopUnitMovement(AttackableUnit Unit = );
    /// <summary>
    /// Test if a hero has a specific item.
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.  This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    /// <param name="ItemID">Numerical ID of the item to look for.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has the item; if False, returns SUCCESS if unit does not.</param>
    public extern static bool TestChampionHasItem(AttackableUnit Unit = , int ItemID = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Pause or unpause the game.
    /// </summary>
    /// <remarks>
    /// Be careful using this!  It is not fully protected for use in a production environment!
    /// </remarks>
    /// <param name="Pause">Pause or unpause the game.</param>
    public extern static bool SetGamePauseState(bool Pause = true);
    /// <summary>
    /// Pan the camera from its current position to a target point.
    /// </summary>
    /// <remarks>
    /// Once the pan starts this node will return RUNNING until the pan completes.  After the pan completes the node will always return SUCCESS. This node locks camera movement while panning, and returns camera movement state to what it was before the pan started.  Be careful if you change camera movement locking state while panning, because it will not stick.
    /// </remarks>
    /// <param name="Unit">The unit whose camera is being manipulated.</param>
    /// <param name="TargetPosition">3D Point containing the target camera position.</param>
    /// <param name="Time">The amount of time the pan should take; this will scale the pan speed. </param>
    public extern static bool PanCameraFromCurrentPositionToPoint(AttackableUnit Unit = , Vector3 TargetPosition = 0;0;0, float Time = 1);
    /// <summary>
    /// Returns the number of item slots filled for a particular champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    /// <param name="Output">The number of items in the target's inventory.</param>
    public extern static bool GetNumberOfInventorySlotsFilled(out int Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the level of the target unit.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose level you want to check.</param>
    /// <param name="Output">The level of the target unit.</param>
    public extern static bool GetUnitLevel(out int Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the current XP total of the target champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  Returns 0 if unit is not champion.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose XP total you want to get.</param>
    /// <param name="Output">The current XP of the target unit.</param>
    public extern static bool GetUnitXP(out float Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the distance between the Unit and the Point
    /// </summary>
    /// <remarks>
    /// Distance is measured from the edge of the unit's bounding box
    /// </remarks>
    /// <param name="Unit">Handle of the unit</param>
    /// <param name="Point">Point</param>
    /// <param name="Output">Destination Reference; holds the distance from the unit to the point</param>
    public extern static bool DistanceBetweenObjectAndPoint(out float Output, AttackableUnit Unit = , Vector3 Point = 0;0;0;);
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
    /// <param name="Output">Destination Reference; holds a collection of units discovered</param>
    public extern static bool GetUnitsInTargetArea(out AttackableUnitCollection Output, AttackableUnit Unit = , Vector3 TargetLocation = 0;0;0;, float Radius = 0, SpellFlags SpellFlags = AlwaysSelf);
    /// <summary>
    /// Test to see if unit is alive
    /// </summary>
    /// <remarks>
    /// Unit is not alive if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is alive; if False, returns SUCCESS if unit is dead or does not exist</param>
    public extern static bool TestUnitCondition(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if unit is invulnerable
    /// </summary>
    /// <remarks>
    /// Unit is not invulnerable if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is invulnerable; if False, returns SUCCESS if unit is not invulnerable or does not exist</param>
    public extern static bool TestUnitIsInvulnerable(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if unit is in brush
    /// </summary>
    /// <remarks>
    /// Unit is not in brush if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is in brush; if False, returns SUCCESS if unit is not in brush or does not exist</param>
    public extern static bool TestUnitInBrush(AttackableUnit Unit = , bool ReturnSuccessIf = true);
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
    public extern static bool TestUnitHasBuff(AttackableUnit TargetUnit = , AttackableUnit CasterUnit = , string BuffName = "", bool ReturnSuccessIf = true);
    /// <summary>
    /// Test to see if a one unit has visibility of another unit
    /// </summary>
    /// <remarks>
    /// If either unit does not exist, then they are not visible; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Viewer">Can this unit see the other?</param>
    /// <param name="TargetUnit">Is this unit visible to the viewer unit?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the target is visible to the viewer; if False, returns SUCCESS if unit is not visible to the viewer or does not exist.</param>
    public extern static bool TestUnitVisibility(AttackableUnit Viewer = , AttackableUnit TargetUnit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Disabled or Enables all user input
    /// </summary>
    /// <remarks>
    /// Disables or Enables all user input, for all users.
    /// </remarks>
    /// <param name="Enabled">If False disables all input for all users. If True, enables it.</param>
    public extern static bool ToggleUserInput(bool Enabled = true);
    /// <summary>
    /// Disabled or Enables the texture for fog of war for all users.
    /// </summary>
    /// <remarks>
    /// This will not reveal any units in the fog of war; perception bubbles are necessary for that.
    /// </remarks>
    /// <param name="Enabled">If False disables the texture for all users for all users. If True, enables it.</param>
    public extern static bool ToggleFogOfWarTexture(bool Enabled = true);
    /// <summary>
    /// Plays a localized VO event
    /// </summary>
    /// <remarks>
    /// Event is a 2D one-shot audio event.  A bad event name fails without complaint.  This node always returns SUCCESS.
    /// </remarks>
    /// <param name="EventID">FMOD event ID</param>
    /// <param name="FolderName">Folder the FMOD event is in in the Dialogue folder of the VO sound bank</param>
    /// <param name="FireAndForget">If true, plays sound as fire-and-forget and the node will return SUCCESS immediately.  If false, node will return RUNNING until the client tells the server that the VO is finished.</param>
    public extern static bool PlayVOAudioEvent(string EventID = "", string FolderName = "", bool FireAndForget = true);
    /// <summary>
    /// Returns the attack range for unit
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitAttackRange(out float Output, AttackableUnit Unit = );
    /// <summary>
    /// Disables or Enables initial neutral minion spawn.
    /// </summary>
    /// <remarks>
    /// Once neutral minion spawning has begun, this node no longer has any effect.
    /// </remarks>
    /// <param name="Enabled">If True, enables neutral minion spawning; if False, delays neutral minion spawning.</param>
    public extern static bool SetNeutralSpawnEnabled(bool Enabled = true);
    /// <summary>
    /// Returns the amount of gold the unit has
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference.</param>
    public extern static bool GetUnitGold(out float Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns unit unspent skill points
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference.</param>
    public extern static bool GetUnitSkillPoints(out int Output, AttackableUnit Unit = );
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
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble</param>
    public extern static bool AddUnitPerceptionBubble(out DWORD BubbleID, AttackableUnit TargetUnit = , float Radius = 0.0, float Duration = 0.0, TeamEnum Team = TEAM_ORDER, bool RevealStealth = false, AttackableUnit SpecificUnitsClientOnly = , AttackableUnit RevealSpecificUnitOnly = );
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
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble</param>
    public extern static bool AddPositionPerceptionBubble(out DWORD BubbleID, Vector3 Position = 0;0;0, float Radius = 0.0, float Duration = 0.0, TeamEnum Team = TEAM_ORDER, bool RevealStealth = false, AttackableUnit SpecificUnitsClientOnly = , AttackableUnit RevealSpecificUnitOnly = );
    /// <summary>
    /// Removes Perception Bubble
    /// </summary>
    /// <remarks>
    /// Used on Bubble IDs returned by the AddUnitPerceptionBubble and AddPositionPerceptionBubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble; returned by AddPerceptionBubble nodes</param>
    public extern static bool RemovePerceptionBubble(DWORD BubbleID = );
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
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; used to remove particle.</param>
    public extern static bool CreateUnitParticle(out DWORD EffectID, AttackableUnit BindObject = , string BoneName = "", string EffectName = "", AttackableUnit TargetObject = , string TargetBoneName = "", Vector3 TargetPosition = 0;0;0, Vector3 OrientTowards = 0;0;0, AttackableUnit SpecificUnitOnly = , TeamEnum SpecificTeamOnly = TEAM_UNKNOWN, float FOWVisibilityRadius = 0.0, TeamEnum FOWTeam = TEAM_UNKNOWN, bool SendIfOnScreenOrDiscard = false);
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
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; used to remove particle.</param>
    public extern static bool CreatePositionParticle(out DWORD EffectID, Vector3 Position = 0;0;0, string EffectName = "", AttackableUnit TargetObject = , string TargetBoneName = "", Vector3 TargetPosition = 0;0;0, Vector3 OrientTowards = 0;0;0, AttackableUnit SpecificUnitOnly = , TeamEnum SpecificTeamOnly = TEAM_UNKNOWN, float FOWVisibilityRadius = 0.0, TeamEnum FOWTeam = TEAM_UNKNOWN, bool SendIfOnScreenOrDiscard = false);
    /// <summary>
    /// Removes Particle
    /// </summary>
    /// <remarks>
    /// Used on Effect IDs returned by the CreateUnitParticle and CreatePositionParticle
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; returned by CreateParticle nodes</param>
    public extern static bool RemoveParticle(DWORD EffectID = );
    /// <summary>
    /// Returns unit Team ID
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference.</param>
    public extern static bool GetUnitTeam(out TeamEnum Output, AttackableUnit Unit = );
    /// <summary>
    /// Sets unit state DisableAmbientGold.  If disabled, unit does not get ambient gold gain (but still gets gold/5 from runes).
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.
    /// </remarks>
    /// <param name="Unit">Sets state of this unit.</param>
    /// <param name="Disabled">If true, ambient gold gain is disabled.</param>
    public extern static bool SetStateDisableAmbientGold(AttackableUnit Unit = , bool Disabled = false);
    /// <summary>
    /// Sets unit level cap.  Level cap 0 means no cap.  Otherwise unit will earn experience up to one XP less than the level cap.
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.  If unit is already higher than the cap, it will earn 0 XP.
    /// </remarks>
    /// <param name="Unit">Sets level cap of this unit.</param>
    /// <param name="LevelCap">If 0, no level cap; otherwise unit cannot get higher than this level.</param>
    public extern static bool SetUnitLevelCap(AttackableUnit Unit = , int LevelCap = 0);
    /// <summary>
    /// Locks all player cameras to their champions.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Lock">If true, locks all player cameras to their champions.  If false, unlocks all player cameras from their champions.</param>
    public extern static bool LockAllPlayerCameras(bool Lock = true);
    /// <summary>
    /// Test to see if Player has camera locking enabled (camera locked to hero).
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.  This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Player">Player to test.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the the player has camera locking enabled; if False, returns SUCCESS if player does not have camera locking enabled, or if the player does not exist.</param>
    public extern static bool TestPlayerCameraLocked(AttackableUnit Player = , bool ReturnSuccessIf = true);
    /// <summary>
    /// A subtree. collapse
    /// </summary>
    /// <remarks>
    /// Subtree
    /// </remarks>
    /// <param name="TreeName"> can not be empty </param>
    public extern static bool SubTree(string TreeName = "");
    /// <summary>
    /// A Procedure call
    /// </summary>
    /// <remarks>
    /// Procedure
    /// </remarks>
    /// <param name="PocedureName"> can not be empty </param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ChatMessage">Chat string</param>
    /// <param name="Output1">Destination reference contains float value.</param>
    /// <param name="Output2">Destination reference contains UnitType value.</param>
    public extern static bool Procedure2To2(out string Output1, out UnitType Output2, string PocedureName = "", AttackableUnit Unit = , string ChatMessage = "Bot Talking Here");
    /// <summary>
    /// Test if game started
    /// </summary>
    /// <remarks>
    /// Tests if game started. True if game started. False if not
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>
    public extern static bool TestGameStarted(bool ReturnSuccessIf = true);
    /// <summary>
    /// Tests if the specified unit is under attack
    /// </summary>
    /// <remarks>
    /// Tests if the specified unit is under attack. May gather enemies of given unit to figure out if under attack
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is under attack; if False, returns SUCCESS if unit is not under attack</param>
    public extern static bool TestUnitUnderAttack(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns the type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference contains the type of the unit.</param>
    public extern static bool GetUnitType(out UnitType Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the creature type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit creature type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference contains the creature type of the unit.</param>
    public extern static bool GetUnitCreatureType(out CreatureType Output, AttackableUnit Unit = );
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
    public extern static bool TestCanCastSpell(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Cast specified Spell
    /// </summary>
    /// <remarks>
    /// Spell cast
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool CastUnitSpell(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
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
    public extern static bool SetUnitSpellIgnoreVisibity(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = , bool IgnoreVisibility = false);
    /// <summary>
    /// Set specified Spell target position
    /// </summary>
    /// <remarks>
    /// Set specified Spell target position
    /// </remarks>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool SetUnitAISpellTargetLocation(Vector3 TargetLocation = 0;0;0;, int SlotIndex = );
    /// <summary>
    /// Set specified Spell target
    /// </summary>
    /// <remarks>
    /// Set specified Spell target
    /// </remarks>
    /// <param name="TargetUnit">Target Input.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool SetUnitAISpellTarget(AttackableUnit TargetUnit = , int SlotIndex = );
    /// <summary>
    /// Clears specified Spell target
    /// </summary>
    /// <remarks>
    /// Clears specified Spell target
    /// </remarks>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool ClearUnitAISpellTarget(int SlotIndex = );
    /// <summary>
    /// Test validity of specified Spell target
    /// </summary>
    /// <remarks>
    /// Test validity of specified Spell target
    /// </remarks>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit spell target is valid</param>
    public extern static bool TestUnitAISpellTargetValid(int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Output">Destination reference contains cooldown</param>
    public extern static bool GetSpellSlotCooldown(out float Output, AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
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
    public extern static bool SetSpellSlotCooldown(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = , float Cooldown = 1);
    /// <summary>
    /// Returns the PAR type for specified unit
    /// </summary>
    /// <remarks>
    /// PAR Type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference.</param>
    public extern static bool GetUnitPARType(out PrimaryAbilityResourceType Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the cost for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cost
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitSpellCost(out float Output, AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Returns the cast range for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cast range
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitSpellCastRange(out float Output, AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Returns the level for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell level
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitSpellLevel(out int Output, AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Levels up a specified spell
    /// </summary>
    /// <remarks>
    /// Levels up a specified spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool LevelUpUnitSpell(AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Tests if the specified unit can level up the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can level up spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use spell</param>
    public extern static bool TestUnitCanLevelUpSpell(AttackableUnit Unit = , int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Gets a handle to the the unit running the behavior tree in OutputRef
    /// </summary>
    /// <remarks>
    /// Gets a handle to the the unit running the behavior tree
    /// </remarks>
    /// <param name="Output">Destination reference; holds a AI object handle</param>
    public extern static bool GetUnitAISelf(out AttackableUnit Output);
    /// <summary>
    /// Unit run logic for first time
    /// </summary>
    /// <remarks>
    /// Unit run logic for first time
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if first time</param>
    public extern static bool TestUnitAIFirstTime(bool ReturnSuccessIf = true);
    /// <summary>
    /// Sets unit to assist
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target unit</param>
    public extern static bool SetUnitAIAssistTarget(AttackableUnit TargetUnit = );
    /// <summary>
    /// Sets unit to target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Source Reference</param>
    public extern static bool SetUnitAIAttackTarget(AttackableUnit TargetUnit = );
    /// <summary>
    /// Gets unit being assisted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitAIAssistTarget(out AttackableUnit Output);
    /// <summary>
    /// Gets unit being targeted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitAIAttackTarget(out AttackableUnit Output);
    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    public extern static bool IssueMoveOrder();
    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="TargetUnit">Target Unit.</param>
    public extern static bool IssueMoveToUnitOrder(AttackableUnit TargetUnit = );
    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="Location">Position to move to</param>
    public extern static bool IssueMoveToPositionOrder(Vector3 Location = 0;0;0);
    /// <summary>
    /// Issue Chase Order
    /// </summary>
    /// <remarks>
    /// Chase
    /// </remarks>
    public extern static bool IssueChaseOrder();
    /// <summary>
    /// Issue Attack Order
    /// </summary>
    /// <remarks>
    /// Attack
    /// </remarks>
    public extern static bool IssueAttackOrder();
    /// <summary>
    /// Issue Wander order
    /// </summary>
    /// <remarks>
    /// Wander
    /// </remarks>
    public extern static bool IssueWanderOrder();
    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="EmoteIndex">Emote ID</param>
    public extern static bool IssueAIEmoteOrder(UnsignedInt EmoteIndex = 0);
    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="EmoteIndex">Emote ID</param>
    public extern static bool IssueGloabalEmoteOrder(AttackableUnit Unit = , UnsignedInt EmoteIndex = 0);
    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    public extern static bool IssueAIChatOrder(string ChatMessage = "Bot Chat", string ChatRcvr = "/all");
    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    public extern static bool IssueImmediateChatOrder(string ChatMessage = "Bot Chat", string ChatRcvr = "/all");
    /// <summary>
    /// Issue disable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    public extern static bool IssueAIDisableTaskOrder();
    /// <summary>
    /// Issue enable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    public extern static bool IssueAIEnableTaskOrder();
    /// <summary>
    /// Clear AI Attack target
    /// </summary>
    /// <remarks>
    /// Clear AI attack target
    /// </remarks>
    public extern static bool ClearUnitAIAttackTarget();
    /// <summary>
    /// Clear AI assist target
    /// </summary>
    /// <remarks>
    /// Clear AI assist target
    /// </remarks>
    public extern static bool ClearUnitAIAssistTarget();
    /// <summary>
    /// Teleport To base
    /// </summary>
    /// <remarks>
    /// Used for Teleporting home
    /// </remarks>
    public extern static bool IssueTeleportToBaseOrder();
    /// <summary>
    /// Returns the number of discrete attackers.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains collection of attacking units.</param>
    public extern static bool GetUnitAIAttackers(out AttackableUnitCollection Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for PAR References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualPARType(PrimaryAbilityResourceType LeftHandSide = PAR_MANA, PrimaryAbilityResourceType RightHandSide = PAR_MANA);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for PAR References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualPARType(PrimaryAbilityResourceType LeftHandSide = PAR_MANA, PrimaryAbilityResourceType RightHandSide = PAR_MANA);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit type References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualUnitType(UnitType LeftHandSide = UNKNOWN_UNIT, UnitType RightHandSide = UNKNOWN_UNIT);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit type References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualUnitType(UnitType LeftHandSide = UNKNOWN_UNIT, UnitType RightHandSide = UNKNOWN_UNIT);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualCreatureType(CreatureType LeftHandSide = UNKNOWN_CREATURE, CreatureType RightHandSide = UNKNOWN_CREATURE);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualCreatureType(CreatureType LeftHandSide = UNKNOWN_CREATURE, CreatureType RightHandSide = UNKNOWN_CREATURE);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualSpellbookType(SpellbookTypeEnum LeftHandSide = SPELLBOOK_UNKNOWN, SpellbookTypeEnum RightHandSide = SPELLBOOK_UNKNOWN);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Creature References
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualSpellbookType(SpellbookTypeEnum LeftHandSide = SPELLBOOK_UNKNOWN, SpellbookTypeEnum RightHandSide = SPELLBOOK_UNKNOWN);
    /// <summary>
    /// Unit can buy next recommended item
    /// </summary>
    /// <remarks>
    /// Unit can buy next recommended item
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit at spell location</param>
    public extern static bool TestUnitAICanBuyRecommendedItem(bool ReturnSuccessIf = true);
    /// <summary>
    /// Buy next recommended item
    /// </summary>
    /// <remarks>
    /// Buy next recommended item
    /// </remarks>
    public extern static bool UnitAIBuyRecommendedItem();
    /// <summary>
    /// Unit can buy item
    /// </summary>
    /// <remarks>
    /// Unit can buy  item
    /// </remarks>
    /// <param name="ItemID">Item to buy.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can buy item</param>
    public extern static bool TestUnitAICanBuyItem(UnsignedInt ItemID = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Buy item
    /// </summary>
    /// <remarks>
    /// Buy item
    /// </remarks>
    /// <param name="ItemID">Item to buy.</param>
    public extern static bool UnitAIBuyItem(UnsignedInt ItemID = 0);
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
    public extern static bool ComputeUnitAISpellPosition(AttackableUnit TargetUnit = , AttackableUnit ReferenceUnit = , float Range = , bool UnitSide = true);
    /// <summary>
    /// Retrieves a position for spell cast
    /// </summary>
    /// <remarks>
    /// Retrieves a position for spell cast
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitAISpellPosition(out Vector3 Output);
    /// <summary>
    /// Clears position for spell cast
    /// </summary>
    /// <remarks>
    /// Clears position for spell cast
    /// </remarks>
    public extern static bool ClearUnitAISpellPosition();
    /// <summary>
    /// Unit precomputed cast location valid 
    /// </summary>
    /// <remarks>
    /// Unit precomputed cast location valid
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit cast location is valid</param>
    public extern static bool TestUnitAISpellPositionValid(bool ReturnSuccessIf = true);
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
    public extern static bool TestUnitAtLocation(AttackableUnit Unit = , Vector3 Location = 0;0;0, float Error = 200, bool ReturnSuccessIf = true);
    /// <summary>
    /// Unit in safe range
    /// </summary>
    /// <remarks>
    /// Unit in safe Range
    /// </remarks>
    /// <param name="Range">Unit in safe Range</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use spell</param>
    public extern static bool TestUnitAIIsInSafeRange(float Range = 600, bool ReturnSuccessIf = true);
    /// <summary>
    /// Computes a safe position for AI unit
    /// </summary>
    /// <remarks>
    /// Computes a safe position for AI unit
    /// </remarks>
    /// <param name="Range">safe range</param>
    /// <param name="UseDefender">If True, use defenders in search</param>
    /// <param name="UseEnemy">If True, use enemies to guide in search</param>
    public extern static bool ComputeUnitAISafePosition(float Range = , bool UseDefender = true, bool UseEnemy = true);
    /// <summary>
    /// Retrieves a safe position for AI unit
    /// </summary>
    /// <remarks>
    /// Retrieves a safe position for AI unit
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitAISafePosition(out Vector3 Output);
    /// <summary>
    /// Clears position for safe
    /// </summary>
    /// <remarks>
    /// Clears position for safe
    /// </remarks>
    public extern static bool ClearUnitAISafePosition();
    /// <summary>
    /// Unit precomputed safe location valid 
    /// </summary>
    /// <remarks>
    /// Unit precomputed safelocation valid
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit cast location is valid</param>
    public extern static bool TestUnitAISafePositionValid(bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns the base location of a given unit
    /// </summary>
    /// <remarks>
    /// Return SUCCES if we can find the base
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference;.</param>
    public extern static bool GetUnitAIBasePosition(out Vector3 Output, AttackableUnit Unit = );
    /// <summary>
    /// Returns the radius AOE of spell in a given slot
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Output">Destination reference;.</param>
    public extern static bool GetUnitSpellRadius(out float Output, AttackableUnit Unit = , SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex = );
    /// <summary>
    /// Returns distance between 2 units
    /// </summary>
    /// <remarks>
    /// takes into account their BB
    /// </remarks>
    /// <param name="SourceUnit">Source unit</param>
    /// <param name="DestinationUnit">Destination unit</param>
    /// <param name="Output">Destination reference;.</param>
    public extern static bool GetDistanceBetweenUnits(out float Output, AttackableUnit SourceUnit = , AttackableUnit DestinationUnit = );
    /// <summary>
    /// Unit target is in range
    /// </summary>
    /// <remarks>
    /// Unit target is in range
    /// </remarks>
    /// <param name="Error">Accepted error for unit location</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has target in range</param>
    public extern static bool TestUnitAIAttackTargetInRange(float Error = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Unit has valid target
    /// </summary>
    /// <remarks>
    /// Unit has valid target, use before getting attack target.
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has valid target</param>
    public extern static bool TestUnitAIAttackTargetValid(bool ReturnSuccessIf = true);
    /// <summary>
    /// Unit can see target
    /// </summary>
    /// <remarks>
    /// Unit can see target
    /// </remarks>
    /// <param name="Unit">Viewer Unit</param>
    /// <param name="TargetUnit">Target  Unit</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can see target</param>
    public extern static bool TestUnitIsVisible(AttackableUnit Unit = , AttackableUnit TargetUnit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Sets item target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target</param>
    /// <param name="ItemID">Item ID</param>
    public extern static bool SetUnitAIItemTarget(AttackableUnit TargetUnit = , int ItemID = 0);
    /// <summary>
    /// Clears item target
    /// </summary>
    /// <remarks>
    /// Clears item target
    /// </remarks>
    public extern static bool ClearUnitAIItemTarget();
    /// <summary>
    /// Unit can use item
    /// </summary>
    /// <remarks>
    /// Unit can use item
    /// </remarks>
    /// <param name="ItemID">Item ID</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use item</param>
    public extern static bool TestUnitAICanUseItem(int ItemID = 0, bool ReturnSuccessIf = true);
    /// <summary>
    /// Issue Use item Order
    /// </summary>
    /// <remarks>
    /// Use item
    /// </remarks>
    /// <param name="ItemID">Item ID</param>
    public extern static bool IssueUseItemOrder(int ItemID = 0);
    /// <summary>
    /// Tests if specified slot has spell toggled ON
    /// </summary>
    /// <remarks>
    /// Tests if specified slot has spell toggled ON
    /// </remarks>
    /// <param name="Unit">Unit to poll</param>
    /// <param name="SlotIndex">spell slot ID</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the spell is toggled ON</param>
    public extern static bool TestUnitSpellToggledOn(AttackableUnit Unit = , int SlotIndex = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Tests if unit is channeling
    /// </summary>
    /// <remarks>
    /// Tests if unit is channeling
    /// </remarks>
    /// <param name="Unit">Unit to poll</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if unit is channeling</param>
    public extern static bool TestUnitIsChanneling(AttackableUnit Unit = , bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns unit that casted a buff on input unit
    /// </summary>
    /// <remarks>
    /// Returns unit that casted a buff on input unit
    /// </remarks>
    /// <param name="Unit">Source unit</param>
    /// <param name="BuffName">Buff name</param>
    /// <param name="Output">Destination reference;.</param>
    public extern static bool GetUnitBuffCaster(out AttackableUnit Output, AttackableUnit Unit = , string BuffName = "");
    /// <summary>
    /// AI Unit has an assigned task
    /// </summary>
    /// <remarks>
    /// AI Unit has an assigned task
    /// </remarks>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is assigned a task</param>
    public extern static bool TestUnitAIHasTask(bool ReturnSuccessIf = true);
    /// <summary>
    /// Returns position computed by a task assigned to the unit
    /// </summary>
    /// <remarks>
    /// Returns position computed by a task assigned to the unit
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    public extern static bool GetUnitAITaskPosition(out Vector3 Output);
    /// <summary>
    /// Permanently modifies a target unit's armor.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatArmorMod(AttackableUnit Unit = , float Delta = );
    /// <summary>
    /// Permanently modifies a target unit's magic resistance.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatMagicResistanceMod(AttackableUnit Unit = , float Delta = );
    /// <summary>
    /// Permanently modifies a target unit's max health.  This will heal the target.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.  Further, this later needs to be converted to a non-healing implementation; it is using the healing approach until Kuo fixes a bug.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatMaxHealthMod(AttackableUnit Unit = , float Delta = );
    /// <summary>
    /// Permanently modifies a target unit's attack damage.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatAttackDamageMod(AttackableUnit Unit = , float Delta = );
    
}
class UnnamedBehaviourTree
{
    bool GarenBehavior()
    {
        return
        (
            GarenInit() &&
            (
                GarenAtBaseHealAndBuy()
                ||
                GarenLevelUp()
                ||
                GarenGameNotStarted()
                ||
                ReduceDamageTaken()
                ||
                GarenHighThreatManagement()
                ||
                GarenReturnToBase()
                ||
                GarenKillChampion()
                ||
                GarenLowThreatManagement()
                ||
                GarenHeal()
                ||
                GarenAttack()
                ||
                GarenPushLane()
            )
        );
    }
    
    bool GarenStrengthEvaluator()
    {
        return
        (
            SetVarFloat(Input: "1", Output: out Global.TotalUnitStrength) &&
            IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Unit)
        );
    }
    
    bool GarenFindClosestTarget()
    {
        return
        (
            SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
            IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Attacker)
        );
    }
    
    bool GarenDeaggroChecker()
    {
        return
        (true, (
            SetVarBool(Input: "false", Output: out Global.LostAggro) &&
            DistanceBetweenObjectAndPoint(Unit: Global.AggroTarget, Point: Global.AggroPosition, Output: out Tree.Distance) &&
            GreaterFloat(LeftHandSide: Tree.Distance, RightHandSide: "800") &&
            LessFloat(LeftHandSide: Tree.Distance, RightHandSide: "1200") &&
            SetVarBool(Input: "true", Output: out Global.LostAggro)
        ));
    }
    
    bool GarenInit()
    {
        return
        (
            GetUnitAISelf(Output: out Global.Self) &&
            GetUnitPosition(Unit: Global.Self, Output: out Global.SelfPosition) &&
            SetVarFloat(Input: "1200", Output: out Global.DeaggroDistance) &&
            (
                (
                    TestUnitAIFirstTime(ReturnSuccessIf: "true") &&
                    SetVarFloat(Input: "0", Output: out Global.AccumulatedDamage) &&
                    GetUnitCurrentHealth(Unit: Global.Self, Output: out Global.PrevHealth) &&
                    GetGameTime(Output: out Global.PrevTime) &&
                    SetVarBool(Input: "false", Output: out Global.LostAggro) &&
                    SetVarFloat(Input: "1", Output: out Global.StrengthRatioOverTime) &&
                    SetVarBool(Input: "false", Output: out Global.AggressiveKillMode) &&
                    SetVarBool(Input: "false", Output: out Global.LowThreatMode) &&
                    SetVarInt(Input: "4", Output: out Global.PotionsToBuy) &&
                    SetVarBool(Input: "false", Output: out Global.TeleportHome)
                )
                ||
                (
                    (true, (
                        GetGameTime(Output: out Tree.CurrentTime) &&
                        SubtractFloat(LeftHandSide: Tree.CurrentTime, RightHandSide: Global.PrevTime, Output: out Tree.TimeDiff) &&
                        (
                            GreaterFloat(LeftHandSide: Tree.TimeDiff, RightHandSide: "1")
                            ||
                            LessFloat(LeftHandSide: Tree.TimeDiff, RightHandSide: "0")
                        ) &&
                        (
                            MultiplyFloat(LeftHandSide: Global.AccumulatedDamage, RightHandSide: "0.8", Output: out Global.AccumulatedDamage) &&
                            MultiplyFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: "0.8", Output: out Global.StrengthRatioOverTime)
                        ) &&
                        (
                            GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "1000", SpellFlags: "AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                            GarenStrengthEvaluator() &&
                            SetVarFloat(Input: Global.TotalUnitStrength, Output: out Tree.EnemyStrength) &&
                            GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectFriends,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                            GarenStrengthEvaluator() &&
                            SetVarFloat(Input: Global.TotalUnitStrength, Output: out Tree.FriendStrength) &&
                            DivideFloat(LeftHandSide: Tree.EnemyStrength, RightHandSide: Tree.FriendStrength, Output: out Tree.StrRatio) &&
                            AddFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: Tree.StrRatio, Output: out Global.StrengthRatioOverTime) &&
                            GetUnitAIAttackers(Unit: Global.Self, Output: out Global.TargetCollection) &&
                            (true, IterateUntilSuccessDecorator(Collection: Global.TargetCollection, Output: out Tree.Unit))
                        ) &&
                        GetGameTime(Output: out Global.PrevTime)
                    )) &&
                    (true, (
                        GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.CurrentHealth) &&
                        SubtractFloat(LeftHandSide: Global.PrevHealth, RightHandSide: Tree.CurrentHealth, Output: out Tree.NewDamage) &&
                        GreaterFloat(LeftHandSide: Tree.NewDamage, RightHandSide: "0") &&
                        AddFloat(LeftHandSide: Global.AccumulatedDamage, RightHandSide: Tree.NewDamage, Output: out Global.AccumulatedDamage)
                    )) &&
                    GetUnitCurrentHealth(Unit: Global.Self, Output: out Global.PrevHealth)
                )
            )
        );
    }
    
    bool GarenAtBaseHealAndBuy()
    {
        return
        (
            GetUnitAIBasePosition(Unit: Global.Self, Output: out Tree.BaseLocation) &&
            DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.BaseLocation, Output: out Tree.Distance) &&
            LessEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: "450") &&
            SetVarBool(Input: "false", Output: out Global.TeleportHome) &&
            (
                (
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Start ----- Heal -----") &&
                    GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.CurrentHealth) &&
                    DivideFloat(LeftHandSide: Tree.CurrentHealth, RightHandSide: Tree.MaxHealth, Output: out Tree.Health_Ratio) &&
                    LessFloat(LeftHandSide: Tree.Health_Ratio, RightHandSide: "0.95") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Success ----- Heal -----")
                )
                ||
                (
                    (
                        TestChampionHasItem(Unit: Global.Self, ItemID: "1054", ReturnSuccessIf: "false") &&
                        TestUnitAICanBuyItem(ItemID: "1054", ReturnSuccessIf: "true") &&
                        UnitAIBuyItem(ItemID: "1054")
                    )
                    ||
                    (
                        TestChampionHasItem(Unit: Global.Self, ItemID: "1001", ReturnSuccessIf: "false") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3009", ReturnSuccessIf: "false") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3117", ReturnSuccessIf: "false") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3020", ReturnSuccessIf: "false") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3006", ReturnSuccessIf: "false") &&
                        TestUnitAICanBuyItem(ItemID: "3111", ReturnSuccessIf: "true") &&
                        UnitAIBuyItem(ItemID: "1001")
                    )
                    ||
                    (
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3105", ReturnSuccessIf: "false") &&
                        (
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1028", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1028", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1028")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1029", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1029", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1029")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1033", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1033", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1033")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3105", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "3105", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "3105")
                            )
                        )
                    )
                    ||
                    (
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3105", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "1001", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3009", ReturnSuccessIf: "false") &&
                        TestUnitAICanBuyItem(ItemID: "3009", ReturnSuccessIf: "true") &&
                        UnitAIBuyItem(ItemID: "3009")
                    )
                    ||
                    (
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3105", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3009", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3068", ReturnSuccessIf: "false") &&
                        (
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1011", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1011", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1011")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1031", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1031", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1031")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3068", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "3068", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "3068")
                            )
                        )
                    )
                    ||
                    (
                        GetUnitGold(Unit: Global.Self, Output: out Tree.temp) &&
                        GreaterFloat(LeftHandSide: Tree.temp, RightHandSide: "0") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3105", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3009", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3068", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3026", ReturnSuccessIf: "false") &&
                        (
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1029", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1029", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1029")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1033", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1033", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1033")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1031", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1031", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1031")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3026", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "3026", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "3026")
                            )
                        )
                    )
                    ||
                    (
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3105", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3009", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3068", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3026", ReturnSuccessIf: "true") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "3142", ReturnSuccessIf: "false") &&
                        (
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "1036", ReturnSuccessIf: "false") &&
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3134", ReturnSuccessIf: "false") &&
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3142", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "1036", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "1036")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3134", ReturnSuccessIf: "false") &&
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3142", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "3134", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "3134")
                            )
                            ||
                            (
                                TestChampionHasItem(Unit: Global.Self, ItemID: "3142", ReturnSuccessIf: "false") &&
                                TestUnitAICanBuyItem(ItemID: "3142", ReturnSuccessIf: "true") &&
                                UnitAIBuyItem(ItemID: "3142")
                            )
                        )
                    )
                    ||
                    (
                        GreaterInt(LeftHandSide: Global.PotionsToBuy, RightHandSide: "0") &&
                        TestChampionHasItem(Unit: Global.Self, ItemID: "2003", ReturnSuccessIf: "false") &&
                        TestUnitAICanBuyItem(ItemID: "2003", ReturnSuccessIf: "true") &&
                        UnitAIBuyItem(ItemID: "2003") &&
                        SubtractInt(LeftHandSide: Global.PotionsToBuy, RightHandSide: "1", Output: out Global.PotionsToBuy)
                    )
                )
            ) &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ At Base Heal & Buy +++")
        );
    }
    
    bool GarenLevelUp()
    {
        return
        (
            GetUnitSkillPoints(Unit: Global.Self, Output: out Tree.SkillPoints) &&
            GreaterInt(LeftHandSide: Tree.SkillPoints, RightHandSide: "0") &&
            GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "0", Output: out Tree.Ability0Level) &&
            GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "1", Output: out Tree.Ability1Level) &&
            GetUnitSpellLevel(Unit: Global.Self, Spellbook: "SPELLBOOK_UNKNOWN", SlotIndex: "2", Output: out Tree.Ability2Level) &&
            (
                (
                    TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "3", ReturnSuccessIf: "true") &&
                    LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 3")
                )
                ||
                (
                    TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "1", ReturnSuccessIf: "true") &&
                    (
                        (
                            GreaterEqualInt(LeftHandSide: Tree.Ability0Level, RightHandSide: "1") &&
                            GreaterEqualInt(LeftHandSide: Tree.Ability2Level, RightHandSide: "1") &&
                            LessEqualInt(LeftHandSide: Tree.Ability1Level, RightHandSide: "0")
                        )
                        ||
                        (
                            GreaterEqualInt(LeftHandSide: Tree.Ability0Level, RightHandSide: "3") &&
                            GreaterEqualInt(LeftHandSide: Tree.Ability2Level, RightHandSide: "3") &&
                            LessEqualInt(LeftHandSide: Tree.Ability1Level, RightHandSide: "1")
                        )
                    ) &&
                    LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                )
                ||
                (
                    (
                        TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "2", ReturnSuccessIf: "true") &&
                        LessEqualInt(LeftHandSide: Tree.Ability2Level, RightHandSide: Tree.Ability0Level) &&
                        LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2") &&
                        DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                    )
                    ||
                    (
                        TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "0", ReturnSuccessIf: "true") &&
                        LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0") &&
                        DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                    )
                )
                ||
                (
                    TestUnitCanLevelUpSpell(Unit: Global.Self, SlotIndex: "1", ReturnSuccessIf: "true") &&
                    LevelUpUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "levelup 0")
                )
            ) &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Level up ++++")
        );
    }
    
    bool GarenGameNotStarted()
    {
        return
        (
            TestGameStarted(ReturnSuccessIf: "false") &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Game Not Started +++")
        );
    }
    
    bool GarenAttack()
    {
        return
        (
            GarenAcquireTarget() &&
            GarenAttackTarget() &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Attack ++++")
        );
    }
    
    bool GarenAcquireTarget()
    {
        return
        (
            (
                SetVarBool(Input: "false", Output: out Global.LostAggro) &&
                TestUnitAIAttackTargetValid(ReturnSuccessIf: "true") &&
                GetUnitAIAttackTarget(Output: out Global.AggroTarget) &&
                SetVarVector(Input: Global.AssistPosition, Output: out Global.AggroPosition) &&
                TestUnitIsVisible(Unit: Global.Self, TargetUnit: Global.AggroTarget, ReturnSuccessIf: "true") &&
                GarenDeaggroChecker() &&
                EqualBool(LeftHandSide: Global.LostAggro, RightHandSide: "false") &&
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Use Previous Target +++")
            )
            ||
            (
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "EnableOrDisableAllyAggro") &&
                SetVarFloat(Input: "800", Output: out Global.CurrentClosestDistance) &&
                GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "800", SpellFlags: "AffectFriends,AffectHeroes,AlwaysSelf", Output: out Tree.FriendlyUnits) &&
                SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
                IterateOverAllDecorator(Collection: Tree.FriendlyUnits, Output: out Tree.unit) &&
                EqualBool(LeftHandSide: Global.ValueChanged, RightHandSide: "true") &&
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Acquired Ally under attack +++")
            )
            ||
            (
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(Input: "800", Output: out Global.CurrentClosestDistance) &&
                GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectBuildings,AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                (
                    GetCollectionCount(Collection: Global.TargetCollection, Output: out Tree.Count) &&
                    GreaterInt(LeftHandSide: Tree.Count, RightHandSide: "0") &&
                    SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
                    IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit)
                ) &&
                EqualBool(LeftHandSide: Global.ValueChanged, RightHandSide: "true") &&
                SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                SetVarVector(Input: Global.SelfPosition, Output: out Global.AssistPosition) &&
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ AcquiredNewTarget +++")
            )
        );
    }
    
    bool GarenAttackTarget()
    {
        return
        (
            GetUnitAIAttackTarget(Output: out Tree.Target) &&
            GetUnitTeam(Unit: Global.Self, Output: out Tree.SelfTeam) &&
            GetUnitTeam(Unit: Tree.Target, Output: out Tree.TargetTeam) &&
            NotEqualUnitTeam(LeftHandSide: Tree.SelfTeam, RightHandSide: Tree.TargetTeam) &&
            (
                (
                    GetUnitType(Unit: Tree.Target, Output: out Tree.UnitType) &&
                    EqualUnitType(LeftHandSide: Tree.UnitType, RightHandSide: "MINION_UNIT") &&
                    GetUnitCurrentHealth(Unit: Tree.Target, Output: out Tree.currentHealth) &&
                    GetUnitMaxHealth(Unit: Tree.Target, Output: out Tree.MaxHealth) &&
                    DivideFloat(LeftHandSide: Tree.currentHealth, RightHandSide: Tree.MaxHealth, Output: out Tree.HP_Ratio) &&
                    LessFloat(LeftHandSide: Tree.HP_Ratio, RightHandSide: "0.2") &&
                    (
                        (
                            GreaterFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: "2") &&
                            GarenCanCastAbility2() &&
                            SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                            GarenCastAbility2()
                        )
                        ||
                        (
                            GarenCanCastAbility0() &&
                            SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                            CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                        )
                    )
                )
                ||
                (
                    GetUnitType(Unit: Tree.Target, Output: out Tree.UnitType) &&
                    EqualUnitType(LeftHandSide: Tree.UnitType, RightHandSide: "HERO_UNIT") &&
                    (
                        (
                            GarenCanCastAbility0() &&
                            SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                            CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                        )
                        ||
                        (
                            GarenCanCastAbility2() &&
                            GarenCastAbility2()
                        )
                    )
                )
                ||
                GarenAutoAttackTarget()
            ) &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++ Attack Success ++")
        );
    }
    
    bool GarenReturnToBase()
    {
        return
        (
            GetUnitAIBasePosition(Unit: Global.Self, Output: out Tree.BaseLocation) &&
            DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.BaseLocation, Output: out Tree.Distance) &&
            GreaterFloat(LeftHandSide: Tree.Distance, RightHandSide: "300") &&
            (
                (
                    GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.Health) &&
                    DivideFloat(LeftHandSide: Tree.Health, RightHandSide: Tree.MaxHealth, Output: out Tree.Health_Ratio) &&
                    (
                        (
                            EqualBool(LeftHandSide: Global.TeleportHome, RightHandSide: "true") &&
                            LessEqualFloat(LeftHandSide: Tree.Health_Ratio, RightHandSide: "0.35")
                        )
                        ||
                        (
                            EqualBool(LeftHandSide: Global.TeleportHome, RightHandSide: "false") &&
                            LessEqualFloat(LeftHandSide: Tree.Health_Ratio, RightHandSide: "0.25") &&
                            SetVarBool(Input: "true", Output: out Global.TeleportHome)
                        )
                    )
                )
                ||
                (
                    DebugAction(RunningLimit: "0", Result: "FAILURE", String: "EmptyNode: HighGold")
                )
            ) &&
            (
                (
                    SetVarFloat(Input: "30000", Output: out Global.CurrentClosestDistance) &&
                    GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "30000", SpellFlags: "AffectFriends,AffectTurrets", Output: out Global.TargetCollection) &&
                    GarenFindClosestTarget() &&
                    EqualBool(LeftHandSide: Global.ValueChanged, RightHandSide: "true") &&
                    (
                        (
                            GetDistanceBetweenUnits(SourceUnit: Global.CurrentClosestTarget, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                            LessFloat(LeftHandSide: Tree.Distance, RightHandSide: "125") &&
                            (
                                (
                                    TestUnitAISpellPositionValid(ReturnSuccessIf: "true") &&
                                    GetUnitAISpellPosition(Output: out Tree.TeleportPosition) &&
                                    DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.TeleportPosition, Output: out Tree.DistanceToTeleportPosition) &&
                                    LessFloat(LeftHandSide: Tree.DistanceToTeleportPosition, RightHandSide: "50")
                                )
                                ||
                                TestUnitAISpellPositionValid(ReturnSuccessIf: "false")
                            ) &&
                            IssueTeleportToBaseOrder() &&
                            ClearUnitAISpellPosition() &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Yo")
                        )
                        ||
                        (
                            (
                                (
                                    TestUnitAISpellPositionValid(ReturnSuccessIf: "false") &&
                                    ComputeUnitAISpellPosition(TargetUnit: Global.CurrentClosestTarget, ReferenceUnit: Global.Self, Range: "150", UnitSide: "false")
                                )
                                ||
                                TestUnitAISpellPositionValid(ReturnSuccessIf: "true")
                            ) &&
                            GetUnitAISpellPosition(Output: out Tree.TeleportPosition) &&
                            IssueMoveToPositionOrder(Location: Tree.TeleportPosition) &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Yo")
                        )
                    )
                )
                ||
                (
                    GetUnitAIBasePosition(Unit: Global.Self, Output: out Tree.BaseLocation) &&
                    IssueMoveToPositionOrder(Location: Tree.BaseLocation)
                )
            ) &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Teleport Home +++")
        );
    }
    
    bool GarenHighThreatManagement()
    {
        return
        (
            (
                (
                    SetVarBool(Input: "false", Output: out Tree.SuperHighThreat) &&
                    TestUnitUnderAttack(Unit: Global.Self, ReturnSuccessIf: "true") &&
                    GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.Health) &&
                    DivideFloat(LeftHandSide: Tree.Health, RightHandSide: Tree.MaxHealth, Output: out Tree.Health_Ratio) &&
                    LessEqualFloat(LeftHandSide: Tree.Health_Ratio, RightHandSide: "0.25") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ LowHealthUnderAttack +++") &&
                    SetVarBool(Input: "true", Output: out Tree.SuperHighThreat)
                )
                ||
                (
                    GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    DivideFloat(LeftHandSide: Global.AccumulatedDamage, RightHandSide: Tree.MaxHealth, Output: out Tree.Damage_Ratio) &&
                    (
                        (
                            EqualBool(LeftHandSide: Global.AggressiveKillMode, RightHandSide: "true") &&
                            GreaterFloat(LeftHandSide: Tree.Damage_Ratio, RightHandSide: "0.15")
                        )
                        ||
                        (
                            EqualBool(LeftHandSide: Global.AggressiveKillMode, RightHandSide: "false") &&
                            GreaterFloat(LeftHandSide: Tree.Damage_Ratio, RightHandSide: "0.02")
                        )
                    ) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ BurstDamage +++")
                )
            ) &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ High Threat +++") &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    EqualBool(LeftHandSide: Tree.SuperHighThreat, RightHandSide: "true") &&
                    GarenCanCastAbility1() &&
                    SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1")
                )
                ||
                (
                    EqualBool(LeftHandSide: Tree.SuperHighThreat, RightHandSide: "true") &&
                    GarenCanCastAbility0() &&
                    SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                )
                ||
                GarenMicroRetreat()
            ) &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ High Threat +++")
        );
    }
    
    bool GarenLowThreatManagement()
    {
        return
        (
            (
                (
                    GreaterFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: "6") &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(Input: "true", Output: out Global.LowThreatMode)
                )
                ||
                (
                    EqualBool(LeftHandSide: Global.LowThreatMode, RightHandSide: "true") &&
                    SetVarBool(Input: "false", Output: out Global.LowThreatMode) &&
                    GreaterFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: "4") &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(Input: "true", Output: out Global.LowThreatMode)
                )
                ||
                (
                    ClearUnitAISafePosition() &&
                    DebugAction(RunningLimit: "0", Result: "FAILURE", String: "DoNotRemoveForcedFail")
                )
            ) &&
            GarenMicroRetreat() &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Low Threat +++")
        );
    }
    
    bool GarenKillChampion()
    {
        return
        (
            SetVarBool(Input: "false", Output: out Global.AggressiveKillMode) &&
            (
                (
                    LessFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: "3") &&
                    GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectEnemies,AffectHeroes", Output: out Global.TargetCollection) &&
                    SetVarFloat(Input: "0.8", Output: out Tree.CurrentLowestHealthRatio) &&
                    SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
                    IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit) &&
                    EqualBool(LeftHandSide: Global.ValueChanged, RightHandSide: "true") &&
                    SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                    SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                    SetVarVector(Input: Global.SelfPosition, Output: out Global.AssistPosition) &&
                    SetVarBool(Input: "false", Output: out Tree.Aggressive) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "PassiveKillChampion")
                )
                ||
                (
                    LessFloat(LeftHandSide: Global.StrengthRatioOverTime, RightHandSide: "5.1") &&
                    GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
                    GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.CurrentHealth) &&
                    DivideFloat(LeftHandSide: Tree.CurrentHealth, RightHandSide: Tree.MaxHealth, Output: out Tree.MyHealthRatio) &&
                    GreaterFloat(LeftHandSide: Tree.MyHealthRatio, RightHandSide: "0.5") &&
                    GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "1000", SpellFlags: "AffectEnemies,AffectHeroes", Output: out Global.TargetCollection) &&
                    SetVarFloat(Input: "0.4", Output: out Tree.CurrentLowestHealthRatio) &&
                    SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
                    IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit) &&
                    EqualBool(LeftHandSide: Global.ValueChanged, RightHandSide: "true") &&
                    SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                    SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                    SetVarVector(Input: Global.SelfPosition, Output: out Global.AssistPosition) &&
                    SetVarBool(Input: "true", Output: out Tree.Aggressive) &&
                    SetVarBool(Input: "true", Output: out Global.AggressiveKillMode) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ AggressiveMode +++")
                )
            ) &&
            (
                (
                    GarenCanCastAbility0() &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                )
                ||
                (
                    EqualBool(LeftHandSide: Tree.Aggressive, RightHandSide: "true") &&
                    GarenCanCastAbility3() &&
                    GarenCastAbility3() &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Use Ultiamte +++")
                )
                ||
                (
                    TestUnitHasBuff(TargetUnit: Global.Self, CasterUnit: Tree., BuffName: "GarenBladestorm", ReturnSuccessIf: "false") &&
                    GarenCanCastAbility2() &&
                    GarenCastAbility2()
                )
                ||
                GarenAutoAttackTarget()
                ||
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Attack Champion+++")
            ) &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++++ Success: Kill  +++")
        );
    }
    
    bool GarenLastHitMinion()
    {
        return
        (
            (
                GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "800", SpellFlags: "AffectEnemies,AffectMinions", Output: out Global.TargetCollection) &&
                SetVarFloat(Input: "0.3", Output: out Tree.CurrentLowestHealthRatio) &&
                SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
                IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.unit) &&
                EqualBool(LeftHandSide: Global.ValueChanged, RightHandSide: "true") &&
                SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                SetVarVector(Input: Global.SelfPosition, Output: out Global.AssistPosition) &&
                SetVarAttackableUnit(Input: Global.CurrentClosestTarget, Output: out Tree.Target)
            ) &&
            GarenAutoAttackTarget() &&
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++++++ Last Hit ++++++++")
        );
    }
    
    bool GarenMicroRetreat()
    {
        return
        (
            (
                TestUnitAISafePositionValid(ReturnSuccessIf: "true") &&
                GetUnitAISafePosition(Output: out Tree.SafePosition) &&
                (
                    (
                        DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Tree.SafePosition, Output: out Tree.Distance) &&
                        LessFloat(LeftHandSide: Tree.Distance, RightHandSide: "50") &&
                        ComputeUnitAISafePosition(Range: "800", UseDefender: "false", UseEnemy: "false") &&
                        DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "------- At location computed new position --------------")
                    )
                    ||
                    (
                        IssueMoveToPositionOrder(Location: Tree.SafePosition) &&
                        DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "------------ Success: Move to safe position ----------")
                    )
                )
            )
            ||
            ComputeUnitAISafePosition(Range: "600", UseDefender: "false", UseEnemy: "false")
        );
    }
    
    bool GarenAutoAttackTarget()
    {
        return
        (
            GetUnitAIAttackTarget(Output: out Tree.Target) &&
            TestUnitAIAttackTargetValid(ReturnSuccessIf: "true") &&
            (
                (
                    GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                    GetUnitAttackRange(Unit: Global.Self, Output: out Tree.AttackRange) &&
                    MultiplyFloat(LeftHandSide: Tree.AttackRange, RightHandSide: "0.9", Output: out Tree.AttackRange) &&
                    LessEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: Tree.AttackRange) &&
                    ClearUnitAIAttackTarget() &&
                    SetUnitAIAttackTarget(TargetUnit: Tree.Target) &&
                    IssueAttackOrder()
                )
                ||
                IssueMoveToUnitOrder(TargetUnit: Tree.Target)
            )
        );
    }
    
    bool GarenCanCastAbility0()
    {
        return
        (
            GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0", Output: out Tree.Cooldown) &&
            LessEqualFloat(LeftHandSide: Tree.Cooldown, RightHandSide: "0") &&
            TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0", ReturnSuccessIf: "true")
        );
    }
    
    bool GarenCanCastAbility1()
    {
        return
        (
            GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1", Output: out Tree.Cooldown) &&
            LessEqualFloat(LeftHandSide: Tree.Cooldown, RightHandSide: "0") &&
            TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1", ReturnSuccessIf: "true")
        );
    }
    
    bool GarenCanCastAbility2()
    {
        return
        (
            GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2", Output: out Tree.Cooldown) &&
            LessEqualFloat(LeftHandSide: Tree.Cooldown, RightHandSide: "0") &&
            TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2", ReturnSuccessIf: "true")
        );
    }
    
    bool GarenCanCastAbility3()
    {
        return
        (
            GetSpellSlotCooldown(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0", Output: out Tree.Cooldown) &&
            LessEqualFloat(LeftHandSide: Tree.Cooldown, RightHandSide: "0") &&
            TestCanCastSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3", ReturnSuccessIf: "true")
        );
    }
    
    bool GarenCastAbility0()
    {
        return
        (
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0", Output: out Tree.Range) &&
            GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                    LessEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: Tree.Range) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 1 Success ----------------")
                )
                ||
                (
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
                )
            )
        );
    }
    
    bool GarenCastAbility1()
    {
        return
        (
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1", Output: out Tree.Range) &&
            GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                    LessEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: Tree.Range) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 1 Success ----------------")
                )
                ||
                (
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
                )
            )
        );
    }
    
    bool GarenCastAbility2()
    {
        return
        (
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    TestUnitHasBuff(TargetUnit: Global.Self, CasterUnit: Tree., BuffName: "GarenBladestorm", ReturnSuccessIf: "true") &&
                    IssueMoveToUnitOrder(TargetUnit: Tree.Target)
                )
                ||
                (
                    GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2", Output: out Tree.Range) &&
                    SetVarFloat(Input: "200", Output: out Tree.Range) &&
                    (
                        (
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 2") &&
                            GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                            LessEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: Tree.Range) &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                            CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "2") &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 2 Success ----------------")
                        )
                        ||
                        (
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                            IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
                        )
                    )
                )
            )
        );
    }
    
    bool GarenCastAbility3()
    {
        return
        (
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "CastSubTree") &&
            GetUnitSpellCastRange(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3", Output: out Tree.Range) &&
            GetUnitAIAttackTarget(Output: out Tree.Target) &&
            (
                (
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(SourceUnit: Tree.Target, DestinationUnit: Global.Self, Output: out Tree.Distance) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "GoingToRangeCheck") &&
                    LessEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: Tree.Range) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Range Check Succses") &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "3") &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Ability 1 Success ----------------")
                )
                ||
                (
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(TargetUnit: Tree.Target) &&
                    DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "Moving To Cast")
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
            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Move To Lane +++")
        );
    }
    
    bool GarenMisc()
    {
        return
        (
            (
                DebugAction(RunningLimit: "0", Result: "FAILURE", String: "??? EnableOrDisablePreviousTarget ???") &&
                TestUnitAIAttackTargetValid(ReturnSuccessIf: "true") &&
                SetVarBool(Input: "false", Output: out Global.LostAggro) &&
                GetUnitAIAttackTarget(Output: out Global.PreviousTarget) &&
                GetUnitTeam(Unit: Global.Self, Output: out Tree.SelfTeam) &&
                GetUnitTeam(Unit: Global.PreviousTarget, Output: out Tree.UnitTeam) &&
                NotEqualUnitTeam(LeftHandSide: Tree.UnitTeam, RightHandSide: Tree.SelfTeam) &&
                GetUnitAIAssistTarget(Output: out Tree.Assist) &&
                (
                    (
                        EqualUnit(LeftHandSide: Tree.Assist, RightHandSide: Global.Self) &&
                        DistanceBetweenObjectAndPoint(Unit: Global.Self, Point: Global.AssistPosition, Output: out Tree.Distance) &&
                        (true, (
                            GreaterEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: Global.DeaggroDistance) &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(Input: "true", Output: out Global.LostAggro) &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ Lost Aggro +++")
                        )) &&
                        LessFloat(LeftHandSide: Tree.Distance, RightHandSide: Global.DeaggroDistance) &&
                        DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ In Aggro Range, Use Previous")
                    )
                    ||
                    (
                        NotEqualUnit(LeftHandSide: Global.Self, RightHandSide: Tree.Assist) &&
                        GetUnitPosition(Unit: Tree.Assist, Output: out Tree.AssistPosition) &&
                        DistanceBetweenObjectAndPoint(Unit: Global.PreviousTarget, Point: Global.SelfPosition, Output: out Tree.Distance) &&
                        (true, (
                            GreaterEqualFloat(LeftHandSide: Tree.Distance, RightHandSide: "1000") &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(Input: "true", Output: out Global.LostAggro) &&
                            DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "------- Losing aggro from assist ----------")
                        )) &&
                        LessFloat(LeftHandSide: Tree.Distance, RightHandSide: "1000") &&
                        DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "============= Use Previous Target: Still close to assist -----------")
                    )
                ) &&
                SetVarBool(Input: "false", Output: out Global.LostAggro) &&
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "++ Use Previous Target ++")
            ) &&
            (
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(Input: "800", Output: out Global.CurrentClosestDistance) &&
                GetUnitsInTargetArea(Unit: Global.Self, TargetLocation: Global.SelfPosition, Radius: "900", SpellFlags: "AffectEnemies,AffectHeroes,AffectMinions,AffectTurrets", Output: out Global.TargetCollection) &&
                (
                    GetCollectionCount(Collection: Global.TargetCollection, Output: out Tree.Count) &&
                    GreaterInt(LeftHandSide: Tree.Count, RightHandSide: "0") &&
                    SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
                    IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Attacker)
                ) &&
                EqualBool(LeftHandSide: Global.ValueChanged, RightHandSide: "true") &&
                SetUnitAIAssistTarget(TargetUnit: Global.Self) &&
                SetUnitAIAttackTarget(TargetUnit: Global.CurrentClosestTarget) &&
                SetVarVector(Input: Global.SelfPosition, Output: out Global.AssistPosition) &&
                DebugAction(RunningLimit: "0", Result: "SUCCESS", String: "+++ AcquiredNewTarget +++")
            )
        );
    }
    
    bool GarenHeal()
    {
        return
        (
            GetUnitCurrentHealth(Unit: Global.Self, Output: out Tree.Health) &&
            GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
            DivideFloat(LeftHandSide: Tree.Health, RightHandSide: Tree.MaxHealth, Output: out Tree.HP_Ratio) &&
            (
                LessFloat(LeftHandSide: Tree.HP_Ratio, RightHandSide: "0.5") &&
                TestUnitAICanUseItem(ItemID: "2003", ReturnSuccessIf: "true") &&
                IssueUseItemOrder(ItemID: "2003")
            )
        );
    }
    
    bool ReduceDamageTaken()
    {
        return
        (
            GetUnitMaxHealth(Unit: Global.Self, Output: out Tree.MaxHealth) &&
            DivideFloat(LeftHandSide: Global.AccumulatedDamage, RightHandSide: Tree.MaxHealth, Output: out Tree.Damage_Ratio) &&
            GreaterEqualFloat(LeftHandSide: Tree.Damage_Ratio, RightHandSide: "0.1") &&
            (
                (
                    GarenCanCastAbility1() &&
                    SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "1")
                )
                ||
                (
                    GarenCanCastAbility0() &&
                    SetUnitAIAttackTarget(TargetUnit: Global.Self) &&
                    CastUnitSpell(Unit: Global.Self, Spellbook: "SPELLBOOK_CHAMPION", SlotIndex: "0")
                )
            )
        );
    }
    
    bool GarenFindClosestVisibleTarget()
    {
        return
        (
            SetVarBool(Input: "false", Output: out Global.ValueChanged) &&
            IterateOverAllDecorator(Collection: Global.TargetCollection, Output: out Tree.Attacker)
        );
    }
}
