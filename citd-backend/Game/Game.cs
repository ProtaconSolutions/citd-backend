using System;
using System.Collections.Generic;
using System.Linq;
using Citd.Roslyn;

namespace Citd.Game
{
    public class Game
    {
        private readonly Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();
    
        private readonly Dictionary<Player, string> _codes = new Dictionary<Player, string>();

        private static readonly string Quest = "Create static class Math with static method Min that receives 3 integeres as inputs and returns the smallest number as result. You can reference only system assembly";

        private static readonly TestFixture TestFixture  = new TestFixture
        {
            TypeName = "Math",
            MethodName = "Min",
            Tests = new List<Test>
                {
                    Test.OfResultType(typeof(int))
                        .WithInput(0,1,2)
                        .WithExpected(0),
                    Test.OfResultType(typeof(int))
                        .WithInput(3,2,1)
                        .WithExpected(1),
                    Test.OfResultType(typeof(int))
                        .WithInput(0,-1,1)
                        .WithExpected(-1),
                    Test.OfResultType(typeof(int))
                        .WithInput(-1,-1,-1)
                        .WithExpected(-1),
                    Test.OfResultType(typeof(int))
                        .WithInput(0,0,1)
                        .WithExpected(0)
                }
        };

        public Guid AddPlayer(string nick)
        {
            var id = new Guid();
            _players.Add(id, new Player(nick));
            return id;
        }

        public void SetPlayerReady(Guid playerId)
        {
            _players[playerId].IsReady = true;

            if (_players.Select(x => x.Value).All(player => player.IsReady))
            {
                // Start the game
                // send the quest to all players
            }
        }

        public void NewRound()
        {
            _codes.Clear();
        }

        public void AddCode(Guid playerId, string code)
        {
            _codes.Add(_players[playerId], code);
        }

        public void RunTests()
        {
            foreach (var code in _codes)
            {
                var result = TestRunner.Run(code.Value, TestFixture);

                if (result.ResultType == TestResultType.Ok)
                {
                    // Game done, someone won
                    // Let rest of the test beacaus we might have multiple winners
                }

            }
        }
    }
}