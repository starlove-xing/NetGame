using UnityEngine;
using System.Collections;

public class JoinDTO
{
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public JoinDTO()
    {

    }
    public JoinDTO(string name)
    {
        Name = name;
    }
    public JoinDTO(string name, Sprite icon)
    {
        Name = name;
        Icon = icon;
    }
}
