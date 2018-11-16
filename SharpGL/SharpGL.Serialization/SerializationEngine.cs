using System;
using System.IO;
using System.Linq;

using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Collections;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections.Generic;

namespace SharpGL.Serialization
{
    /// <summary>
    /// The serialization engine is a singleton that allows
    /// scene objects and their contents to be saved and loaded.
    /// </summary>
    public class SerializationEngine
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static SerializationEngine instance = new SerializationEngine();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static SerializationEngine Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SerializationEngine"/> class from being created.
        /// </summary>
        private SerializationEngine()
        {
            //  Compose.
            Compose();
        }

        /// <summary>
        /// Composes this instance.
        /// </summary>
        private void Compose()
        {
            var catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        /// <summary>
        /// Determines whether [is format valid for path] [the specified file format].
        /// </summary>
        /// <param name="fileFormat">The file format.</param>
        /// <param name="path">The path.</param>
        /// <returns>
        ///   <c>true</c> if [is format valid for path] [the specified file format]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsFormatValidForPath(IFileFormat fileFormat, string path)
        {
            //  Get the extension.
            string extension = Path.GetExtension(path).Substring(1);

            //  Go through each file type and see if we
            //  match it.
            foreach (var fileType in fileFormat.FileTypes)
                if (String.Compare(extension, fileType, true) == 0)
                    return true;

            return false;
        }

        /// <summary>
        /// Gets the format for path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private IFileFormat GetFormatForPath(string path)
        {
            return (from f in FileFormats where IsFormatValidForPath(f, path) select f).FirstOrDefault();
        }

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual Scene LoadScene(string path)
        {
            //  Go through every format.
            var fileFormat = GetFormatForPath(path);
            if (fileFormat == null)
                return null;

            //  Load the scene.
            return fileFormat.LoadData(path);
        }

        /// <summary>
        /// Saves the scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual bool SaveScene(Scene scene, string path)
        {
            //  Go through every format.
            var fileFormat = GetFormatForPath(path);
            if (fileFormat == null)
                return true;

            //  Load the scene.
            return fileFormat.SaveData(scene, path);
        }

        /// <summary>
        /// The file formats, composed via MEF.
        /// </summary>
        [ImportMany]
        private IEnumerable<IFileFormat> fileFormats = null;

        /// <summary>
        /// Gets the file formats.
        /// </summary>
        public IEnumerable<IFileFormat> FileFormats
        {
            get { return fileFormats; }
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        public string Filter
        {
            get
            {
                string allSupportedTypes = string.Empty;
                foreach (var f in fileFormats)
                    foreach (var ext in f.FileTypes)
                        allSupportedTypes += "*." + ext + ";";
                return string.Join("|", (from IFileFormat f in fileFormats select f.Filter))
                    + "|All Supported File Types|" + allSupportedTypes;
            }
        }
    }
}