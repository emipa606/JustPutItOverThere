# GitHub Copilot Instructions for "Just Put It Over There" Mod

## Mod Overview and Purpose

"Just Put It Over There" is a RimWorld mod designed to enhance the trading and gifting experience within the game. When players make a trade deal with a caravan, the mod ensures that the items purchased are placed into storage areas rather than simply being dropped on the ground. This adds a layer of realism and convenience, as players no longer have to manually haul items to their storage spaces.

The functionality extends to guests leaving presents. If there is available storage space, visitors will store their gifts instead of leaving them on the floor. This feature also supports integration with the "Hospitality" mod, ensuring seamless interaction with guest behavior.

## Key Features and Systems

- **Trade Enhancement:** Caravans attempt to place bought goods directly into accessible storage spaces.
- **Gift Storage:** Ensure that any gifts from guests are stored appropriately if possible.
- **Seamless Integration:** Works in conjunction with "Hospitality" mod for improved guest interactions.
- **Fallback Handling:** Items are left on the ground if storage is unavailable or if the caravan is leaving.

## Coding Patterns and Conventions

- **C# Conventions:**
  - Use PascalCase for class and method names.
  - Use camelCase for local variables and method parameters.
  - Ensure clear and descriptive names for methods and variables.
  - Maintain concise and informative comments to describe the purpose and functionality of key methods.

## XML Integration

This mod does not currently include any XML files for RimWorld definition types. Future XML integrations might involve custom storage or item definitions.

## Harmony Patching

- **Dependencies:** The mod relies on Harmony for method patching. Ensure `Harmony` dependency (`brrainz.harmony`) is included in your project references.
  
- **Patches:**
  - **Pawn_TraderTracker_GiveSoldThingToPlayer.cs:** Includes `Prefix` patch to modify the behavior of item placement during trades.
  - **VisitorGiftForPlayerUtility_GiveGift.cs:** Utilizes a `Postfix` patch to alter guest behavior when leaving gifts.

- **Usage:** Leverage Harmony's patching capabilities to intercept and modify base game methods without altering core game files. This allows for improved mod compatibility and maintainability.

## Suggestions for GitHub Copilot

1. **Code Suggestions:** Use Copilot to generate boilerplate code for Harmony patches, such as the `Prefix` and `Postfix` methods.
2. **Edge Cases:** Explore additional error-handling logic for scenarios where storage might be dynamically altered during a trade.
3. **Performance Optimizations:** Use Copilot to suggest refactoring opportunities to improve performance, such as optimizing storage search algorithms.
4. **Future Features:** Suggest possible features or enhancements, such as prioritizing types of storage or implementing more detailed logging.

By following these guidelines and utilizing GitHub Copilot effectively, you can enhance the quality and functionality of the "Just Put It Over There" mod. Happy modding!

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
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

