using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLinkOnClick : MonoBehaviour
{
    // The URL you want to open
    public string url = "https://www.example.com";

    // Method to open the link
    public void OpenExternalLink()
    {
        Application.OpenURL(url);
    }
}
