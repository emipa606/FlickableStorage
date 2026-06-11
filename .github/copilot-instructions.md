# GitHub Copilot Instructions for Flickable Storage Mod

## Mod Overview and Purpose

The Flickable Storage mod for RimWorld offers players enhanced control over the storage systems in their colonies. By setting storage buildings and stockpiles to one of four distinct modes—Storage On, Storage Off, Accept Only, and Retrieve Only—players can more effectively manage their resources. This customization allows for strategic planning of resource distribution, ensuring that items are available exactly when and where they are needed without unnecessary hauling activities.

## Key Features and Systems

- **Storage On:** Functions as standard storage, allowing items to be both stored and retrieved.
- **Storage Off:** Disables the acceptance or release of items, effectively locking the storage.
- **Accept Only:** Items can be stored but not retrieved, ideal for maintaining stockpiles of essential materials.
- **Retrieve Only:** Allows for retrieval of items but prevents new items from being stored, useful for depleting stockpiles strategically.
- **Compatibility:** Designed to function with any mods that use the `IHaulDestination` class, tested with LWM's Deep Storage.
- **Linked Storages:** Changes to flick settings are applied across all linked storages.

## Coding Patterns and Conventions

- **C# Best Practices:** Utilize clear and descriptive naming conventions for classes and methods. Follow proper access control practices to ensure encapsulation.
- **Modular Design:** Maintain separation of concerns by organizing code into distinct classes with single responsibilities.
- **Error Handling:** Implement error handling to manage unexpected scenarios gracefully.

## XML Integration

- **Note:** This mod primarily involves C# and does not include any XML definitions. Any future expansion to include XML should adhere to RimWorld's def-based XML structure.

## Harmony Patching

- **Harmony Library:** The mod uses the Harmony library for non-invasive method patching to alter the game's storage behaviors.
- **Patch Structures:**
  - **Prefix and Postfix Methods:** Utilize these Harmony features to modify the behavior of existing methods such as the `Accept-check` and `Forbidden check`.
  - **Target Methods:** Specify which methods in the base game are targeted by each Harmony patch.

## Suggestions for GitHub Copilot

To assist with future development:
- **C# Code Generation:** Use Copilot to suggest boilerplate code for new features or patches, especially when creating new methods for handling storage states.
- **Harmony Patches:** Leverage Copilot's recommendations when creating Harmony patches to ensure correctly formatted and efficient patches.
- **Error Messages and Logging:** Copilot can help in drafting informative error messages and implementing logging features, enhancing debugging and performance tracking.
- **Multiplayer Support:** Given the mod's multiplayer compatibility, use Copilot to suggest solutions for synchronizing storage states across different players' sessions.

## Contributors and Credits

- **notfood:** Provided support for multiplayer functionality, optimization, and generalization.
- **Waveshaper:** Assisted in debugging processes.
- **HawnHan:** Contributed the Chinese translation.
- **Velcroboy333:** Originated the mod's conceptual idea.

These instructions serve as a guide for using GitHub Copilot effectively within the context of the Flickable Storage mod development, ensuring a streamlined and consistent coding process aligned with the mod's goals.

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

