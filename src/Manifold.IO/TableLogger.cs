namespace Manifold.IO;

public static class TableLogger
{
    /// <summary>
    ///     A delegate for analyzing an array of values type <typeparamref name="T"/>,
    ///     where each value has an associated output file.
    /// </summary>
    /// <remarks>
    ///     Created for use with TableLog types.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="outputFilePath"></param>
    public delegate void Analyzer<T>(T[] values, string outputFilePath);

    /// <summary>
    ///     Record of value pair for function which analyzes files with associated file output.
    /// </summary>
    /// <param name="AnalysisFunction"></param>
    /// <param name="FileName"></param>
    public readonly record struct LogFuncFile<T>(Analyzer<T> AnalysisFunction, string FileName);
}
