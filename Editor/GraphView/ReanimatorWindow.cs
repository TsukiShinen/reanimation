using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aarthificial.Reanimation.Editor
{
    public class ReanimatorWindow : EditorWindow
    {
        private ReanimatorGraphView _graphView;

        [MenuItem("Tools/Reanimator")]
        public static void ShowWindow()
        {
            EditorWindow wnd = GetWindow<ReanimatorWindow>();
            wnd.titleContent = new GUIContent("Reanimator");
        }

        private void OnEnable()
        {
            ContructGraphView();
            GenerateToolBar();
        }

        private void ContructGraphView()
        {
            _graphView = new ReanimatorGraphView()
            {
                name = "Reanimator Graph"
            };

            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void GenerateToolBar()
        {
            var toolbar = new Toolbar();

            var nodeCreateButton = new Button(() =>
            {
                _graphView.CreateNode("Node");
            });
            nodeCreateButton.text = "Create Node";
            toolbar.Add(nodeCreateButton);
            rootVisualElement.Add(toolbar);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }

        public void CreateGUI()
        {
            //var root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Reanimator/Editor/GraphView/UI Builder/Reanimator.uxml");
            //rootVisualElement.Add(root.Instantiate());
        }
    }
}
