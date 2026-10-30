// Historical stage12 change, reduced to the relevant typed UAssetAPI operations.
// This is an explanatory fragment, not a complete command-line program.
// `asset` must be a dependency-resolved UAsset opened with the matching UE5.3 usmap.
// Verify the original AST before applying this version-specific edit.

var function = asset.Exports
    .OfType<FunctionExport>()
    .Single(e => e.ObjectName.ToString() == "SetCanUseItem");

// Original stage12 input: 14 expressions, with the CanUseItem=false assignment
// at [11], Return(EX_Nothing) at [12], and EndOfScript at [13].
// The original builder checked the variable path, owner, terminators, and ABI.
var assignment = (EX_LetBool)function.ScriptBytecode[11];
assignment.AssignmentExpression = new EX_True();
function.ScriptBytecode = new KismetExpression[]
{
    assignment,                    // Preserve the original CanUseItem field pointer.
    function.ScriptBytecode[12],   // Preserve the original return.
    function.ScriptBytecode[13]    // Preserve EndOfScript.
};

// Serialize to a separate copy, reparse with all dependencies, and compare the
// complete asset. Other functions and the Ubergraph must remain unchanged.
// Do not reproduce this by changing a CDO checkbox or a fixed binary offset.
