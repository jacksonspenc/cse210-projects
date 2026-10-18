// Eternal Quest Coding Assignment
//
// I exceeded the requirements in a few ways. I added a few different goal 
// types as they are easy to develop using polymorphism. One of them is to 
// track bad habits, and the other one is progress goals where you gain points 
// for every unit that you complete for more long term goals. 
// 
// I have been talking about points because I made a points system in order to 
// gamify the program and make it more fun to track progress. It comes with a 
// progress bar and a different title for each time you rank up. 
//  
// I also added small features like a 10 percent change to double points, and 
// milestones for every 10 goals that you record.
using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}