using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase
{
	protected GameObject gameObject;
    

	public virtual void Initialize()
	{
		gameObject = GameObjectExtension.Find("Player");
	}

    public virtual void UpdateByFrame()
	{
		
	}

    public virtual void UpdateByFixed()
	{
		
	}
}

