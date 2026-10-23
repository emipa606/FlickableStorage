# GitHub Copilot Instructions for Flickable Storage Mod

## Mod Overview and Purpose

**Mod Name:** Flickable Storage  
**Author:** Mlie  
**Display Name:** Flickable Storage  
**PackageId:** Mlie.FlickableStorage  
**Dependencies:** brrainz.harmony  

The **Flickable Storage** mod for RimWorld offers enhanced storage management options by introducing configurable storage settings for stockpiles and storage buildings. This mod provides RimWorld players with greater control over the accessibility and management of their resources.

## Key Features and Systems

- **Configurable Storage Settings:** Players can set each storage unit or stockpile to one of four modes:
  - **Storage On:** Standard functionality allowing normal stockpile behavior.
  - **Storage Off:** Prevents both storing and retrieving items.
  - **Accept Only:** Allows pawns to store items but not retrieve them.
  - **Retrieve Only:** Allows pawns to retrieve items but not store them.

- **Custom Examples:**
  - Disable food deliveries to prison cells with deactivated inmates by setting shelves to "retrieve only."
  - Reserve a stockpile of emergency materials by using the "store only" setting with a high priority.

- **Technical Implementation:**
  - The mod modifies the "Accept" check to block storing.
  - It uses a modified "Forbidden" check to block retrieving, which pawns in a mental break can bypass, ensuring critical items can still be accessed in emergencies.

- **Compatibility:**
  - Compatible with other mods using the `IHaulDestination` interface, such as LWM's Deep Storage.
  - Automatically updates flick settings across all linked storage units.

- **Limitations:**
  - The mod cannot forbid chunks, requiring an additional mod like "Forbiddable Debris" for full control.

## Coding Patterns and Conventions

- **Namespace Organization:** The mod uses a structured namespace with one class per C# file:
  - **ForbidUtility_IsForbidden_Thing.cs** - Handles forbidden checks for items.
  - **GlobalStorageTracker.cs** - Manages global storage settings.
  - **StorageTracker.cs** - Manages individual storage data via `ExposeData`.
  - **IHaulDestination_HaulDestinationEnabled.cs** - Patches to manage haul destination functionalities.
  - **FlickableStorage.cs** - Main entry point, initializes mod settings.

- **Conventions:**
  - Use descriptive method and variable names that clearly indicate their purpose.
  - Consistent use of prefixes and postfixes for Harmony patches.

## XML Integration

- This mod does not currently utilize XML files. All functionality is implemented directly through C# code.

## Harmony Patching

- **Dependency:** The mod relies on `brrainz.harmony` for runtime code alterations.
- **Approach:** 
  - **Prefix/Postfix Methods:** Modify or extend game logic using prefix and postfix methods strategically.
  - **Patch Example in `ForbidUtility_IsForbidden_Thing`:** The `Postfix` method modifies the retrieval logic by checking if an item is locked according to the mod's settings.

## Suggestions for Copilot

To enhance the development of this mod using GitHub Copilot, consider:

- **Adding Commentaries:** Provide inline comments to help clarify the logic for future developers.
- **Integration Testing:** Use Copilot to generate tests that ensure compatibility with other popular storage mods.
- **Debugging Aids:** Introduce debug messages during patches to confirm correct application of mod features.
- **Performance Optimization:** Leverage Copilot to suggest possible optimizations, especially in areas affecting game performance such as storage checks.

By following these guidelines and leveraging Copilot's capabilities, development and maintenance can be streamlined, ensuring that the Flickable Storage mod remains robust and user-friendly.

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

