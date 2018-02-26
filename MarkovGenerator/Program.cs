
using MarkovSharp.TokenisationStrategies;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarkovGen
{
    public class Program
    {
        static void Main(string[] args)
        {
            var midiModel = new SanfordMidiMarkov(2);
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

            Console.ReadLine();
            //var tracks = midiModel.Walk();
            //foreach (var track in tracks)
            //{
            //    sequenceNew.Add(track);
            //}
            var track = midiModel.Walk().First();
            sequenceNew.Add(track);

            sequenceNew.Save("dubstep2.mid");
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
