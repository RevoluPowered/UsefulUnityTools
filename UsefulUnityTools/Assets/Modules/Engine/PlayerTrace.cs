using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class contains ray trace information.
/// It is designed so you can check if the hit is valid.
/// This is fine to use.
/// </summary>
public class PlayerTrace
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNS.Trace"/> struct.
    /// </summary>
    /// <param name="hit">If set to <c>true</c> hit.</param>
    /// <param name="info">If set to <c>true</c> info.</param>
    public PlayerTrace(bool hit, RaycastHit info)
    {
        m_hit = hit;
        m_info = info;
        m_layerName = "";
    }

    /// <summary>
    /// Allow empty traces.
    /// </summary>
    public PlayerTrace()
    { }

    public bool m_hit;
    public RaycastHit m_info;
    public string m_layerName;
}

