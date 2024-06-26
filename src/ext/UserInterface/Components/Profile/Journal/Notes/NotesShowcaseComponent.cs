﻿using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class NotesShowcaseComponent : ComponentBuilder
{
    public NotesShowcaseComponent()
        => WithButton(new BackToNotesButton());
}