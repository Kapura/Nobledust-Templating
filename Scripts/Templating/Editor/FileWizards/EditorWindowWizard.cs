/*
 *  EditorWindowWizard
 *  Author: Max Golden
 *  Created: 24 September 2018
 */

using UnityEditor;

namespace Nobledust.Templating
{
    public class EditorWindowWizard : BaseTemplatingWizard
    {
        protected const string TOKEN_MENU_PATH = "$$MENU_PATH$$";

        protected override string TemplateFileName { get { return "EditorWindow_FileTemplate"; } }
        protected override string InitialNewFileName { get { return "NewEditorWindow"; } }

        public string MenuPath;

        [MenuItem("Assets/Create Script/New Editor Window...", false, 16)]
        private static void CreateWizard()
        {
            DisplayWizard<EditorWindowWizard>("New Editor Window", "Generate");
        }

        protected override void Awake()
        {
            base.Awake();
            MenuPath = "Windows/New Window";
        }

        protected override void AddEngineTransforms(TemplatingEngine engine)
        {
            base.AddEngineTransforms(engine);
            engine.AddDataTransform(TOKEN_MENU_PATH, MenuPath);
        }
    }
}