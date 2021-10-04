using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace RFG.Audio
{
  [CustomEditor(typeof(AudioManager))]
  public class AudioManagerControls : Editor
  {
    private VisualElement rootElement;
    private Editor editor;

    public void OnEnable()
    {
      rootElement = new VisualElement();

      var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/RFGAudio/Editor/AudioManagerControls/AudioManagerControls.uss");
      rootElement.styleSheets.Add(styleSheet);
    }

    public override VisualElement CreateInspectorGUI()
    {
      UnityEngine.Object.DestroyImmediate(editor);
      editor = Editor.CreateEditor(this);
      IMGUIContainer container = new IMGUIContainer(() =>
      {
        if (target)
        {
          OnInspectorGUI();
        }
      });
      rootElement.Add(container);

      var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/RFGAudio/Editor/AudioManagerControls/AudioManagerControls.uxml");
      visualTree.CloneTree(rootElement);

      AudioManager AudioManagerTarget = (AudioManager)target;

      Button generateAudioSourceButton = rootElement.Q<Button>("generate-audio-source");
      generateAudioSourceButton.clicked += () =>
      {
        AudioManagerTarget.GenerateAllAudioSources();
      };

      return rootElement;
    }
  }
}