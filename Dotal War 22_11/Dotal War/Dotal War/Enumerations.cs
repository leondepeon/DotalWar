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

        #region Enumeration

        EntityState,
        TargetType,
        SelectionType,
        CollisionType,

        #endregion

        #region Misc

        TargetIndex,
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

    public enum CollisionType
    {
        Static,
        Dynamic,
        Projectile,
    }

}
