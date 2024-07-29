using UnityEngine;

public class b : a
{
    protected  override void Launch()
    {
        base.Launch();
        Debug.LogError("b launch");
        //OnLaunch();
    }


    protected override void OnLaunch()
    {
        base.OnLaunch();
        Debug.LogError("b OnLaunch");
    }
}

