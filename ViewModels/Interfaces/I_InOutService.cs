namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface I_InOutService
    {
        bool CreateDirectory(string path);
        bool RemoveDirectory(string path);
        bool RemoveDirectoryWithFiles(string path);
        bool CopyFile(string path);
    }
}
