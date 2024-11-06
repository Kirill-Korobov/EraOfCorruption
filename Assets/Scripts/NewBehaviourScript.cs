using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SFB;

public class NewBehaviourScript : MonoBehaviour
{
    public Image displayImage;

    
    public void PickFile()
    {
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg") // Фільтр для файлів PNG і JPG
        };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Виберіть зображення", "", extensions, false);

        if (paths.Length > 0 && File.Exists(paths[0]))
        {
            LoadImage(paths[0]);
        }
    }

    private void LoadImage(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Відображення на UI Image
        if (displayImage != null)
        {
            displayImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}
