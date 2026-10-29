using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatandMouseGame;

public class Player
{
    public string Name { get; }
    public int Location { get; private set; } = -1;
    public State State { get; set; } = State.NotInGame;
    public int DistanceTraveled { get; private set; } = 0;

    private readonly int _fieldSize;
    private bool _started = false;

    public Player(string name, int fieldSize)
    {
        Name = name;
        _fieldSize = fieldSize;
    }

    public void Move(int steps)
    {
        if (!_started)
        {
            Location = Normalize(steps);
            State = State.Playing;
            _started = true;
            return;
        }

        Location = Normalize(Location + steps);
        DistanceTraveled += Math.Abs(steps);
    }

    private int Normalize(int pos)
    {
        int zeroBased = (pos - 1) % _fieldSize;
        if (zeroBased < 0) zeroBased += _fieldSize;
        return zeroBased + 1;
    }
}