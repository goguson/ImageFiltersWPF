namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IXmlManagmentService
    {
        bool XmlSerialize<T>(T model, string destinationPath);
        bool XmlDeserialize<T>(T model, string sourcePath);

    }
}
