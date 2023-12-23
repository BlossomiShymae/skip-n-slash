using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomiShymae.SkipNSlash.Core.Models
{
    public record JsonFile
    {
        public required string Name { get; init; }
        public required string Type { get; init; }
        public required string Mtime { get; init; }
    }
}