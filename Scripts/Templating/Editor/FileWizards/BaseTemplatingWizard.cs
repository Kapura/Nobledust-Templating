/*
 *  BaseTemplatingWizard
 *  Author: Max Golden
 *  Created: 23 September 2018
 */

using System.IO;
using UnityEngine;
using UnityEditor;

namespace Nobledust.Templating
{
    public abstract class BaseTemplatingWizard : ScriptableWizard
    {
        private const string DATE_FORMAT_STRING = "d MMMM yyyy";

        // Common substitution tokens
        protected const string TOKEN_AUTHOR     = "$$AUTHOR$$";
        protected const string TOKEN_DATE       = "$$DATE$$";
        protected const string TOKEN_NAMESPACE  = "$$NAMESPACE$$";
        protected const string TOKEN_CLASSNAME  = "$$CLASSNAME$$";

        protected static string PROJECT_NAME
        {
            get
            {
                string[] path = Application.persistentDataPath.Split('/');
                return path[path.Length - 1];
            }
        }

        // PlayerPrefs keys for caching info
        private static string PPKEY_AUTHOR      { get { return PROJECT_NAME + "_authorkey"; } } 
        private static string PPKEY_NAMESPACE   { get { return PROJECT_NAME + "_namespacekey"; } }

        protected abstract  string TemplateFileName     { get; }  // Used to locate TemplateFile, if not populated.
        protected abstract  string InitialNewFileName   { get; }
        protected virtual   string NewFileExtension     { get { return ".cs"; } }
        protected virtual   string DefaultNamespace     { get { return PROJECT_NAME; } }  // Used if there is no cached namespace

        public TextAsset TemplateFile;
        [Space]

        // Common fields
        public string NewFileName;
        public string Author;
        public string Namespace;
        public string OutputDirectory;

        protected virtual void Awake()
        {
            GetTemplateFile();

            Author = PlayerPrefs.GetString(PPKEY_AUTHOR, "Author Name");
            Namespace = PlayerPrefs.GetString(PPKEY_NAMESPACE, PROJECT_NAME);
            OutputDirectory = GetInitalOutputDirectory();
            NewFileName = InitialNewFileName;
        }

        // Any wizard that adds fields is expected to extend this function.
        protected virtual void AddEngineTransforms(TemplatingEngine engine)
        {
            engine.AddDataTransform(TOKEN_AUTHOR, Author);
            engine.AddDataTransform(TOKEN_DATE, System.DateTime.Now.ToString(DATE_FORMAT_STRING));
            engine.AddDataTransform(TOKEN_NAMESPACE, Namespace);
            engine.AddDataTransform(TOKEN_CLASSNAME, NewFileName);
        }

        protected virtual void OnWizardCreate()
        {
            if (TemplateFile == null && !GetTemplateFile())
            {
                Debug.LogError("Couldn't locate template file.");
                return;
            }

            PlayerPrefs.SetString(PPKEY_AUTHOR, Author);
            PlayerPrefs.SetString(PPKEY_NAMESPACE, Namespace);

            string filePath = OutputDirectory + "/" + NewFileName + NewFileExtension;

            try
            {
                if (!Directory.Exists(OutputDirectory))
                    Directory.CreateDirectory(OutputDirectory);

                TemplatingEngine engine = new TemplatingEngine(TemplateFile.text);
                AddEngineTransforms(engine);

                File.WriteAllText(filePath, engine.GetTransformedText());

                AssetDatabase.ImportAsset(filePath, ImportAssetOptions.ForceUpdate);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return;
            }

            Debug.LogFormat("Created file {0}", filePath);
        }

        protected virtual string GetInitalOutputDirectory()
        {
            if (Selection.activeObject == null)
                return "Assets";

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return File.Exists(path) ? Path.GetDirectoryName(path) : path;
        }

        protected virtual bool GetTemplateFile()
        {
            string[] guids = AssetDatabase.FindAssets(TemplateFileName);

            if (guids.Length < 1)
            {
                Debug.LogErrorFormat("Coultn't find template text file named: '{0}'", TemplateFileName);
                return false;
            }

            TemplateFile = AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(guids[0]));

            return (TemplateFile == null) ? false : true;
        }
    }
}
