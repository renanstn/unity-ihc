﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public TextAsset m_TextFile;
    public Text m_TextUI;
    public int m_RoleSize = 36;
    public int m_PersonSize = 22;
    public int m_SpaceBetweenLines = 40;
    public float m_RollSpeed = 2.0f;

    private void Start()
    {
        string[] lines = m_TextFile.text.Replace("\r", "").Split('\n');
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (line.Contains("t:"))
            {
                line = line.Replace("t:", "").ToUpperInvariant();
                line = $"<b><size={m_RoleSize}>{line}</size></b>";
            }
            else
            {
                line = $"<b><size={m_PersonSize}>{line}</size></b>";
            }

            builder.Append(line);
            builder.Append($"<size={m_SpaceBetweenLines}>\n</size>");
        }

        m_TextUI.text = builder.ToString();
        Canvas.ForceUpdateCanvases();
    }

    private void Update()
    {
        transform.Translate(Vector3.up * m_RollSpeed * Time.deltaTime);
    }
}
