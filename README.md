# FlickableStorage

![Image](https://i.imgur.com/WAEzk68.png)


Storage-buildings and stockpiles can be set to one of four settings



-  Storage on - Works as normal
-  Storage off - Will not accept or release any items
-  Accept only - Pawns can only store items, not retrieve them
-  Retrieve only - Pawns can only retrieve items, not store them



## Examples

Your prison cells have a storage unit for food, but when there is no prisoners you want to stop hauling food for it without changing storage filters or priorities. You turn the shelf to retrieve only when there are no prisoners.

You want to have a stack of steel left at all times for emergency trap-building. You set one shelf to store only with highest priority. 

## Technical

The blocking of storing new items is done by modifying the Accept-check for the storage.
The blocking of retrieving items is done by modifying the Forbidden check for the items in the storage. Pawns with mental break ignores the forbidden check so they will still be able to fetch items.

Should work with all mods that add storage buildings using the IHaulDestination class. Tested for example with https://steamcommunity.com/sharedfiles/filedetails/?id=1617282896]LWM&apos;s Deep Storage

NOTE: Chunks cannot be forbidden so are ignored by this mods settings. You can fix this by adding  https://steamcommunity.com/sharedfiles/filedetails/?id=2054653797][XND] Forbiddable Debris

## Credits

notfood: Support for https://steamcommunity.com/sharedfiles/filedetails/?id=1752864297]Multiplayer, optimization and generalization
Waveshaper: Debugging
HawnHan: Chinese translation
Velcroboy333: Original idea

[table]
    [tr]
        [td]https://invite.gg/Mlie]![Image](https://i.imgur.com/zdzzBrc.png)
[/td]
        [td]https://github.com/emipa606/FlickableStorage]![Image](https://i.imgur.com/kTkpTOE.png)
[/td]
    [/tr]
[/table]
	
![Image](https://i.imgur.com/pgjQLXV.png)

Since modding is just a hobby for me I expect no donations to keep modding. If you still want to show your support you can gift me anything from my https://store.steampowered.com/wishlist/id/Mlie]Wishlist or buy me a cup of tea.

https://ko-fi.com/G2G55DDYD]![Image](https://i.ibb.co/VWJJb3w/Support-Me-dark-2x.png)


![Image](https://i.imgur.com/Rs6T6cr.png)



-  See if the the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Post your error-log using https://steamcommunity.com/workshop/filedetails/?id=818773962]HugsLib and command Ctrl+F12
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.



