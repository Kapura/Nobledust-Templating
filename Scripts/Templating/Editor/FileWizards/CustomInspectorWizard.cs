/*
 *  CustomInspectorWizard
 *  Author: Max Golden
 *  Created: 24 September 2018
 */

using UnityEditor;

namespace Nobledust.Templating
{
    public class CustomInspectorWizard : BaseTemplatingWizard
    {
        protected const string TOKEN_INSPECTED_TYPE = "$$INSPECTED_TYPE$$";

        protected override string TemplateFileName { get { return "CustomInspector_FileTemplate"; } }
        protected override string InitialNewFileName { get { return "NewInspector"; } }

        public string InspectedType;

        [MenuItem("Assets/Create Script/New Custom Inspector...", false, 1)]
        private static void CreateWizard()
        {
            DisplayWizard<CustomInspectorWizard>("New Custom Inspector", "Generate");
        }

        protected override void Awake()
        {
            base.Awake();
            InspectedType = string.Empty;
        }

        protected override void AddEngineTransforms(TemplatingEngine engine)
        {
            base.AddEngineTransforms(engine);
            engine.AddDataTransform(TOKEN_INSPECTED_TYPE, InspectedType);
        }
    }
}