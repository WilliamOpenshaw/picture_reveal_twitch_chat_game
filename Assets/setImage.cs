using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class setImage : MonoBehaviour
{
    public string[] imageFiles; // Array to hold the image file names
    public string imagePath; // Path to the image file
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image myImage; // UI image to be covered by the grid

    // Load a .jpg or .png file by adding .bytes extensions to the file
    // and dragging it on the imageAsset variable.
    public TextAsset imageAsset;
    void Start()
    {
        // set image file in resources folder to the source image component of the ui image game object
        myImage = gameObject.GetComponent<Image>(); // Get the Image component attached to this GameObject
        //myImage.sprite = Resources.Load<Sprite>(""); // Load the image from the Resources folder

        // Get all image file names in the Resources folder
        // Note: The Resources folder must be in the Assets directory for this to work
        /*
        imageFiles = System.IO.Directory.GetFiles(Application.dataPath + "/Resources", "*.png");
        //remove everything from path except the file name
        for (int i = 0; i < imageFiles.Length; i++)
        {
            imageFiles[i] = System.IO.Path.GetFileName(imageFiles[i]); // Get the file name only
            Debug.Log("Image file: " + imageFiles[i]); // Log the image file name for debugging
        }
        // set myImage to the first image in the resources folder
        if (imageFiles.Length > 0)
        {
            imagePath = imageFiles[0].Replace(Application.dataPath + "/Resources/", "").Replace(".png", "");
            imagePath = imagePath.Replace("\\", "/"); // Replace backslashes with forward slashes for Unity
            Debug.Log("Image path: " + imagePath); // Log the image path for debugging

            myImage.sprite = Resources.Load<Sprite>(imagePath); // Load the image from the Resources folder
        }
        else
        {
            Debug.Log("No images found in the Resources folder.");
        }
        */
        // load user image from folder
        //string imagePath2 = System.IO.Path.Combine(Application.persistentDataPath, "1.png"); // Path to the image file
        string imagePath2 = System.IO.Path.Combine(Application.dataPath, "1.png"); // Path to the image file
        // load image from the persistent data path
        imagePath2 = imagePath2.Replace("\\", "/"); // Replace backslashes with forward slashes for Unity
        myImage.sprite = Resources.Load<Sprite>(imagePath2); // Load the image from the persistent data path
        Debug.Log(imagePath2);
        Debug.Log(Application.dataPath);

        // Create a texture. Texture size does not matter, since
        // LoadImage will replace with the size of the incoming image.
        //ImageConversion.LoadImage(tex, imageAsset.bytes);
        //Sprite.Create(texture, rect, pivot);
        Texture2D tex2 = LoadPNG(imagePath2);
        Rect rect = new Rect(0, 0, tex2.width, tex2.height); // Create a rectangle with the texture dimensions
        Vector2 pivot = new Vector2(0.5f, 0.5f); // Set the pivot point to the center of the texture
        myImage.sprite = Sprite.Create(tex2, rect, pivot); // Create a sprite from the texture and assign it to the Image component
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
