using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    class Program
    {
        /// <summary>
        /// Lance une partie en initialisant une classe jau et en lancant les fonctions importantes de la classe jeu;
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Dictionnaire d = new Dictionnaire( "FR");
            Plateau p = new Plateau(null, 1);
            Joueur j1 = new Joueur("a");
            Joueur j2 = new Joueur("b");
            Jeu Partie = new Jeu(d, p, j1, j2);
            Partie.AffichageDebut();
            for (int j = 0; j < 5; ++j)
            {
                Console.WriteLine(j1.toString());
                Console.WriteLine(j2.toString());
                string a = null;
                do
                {
                    Console.WriteLine("pour ce round fichier ou random?");
                    a = Console.ReadLine();
                } while (a != "fichier" && a != "random");
                if (a == "random")
                {
                    Partie.exécutionJeuRand();
                }
                else
                {
                    Partie.executionJeuFile();
                }
            }
            Console.WriteLine(Partie.victoire());

        }


    }
}
