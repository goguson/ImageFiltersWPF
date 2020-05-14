namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IXmlManagmentService
    {
        bool XmlSerialize<T>(T, string destinationPath);
        bool XmlDeserialize<T>(T, string sourcePath);

    }
}
