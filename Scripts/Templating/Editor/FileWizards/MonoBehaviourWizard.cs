/*
 *  MonoBehaviourWizard
 *  Author: Max Golden
 *  Created: 23 September 2018
 */

using UnityEditor;

namespace Nobledust.Templating
{
    public class MonoBehaviourWizard : BaseTemplatingWizard
    {
        [MenuItem("Assets/Create Script/New MonoBehaviour...", false, -10)]
        private static void CreateWizard()
        {
            DisplayWizard<MonoBehaviourWizard>("New MonoBehaviour", "Generate");
        }

        protected override string TemplateFileName { get { return "MonoBehaviour_FileTemplate"; } }
        protected override string InitialNewFileName { get { return "NewMonoBehaviour"; } }
    }
}
