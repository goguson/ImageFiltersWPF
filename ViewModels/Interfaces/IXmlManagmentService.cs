namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IXmlManagmentService
    {
        bool XmlSerialize<T>(T objectToSerialize, string destinationPath);
        bool XmlDeserialize<T>(string sourcePath, out T deserializedObject);

    }
}
