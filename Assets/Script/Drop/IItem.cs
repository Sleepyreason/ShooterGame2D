using System;
using UnityEngine;

public interface  IItem
{
    public string Name { get; }
    public Sprite UIIcon => throw new NotImplementedException();

}