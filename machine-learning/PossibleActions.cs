enum PossibleActions
{
    KeepClock = 0,
    IncreaseClock = 1,

    LowerClock = 2,
}

static class PossibleActionsExtensions
{
    public static PossibleActions GetFromNumber(int action)
    {
        return action switch
        {
            1 => PossibleActions.IncreaseClock,
            2 => PossibleActions.LowerClock,
            _ => PossibleActions.KeepClock
        };
    }
}