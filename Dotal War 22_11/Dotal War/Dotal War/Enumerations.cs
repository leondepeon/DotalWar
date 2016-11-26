namespace Dotal_War
{
    public enum GameState
    {
        StartMenu,
        Gameplay,
        Options,
    }

    public enum DataType
    {
        #region Vectors

        Position,
        Target,
        SpawnLocation,

        #endregion

        #region Boolians

        IsMoveValid,
        IsSelected,
        IsHoverOver,
        AllowCollision,

        #endregion

        #region floads&intergers

        Speed,
        HealthPoints,
        AlianceID,
        Rotation,
        SpawnTime,
        CurrentTime,

        #endregion

        #region Rendering

        Sprite,
        DrawRectangle,
        HealthRectangle,
        defaultColor,

        #endregion

        #region Misc

        EntityState,
        TargetType,
        TargetIndex,
        SelectionType,
        SpawnRadius,
        #endregion

    }

    public enum EntityState
    {
        Alive,
        Disabled,
        Dead
    }

    public enum TargetType
    {
        Individual,
        Swipe,
        Empty
    }

    public enum SelectionType
    {
        Units,
        Buildings
    }

}
