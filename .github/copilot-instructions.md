# GitHub Copilot Instructions for "Just Put It Over There" RimWorld Mod

## Mod Overview and Purpose
The "Just Put It Over There" mod enhances the quality of life for players in RimWorld by optimizing the placement of traded items and gifts. After a trade is completed with a caravan, this mod ensures that the purchased items are placed into designated storage areas instead of being left on the ground. This functionality is contingent on the availability of accessible storage space. If no such space is available, or if the caravan is leaving the map, items will remain on the ground as usual. Additionally, when guests leave gifts, they will attempt to place them in storage, which includes interactions with the Hospitality mod if installed.

## Key Features and Systems
- Automatic placement of traded items into player-designed storage areas when available.
- Effective fallbacks to ground placement when storage is unreachable or maps are being exited.
- Support for guest interaction, integrating with the Hospitality mod to enhance guest gifting behavior.

## Coding Patterns and Conventions
- **Namespace Usage:** The mod's code is encapsulated within a namespace that reflects the mod's name, maintaining organization and preventing potential class name conflicts.
- **Class Names:** Each class name is descriptive of its responsibility, e.g., `VisitorGiftForPlayerUtility_GiveGift` and `Pawn_TraderTracker_GiveSoldThingToPlayer`.
- **Method Conventions:** Public methods are PascalCase and aim to clearly describe their functionality, with examples like `GetHaulJob` and `LogMessage`.
- **Logging and Messaging:** Helper methods such as `LogMessage` are utilized for clean and consistent debug information logging.

## XML Integration
While the current iteration of "Just Put It Over There" doesn't involve direct XML definitions, consider these practices for future XML integrations:
- **Def Naming:** Use clear, descriptive names for definitions that reflect their purpose and link succinctly with C# code functionality.
- **Integration Points:** Leverage XML to define mod settings, which can be surfaced easily via language data types tie-ins.

## Harmony Patching
The mod employs Harmony for runtime patching, allowing changes without altering base game files:
- **Prefix and Postfix Methods:** C# methods such as `Prefix` and `Postfix` are used for injecting logic before and after standard game flow executions.
- **Robust Patching:** Ensure that methods are patched securely, performing condition checks and implementing try-catch blocks where possible to maintain game stability.

## Suggestions for Copilot
1. **Refactoring Opportunities:** Use Copilot to identify classes or methods that might benefit from refactoring into smaller, more manageable components to improve readability.
2. **Automatic Error Checks:** Employ Copilot to automate error-checking patterns within Harmony patches to preemptively address common issues.
3. **Code Documentation:** Leverage suggestions for XML comments and inline documentation to maintain the clarity of code logic and anticipated uses.
4. **Edge Case Handling:** Utilize Copilot's recommendations to cover edge cases for item placement and guest behaviours, ensuring comprehensive feature coverage.

By adhering to the outlined practices and utilizing GitHub Copilot's intelligent coding assistance, contributors can maintain and enhance the "Just Put It Over There" mod efficiently and effectively.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).

