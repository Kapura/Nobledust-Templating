/*
 *  EditorScriptWizard
 *  Author: Max Golden
 *  Created: 23 September 2018
 */

using UnityEditor;

namespace Nobledust.Templating
{
    public class EditorScriptWizard : BaseTemplatingWizard
    {
        [MenuItem("Assets/Create Script/New Editor Script...", false, 3)]
        private static void CreateWizard()
        {
            DisplayWizard<EditorScriptWizard>("New Editor Script", "Generate");
        }

        protected override string TemplateFileName { get { return "EditorScript_FileTemplate"; } }
        protected override string InitialNewFileName { get { return "NewEditorScript"; } }
    }
}
