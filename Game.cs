using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CatandMouseGame;
public class Game
{
    public static string InputFile = "1.ChaseData.txt";
    public static string OutFile = "1.PursuitLog.txt";

    private readonly int _size;
    private readonly Player _cat;
    private readonly Player _mouse;
    private GameState _state;

    private readonly List<string> _output = new();
    private bool _catchReported = false;

    public Game(int size)
    {
        if (size <= 0 || size > 10000)
        throw new ArgumentOutOfRangeException(nameof(size));
        _size = size;
        _cat = new Player("Cat", size);
        _mouse = new Player("Mouse", size);
        _state = GameState.Start;
    }
    public void Run()
    {
        _output.Add("Cat and Mouse");
        _output.Add("");
        _output.Add("Cat Mouse  Distance");
        _output.Add("-------------------");

        string[] lines = File.ReadAllLines(InputFile);

        for (int i = 1; i < lines.Length; i++)
        {
            string raw = lines[i];
            if (string.IsNullOrWhiteSpace(raw)) continue;

            string[] parts = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            char cmd = parts[0][0];

            if (cmd == 'M' || cmd == 'C')
            {
                int steps = int.Parse(parts[1]);
                DoMoveCommand(cmd, steps);

                if (CheckCatch())
                {
                    _catchReported = true;
                    _state = GameState.End;
                    break;
                }
            }
            else if (cmd == 'P')
            {
                DoPrintCommand();
            }
        }

        _output.Add("-------------------");
        _output.Add("");
        _output.Add("");
        _output.Add("Distance traveled:   Mouse    Cat");
        _output.Add($"                     {_mouse.DistanceTraveled,4}    {_cat.DistanceTraveled,4}");
        _output.Add("");

        if (_catchReported)
            _output.Add($"Mouse caught at: {_mouse.Location}");
        else
            _output.Add("Mouse evaded Cat");

        File.WriteAllLines(OutFile, _output);
    }

    private void DoMoveCommand(char command, int steps)
    {
        switch (command)
        {
            case 'M': _mouse.Move(steps); break;
            case 'C': _cat.Move(steps); break;
        }
    }

    private void DoPrintCommand()
    {
        string catStr = _cat.State == State.NotInGame ? "??" : _cat.Location.ToString();
        string mouseStr = _mouse.State == State.NotInGame ? "??" : _mouse.Location.ToString();

        if (_cat.State == State.NotInGame || _mouse.State == State.NotInGame)
        {
            _output.Add($"{catStr,4} {mouseStr,5}");
        }
        else
        {
            int dist = GetDistance();
            _output.Add($"{catStr,4} {mouseStr,5} {dist,9}");
        }
    }

    private int GetDistance()
    {
        int diff = Math.Abs(_cat.Location - _mouse.Location);
        return Math.Min(diff, _size - diff);
    }

    private bool CheckCatch()
    {
        if (_cat.State != State.Playing || _mouse.State != State.Playing) return false;
        if (_cat.Location == _mouse.Location)
        {
            _cat.State = State.Winner;
            _mouse.State = State.Loser;
            return true;
        }
        return false;
    }
}

