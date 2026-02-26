using FrameworkHandler.State;
using Nomad.Core.Events;
using UnityEngine;

public class TestFramework : MonoBehaviour
{
    private IGameEvent<int> _testEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _testEvent = GameEventRegistry.GetEvent<int>("TestEvent", "Test");
        _testEvent.Subscribe(this, OnEventPublished);
    }

    // Update is called once per frame
    void Update()
    {
        _testEvent.Publish(21);
    }

    private void OnEventPublished(in int args)
    {
        Debug.Log("Event published!");
    }
}
