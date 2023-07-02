namespace Manifold.IO
{
    /// <summary>
    /// Interface which enables types to define how it is serialized to and 
    /// deserialized from a bit stream.
    /// </summary>
    public interface IBitSerializable
    {
        /// <summary>
        /// Deserializes this type from a bit stream.
        /// </summary>
        /// <param name="reader">The bit reader to read from.</param>
        void Deserialize(BitStreamReader reader);

        /// <summary>
        /// Serializes this type to a bit stream.
        /// </summary>
        /// <param name="writer">The bit writer to write to.</param>
        void Serialize(BitStreamWriter writer);
    }
}
