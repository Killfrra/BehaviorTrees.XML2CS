using static SBL;
using System.Numerics;
using static AllEnumMembers;
public static class SBL
{
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
    public extern static Task<bool> DebugAction(int RunningLimit = 0, BehaveResult Result = SUCCESS, string String = "Yo");
    /// <summary>
    /// Decorator that masks FAILURE.
    /// </summary>
    /// <remarks>
    /// Running still returns running
    /// </remarks>
    public extern static Task<bool> MaskFailure(Func<Task<bool>>? Child0 = null);
    /// <summary>
    /// Decorator that allows user to specify the number of times the subtree will run.
    /// </summary>
    /// <remarks>
    /// Need comment
    /// </remarks>
    /// <param name="RunningLimit">The number of times the sub tree is to be executed</param>
    public extern static bool LoopNTimes(int RunningLimit = 1, Func<Task<bool>>? Child0 = null);
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will always return SUCCESS.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    public extern static bool IterateOverAllDecorator(out AttackableUnit Output, IEnumerable<AttackableUnit> Collection, Func<Task<bool>>? Child0 = null);
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns FAILURE.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will return SUCCESS if all children return SUCCESS and FAILURE if one child returns FAILURE.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    public extern static bool IterateUntilFailureDecorator(out AttackableUnit Output, IEnumerable<AttackableUnit> Collection, Func<Task<bool>>? Child0 = null);
    /// <summary>
    /// Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns SUCCESS.
    /// </summary>
    /// <remarks>
    /// Right now this only supports AttackableUnit collections.  This will return SUCCESS if a child returns SUCCESS and FAILURE if all children return FAILURE.
    /// </remarks>
    /// <param name="Collection">The collection that the iterator should loop over.</param>
    public extern static bool IterateUntilSuccessDecorator(out AttackableUnit Output, IEnumerable<AttackableUnit> Collection, Func<Task<bool>>? Child0 = null);
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
    public extern static bool SetVarBool(out bool Output, bool Input = true);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static bool SetVarAttackableUnit(out AttackableUnit Output, AttackableUnit Input = True);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static bool SetVarInt(out int Output, int Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static bool SetVarDWORD(out DWORD Output, DWORD Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static bool SetVarString(out string Output, string Input = "DEFAULT STRING");
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static bool SetVarFloat(out float Output, float Input = 0);
    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for Vector References.  If you want to make a vector out of 3 floats, use MakeVector.
    /// </remarks>
    /// <param name="Input">Source Reference</param>
    public extern static bool SetVarVector(out Vector3 Output, Vector3 Input = Vector3.Zero);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit team
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool EqualUnitTeam(TeamID LeftHandSide = TEAM_UNKNOWN, TeamID RightHandSide = TEAM_UNKNOWN);
    /// <summary>
    /// Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not
    /// </summary>
    /// <remarks>
    /// This version is for Unit team
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    public extern static bool NotEqualUnitTeam(TeamID LeftHandSide = TEAM_UNKNOWN, TeamID RightHandSide = TEAM_UNKNOWN);
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
    public extern static bool AddInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool SubtractInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool MultiplyInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool DivideInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool ModulusInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool MinInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool MaxInt(out int Output, int LeftHandSide = 0, int RightHandSide = 0);
    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool AddFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool SubtractFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool MultiplyFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool DivideFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool MinFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.  This will always return SUCCESS. 
    /// </remarks>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    public extern static bool MaxFloat(out float Output, float LeftHandSide = 0, float RightHandSide = 0);
    /// <summary>
    /// Gets a handle to the player and puts it in OutputRef
    /// </summary>
    /// <remarks>
    /// Only works in Tutorial, or other situation where there's only one player.  Works by getting the first player in the roster that has a legal client ID.
    /// </remarks>
    public extern static bool GetTutorialPlayer(out AttackableUnit Output);
    /// <summary>
    /// Returns a handle to a collection containing all champions in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains champions who have disconnected or are played by bots.
    /// </remarks>
    public extern static bool GetChampionCollection(out IEnumerable<Champion> Output);
    /// <summary>
    /// Returns a handle to a collection containing all turrets alive in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains turrets on both teams.
    /// </remarks>
    public extern static bool GetTurretCollection(out IEnumerable<Turret> Output);
    /// <summary>
    /// Gets a handle to the turret in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the turret is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the turrets to be checked.</param>
    /// <param name="Lane">Lane of the turret.  Check the level script for the enum.</param>
    /// <param name="Position">Position of the turret.  Check the level script for the enum.</param>
    public extern static bool GetTurret(out AttackableUnit Turret, TeamID Team = TEAM_ORDER, int Lane = 1, int Position = 1);
    /// <summary>
    /// Gets a handle to the inhibitor in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the inhibitor is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the inhibitor to be checked.</param>
    /// <param name="Lane">Lane of the inhibitor.  Check the level script for the enum.</param>
    public extern static bool GetInhibitor(out AttackableUnit Inhibitor, TeamID Team = TEAM_ORDER, int Lane = 1);
    /// <summary>
    /// Gets a handle to the nexus on a specific teamin a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the Nexus is not alive, should confirm.
    /// </remarks>
    /// <param name="Team">Team of the nexus to return.</param>
    public extern static bool GetNexus(out AttackableUnit Nexus, TeamID Team = TEAM_ORDER);
    /// <summary>
    /// Returns the current position of a specific unit
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitPosition(out Vector3 Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the current elapsed game time.  This will be affected by pausing, cheats, or other things.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    public extern static bool GetGameTime(out float Output);
    /// <summary>
    /// Returns the lane position of a turret.
    /// </summary>
    /// <remarks>
    /// This position is defined in the level script and is map specific.
    /// </remarks>
    /// <param name="Turret">Turret to poll.</param>
    public extern static bool GetTurretPosition(out int Output, AttackableUnit Turret);
    /// <summary>
    /// Returns the max health of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitMaxHealth(out float Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the current health of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitCurrentHealth(out float Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the current Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    public extern static bool GetUnitCurrentPAR(out float Output, AttackableUnit Unit, PrimaryAbilityResourceType PrimaryAbilityResourceType = PAR_MANA);
    /// <summary>
    /// Returns the maximum Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX PAR
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    public extern static bool GetUnitMaxPAR(out float Output, AttackableUnit Unit, PrimaryAbilityResourceType PrimaryAbilityResourceType = PAR_MANA);
    /// <summary>
    /// Returns the current armor of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT armor
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitArmor(out float Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the number of discrete elements contained within the collection.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Collection">Collection to count.</param>
    public extern static bool GetCollectionCount(out int Output, IEnumerable<AttackableUnit> Collection);
    /// <summary>
    /// Returns the current skin of a specific unit
    /// </summary>
    /// <remarks>
    /// Since buildings don't hame skins, it will return the name of the building.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitSkinName(out string Output, AttackableUnit Unit);
    /// <summary>
    /// Turns on or off the highlight of a unit.
    /// </summary>
    /// <remarks>
    /// Creates a unit highlight akin to what is used in the tutorial.  This highlight is by default blue.  Always returns SUCCESS.
    /// </remarks>
    /// <param name="Enable">Should the Highlight be turned on or turned off?</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    public extern static bool ToggleUnitHighlight(bool Enable, AttackableUnit TargetUnit);
    /// <summary>
    /// Pings a unit on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.  Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.  Important for team coloration and chat info.</param>
    /// <param name="TargetUnit">Unit to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    public extern static bool PingMinimapUnit(AttackableUnit PingingUnit, AttackableUnit TargetUnit, bool PlayAudio = true);
    /// <summary>
    /// Pings a location on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.  Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.  Important for team coloration and chat info.</param>
    /// <param name="TargetPosition">Location to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    public extern static bool PingMinimapLocation(AttackableUnit PingingUnit, Vector3 TargetPosition = Vector3.Zero, bool PlayAudio = true);
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
    public extern static bool ActivateQuest(out int QuestId, string String = "EMPTY STRING", AttackableUnit Player, QuestType QuestType = PRIMARY_QUEST, bool HandleRollOver = false, string Tooltip = "");
    /// <summary>
    /// Plays a quest completion animation and then removes it from the HUD
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    public extern static bool CompleteQuest(int QuestId);
    /// <summary>
    /// Removes quest from the HUD immediately
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node; there is no ceremony involved in quest removal
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    public extern static bool RemoveQuest(int QuestId);
    /// <summary>
    /// Test to see if the quest has the mouse rolled over it
    /// </summary>
    /// <remarks>
    /// This quest must have been activated with HandleRollOver=true in ActivateQuest
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    public extern static bool TestQuestRolledOver(int QuestId);
    /// <summary>
    /// Test to see if the quest is being clicked right now with the mouse down over it.
    /// </summary>
    /// <remarks>
    /// Tests to see if the quest is being clicked right now, or if the mouse is not clicking it right now.
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    public extern static bool TestQuestClicked(int QuestId);
    /// <summary>
    /// Create a new Tip and display it in the TipTracker
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipCategory">The localized string for the Tip Category.</param>
    public extern static bool ActivateTip(out int TipId, AttackableUnit Player, string TipName = "EMPTY STRING", string TipCategory = "EMPTY STRING");
    /// <summary>
    /// Removes Tip from the Tip Tracker immediately
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip node; there is no ceremony involved in Tip removal
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip</param>
    public extern static bool RemoveTip(int TipId);
    /// <summary>
    /// Enables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to enable</param>
    public extern static bool EnableTipEvents(AttackableUnit Player);
    /// <summary>
    /// Disables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to disable</param>
    public extern static bool DisableTipEvents(AttackableUnit Player);
    /// <summary>
    /// Tests to see if a Tip in the Tip Tracker or a Tip Dialogue has been clicked by the user
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip and ActivateTipDialogue nodes. Use ReturnSuccessIf to control the output.  This will return as if the Tip has NOT been clicked if the Tip Id is invalid.
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip or ActivateTipDialogue</param>
    public extern static bool TestTipClicked(int TipId);
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
    public extern static bool ActivateTipDialogue(out int TipId, AttackableUnit Player, string TipName = "EMPTY STRING", string TipBody = "EMPTY STRING", string TipImage = "");
    /// <summary>
    /// Enables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to enable</param>
    public extern static bool EnableTipDialogueEvents(AttackableUnit Player);
    /// <summary>
    /// Disables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to disable</param>
    public extern static bool DisableTipDialogueEvents(AttackableUnit Player);
    /// <summary>
    /// Creates a vector from three static components
    /// </summary>
    /// <remarks>
    /// If you want to copy a Vector, use SetVarVector.
    /// </remarks>
    /// <param name="X">X component</param>
    /// <param name="Y">Y component</param>
    /// <param name="Z">Z component</param>
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
    public extern static bool RegisterScoreboardOpened(out bool Output, AttackableUnit Unit);
    /// <summary>
    /// Keeps track of the number of minions (not neutrals) not on the attacker's team killed by an attacker
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Unit">Handle of the attacking unit</param>
    public extern static bool RegisterMinionKillCounter(out int Output, AttackableUnit Unit);
    /// <summary>
    /// Returns an int containing the number of kills the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    public extern static bool GetChampionKills(out int Output, AttackableUnit Unit);
    /// <summary>
    /// Returns an int containing the number of deaths the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    public extern static bool GetChampionDeaths(out int Output, AttackableUnit Unit);
    /// <summary>
    /// Returns an int containing the number of assists the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to poll</param>
    public extern static bool GetChampionAssists(out int Output, AttackableUnit Unit);
    /// <summary>
    /// Gives target champion a variable amount of gold.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to give gold to.</param>
    /// <param name="GoldAmount">Amount of gold to give the champion.</param>
    public extern static bool GiveChampionGold(AttackableUnit Unit, float GoldAmount);
    /// <summary>
    /// Orders a unit to stop its movement.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  How this block will interract with forced move orders (say from a skill) is currently untested.
    /// </remarks>
    /// <param name="Unit">Handle of the champion to order.</param>
    public extern static bool StopUnitMovement(AttackableUnit Unit);
    /// <summary>
    /// Test if a hero has a specific item.
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.  This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    /// <param name="ItemID">Numerical ID of the item to look for.</param>
    public extern static bool TestChampionHasItem(AttackableUnit Unit, int ItemID = 0);
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
    public extern static Task<bool> PanCameraFromCurrentPositionToPoint(AttackableUnit Unit, Vector3 TargetPosition = Vector3.Zero, float Time = 1);
    /// <summary>
    /// Returns the number of item slots filled for a particular champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    public extern static bool GetNumberOfInventorySlotsFilled(out int Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the level of the target unit.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose level you want to check.</param>
    public extern static bool GetUnitLevel(out int Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the current XP total of the target champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.  Returns 0 if unit is not champion.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose XP total you want to get.</param>
    public extern static bool GetUnitXP(out float Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the distance between the Unit and the Point
    /// </summary>
    /// <remarks>
    /// Distance is measured from the edge of the unit's bounding box
    /// </remarks>
    /// <param name="Unit">Handle of the unit</param>
    /// <param name="Point">Point</param>
    public extern static bool DistanceBetweenObjectAndPoint(out float Output, AttackableUnit Unit, Vector3 Point = Vector3.Zero);
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
    public extern static bool GetUnitsInTargetArea(out IEnumerable<AttackableUnit> Output, AttackableUnit Unit, Vector3 TargetLocation = Vector3.Zero, float Radius = 0, SpellFlags SpellFlags = AlwaysSelf);
    /// <summary>
    /// Test to see if unit is alive
    /// </summary>
    /// <remarks>
    /// Unit is not alive if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    public extern static bool TestUnitCondition(AttackableUnit Unit);
    /// <summary>
    /// Test to see if unit is invulnerable
    /// </summary>
    /// <remarks>
    /// Unit is not invulnerable if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    public extern static bool TestUnitIsInvulnerable(AttackableUnit Unit);
    /// <summary>
    /// Test to see if unit is in brush
    /// </summary>
    /// <remarks>
    /// Unit is not in brush if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    public extern static bool TestUnitInBrush(AttackableUnit Unit);
    /// <summary>
    /// Test to see if unit has a specific buff
    /// </summary>
    /// <remarks>
    /// Unit does not have buff if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="TargetUnit">Unit to be tested</param>
    /// <param name="CasterUnit">OPTIONAL.  Additional filter to check if buff was cast by a specific unit</param>
    /// <param name="BuffName">Name of buff to be tested</param>
    public extern static bool TestUnitHasBuff(AttackableUnit TargetUnit, AttackableUnit CasterUnit, string BuffName = "");
    /// <summary>
    /// Test to see if a one unit has visibility of another unit
    /// </summary>
    /// <remarks>
    /// If either unit does not exist, then they are not visible; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Viewer">Can this unit see the other?</param>
    /// <param name="TargetUnit">Is this unit visible to the viewer unit?</param>
    public extern static bool TestUnitVisibility(AttackableUnit Viewer, AttackableUnit TargetUnit);
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
    public extern static Task<bool> PlayVOAudioEvent(string EventID = "", string FolderName = "", bool FireAndForget = true);
    /// <summary>
    /// Returns the attack range for unit
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitAttackRange(out float Output, AttackableUnit Unit);
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
    public extern static bool GetUnitGold(out float Output, AttackableUnit Unit);
    /// <summary>
    /// Returns unit unspent skill points
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitSkillPoints(out int Output, AttackableUnit Unit);
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
    public extern static bool AddUnitPerceptionBubble(out DWORD BubbleID, AttackableUnit TargetUnit, float Radius = 0.0f, float Duration = 0.0f, TeamID Team = TEAM_ORDER, bool RevealStealth = false, AttackableUnit SpecificUnitsClientOnly, AttackableUnit RevealSpecificUnitOnly);
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
    public extern static bool AddPositionPerceptionBubble(out DWORD BubbleID, Vector3 Position = Vector3.Zero, float Radius = 0.0f, float Duration = 0.0f, TeamID Team = TEAM_ORDER, bool RevealStealth = false, AttackableUnit SpecificUnitsClientOnly, AttackableUnit RevealSpecificUnitOnly);
    /// <summary>
    /// Removes Perception Bubble
    /// </summary>
    /// <remarks>
    /// Used on Bubble IDs returned by the AddUnitPerceptionBubble and AddPositionPerceptionBubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble; returned by AddPerceptionBubble nodes</param>
    public extern static bool RemovePerceptionBubble(DWORD BubbleID);
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
    public extern static bool CreateUnitParticle(out DWORD EffectID, AttackableUnit BindObject, string BoneName = "", string EffectName = "", AttackableUnit TargetObject, string TargetBoneName = "", Vector3 TargetPosition = Vector3.Zero, Vector3 OrientTowards = Vector3.Zero, AttackableUnit SpecificUnitOnly, TeamID SpecificTeamOnly = TEAM_UNKNOWN, float FOWVisibilityRadius = 0.0f, TeamID FOWTeam = TEAM_UNKNOWN, bool SendIfOnScreenOrDiscard = false);
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
    public extern static bool CreatePositionParticle(out DWORD EffectID, Vector3 Position = Vector3.Zero, string EffectName = "", AttackableUnit TargetObject, string TargetBoneName = "", Vector3 TargetPosition = Vector3.Zero, Vector3 OrientTowards = Vector3.Zero, AttackableUnit SpecificUnitOnly, TeamID SpecificTeamOnly = TEAM_UNKNOWN, float FOWVisibilityRadius = 0.0f, TeamID FOWTeam = TEAM_UNKNOWN, bool SendIfOnScreenOrDiscard = false);
    /// <summary>
    /// Removes Particle
    /// </summary>
    /// <remarks>
    /// Used on Effect IDs returned by the CreateUnitParticle and CreatePositionParticle
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; returned by CreateParticle nodes</param>
    public extern static bool RemoveParticle(DWORD EffectID);
    /// <summary>
    /// Returns unit Team ID
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitTeam(out TeamID Output, AttackableUnit Unit);
    /// <summary>
    /// Sets unit state DisableAmbientGold.  If disabled, unit does not get ambient gold gain (but still gets gold/5 from runes).
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.
    /// </remarks>
    /// <param name="Unit">Sets state of this unit.</param>
    /// <param name="Disabled">If true, ambient gold gain is disabled.</param>
    public extern static bool SetStateDisableAmbientGold(AttackableUnit Unit, bool Disabled = false);
    /// <summary>
    /// Sets unit level cap.  Level cap 0 means no cap.  Otherwise unit will earn experience up to one XP less than the level cap.
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.  If unit is already higher than the cap, it will earn 0 XP.
    /// </remarks>
    /// <param name="Unit">Sets level cap of this unit.</param>
    /// <param name="LevelCap">If 0, no level cap; otherwise unit cannot get higher than this level.</param>
    public extern static bool SetUnitLevelCap(AttackableUnit Unit, int LevelCap = 0);
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
    public extern static bool TestPlayerCameraLocked(AttackableUnit Player);
    /// <summary>
    /// A Procedure call
    /// </summary>
    /// <remarks>
    /// Procedure
    /// </remarks>
    /// <param name="PocedureName"> can not be empty </param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ChatMessage">Chat string</param>
    public extern static bool Procedure2To2(out string Output1, out UnitType Output2, string PocedureName = "", AttackableUnit Unit, string ChatMessage = "Bot Talking Here");
    /// <summary>
    /// Test if game started
    /// </summary>
    /// <remarks>
    /// Tests if game started. True if game started. False if not
    /// </remarks>
    public extern static bool TestGameStarted();
    /// <summary>
    /// Tests if the specified unit is under attack
    /// </summary>
    /// <remarks>
    /// Tests if the specified unit is under attack. May gather enemies of given unit to figure out if under attack
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool TestUnitUnderAttack(AttackableUnit Unit);
    /// <summary>
    /// Returns the type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitType(out UnitType Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the creature type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit creature type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitCreatureType(out CreatureType Output, AttackableUnit Unit);
    /// <summary>
    /// Tests if the specified unit can use the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can cast spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool TestCanCastSpell(AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
    /// <summary>
    /// Cast specified Spell
    /// </summary>
    /// <remarks>
    /// Spell cast
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool CastUnitSpell(AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
    /// <summary>
    /// Set ignore visibility for a specific spell
    /// </summary>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="IgnoreVisibility">Ignore visibility ?</param>
    public extern static bool SetUnitSpellIgnoreVisibity(AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex, bool IgnoreVisibility = false);
    /// <summary>
    /// Set specified Spell target position
    /// </summary>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool SetUnitAISpellTargetLocation(Vector3 TargetLocation = Vector3.Zero, int SlotIndex);
    /// <summary>
    /// Set specified Spell target
    /// </summary>
    /// <param name="TargetUnit">Target Input.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool SetUnitAISpellTarget(AttackableUnit TargetUnit, int SlotIndex);
    /// <summary>
    /// Clears specified Spell target
    /// </summary>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool ClearUnitAISpellTarget(int SlotIndex);
    /// <summary>
    /// Test validity of specified Spell target
    /// </summary>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool TestUnitAISpellTargetValid(int SlotIndex);
    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool GetSpellSlotCooldown(out float Output, AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
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
    public extern static bool SetSpellSlotCooldown(AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex, float Cooldown = 1);
    /// <summary>
    /// Returns the PAR type for specified unit
    /// </summary>
    /// <remarks>
    /// PAR Type
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitPARType(out PrimaryAbilityResourceType Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the cost for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cost
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool GetUnitSpellCost(out float Output, AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
    /// <summary>
    /// Returns the cast range for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cast range
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool GetUnitSpellCastRange(out float Output, AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
    /// <summary>
    /// Returns the level for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell level
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool GetUnitSpellLevel(out int Output, AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
    /// <summary>
    /// Levels up a specified spell
    /// </summary>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool LevelUpUnitSpell(AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
    /// <summary>
    /// Tests if the specified unit can level up the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can level up spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool TestUnitCanLevelUpSpell(AttackableUnit Unit, int SlotIndex);
    /// <summary>
    /// Gets a handle to the the unit running the behavior tree in OutputRef
    /// </summary>
    /// <remarks>
    /// Gets a handle to the the unit running the behavior tree
    /// </remarks>
    public extern static bool GetUnitAISelf(out AttackableUnit Output);
    /// <summary>
    /// Unit run logic for first time
    /// </summary>
    public extern static bool TestUnitAIFirstTime();
    /// <summary>
    /// Sets unit to assist
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target unit</param>
    public extern static bool SetUnitAIAssistTarget(AttackableUnit TargetUnit);
    /// <summary>
    /// Sets unit to target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Source Reference</param>
    public extern static bool SetUnitAIAttackTarget(AttackableUnit TargetUnit);
    /// <summary>
    /// Gets unit being assisted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    public extern static bool GetUnitAIAssistTarget(out AttackableUnit Output);
    /// <summary>
    /// Gets unit being targeted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
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
    public extern static bool IssueMoveToUnitOrder(AttackableUnit TargetUnit);
    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="Location">Position to move to</param>
    public extern static bool IssueMoveToPositionOrder(Vector3 Location = Vector3.Zero);
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
    public extern static bool IssueAIEmoteOrder(uint EmoteIndex = 0);
    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="EmoteIndex">Emote ID</param>
    public extern static bool IssueGloabalEmoteOrder(AttackableUnit Unit, uint EmoteIndex = 0);
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
    public extern static bool ClearUnitAIAttackTarget();
    /// <summary>
    /// Clear AI assist target
    /// </summary>
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
    public extern static bool GetUnitAIAttackers(out IEnumerable<AttackableUnit> Output, AttackableUnit Unit);
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
    public extern static bool TestUnitAICanBuyRecommendedItem();
    /// <summary>
    /// Buy next recommended item
    /// </summary>
    public extern static bool UnitAIBuyRecommendedItem();
    /// <summary>
    /// Unit can buy item
    /// </summary>
    /// <param name="ItemID">Item to buy.</param>
    public extern static bool TestUnitAICanBuyItem(uint ItemID = 0);
    /// <summary>
    /// Buy item
    /// </summary>
    /// <param name="ItemID">Item to buy.</param>
    public extern static bool UnitAIBuyItem(uint ItemID = 0);
    /// <summary>
    /// Computes a position for spell cast
    /// </summary>
    /// <param name="TargetUnit">target unit</param>
    /// <param name="ReferenceUnit">Reference unit</param>
    /// <param name="Range">Spell range</param>
    /// <param name="UnitSide">Which side of target are we going to (in between our out)</param>
    public extern static bool ComputeUnitAISpellPosition(AttackableUnit TargetUnit, AttackableUnit ReferenceUnit, float Range, bool UnitSide = true);
    /// <summary>
    /// Retrieves a position for spell cast
    /// </summary>
    public extern static bool GetUnitAISpellPosition(out Vector3 Output);
    /// <summary>
    /// Clears position for spell cast
    /// </summary>
    public extern static bool ClearUnitAISpellPosition();
    /// <summary>
    /// Unit precomputed cast location valid 
    /// </summary>
    public extern static bool TestUnitAISpellPositionValid();
    /// <summary>
    /// Unit at precomputed spell cast location
    /// </summary>
    /// <remarks>
    /// Unit at precomputed spell location
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Location">Source Reference</param>
    /// <param name="Error">Accepted error</param>
    public extern static bool TestUnitAtLocation(AttackableUnit Unit, Vector3 Location = Vector3.Zero, float Error = 200);
    /// <summary>
    /// Unit in safe range
    /// </summary>
    /// <param name="Range">Unit in safe Range</param>
    public extern static bool TestUnitAIIsInSafeRange(float Range = 600);
    /// <summary>
    /// Computes a safe position for AI unit
    /// </summary>
    /// <param name="Range">safe range</param>
    /// <param name="UseDefender">If True, use defenders in search</param>
    /// <param name="UseEnemy">If True, use enemies to guide in search</param>
    public extern static bool ComputeUnitAISafePosition(float Range, bool UseDefender = true, bool UseEnemy = true);
    /// <summary>
    /// Retrieves a safe position for AI unit
    /// </summary>
    public extern static bool GetUnitAISafePosition(out Vector3 Output);
    /// <summary>
    /// Clears position for safe
    /// </summary>
    public extern static bool ClearUnitAISafePosition();
    /// <summary>
    /// Unit precomputed safe location valid 
    /// </summary>
    public extern static bool TestUnitAISafePositionValid();
    /// <summary>
    /// Returns the base location of a given unit
    /// </summary>
    /// <remarks>
    /// Return SUCCES if we can find the base
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    public extern static bool GetUnitAIBasePosition(out Vector3 Output, AttackableUnit Unit);
    /// <summary>
    /// Returns the radius AOE of spell in a given slot
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    public extern static bool GetUnitSpellRadius(out float Output, AttackableUnit Unit, SpellbookTypeEnum Spellbook = SPELLBOOK_UNKNOWN, int SlotIndex);
    /// <summary>
    /// Returns distance between 2 units
    /// </summary>
    /// <remarks>
    /// takes into account their BB
    /// </remarks>
    /// <param name="SourceUnit">Source unit</param>
    /// <param name="DestinationUnit">Destination unit</param>
    public extern static bool GetDistanceBetweenUnits(out float Output, AttackableUnit SourceUnit, AttackableUnit DestinationUnit);
    /// <summary>
    /// Unit target is in range
    /// </summary>
    /// <param name="Error">Accepted error for unit location</param>
    public extern static bool TestUnitAIAttackTargetInRange(float Error = 0);
    /// <summary>
    /// Unit has valid target
    /// </summary>
    /// <remarks>
    /// Unit has valid target, use before getting attack target.
    /// </remarks>
    public extern static bool TestUnitAIAttackTargetValid();
    /// <summary>
    /// Unit can see target
    /// </summary>
    /// <param name="Unit">Viewer Unit</param>
    /// <param name="TargetUnit">Target  Unit</param>
    public extern static bool TestUnitIsVisible(AttackableUnit Unit, AttackableUnit TargetUnit);
    /// <summary>
    /// Sets item target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target</param>
    /// <param name="ItemID">Item ID</param>
    public extern static bool SetUnitAIItemTarget(AttackableUnit TargetUnit, int ItemID = 0);
    /// <summary>
    /// Clears item target
    /// </summary>
    public extern static bool ClearUnitAIItemTarget();
    /// <summary>
    /// Unit can use item
    /// </summary>
    /// <param name="ItemID">Item ID</param>
    public extern static bool TestUnitAICanUseItem(int ItemID = 0);
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
    /// <param name="Unit">Unit to poll</param>
    /// <param name="SlotIndex">spell slot ID</param>
    public extern static bool TestUnitSpellToggledOn(AttackableUnit Unit, int SlotIndex);
    /// <summary>
    /// Tests if unit is channeling
    /// </summary>
    /// <param name="Unit">Unit to poll</param>
    public extern static bool TestUnitIsChanneling(AttackableUnit Unit);
    /// <summary>
    /// Returns unit that casted a buff on input unit
    /// </summary>
    /// <param name="Unit">Source unit</param>
    /// <param name="BuffName">Buff name</param>
    public extern static bool GetUnitBuffCaster(out AttackableUnit Output, AttackableUnit Unit, string BuffName = "");
    /// <summary>
    /// AI Unit has an assigned task
    /// </summary>
    public extern static bool TestUnitAIHasTask();
    /// <summary>
    /// Returns position computed by a task assigned to the unit
    /// </summary>
    public extern static bool GetUnitAITaskPosition(out Vector3 Output);
    /// <summary>
    /// Permanently modifies a target unit's armor.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatArmorMod(AttackableUnit Unit, float Delta);
    /// <summary>
    /// Permanently modifies a target unit's magic resistance.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatMagicResistanceMod(AttackableUnit Unit, float Delta);
    /// <summary>
    /// Permanently modifies a target unit's max health.  This will heal the target.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.  Further, this later needs to be converted to a non-healing implementation; it is using the healing approach until Kuo fixes a bug.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatMaxHealthMod(AttackableUnit Unit, float Delta);
    /// <summary>
    /// Permanently modifies a target unit's attack damage.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    public extern static bool IncPermanentFlatAttackDamageMod(AttackableUnit Unit, float Delta);
}
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
    
    async Task<bool> GarenBehavior()
    {
        
        return
        (
            await GarenInit() &&
            (
                await GarenAtBaseHealAndBuy() ||
                await GarenLevelUp() ||
                await GarenGameNotStarted() ||
                ReduceDamageTaken() ||
                await GarenHighThreatManagement() ||
                await GarenReturnToBase() ||
                await GarenKillChampion() ||
                await GarenLowThreatManagement() ||
                GarenHeal() ||
                await GarenAttack() ||
                await GarenPushLane()
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
            IterateOverAllDecorator(out Unit, this.TargetCollection, async () => (
                TestUnitIsVisible(this.Self, Unit) &&
                (
                    (
                        GetUnitType(out UnitType, Unit) &&
                        EqualUnitType(UnitType, MINION_UNIT) &&
                        AddFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 20)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        EqualUnitType(UnitType, HERO_UNIT) &&
                        AddFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 30)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        EqualUnitType(UnitType, TURRET_UNIT) &&
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
            IterateOverAllDecorator(out Attacker, this.TargetCollection, async () => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                LessFloat(Distance, this.CurrentClosestDistance) &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }
    
    async Task<bool> GarenDeaggroChecker()
    {
        float Distance;
        return
        await MaskFailure(async () => (
            SetVarBool(out this.LostAggro, false) &&
            DistanceBetweenObjectAndPoint(out Distance, this.AggroTarget, this.AggroPosition) &&
            GreaterFloat(Distance, 800) &&
            LessFloat(Distance, 1200) &&
            SetVarBool(out this.LostAggro, true)
        ));
    }
    
    async Task<bool> GarenInit()
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
                    await MaskFailure(async () => (
                        GetGameTime(out CurrentTime) &&
                        SubtractFloat(out TimeDiff, CurrentTime, this.PrevTime) &&
                        (
                            GreaterFloat(TimeDiff, 1) ||
                            LessFloat(TimeDiff, 0)
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
                            await MaskFailure(async () => IterateUntilSuccessDecorator(out Unit, this.TargetCollection, async () => (
                                GetUnitType(out UnitType, Unit) &&
                                EqualUnitType(UnitType, TURRET_UNIT) &&
                                AddFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 8)
                            )))
                        ) &&
                        GetGameTime(out this.PrevTime)
                    )) &&
                    await MaskFailure(async () => (
                        GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                        SubtractFloat(out NewDamage, this.PrevHealth, CurrentHealth) &&
                        GreaterFloat(NewDamage, 0) &&
                        AddFloat(out this.AccumulatedDamage, this.AccumulatedDamage, NewDamage)
                    )) &&
                    GetUnitCurrentHealth(out this.PrevHealth, this.Self)
                )
            )
        );
    }
    
    async Task<bool> GarenAtBaseHealAndBuy()
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
            LessEqualFloat(Distance, 450) &&
            SetVarBool(out this.TeleportHome, false) &&
            (
                (
                    await DebugAction(0, SUCCESS, "Start ----- Heal -----") &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    LessFloat(Health_Ratio, 0.95f) &&
                    await DebugAction(0, SUCCESS, "Success ----- Heal -----")
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
                        GreaterFloat(temp, 0) &&
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
                        GreaterInt(this.PotionsToBuy, 0) &&
                        !TestChampionHasItem(this.Self, 2003) &&
                        TestUnitAICanBuyItem(2003) &&
                        UnitAIBuyItem(2003) &&
                        SubtractInt(out this.PotionsToBuy, this.PotionsToBuy, 1)
                    )
                )
            ) &&
            await DebugAction(0, SUCCESS, "++++ At Base Heal & Buy +++")
        );
    }
    
    async Task<bool> GarenLevelUp()
    {
        int SkillPoints;
        int Ability0Level;
        int Ability1Level;
        int Ability2Level;
        return
        (
            GetUnitSkillPoints(out SkillPoints, this.Self) &&
            GreaterInt(SkillPoints, 0) &&
            GetUnitSpellLevel(out Ability0Level, this.Self, SPELLBOOK_UNKNOWN, 0) &&
            GetUnitSpellLevel(out Ability1Level, this.Self, SPELLBOOK_UNKNOWN, 1) &&
            GetUnitSpellLevel(out Ability2Level, this.Self, SPELLBOOK_UNKNOWN, 2) &&
            (
                (
                    TestUnitCanLevelUpSpell(this.Self, 3) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                    await DebugAction(0, SUCCESS, "levelup 3")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    (
                        (
                            GreaterEqualInt(Ability0Level, 1) &&
                            GreaterEqualInt(Ability2Level, 1) &&
                            LessEqualInt(Ability1Level, 0)
                        ) ||
                        (
                            GreaterEqualInt(Ability0Level, 3) &&
                            GreaterEqualInt(Ability2Level, 3) &&
                            LessEqualInt(Ability1Level, 1)
                        )
                    ) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    await DebugAction(0, SUCCESS, "levelup 0")
                ) ||
                (
                    (
                        TestUnitCanLevelUpSpell(this.Self, 2) &&
                        LessEqualInt(Ability2Level, Ability0Level) &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                        await DebugAction(0, SUCCESS, "levelup 0")
                    ) ||
                    (
                        TestUnitCanLevelUpSpell(this.Self, 0) &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                        await DebugAction(0, SUCCESS, "levelup 0")
                    )
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    await DebugAction(0, SUCCESS, "levelup 0")
                )
            ) &&
            await DebugAction(0, SUCCESS, "++++ Level up ++++")
        );
    }
    
    async Task<bool> GarenGameNotStarted()
    {
        
        return
        (
            !TestGameStarted() &&
            await DebugAction(0, SUCCESS, "+++ Game Not Started +++")
        );
    }
    
    async Task<bool> GarenAttack()
    {
        
        return
        (
            await GarenAcquireTarget() &&
            await GarenAttackTarget() &&
            await DebugAction(0, SUCCESS, "++++ Attack ++++")
        );
    }
    
    async Task<bool> GarenAcquireTarget()
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
                await GarenDeaggroChecker() &&
                EqualBool(this.LostAggro, false) &&
                await DebugAction(0, SUCCESS, "+++ Use Previous Target +++")
            ) ||
            (
                await DebugAction(0, SUCCESS, "EnableOrDisableAllyAggro") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends|AffectHeroes|AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                IterateOverAllDecorator(out unit, FriendlyUnits, async () => (
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    GarenFindClosestVisibleTarget() &&
                    EqualBool(this.ValueChanged, true) &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    EqualUnit(unit, this.Self) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                )) &&
                EqualBool(this.ValueChanged, true) &&
                await DebugAction(0, SUCCESS, "+++ Acquired Ally under attack +++")
            ) ||
            (
                await DebugAction(0, SUCCESS, "??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectBuildings|AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                (
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    GreaterInt(Count, 0) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    IterateOverAllDecorator(out unit, this.TargetCollection, async () => (
                        DistanceBetweenObjectAndPoint(out Distance, unit, this.SelfPosition) &&
                        LessFloat(Distance, this.CurrentClosestDistance) &&
                        TestUnitIsVisible(this.Self, unit) &&
                        (
                            (
                                EqualBool(this.LostAggro, true) &&
                                GetUnitAIAttackTarget(out this.AggroTarget) &&
                                NotEqualUnit(this.AggroTarget, unit)
                            ) ||
                            EqualBool(this.LostAggro, false)
                        ) &&
                        SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    ))
                ) &&
                EqualBool(this.ValueChanged, true) &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                await DebugAction(0, SUCCESS, "+++ AcquiredNewTarget +++")
            )
        );
    }
    
    async Task<bool> GarenAttackTarget()
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
            NotEqualUnitTeam(SelfTeam, TargetTeam) &&
            (
                (
                    GetUnitType(out UnitType, Target) &&
                    EqualUnitType(UnitType, MINION_UNIT) &&
                    GetUnitCurrentHealth(out currentHealth, Target) &&
                    GetUnitMaxHealth(out MaxHealth, Target) &&
                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                    LessFloat(HP_Ratio, 0.2f) &&
                    (
                        (
                            GreaterFloat(this.StrengthRatioOverTime, 2) &&
                            GarenCanCastAbility2() &&
                            SetUnitAIAttackTarget(this.Self) &&
                            await GarenCastAbility2()
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
                    EqualUnitType(UnitType, HERO_UNIT) &&
                    (
                        (
                            GarenCanCastAbility0() &&
                            SetUnitAIAttackTarget(this.Self) &&
                            CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                        ) ||
                        (
                            GarenCanCastAbility2() &&
                            await GarenCastAbility2()
                        )
                    )
                ) ||
                GarenAutoAttackTarget()
            ) &&
            await DebugAction(0, SUCCESS, "++ Attack Success ++")
        );
    }
    
    async Task<bool> GarenReturnToBase()
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
            GreaterFloat(Distance, 300) &&
            (
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    (
                        (
                            EqualBool(this.TeleportHome, true) &&
                            LessEqualFloat(Health_Ratio, 0.35f)
                        ) ||
                        (
                            EqualBool(this.TeleportHome, false) &&
                            LessEqualFloat(Health_Ratio, 0.25f) &&
                            SetVarBool(out this.TeleportHome, true)
                        )
                    )
                ) ||
                (
                    await DebugAction(0, FAILURE, "EmptyNode: HighGold")
                )
            ) &&
            (
                (
                    SetVarFloat(out this.CurrentClosestDistance, 30000) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends|AffectTurrets) &&
                    GarenFindClosestTarget() &&
                    EqualBool(this.ValueChanged, true) &&
                    (
                        (
                            GetDistanceBetweenUnits(out Distance, this.CurrentClosestTarget, this.Self) &&
                            LessFloat(Distance, 125) &&
                            (
                                (
                                    TestUnitAISpellPositionValid() &&
                                    GetUnitAISpellPosition(out TeleportPosition) &&
                                    DistanceBetweenObjectAndPoint(out DistanceToTeleportPosition, this.Self, TeleportPosition) &&
                                    LessFloat(DistanceToTeleportPosition, 50)
                                ) ||
                                !TestUnitAISpellPositionValid()
                            ) &&
                            IssueTeleportToBaseOrder() &&
                            ClearUnitAISpellPosition() &&
                            await DebugAction(0, SUCCESS, "Yo")
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
                            await DebugAction(0, SUCCESS, "Yo")
                        )
                    )
                ) ||
                (
                    GetUnitAIBasePosition(out BaseLocation, this.Self) &&
                    IssueMoveToPositionOrder(BaseLocation)
                )
            ) &&
            await DebugAction(0, SUCCESS, "+++ Teleport Home +++")
        );
    }
    
    async Task<bool> GarenHighThreatManagement()
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
                    LessEqualFloat(Health_Ratio, 0.25f) &&
                    await DebugAction(0, SUCCESS, "+++ LowHealthUnderAttack +++") &&
                    SetVarBool(out SuperHighThreat, true)
                ) ||
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    DivideFloat(out Damage_Ratio, this.AccumulatedDamage, MaxHealth) &&
                    (
                        (
                            EqualBool(this.AggressiveKillMode, true) &&
                            GreaterFloat(Damage_Ratio, 0.15f)
                        ) ||
                        (
                            EqualBool(this.AggressiveKillMode, false) &&
                            GreaterFloat(Damage_Ratio, 0.02f)
                        )
                    ) &&
                    await DebugAction(0, SUCCESS, "+++ BurstDamage +++")
                )
            ) &&
            await DebugAction(0, SUCCESS, "+++ High Threat +++") &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    EqualBool(SuperHighThreat, true) &&
                    GarenCanCastAbility1() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    EqualBool(SuperHighThreat, true) &&
                    GarenCanCastAbility0() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                await GarenMicroRetreat()
            ) &&
            await DebugAction(0, SUCCESS, "+++ High Threat +++")
        );
    }
    
    async Task<bool> GarenLowThreatManagement()
    {
        
        return
        (
            (
                (
                    GreaterFloat(this.StrengthRatioOverTime, 6) &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    EqualBool(this.LowThreatMode, true) &&
                    SetVarBool(out this.LowThreatMode, false) &&
                    GreaterFloat(this.StrengthRatioOverTime, 4) &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    ClearUnitAISafePosition() &&
                    await DebugAction(0, FAILURE, "DoNotRemoveForcedFail")
                )
            ) &&
            await GarenMicroRetreat() &&
            await DebugAction(0, SUCCESS, "++++ Low Threat +++")
        );
    }
    
    async Task<bool> GarenKillChampion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        bool Aggressive;
        float MyHealthRatio;
        AttackableUnit ;
        return
        (
            SetVarBool(out this.AggressiveKillMode, false) &&
            (
                (
                    LessFloat(this.StrengthRatioOverTime, 3) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.8f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    IterateOverAllDecorator(out unit, this.TargetCollection, async () => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        LessFloat(HP_Ratio, CurrentLowestHealthRatio) &&
                        TestUnitIsVisible(this.Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    )) &&
                    EqualBool(this.ValueChanged, true) &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, false) &&
                    await DebugAction(0, SUCCESS, "PassiveKillChampion")
                ) ||
                (
                    LessFloat(this.StrengthRatioOverTime, 5.1f) &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    GreaterFloat(MyHealthRatio, 0.5f) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.4f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    IterateOverAllDecorator(out unit, this.TargetCollection, async () => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        LessFloat(HP_Ratio, CurrentLowestHealthRatio) &&
                        TestUnitIsVisible(this.Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    )) &&
                    EqualBool(this.ValueChanged, true) &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, true) &&
                    SetVarBool(out this.AggressiveKillMode, true) &&
                    await DebugAction(0, SUCCESS, "+++ AggressiveMode +++")
                )
            ) &&
            (
                (
                    GarenCanCastAbility0() &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                (
                    EqualBool(Aggressive, true) &&
                    GarenCanCastAbility3() &&
                    await GarenCastAbility3() &&
                    await DebugAction(0, SUCCESS, "+++ Use Ultiamte +++")
                ) ||
                (
                    !TestUnitHasBuff(this.Self, , "GarenBladestorm") &&
                    GarenCanCastAbility2() &&
                    await GarenCastAbility2()
                ) ||
                GarenAutoAttackTarget() ||
                await DebugAction(0, SUCCESS, "+++ Attack Champion+++")
            ) &&
            await DebugAction(0, SUCCESS, "++++ Success: Kill  +++")
        );
    }
    
    async Task<bool> GarenLastHitMinion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
         Target;
        return
        (
            (
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 800, AffectEnemies|AffectMinions) &&
                SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
                SetVarBool(out this.ValueChanged, false) &&
                IterateOverAllDecorator(out unit, this.TargetCollection, async () => (
                    GetUnitCurrentHealth(out CurrentHealth, unit) &&
                    GetUnitMaxHealth(out MaxHealth, unit) &&
                    DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                    LessFloat(HP_Ratio, CurrentLowestHealthRatio) &&
                    SetVarBool(out this.ValueChanged, true) &&
                    SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                    SetVarAttackableUnit(out this.CurrentClosestTarget, unit)
                )) &&
                EqualBool(this.ValueChanged, true) &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                SetVarAttackableUnit(out Target, this.CurrentClosestTarget)
            ) &&
            GarenAutoAttackTarget() &&
            await DebugAction(0, SUCCESS, "+++++++ Last Hit ++++++++")
        );
    }
    
    async Task<bool> GarenMicroRetreat()
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
                        LessFloat(Distance, 50) &&
                        ComputeUnitAISafePosition(800, false, false) &&
                        await DebugAction(0, SUCCESS, "------- At location computed new position --------------")
                    ) ||
                    (
                        IssueMoveToPositionOrder(SafePosition) &&
                        await DebugAction(0, SUCCESS, "------------ Success: Move to safe position ----------")
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
                    LessEqualFloat(Distance, AttackRange) &&
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
            LessEqualFloat(Cooldown, 0) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        );
    }
    
    bool GarenCanCastAbility1()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 1) &&
            LessEqualFloat(Cooldown, 0) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 1)
        );
    }
    
    bool GarenCanCastAbility2()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 2) &&
            LessEqualFloat(Cooldown, 0) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 2)
        );
    }
    
    bool GarenCanCastAbility3()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            LessEqualFloat(Cooldown, 0) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 3)
        );
    }
    
    async Task<bool> GarenCastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            await DebugAction(0, SUCCESS, "CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    await DebugAction(0, SUCCESS, "Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    await DebugAction(0, SUCCESS, "GoingToRangeCheck") &&
                    LessEqualFloat(Distance, Range) &&
                    await DebugAction(0, SUCCESS, "Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                    await DebugAction(0, SUCCESS, "Ability 1 Success ----------------")
                ) ||
                (
                    await DebugAction(0, SUCCESS, "MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    await DebugAction(0, SUCCESS, "Moving To Cast")
                )
            )
        );
    }
    
    async Task<bool> GarenCastAbility1()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            await DebugAction(0, SUCCESS, "CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    await DebugAction(0, SUCCESS, "Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    await DebugAction(0, SUCCESS, "GoingToRangeCheck") &&
                    LessEqualFloat(Distance, Range) &&
                    await DebugAction(0, SUCCESS, "Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    await DebugAction(0, SUCCESS, "Ability 1 Success ----------------")
                ) ||
                (
                    await DebugAction(0, SUCCESS, "MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    await DebugAction(0, SUCCESS, "Moving To Cast")
                )
            )
        );
    }
    
    async Task<bool> GarenCastAbility2()
    {
        AttackableUnit Target;
        AttackableUnit ;
        float Range;
        float Distance;
        return
        (
            await DebugAction(0, SUCCESS, "CastSubTree") &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    TestUnitHasBuff(this.Self, , "GarenBladestorm") &&
                    IssueMoveToUnitOrder(Target)
                ) ||
                (
                    GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
                    SetVarFloat(out Range, 200) &&
                    (
                        (
                            await DebugAction(0, SUCCESS, "Pareparing to cast ability 2") &&
                            GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                            await DebugAction(0, SUCCESS, "GoingToRangeCheck") &&
                            LessEqualFloat(Distance, Range) &&
                            await DebugAction(0, SUCCESS, "Range Check Succses") &&
                            CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                            await DebugAction(0, SUCCESS, "Ability 2 Success ----------------")
                        ) ||
                        (
                            await DebugAction(0, SUCCESS, "MoveIntoRangeSequence------------------") &&
                            IssueMoveToUnitOrder(Target) &&
                            await DebugAction(0, SUCCESS, "Moving To Cast")
                        )
                    )
                )
            )
        );
    }
    
    async Task<bool> GarenCastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            await DebugAction(0, SUCCESS, "CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 3) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    await DebugAction(0, SUCCESS, "Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    await DebugAction(0, SUCCESS, "GoingToRangeCheck") &&
                    LessEqualFloat(Distance, Range) &&
                    await DebugAction(0, SUCCESS, "Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                    await DebugAction(0, SUCCESS, "Ability 1 Success ----------------")
                ) ||
                (
                    await DebugAction(0, SUCCESS, "MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    await DebugAction(0, SUCCESS, "Moving To Cast")
                )
            )
        );
    }
    
    async Task<bool> GarenPushLane()
    {
        
        return
        (
            ClearUnitAIAttackTarget() &&
            IssueMoveOrder() &&
            await DebugAction(0, SUCCESS, "+++ Move To Lane +++")
        );
    }
    
    async Task<bool> GarenMisc()
    {
        TeamID SelfTeam;
        TeamID UnitTeam;
        AttackableUnit Assist;
        float Distance;
         AssistPosition;
        int Count;
        AttackableUnit Attacker;
        return
        (
            (
                await DebugAction(0, FAILURE, "??? EnableOrDisablePreviousTarget ???") &&
                TestUnitAIAttackTargetValid() &&
                SetVarBool(out this.LostAggro, false) &&
                GetUnitAIAttackTarget(out this.PreviousTarget) &&
                GetUnitTeam(out SelfTeam, this.Self) &&
                GetUnitTeam(out UnitTeam, this.PreviousTarget) &&
                NotEqualUnitTeam(UnitTeam, SelfTeam) &&
                GetUnitAIAssistTarget(out Assist) &&
                (
                    (
                        EqualUnit(Assist, this.Self) &&
                        DistanceBetweenObjectAndPoint(out Distance, this.Self, this.AssistPosition) &&
                        await MaskFailure(async () => (
                            GreaterEqualFloat(Distance, this.DeaggroDistance) &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out this.LostAggro, true) &&
                            await DebugAction(0, SUCCESS, "+++ Lost Aggro +++")
                        )) &&
                        LessFloat(Distance, this.DeaggroDistance) &&
                        await DebugAction(0, SUCCESS, "+++ In Aggro Range, Use Previous")
                    ) ||
                    (
                        NotEqualUnit(this.Self, Assist) &&
                        GetUnitPosition(out AssistPosition, Assist) &&
                        DistanceBetweenObjectAndPoint(out Distance, this.PreviousTarget, this.SelfPosition) &&
                        await MaskFailure(async () => (
                            GreaterEqualFloat(Distance, 1000) &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out this.LostAggro, true) &&
                            await DebugAction(0, SUCCESS, "------- Losing aggro from assist ----------")
                        )) &&
                        LessFloat(Distance, 1000) &&
                        await DebugAction(0, SUCCESS, "============= Use Previous Target: Still close to assist -----------")
                    )
                ) &&
                SetVarBool(out this.LostAggro, false) &&
                await DebugAction(0, SUCCESS, "++ Use Previous Target ++")
            ) &&
            (
                await DebugAction(0, SUCCESS, "??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                (
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    GreaterInt(Count, 0) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    IterateOverAllDecorator(out Attacker, this.TargetCollection, async () => (
                        (
                            (
                                EqualBool(this.LostAggro, true) &&
                                NotEqualUnit(Attacker, this.PreviousTarget)
                            ) ||
                            EqualBool(this.LostAggro, false)
                        ) &&
                        DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                        LessFloat(Distance, this.CurrentClosestDistance) &&
                        SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                        SetVarBool(out this.ValueChanged, true)
                    ))
                ) &&
                EqualBool(this.ValueChanged, true) &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                await DebugAction(0, SUCCESS, "+++ AcquiredNewTarget +++")
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
                LessFloat(HP_Ratio, 0.5f) &&
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
            GreaterEqualFloat(Damage_Ratio, 0.1f) &&
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
            IterateOverAllDecorator(out Attacker, this.TargetCollection, async () => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                LessFloat(Distance, this.CurrentClosestDistance) &&
                TestUnitIsVisible(this.Self, Attacker) &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }
}
