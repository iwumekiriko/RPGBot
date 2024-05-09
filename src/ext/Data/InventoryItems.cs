using RPGBot.Database;

namespace RPGBot.Data;

public class InventoryItems
{
    public static Item[] GetItems()
    {
        var sword = new Weapon { Id = 201, Name = "Sword", Weight = 2.0, Description = "Cool2 stuff", Damage = 2};
        var knife = new Weapon { Id = 202, Name = "Knife", Weight = 1.0, Description = "Cool stuff", Damage = 1 };
        var ring = new Accessory(3) { Id = 203, Name = "Ring", Weight = 0.2, Description = "Cool2"};
        var scroll = new Accessory { Id = 204, Name = "Scroll", Weight = 0.1, Description = "Cool212313"};

        return [sword, knife, ring, scroll]; //TODO make an item repository service with Create, Delete, Get functions.
    }
}
