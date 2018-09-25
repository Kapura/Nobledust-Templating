/*
 *  ScriptableObjectWizard
 *  Author: Max Golden
 *  Created: 23 September 2018
 */

using UnityEditor;

namespace Nobledust.Templating
{
    public class ScriptableObjectWizard : BaseTemplatingWizard
    {
        protected const string TOKEN_OBJECT_MENU_NAME = "$$OBJECT_MENU_NAME$$";
        protected const string TOKEN_OBJECT_FILE_NAME = "$$OBJECT_FILE_NAME$$";

        protected override string TemplateFileName { get { return "ScriptableObject_FileTemplate"; } }
        protected override string InitialNewFileName { get { return "NewScriptableObject"; } }

        public string NewObjectMenuName;
        public string NewObjectFileName;

        [MenuItem("Assets/Create Script/New ScriptableObject...", false, 1)]
        private static void CreateWizard()
        {
            DisplayWizard<ScriptableObjectWizard>("New Scriptable Object", "Generate");
        }

        protected override void Awake()
        {
            base.Awake();
            NewObjectMenuName = "Data/New Object";
            NewObjectFileName = "NewObject.asset";
        }

        protected override void AddEngineTransforms(TemplatingEngine engine)
        {
            base.AddEngineTransforms(engine);
            engine.AddDataTransform(TOKEN_OBJECT_MENU_NAME, NewObjectMenuName);
            engine.AddDataTransform(TOKEN_OBJECT_FILE_NAME, NewObjectFileName);
        }
    }
}
