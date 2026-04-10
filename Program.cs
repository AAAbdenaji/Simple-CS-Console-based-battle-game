namespace SimpleGameCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Simple CS Game. ");
            List<player> players = new List<player>();
            bool IsSelecting = true;
            while (IsSelecting)                 
            {
                Console.WriteLine("Add a Player?:(Y/N)");
                string AoN = Console.ReadLine(); 
                if (AoN == "Y")
                {
                    Console.WriteLine("Name?:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Attack Power?:");
                    int ap =  Convert.ToInt32(Console.ReadLine());
                    player NewPlayer = new player(name, ap);
                    players.Add(NewPlayer);

                }
                else if (AoN == "N")
                {
                    IsSelecting = false;
                }
            }
            if (players.Count < 2)
            {
                Console.WriteLine("Insufficient Player Number.");
                Console.WriteLine("Player Number: {0}", players.Count);
            }
            else
            {
                int round = 0;
                bool playing = (player.NumOP - CountDeadPlayers(players)) > 1;
                while (playing)
                {
                    round += 1;
                    Thread.Sleep(1000);
                    int turns = 0;
                    Console.WriteLine("Round {0}:", round);
                    for (int i = 0; i < players.Count; i++)
                    {
                        
                        if (!players[i].isDead)
                        {
                            turns += 1;
                            Thread.Sleep(750);
                            Console.WriteLine("Turn {0}:", turns);
                            Random rand = new Random();
                            int target = rand.Next(0, players.Count);
                            while (target == i || players[target].isDead || !((player.NumOP - CountDeadPlayers(players)) >= 2))
                            {
                                target = rand.Next(0, players.Count);

                            }
                            players[i].damage(players[target]);
                            Console.WriteLine("{0} has attacked {1}! with an attack power of {2} making their hp {3}", players[i].name, players[target].name, players[i].attkpower, players[target].hp);
                            if (players[target].isDead)
                            {
                                Console.WriteLine("He is Dead!");
                            }
                        }
                    }
                    playing = (player.NumOP - CountDeadPlayers(players)) > 1;
                    if (!playing)
                    {
                        if (CountDeadPlayers(players) == player.NumOP)
                        {
                            Console.WriteLine("No one left alive! Nobody Wins!");
                        }
                        else
                        {
                            for (int i = 0; i < players.Count; i++)
                            {
                                if (!players[i].isDead)
                                {
                                    Console.WriteLine("{0} is the only one left standing! he is the winner!", players[i].name);
                                }
                            }
                        }
                    }
                }
                
            }
            Console.WriteLine("Game Has Ended");
            Console.ReadKey();

        }
        static int CountDeadPlayers(List<player> players)
        {
            int res = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].isDead)
                {
                    res += 1;
                }
            }
            return res;
        }
    }
    class player
    {
        public string name;
        public int hp;
        public static int NumOP = 0;
        public int attkpower;
        public bool isDead;

        public player(string name,int attkpower)
        {
            this.name = name;
            NumOP++;
            this.hp = 100;
            this.isDead = false;
            this.attkpower = attkpower;
        }


        public void damage(player player)
        {
            player.hp -= this.attkpower;
            if (player.hp <= 0)
            {
                player.isDead = true;
            }
        }

    }
}
