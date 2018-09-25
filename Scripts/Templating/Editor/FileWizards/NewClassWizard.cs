/*
 *  NewClassWizard
 *  Author: Max Golden
 *  Created: 23 September 2018
 */

using UnityEditor;

namespace Nobledust.Templating
{
    public class NewClassWizard : BaseTemplatingWizard
    {
        [MenuItem("Assets/Create Script/New Class...", false, -8)]
        private static void CreateWizard()
        {
            DisplayWizard<NewClassWizard>("New Class", "Generate");
        }

        protected override string TemplateFileName { get { return "NewClass_FileTemplate"; } }
        protected override string InitialNewFileName { get { return "NewClass"; } }
    }
}
