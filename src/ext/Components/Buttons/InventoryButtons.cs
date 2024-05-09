using Discord;

namespace RPGBot.Components.Buttons;

public class InventoryButton : ButtonBuilder
{
    public InventoryButton()
    {
        Label = "Inventory";
        Style = ButtonStyle.Primary;
        CustomId = "inventoryButton";
    }
}
public class InventoryBackButton : ButtonBuilder
{
    public InventoryBackButton()
    {
        Label = "Back";
        Style = ButtonStyle.Primary;
        CustomId = "inventoryBackButton";
    }
}
public class UseItemButton : ButtonBuilder
{
    public UseItemButton()
    {
        Label = "Use Item";
        Style = ButtonStyle.Success;
        CustomId = "useItemButton";
    }
}
public class DropItemButton : ButtonBuilder
{
    public DropItemButton()
    {
        Label = "Drop Item";
        Style = ButtonStyle.Danger;
        CustomId = "dropItemButton";
    }
}
