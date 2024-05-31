﻿using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToInventoryButton : ButtonBuilder
{
    public BackToInventoryButton()
    {
        Label = "Back to inventory";
        Style = ButtonStyle.Primary;
        CustomId = "backToInventoryButton";
    }
}