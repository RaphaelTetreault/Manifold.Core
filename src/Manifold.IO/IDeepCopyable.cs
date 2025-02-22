namespace Manifold.IO;

/// <summary>
///     
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDeepCopyable<T>
{
    T CreateDeepCopy();
}
