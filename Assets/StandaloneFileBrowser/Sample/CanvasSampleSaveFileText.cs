using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : MonoBehaviour, IPointerDownHandler {
    //public Text output;
    public string defaultFileName = "File_Name";

    // Sample text data
    //private string _data = "Example text created by StandaloneFileBrowser";

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Broser plugin should be called in OnPointerDown.
    public void OnPointerDown(PointerEventData eventData) {
        DataSaveingScript.SaveJSON();
        
        var bytes = Encoding.UTF8.GetBytes(DataSaveingScript.JSONFileText);
        DownloadFile(gameObject.name, "OnFileDownload", defaultFileName + ".json", bytes, bytes.Length);
    }

    // Called from browser
    public void OnFileDownload() {
        //output.text = "File Successfully Downloaded";
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    // Listen OnClick event in standlone builds
    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        
    }
    public void OnClick() {
        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", defaultFileName, "json");
        if (!string.IsNullOrEmpty(path)) {
            DataSaveingScript.SaveJSON();
            File.WriteAllText(path, DataSaveingScript.JSONFileText);
        }
    }
#endif
}