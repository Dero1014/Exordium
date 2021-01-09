
INPUT:

OnPlayer:
WASD - movement
F1, F2, F3, F4 - selection of pickup options as they are set in the exordium file
Q - randomly spawn items

OnUi:
Left Mouse - picks up item from inventory or equip into air
In Air Status: item can be placed with left mouse button into any slot and if something is in that slot it will change spots if it can
	       if the item is dropped where there are no slots then the item is dropped on the ground.	
Right Mouse - Equips an equipable item and dequips if it is already equiped
Middle Mouse - Consumes an item
Hover over an item + DELETE - removes item from inventory
Hover over an item + CTRL - Opens Split Stack window


INPUT MOBILE:

OnPlayer:
JOYSTICK - movement
Proximity pick up applied

OnUi:
Hold over item - picks up item from inventory or equip into air
In Air Status: item can be placed with letting go of the finger into any slot and if something is in that slot it will change spots if it can
	       if the item is dropped where there are no slots then the item is dropped on the ground.	
Double Tap - Equips an equipable item and dequips if it is already equiped
Double Tap- Consumes an item
Tap - item description


Scripts:

ItemComponent - used to set an item gameobject its item status
SpawnItem - used for point 8.3 spawning items
PlayerMovement - physics based movement
PlayerInput - Now only holds input and sends data to Player_Movement also if it has a joystick it will switch to joystick inputs instead of keyboard
PlayerPickUp - Created a method that accepts bool inputs for the type of pickups, analyitics for 10.3.2.
PredefinedSpatialProximity - Type of pick up script
TriggerCollisionItem - Type of pick up script
ProxDirectionItemPicker - Type of pick up script
ProxyItemPicker - Type of pick up script
PlayerAttributes - Attributes don't update anymore every frame instead it is called whenever a change to attribute happens, added buffs for task 8.
ItemBaseObject(SO) - the base which all item objects use to be created setting the type, stack type and equip type, added extra atributes for task 8.1. and 7.2
InfItemObject(SO) - item type with no stack limit 
EquipItemObject(SO)- equipable item type, now with durrability
MaxedItemObject(SO) - item type with stack limit it can also be use to not be stacked by setting the max stack to 1
PermamentItem(SO) - item type that is applied on pick up 
InventoryObject(SO) - system which saves the data on the picked up items both for the inventory and equipment, has the ability to split a item slot 
AttributeDisplay - shows the values of player attributes, updates using events from CustomEvents
ButtonUIInteraction - Buttons on ui responsible for opening and closing panels and buttons themselves as well as using shortcuts for them, removed the repeting code and added it into a class it's also responsible for Analytics of 10.3.5.
EquipDisplay - displays the items on equip screen, updates on equipment change, responsible for item degredation on task 6.
InventoryDisplay - displays the items on inventory 
InventoryInteraction - manipulates the graphics side of things as well as some data mostly everything from the point, pre-fetched indexes and repeting objects, also reducing the same code in if-else paths, SplitScreen control, analyitics for 10.3.3., 10.3.4
ItemUiComponent - used to identify some items in equipment and inventory      
SlotComponent - used to identify slots in equipment and inventory 
TooltipWindow - display the tooltip for 7.9.
PlayerAnimator - controls the animation for the player using id instead of string
CustomEvents - holds events for variable change on attributes used to update attributes text
FocusObject - on the upper right and left are trigger where it will trigger focus on an object an unable player input during that time
SplitStackWindow - controlls the amount to split

For analyitics:
AnalyticsCollisionTracker - sends out an event when a player colided with something, once it collides with something it has 5 seconds until it will send out event again
AnalyticsMovementTracker - tracks movement for 10 units it will only repeat 5 times in a single build
AnayliticStartUp - tracks every time the game started


Link to the repository https://github.com/Dero1014/Exordium