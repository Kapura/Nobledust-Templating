using System.Collections.Generic;

namespace Nobledust.Templating
{
    public delegate string DataTransformFunction();

    public class DataTransform
    {
        public readonly string SearchString;
        public readonly DataTransformFunction TransformFunction;

        public DataTransform(string searchString, DataTransformFunction transformFunction)
        {
            SearchString = searchString;
            TransformFunction = transformFunction;
        }
    }

    public class TemplatingEngine
    {
        public readonly string InputText;

        private List<DataTransform> dataTransforms = new List<DataTransform>();

        private string transformedText = string.Empty;
        private bool dirty = true;

        public TemplatingEngine(string inputText)
        {
            InputText = inputText;
        }

        public void AddDataTransform(DataTransform dataTransform)
        {
            dataTransforms.Add(dataTransform);
            dirty = true;
        }

        public void AddDataTransform(string searchString, DataTransformFunction transformFunction)
        {
            AddDataTransform(new DataTransform(searchString, transformFunction));
        }

        public void AddDataTransform(string searchString, string replaceString)
        {
            AddDataTransform(searchString, () => { return replaceString; });
        }

        public string GetTransformedText()
        {
            if (dirty)
            {
                transformedText = InputText;
                Dictionary<string, string> outstringsByHash = new Dictionary<string, string>();

                foreach (DataTransform transform in dataTransforms)
                {
                    string hashString = transform.SearchString.GetHashCode().ToString();
                    transformedText = transformedText.Replace(transform.SearchString, hashString);
                    outstringsByHash[hashString] = transform.TransformFunction();

                    transformedText = transformedText.Replace(hashString, transform.TransformFunction());
                }

                foreach (KeyValuePair<string, string> kvp in outstringsByHash)
                {
                    transformedText.Replace(kvp.Key, kvp.Value);
                }

                dirty = false;
            }

            return transformedText;
        }
    }
}