using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ProfilManager : MonoBehaviour
{
    public Image[] profileImageDisplay;

    private const string userDataFileName = "userData.json";
    private UserData userData;

    private void Start()
    {
        LoadUserData();
        UpdateProfileImage();
    }

    private void LoadUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, userDataFileName);

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            userData = JsonUtility.FromJson<UserData>(jsonData);
        }
        else
        {
            userData = new UserData();
        }
    }

    private void SaveUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, userDataFileName);
        string jsonData = JsonUtility.ToJson(userData);
        File.WriteAllText(path, jsonData);
    }

    public void ChangeProfileImage()
    {
        string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select Profile Image", "", "png,jpg,jpeg");

        if (!string.IsNullOrEmpty(imagePath))
        {
            Texture2D selectedImage = LoadImageFromFile(imagePath);
            if (selectedImage != null)
            {
                Sprite profileSprite = Sprite.Create(selectedImage, new Rect(0, 0, selectedImage.width, selectedImage.height), Vector2.one * 0.5f);
                for(int i = 0; i < profileImageDisplay.Length; i++)
                {
                    profileImageDisplay[i].sprite = profileSprite;
                }
                byte[] imageData = selectedImage.EncodeToPNG();
                userData.profileImageBase64 = System.Convert.ToBase64String(imageData);

                SaveUserData();
            }
        }
    }

    private void UpdateProfileImage()
    {
        if (!string.IsNullOrEmpty(userData.profileImageBase64))
        {
            byte[] imageData = System.Convert.FromBase64String(userData.profileImageBase64);
            Texture2D profileTexture = new Texture2D(2, 2);
            profileTexture.LoadImage(imageData);

            Sprite profileSprite = Sprite.Create(profileTexture, new Rect(0, 0, profileTexture.width, profileTexture.height), Vector2.one * 0.5f);
            for (int i = 0; i < profileImageDisplay.Length; i++)
            {
                profileImageDisplay[i].sprite = profileSprite;
            }
        }
    }

    private Texture2D LoadImageFromFile(string path)
    {
        byte[] data = File.ReadAllBytes(path);
        Texture2D image = new Texture2D(2, 2);
        if (image.LoadImage(data))
        {
            return image;
        }
        return null;
    }
    
}
