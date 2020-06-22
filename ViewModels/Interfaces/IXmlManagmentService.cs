namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// interface for declaring serializaion and deserialziation of xml files
    /// </summary>
    public interface IXmlManagmentService
    {
        bool XmlSerialize<T>(T objectToSerialize, string destinationPath);

        bool XmlDeserialize<T>(string sourcePath, out T deserializedObject);
    }
}