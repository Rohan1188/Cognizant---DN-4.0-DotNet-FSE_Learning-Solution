using System;

// Step 2: Define Command Interface
public interface ICommand
{
    void Execute();
}

// Step 5: Implement Receiver Class
public class Light
{
    public void On()
    {
        Console.WriteLine("Light is ON");
    }

    public void Off()
    {
        Console.WriteLine("Light is OFF");
    }
}

// Step 3: Implement Concrete Commands

public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.On();
    }
}

public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.Off();
    }
}

// Step 4: Implement Invoker Class
public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        if (_command != null)
        {
            _command.Execute();
        }
        else
        {
            Console.WriteLine("No command assigned.");
        }
    }
}

// Step 6: Test the Command Implementation
class Program
{
    static void Main(string[] args)
    {
        Light livingRoomLight = new Light();

        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand lightOff = new LightOffCommand(livingRoomLight);

        RemoteControl remote = new RemoteControl();

        // Turn light ON
        remote.SetCommand(lightOn);
        remote.PressButton();

        // Turn light OFF
        remote.SetCommand(lightOff);
        remote.PressButton();
    }
}
