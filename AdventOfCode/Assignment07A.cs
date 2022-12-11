﻿namespace AdventOfCode;

public class Assignment07A : Assignment, IAmAnAssignment
{
    public Assignment07A()
    {
        Load("Input/07.txt");
    }

    private Dir baseDir = new Dir { Name = "/" };
    private Dir currentDir;

    private bool dirmode = false;
//    private bool dirReadMode
    
    private bool skip = true;
    class Dir
    {
        public Dir parent { get; set; }
        public List<Dir> dirs = new List<Dir>();
        public List<Fil> files = new List<Fil>();
        public string Name { get; set; }

        public int Size()
        {
            var total = 0;

            foreach (var file in files)
            {
                total += file.Size;
            }

            foreach (var dir in dirs)
            {
                total += dir.Size();
            }

            return total;
        }

        public List<Dir> Test()
        {
            var list = new List<Dir>();
            
            foreach (var dir in dirs)
            {
                if (dir.Size() <= 100000)
                {
                    list.Add(dir);
                }
                list.AddRange(dir.Test());
            }

            return list;
        }
    }

    class Fil
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }

    protected override void ReadLine(string line)
    {
        if (skip)
        {
            currentDir = baseDir; 
            skip = false;
            return;
        }

        var commands = line.Split(" "); 
        var command = commands[0];

        if (commands[0] == "$")
            dirmode = false;
        
        if (dirmode)
        {
            if (command == "dir")
            {
                currentDir.dirs.Add(new Dir { Name = commands[1], parent = currentDir });
            }
            else
            {
                currentDir.files.Add(new Fil { Name = commands[1], Size = int.Parse(commands[0]) });
            }
        }
        else
        {
            if (commands[1] == "cd")
            {
                if (commands[2] != "..")
                    currentDir = currentDir.dirs.Single(d => d.Name == commands[2]);
                else
                    currentDir = currentDir.parent;
            }
            else if (commands[1] == "ls")
            {
                dirmode = true;
            }
        }

    }
    
    public override void Process()
    {
        var total = 0;

        List<Dir> selected = baseDir.Test();

        Output = selected.Select(s => s.Size()).Sum().ToString();

    }
        
}