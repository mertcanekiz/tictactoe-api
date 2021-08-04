using System;
using System.Linq;
using System.Reflection;
using TicTacToe.Domain.Strategies.WinCondition;

namespace TicTacToe.Domain.Factories
{
    public static class WinCheckerFactory
    {
        public static IWinChecker CreateWinChecker(string name)
        {
            var type = typeof(IWinChecker);
            var winCheckers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => type.IsAssignableFrom(x) && !x.IsInterface);
            var winCheckerType =
                winCheckers.FirstOrDefault(x => x.Name.ToLowerInvariant().StartsWith(name.ToLowerInvariant()));
            return (IWinChecker)Activator.CreateInstance(winCheckerType ?? throw new InvalidOperationException());
        }
    }
}