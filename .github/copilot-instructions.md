# GitHub Copilot Instructions for RimWorld Mod: Flickable Storage

## Mod Overview and Purpose

**Flickable Storage** is a mod designed for RimWorld to provide players with enhanced control over their storage solutions. Storage buildings and stockpiles can now be set to one of four unique operational modes, allowing players to optimize resource management without altering storage filters or priorities. These modes are:

1. **Storage On** - Functions normally.
2. **Storage Off** - Does not accept or release any items.
3. **Accept Only** - Pawns can only store items, not retrieve them.
4. **Retrieve Only** - Pawns can only retrieve items, not store them.

## Key Features and Systems

- **Mode Switching:** Customize how each storage building or stockpile behaves based on your colony's needs.
- **Integration with IHaulDestination:** Compatible with all mods using the `IHaulDestination` interface, including popular ones like LWM's Deep Storage.
- **Mental Break Consideration:** Pawns experiencing a mental break bypass restrictions, maintaining gameplay dynamics.
- **Linked Storage Flicking:** Changes to flick settings affect all linked storages.
- **Chunk Exclusion:** Chunks, which cannot be forbidden, are ignored by the mod's settings. Consider using the Forbiddable Debris mod to address this.

## Coding Patterns and Conventions

- **Class Structure:** The code design utilizes both public and internal classes, leveraging C# access modifiers to encapsulate functionality where appropriate.
- **Singleton Utilization:** Internal static classes, such as the `Multiplayer` management, facilitate consistent state handling across sessions.

## XML Integration

- Ensure that XML vocabulary aligns with RimWorld's conventions to define new storage behaviors and integrate seamlessly with existing game mechanics.
- Test and validate XML patches for compatibility with other mods, especially those using the `IHaulDestination` class.

## Harmony Patching

- Modify the Accept-check and Forbidden check through Harmony patches to implement the custom storage modes effectively.
- Example files for patching include `ForbidUtility_IsForbidden_Thing` and `IHaulDestination_HaulDestinationEnabled`.

## Suggestions for Copilot

- **Code Structure:** Follow the modular structure provided, ensuring new features fit within the existing class hierarchy, such as extending `StorageTracker`.
- **Add Methods:** Use Copilot for generating new functions to integrate additional game mechanics or improve existing capabilities.
- **Testing Utilities:** Implement testing functions using Copilot to simulate different storage mode operations.
- **Debugging Support:** Collaborate on debugging tasks by providing detailed in-code comments for areas anticipated to encounter issues under certain game scenarios.
- **Documentation:** Help maintain comprehensive XML comments and in-code documentation to ensure contributors have a clear understanding of system interactions.

---

## Technical Contribution Acknowledgements

- **notfood:** Support for multiplayer compatibility, optimization, and code generalization.
- **Waveshaper:** Rigorous debugging efforts.
- **HawnHan:** Provided Chinese translation.
- **Velcroboy333:** Originator of the mod concept.

This mod serves to enhance the quality of life for players seeking better-controlled resource management, whether for strategic stockpiling or operational efficiency.
