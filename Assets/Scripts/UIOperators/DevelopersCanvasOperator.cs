using UnityEngine;

public class DevelopersCanvasOperator : MonoBehaviour
{
    [SerializeField] private GameObject startMenuContent;

    public void CloseDevelopersCanvas()
    {
        gameObject.SetActive(false);
        startMenuContent.SetActive(true);
    }

    public void OpenOfficialGDD()
    {
        Application.OpenURL("https://docs.google.com/document/d/1D1X_rQgcWjoudHkWmlUg0B2Kua9KZCN6yfS5T_ZcETk/edit?tab=t.0#heading=h.ska4gce2r2pt");
    }
}