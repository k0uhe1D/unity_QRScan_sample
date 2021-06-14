using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
using ZXing;

public class SampleQRReader : MonoBehaviour
{

    [SerializeField] private RawImage _camPreview;
    [SerializeField] private Text _scanResult;

    [SerializeField] private Text _monoUsedSize;
    [SerializeField] private Text _monoReservedSize;
    [SerializeField] private Text _unityUsedSize;
    [SerializeField] private Text _unityReservedSize;

    WebCamTexture _webCam;

    void Awake()
    {

        //  権限取得
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
        Debug.Log(Application.HasUserAuthorization(UserAuthorization.WebCam));
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
        }
        else
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices == null || devices.Length == 0)
            {
                Debug.Log("err");
            }
            else
            {
                //WebCamTexture(カメラの指定,スクリーンサイズ,スクリーンサイズ,フレームレート)
                _webCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 30);
                //cam preview
                _camPreview.texture = _webCam;
                //cam start
                _webCam.Play();
            }

        }
    }

    static public string Read(WebCamTexture tex)
    {
        BarcodeReader reader = new BarcodeReader();
        int w = tex.width;
        int h = tex.height;
        var pixel32s = tex.GetPixels32();
        var r = reader.Decode(pixel32s, w, h);
        if (r != null)
            return r.Text;
        else
            return "error";
    }

    void Update()
    {

        if (_webCam != null)
        {
            _scanResult.text = "code: " + Read(_webCam);
        }
        // _monoUsedSize = Profiler.GetMonoUsedSizeLong() / 1024f / 1024f;
        // _monoReservedSize = Profiler.GetMonoHeapSizeLong() / 1024f / 1024f;
        // _unityUsedSize = Profiler.GetTotalAllocatedMemoryLong() / 1024f / 1024f;
        // _unityReservedSize = Profiler.GetTotalReservedMemoryLong() / 1024f / 1024f;
    }







}