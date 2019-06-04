using System.Xml;
using System.Xml.Serialization;

namespace Invoice_handler.Utilities
{
    /// <summary>
    /// Serializer
    /// </summary>
    public static class Serializer
    {
        #region Public methods

        /// <summary>
        /// DeSerialize the result from Xml.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="location">The source location</param>
        /// <returns>the deserialized object</returns>
        public static T DeSerialize<T>(string location) where T : class
        {
            T settings = null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = XmlReader.Create(location))
            {
                settings = serializer.Deserialize(reader) as T;
            }

            return settings;
        }

        /// <summary>
        /// Serialize the result to Xml.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="settings">The serializable object</param>
        /// <param name="location">The target location</param>
        public static void Serialize<T>(T settings, string location) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (XmlWriter writer = XmlWriter.Create(location))
            {
                serializer.Serialize(writer, settings);
            }
        }

        #endregion
    }

}
