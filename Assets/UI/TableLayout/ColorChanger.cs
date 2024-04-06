using System.Collections;
using System.Collections.Generic;
using UI.Tables;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color rowColor = Color.white;
    public Color cellColor = Color.white;

    void Start()
    {
        // Find all objects with TableLayout script
        TableLayout[] tableLayouts = FindObjectsOfType<TableLayout>();

        // Change colors for each TableLayout found
        foreach (TableLayout tableLayout in tableLayouts)
        {
            tableLayout.SetRowBackgroundColor(rowColor);
            tableLayout.SetCellBackgroundColor(cellColor);
        }
    }
}
