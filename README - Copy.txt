
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

Item_Component - used to set an item gameobject its item status
Spawn_Item - used for point 8.3 spawning items 
Player_Movement - 1.1. and 1.5. physics based movement
Player_Input - contributes to 1.1. for input as well as the 1.2. with 8 directional sprite sheet animation
Player_PickUp - in charge of picking up items as well as switching between the possible option of switching items (2.1.5)
Predefined_Spatial_Proximity - Option 1, of 2.2.1.
Trigger_Collision_Item - Option 2, 2.1.2.
Prox_Direction_ItemPicker - Option 3, 2.1.3.
Proxy_ItemPicker - Option 4, 2.1.4.
Player_Attributes - in charge of holding and changing the attributes values of the player (9.1., 9.2.)
Item_Base_Object(SO) - the base which all item objects use to be created setting the type, stack type and equip type (8.2.1, 8.2.2, 8.2.2.2, 8.2.2.1.1.)
Inf_Item_Object(SO) - item type with no stack limit (8.2.2.2.1.3)
Equip_Item_Object(SO)- equipable item type (8.2.1.)
Maxed_Item_Object(SO) - item type with stack limit it can also be use to not be stacked by setting the max stack to 1
Permament_Item(SO) - item type that is applied on pick up (8.2.1.)  
Inventory_Object(SO) - system which saves the data on the picked up items both for the inventory and equipment(8.4)
Attribute_Display - shows the values of player attributes (9.2)
Button_UI_Interaction - Buttons on ui responsible for opening and closing panels and buttons themselves as well as using shortcuts for them (3., 9.3, 9.4, 9.5)
Equip_Display - displays the items on equip screen(6.1)
Inventory_Display - displays the items on inventory (6.1.)
Inventory_Interaction - manipulates the graphics side of things as well as some data mostly everything from the point 7. 
Item_Ui_Component - used to identify some items in equipment and inventory (7.1.)       
Slot_Component - used to identify slots in equipment and inventory (7.1.1.)
Tooltip_Window - display the tooltip for 7.9.

Link to the repository https://github.com/Dero1014/Exordium I have sent exordium.sandbox1@gmail.com an invite