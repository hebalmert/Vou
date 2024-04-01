namespace Vou.API.Helper
{
    public interface IFileStorage
    {
        //Manejo de Imagenes para AZURE Containers

        Task<string> SaveFileAsync(byte[] content, string extention, string containerName);

        Task RemoveFileAsync(string path, string containerName);

        async Task<string> EditFileAsync(byte[] content, string extention, string containerName, string path)
        {
            if (path is not null)
            {
                await RemoveFileAsync(path, containerName);
            }

            return await SaveFileAsync(content, extention, containerName);
        }

        //Para Guardado en Disco
        Task<string> UploadImage(IFormFile imageFile, string ruta, string guid);

        Task<string> UploadImage(byte[] imageFile, string extention, string NameFolder);

        bool DeleteImage(string ruta, string guid);
    }
}
