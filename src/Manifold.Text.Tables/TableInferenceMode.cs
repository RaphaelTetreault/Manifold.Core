namespace Manifold.Text.Tables
{
    [System.Flags]
    public enum TableInferenceMode
    {
        // Table name
        NoTableName = 1 << 0,
        SingleEntryTableName = 1 << 1,
        FirstEntryTableName = 1 << 2,

        //
        // left-adjusted table?
        // has left space?
    }
}
