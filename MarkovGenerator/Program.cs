using MarkovSharp.TokenisationStrategies;
using Sanford.Multimedia.Midi;
using System;
using System.IO;

namespace MarkovGen
{
    public class Program
    {
        static void Main(string[] args)
        {
            var midiModel = new SanfordMidiMarkov(3);
            midiModel.EnsureUniqueWalk = true;

            var files = Directory.GetFiles("music");
            
            int division = 0;
            foreach (var file in files)
            {
                var sequence = new Sequence(file);
                division = sequence.Division;
                midiModel.Learn(sequence);
            }
            var sequenceNew = new Sequence(division);

            var input = Console.ReadLine();
            var tracks = midiModel.Walk(3);
            foreach (var track in tracks)
            {
                sequenceNew.Add(track);
            }

            sequenceNew.Save(input + ".mid");
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
