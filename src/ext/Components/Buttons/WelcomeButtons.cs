using Discord;

namespace RPGBot.Components.Buttons;
public class NextButton : ButtonBuilder
{
    public NextButton() 
    {
        Label = "Next";
        Style = ButtonStyle.Success;
        CustomId = "nextButton";
    }
}
public class CreditsButton : ButtonBuilder
{
    public CreditsButton()
    {
        Label = "Credits";
        Style = ButtonStyle.Secondary;
        CustomId = "creditsButton";
    }
}
public class SubmitClassButton : ButtonBuilder
{
    public SubmitClassButton()
    {
        Label = "Submit";
        Style = ButtonStyle.Success;
        CustomId = "submitClassButton";
    }
}
