/*
 *  CustomPropertyDrawerWizard
 *  Author: Max Golden
 *  Created: 24 September 2018
 */

using UnityEditor;

namespace Nobledust.Templating
{
    public class CustomPropertyDrawerWizard : BaseTemplatingWizard
    {
        protected const string TOKEN_PROPERTY_TYPE = "$$PROPERTY_TYPE$$";

        protected override string TemplateFileName { get { return "CustomPropertyDrawer_FileTemplate"; } }
        protected override string InitialNewFileName { get { return "NewPropertyDrawer"; } }

        public string PropertyType;

        [MenuItem("Assets/Create Script/New Custom Property Drawer...", false, 1)]
        private static void CreateWizard()
        {
            DisplayWizard<CustomPropertyDrawerWizard>("New Custom Property Drawer", "Generate");
        }

        protected override void Awake()
        {
            base.Awake();
            PropertyType = string.Empty;
        }

        protected override void AddEngineTransforms(TemplatingEngine engine)
        {
            base.AddEngineTransforms(engine);
            engine.AddDataTransform(TOKEN_PROPERTY_TYPE, PropertyType);
        }
    }
}