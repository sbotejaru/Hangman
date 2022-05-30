using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMA2
{
    public class User
    {
        public String Username { get; set; }
        public String AvatarPath { get; set; }
        public int AvatarIndex { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
    }
}