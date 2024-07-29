using System;
using System.Threading.Tasks;
using UnityEngine;

public class a : MonoBehaviour
{
    public event Action OnLaunchCompleted;
    private void Start()
    {
        OnLaunchCompleted += OnLaunch; // ×¢²á»Øµ÷
        Launch();
    }
    protected virtual async void Launch()
    {
        Debug.LogError("a launch");
        Task task = Task.Run(something);
        await Task.WhenAll(task);

        //OnLaunch();
        OnLaunchCompleted?.Invoke();
    }


    protected virtual void OnLaunch()
    {
        Debug.LogError("a OnLaunch");
    }

    public void something()
    {
        Debug.LogError("a something");

    }
}

