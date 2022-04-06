
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ColorSchemaWindow : EditorWindow
{
    List<Color> colors;

    string colorPalleteName = "";

    int colorNumber = 0;

    private void Awake()
    {
        colors = new List<Color>();
    }

    [MenuItem("Window/Tools/ColorCollection Editor")]
    static void ShowWindow()
    {
        var window = EditorWindow.GetWindow<ColorSchemaWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        var _colorNumber = EditorGUILayout.IntSlider(colorNumber, 0, 20);

        if (_colorNumber != colorNumber)
        {
            var indexCount = _colorNumber - colorNumber;
            if (indexCount > 0)
                for (var index = 0; index < indexCount; index++)
                {
                    colors.Add(Color.white);
                }
            else
            {
                for (var index = indexCount; index < 0; index++)
                {
                    colors.Remove(colors.LastOrDefault());
                }
            }
            colorNumber = _colorNumber;
        }

        for (var index = 0; index < colorNumber; index++)
        {
            colors[index] = EditorGUILayout.ColorField("New Color", colors[index]);
        }

        colorPalleteName = EditorGUILayout.TextField("Color Pallete Name: ", colorPalleteName);

        if (GUILayout.Button("Save Color"))
        {
            var colorPallete = new ColorCollection();
            colorPallete.Colors = colors;
            AssetDatabase.CreateAsset(colorPallete, "Assets/Scripts/" + colorPalleteName + ".asset");
        }
    }

}
