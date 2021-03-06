﻿using DataAccessLibrary.Models;
using HoopStackWebsite.Controllers;
using HoopStackWebsite.Models;
using HoopStackWebsite.Solver;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HoopStackWebsite.Models.Level
{
    public class Level
    {
        [JsonPropertyName("levelNum")]
        public int LevelNum { get; set; } //level number
        [JsonPropertyName("stacks")]
        public List<List<string>> Stacks { get; set; } //stacks of the level
        [JsonPropertyName("instructions")]
        public List<string> Instructions { get; set; } //instructions for the solution
        [JsonPropertyName("error")]
        public string Error { get; set; } //possible variable for reporting bugs in the solver?
        [JsonIgnore]
        public List<List<Move>> Solutions { get; set; }
        [JsonIgnore]
        public List<Move> WrongMoves { get; set; }

        public Level(LevelEntryModel LevelModel)
        {
            //create level from levelentrymodel
            this.LevelNum = LevelModel.LevelNum; //get level number
            this.Stacks = new List<List<string>>();
            this.Instructions = new List<string>();
            this.Solutions = new List<List<Move>>();
            this.WrongMoves = new List<Move>();
            int numStacks = LevelModel.NumStacks; //get the colors for numStacks stacks
            for (int i = 0; i < numStacks; i++)
            {
                string stack = i switch
                {
                    0 => LevelModel.Stack1,
                    1 => LevelModel.Stack2,
                    2 => LevelModel.Stack3,
                    3 => LevelModel.Stack4,
                    4 => LevelModel.Stack5,
                    5 => LevelModel.Stack6,
                    6 => LevelModel.Stack7,
                    7 => LevelModel.Stack8,
                    8 => LevelModel.Stack9,
                    9 => LevelModel.Stack10,
                    _ => "Could not retrieve stack.",
                };
                /*string[] words = stack.Split(",");*/
                string[] words = Regex.Split(stack, @"(?:\s*,\s*)|\s+");
                List<string> temp = new List<string>();
                foreach (var word in words)
                    temp.Add(word);
                this.Stacks.Add(temp);
            }
            //solve the stacks in this constructor or do it another way?
            HoopStackSolver.solveLevel(this);
        }

        public Level(LevelModel LevelModel)
        {
            //create level from LevelModel
            this.LevelNum = LevelModel.LevelNum; //get level number
            this.Stacks = new List<List<string>>();
            this.Instructions = new List<string>();
            this.Solutions = new List<List<Move>>();
            this.WrongMoves = new List<Move>();
            int numStacks = LevelModel.NumStacks; //get the colors for numStacks stacks
            for (int i = 0; i < numStacks; i++)
            {
                string stack = i switch
                {
                    0 => LevelModel.Stack1,
                    1 => LevelModel.Stack2,
                    2 => LevelModel.Stack3,
                    3 => LevelModel.Stack4,
                    4 => LevelModel.Stack5,
                    5 => LevelModel.Stack6,
                    6 => LevelModel.Stack7,
                    7 => LevelModel.Stack8,
                    8 => LevelModel.Stack9,
                    9 => LevelModel.Stack10,
                    _ => "Could not retrieve stack.",
                };
                string[] words = stack.Split(",");
                List<string> temp = new List<string>();
                foreach (var word in words)
                    temp.Add(word);
                this.Stacks.Add(temp);
            }
            //solve the stacks in this constructor or do it another way?
            HoopStackSolver.solveLevel(this);
        }

        public Level() //parameterless constructor needed for json deserialization
        {
            this.LevelNum = 0;
            this.Error = null;
            this.Stacks = new List<List<string>>();
            this.Instructions = new List<string>();
            this.Solutions = new List<List<Move>>();
            this.WrongMoves = new List<Move>();
            this.Stacks = new List<List<string>>();
        }

        public Level(int LevelNum, List<List<string>> Stacks) 
        {
            this.LevelNum = LevelNum;
            this.Error = null;
            this.Stacks = Stacks;
            this.Instructions = new List<string>();
            this.Solutions = new List<List<Move>>();
            this.WrongMoves = new List<Move>();
            this.Stacks = new List<List<string>>();
            HoopStackSolver.solveLevel(this);
        }

        public LevelModel toLevelModel(Level newLevel) //method to convert LevelModel to Level
        {
            LevelModel newLevelModel = new LevelModel(); //make LevelModel from newLevel
            newLevelModel.LevelNum = newLevel.LevelNum;
            newLevelModel.NumStacks = newLevel.Stacks.Count;
            if (newLevel.Stacks.Count >= 1)
                newLevelModel.Stack1 = string.Join(",", newLevel.Stacks[0]);
            if (newLevel.Stacks.Count >= 2)
                newLevelModel.Stack2 = string.Join(",", newLevel.Stacks[1]);
            if (newLevel.Stacks.Count >= 3)
                newLevelModel.Stack3 = string.Join(",", newLevel.Stacks[2]);
            if (newLevel.Stacks.Count >= 4)
                newLevelModel.Stack4 = string.Join(",", newLevel.Stacks[3]);
            if (newLevel.Stacks.Count >= 5)
                newLevelModel.Stack5 = string.Join(",", newLevel.Stacks[4]);
            if (newLevel.Stacks.Count >= 6)
                newLevelModel.Stack6 = string.Join(",", newLevel.Stacks[5]);
            if (newLevel.Stacks.Count >= 7)
                newLevelModel.Stack7 = string.Join(",", newLevel.Stacks[6]);
            if (newLevel.Stacks.Count >= 8)
                newLevelModel.Stack8 = string.Join(",", newLevel.Stacks[7]);
            if (newLevel.Stacks.Count >= 9)
                newLevelModel.Stack9 = string.Join(",", newLevel.Stacks[8]);
            if (newLevel.Stacks.Count >= 10)
                newLevelModel.Stack10 = string.Join(",", newLevel.Stacks[9]);

            return newLevelModel;
        }

        public override string ToString() //turn this level obj into a json format
        {
            return JsonSerializer.Serialize<Level>(this);
        }
    }
}
