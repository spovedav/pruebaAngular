namespace NUGET.IO.LOGGER
{
    public class Logger
    {
        private readonly string _logFilePath;

        /// <summary>
        /// Constructor para inicializar la ruta del archivo de log.
        /// </summary>
        /// <param name="basePath">Ruta base donde se guardarán los logs.</param>
        /// <param name="folderName">Nombre de la carpeta donde se almacenarán los logs.</param>
        /// <param name="fileName">Nombre del archivo de log.</param>
        public Logger(string basePath, string folderName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentException("La ruta base no puede estar vacía.", nameof(basePath));
            if (string.IsNullOrWhiteSpace(folderName))
                throw new ArgumentException("El nombre de la carpeta no puede estar vacío.", nameof(folderName));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(fileName));

            // Crear carpeta si no existe
            string fullFolderPath = Path.Combine(basePath, folderName);
            Directory.CreateDirectory(fullFolderPath);

            // Configurar la ruta completa del archivo
            _logFilePath = Path.Combine(fullFolderPath, fileName);
        }

        /// <summary>
        /// Guarda un mensaje en el log.
        /// </summary>
        /// <param name="message">El mensaje que se guardará en el log.</param>
        public void Log(string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

    }
}
