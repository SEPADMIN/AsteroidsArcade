public static class GameState
{
    public static States State { get; set; }

    public enum States
    {
        Inactive, Active
    }

    static GameState()
    {
        State = States.Active;
    }

    public static bool IsActive()
    {
        return (State == States.Active);
    }
}