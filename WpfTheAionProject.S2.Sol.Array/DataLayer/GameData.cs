using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;

namespace WpfTheAionProject.DataLayer
{
    /// <summary>
    /// static class to store the game data set
    /// </summary>
    public static class GameData
    {
        public static Player PlayerData()
        {
            return new Player()
            {
                Id = 1,
                Name = "",
                Age = 26,
                JobTitle = Player.Profession.Explorer,
                Race = Character.RaceType.Human,
                Health = 100,
                Lives = 1,
                Sanity = 0,
                LocationId = 0
            };
        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "\tYou have been hired by the Corporation to participate in its latest endeavor, the Aion Project. Your task is to escape the room before the time is up, if you complete the task. Your reward will be all your debts will be cleared and you will recieve a million dollars." +
                "\tIf you accept you will be picked up by someone who is from the Corporation and taken to an undisclosed location."
            };
        }

        public static GameMapCoordinates InitialGameMapLocation()
        {
            return new GameMapCoordinates() { Row = 0, Column = 0 };
        }

        public static Map GameMap()
        {
            int rows = 3;
            int columns = 4;

            Map gameMap = new Map(rows, columns);

            //
            // row 1
            //
            gameMap.MapLocations[0, 0] = new Location()
            {
                Id = 1,
                Name = "Lobby",
                Description = "You wake up in a room it is lightly dimmed, the last thing you remember is getting into the car and being taken to the Corporations location for the escape.",
                Accessible = true,
                ModifySanity = 1,
                Message = "A strange creature walks into the room, he says his name is Jetto the goblin. He begins to tell you the rule for your task, they're are no rules you can use anything nessacary to complete the." + 
                "He tells you if you don't complete your task in the alloted time you will be left here forever. The goblin gives you one last chance to back out and forget all about this." + 
                "if you accept please walk through the door to your right, if you decline go through the door to your left."
            };
            gameMap.MapLocations[0, 1] = new Location()
            {
                Id = 2,
                Name = "Room One",
                Description = "You walk through the door to the right, the door behind you closes and is locked. the room is dark with faintly dimmed light,"+ 
                "you find a lantern and light it what you see is just a dark hall way that seems to go on for miles. You start to walk down the hall way you come across a door infront of you."+ 
                "as there is no where else to go you go through the door. the door closes behind you and disappears, you hear a faint sound of a laugh do you check it out or try to avoid it?",
                Accessible = true,
                ModifySanity = 5,
                Message = "You hear the goblins voice coming from somewhere saying good luck as he laughs"
            };

            //
            // row 2
            //
            gameMap.MapLocations[1, 1] = new Location()
            {
                Id = 3,
                Name = "Room Two",
                Description = "You decided to go check out the sound, it appears to be a young girl playing with something you get closer with the light to see what it is, you finally can make out what it is it's a human skull." + 
                "She looks up at you and stares at you and she stabs you."+ "You are dead FeelsBadMan",
                Accessible = true,
                ModifyLives = 0,
                ModifySanity = 10
                
            };
            gameMap.MapLocations[1, 2] = new Location()
            {
                Id = 4,
                Name = "HallWay Two",
                Description = "You decide to avoide the creepy sound and continue to move forward, It feel you like you have been walking for hours everything looks the same." + 
                "You eventually come accross two doors one to your left and one two your right, you filp a coin if heads you go right, tales you go left." + "The coin lands on heads you enter the room and a random creature starts running at you, you quickly leave the room and close the door." +
                "your hearts racing you quickly go into the door on the left.",
                Accessible = true,
                ModifySanity = 20
            };

            //
            // row 3
            //
            gameMap.MapLocations[2, 0] = new Location()
            {
                Id = 5,
                Name = "Room Three",
                Description = "After you entered the room and calmed down you start to look around and again everything seems the same as the previous hall ways dark" +
                "you are tired you decide to sit down and rest for a moment as your about to sleep you hear the goblins voice.",
                Accessible = true,
                ModifySanity = -5,
                Message = "He laughs and asks hows it going, before he leaves he hands you a flash light and some food and water and then disappears again."
            };
            gameMap.MapLocations[2, 1] = new Location()
            {
                Id = 6,
                Name = "The Tamfasia Galactic Academy",
                Description = "The Tamfasia Galactic Academy was founded in the early 4th galactic metachron. " +
                "You are currently in the library, standing next to the protoplasmic encabulator that stores all " +
                "recorded information of the galactic history.",
                Accessible = true,
                //ModifySanity = 10
            };

            return gameMap;
        }
    }
}
