namespace Manifold.Text.Tables
{
    [System.Flags]
    public enum TableInferenceMode
    {
        None = 0,
        InferWithoutName,
        InferWithName,

        // Table name
        NoTableName = 1 << 0,
        SingleEntryTableName = 1 << 1,
        FirstEntryTableName = 1 << 2,
    }
}
