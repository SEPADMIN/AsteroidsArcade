public static class PlayerState
{
    public static States State { get; set; }

    public enum States
    {
        Dead, Alive
    }

    static PlayerState()
    {
        State = States.Alive;
    }

    public static bool IsActive()
    {
        return (State == States.Alive);
    }
}