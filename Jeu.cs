using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    internal class Jeu
    {
        private Dictionnaire d;
        private Plateau p;
        private Joueur joueur1;
        private Joueur joueur2;
        private int TempsPartie;
/// <summary>
/// constructeur initialise les variables de classe
/// </summary>
/// <param name="d">Dictionnair</param>
/// <param name="p">le plateau</param>
/// <param name="joueur1">le premier joueur</param>
/// <param name="joueur2">le deuxième joueur</param>
        public Jeu(Dictionnaire d, Plateau p, Joueur joueur1, Joueur joueur2)
        {
            this.d = d;
            this.p = p;
            this.joueur1 = joueur1;
            this.joueur2 = joueur2;
            this.TempsPartie = 0;

        }
       /// <summary>
       /// interface du début du jeu crée le dictionnaire pour la partie et donne le temps de la partie
       /// </summary>
        public void AffichageDebut()
        {
            Console.WriteLine(" Le jeu commence ");
            Console.WriteLine("Saisir le nom du joueur 1");
            string nomJoueur1 = Console.ReadLine();
            joueur1.getnom = nomJoueur1;
            Console.WriteLine("Saisir le nom du joueur 2");
            string nomJoueur2 = Console.ReadLine();
            joueur2.getnom = nomJoueur2;
            Console.WriteLine("Avec quelle langue voulez-vous jouer? (FR/EN) ");
            string langue = Console.ReadLine();
            while (langue != "FR" && langue != "EN")
            {
                Console.WriteLine("Cette langue n'existe pas. Veuillez choisir entre FR et EN");
                langue = Console.ReadLine();
            }
            d.getlangue = langue;
            d.Dicoappliquer();
            d.dicosetup();
            Console.WriteLine(d.toString());
            Console.WriteLine("Combien de temps dispose les joueurs pour jouer (en secondes) ?");
            TempsPartie = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

        }
        /// <summary>
        /// Execute un plateu en le construisant aléatoirement
        /// </summary>
        public void exécutionJeuRand()
        {
            int a = 0;
            int b = 0;
            int c = 0;
            do
            {
                Console.WriteLine("Quelle difficulté?");
                a = Convert.ToInt32(Console.ReadLine());
            } while (a < 0 && a > 5);
            Plateau p = new Plateau(null, a);
            p.boardset(a);
            if (a == 1 || a == 2 || a == 3)
            {

                c = 12;
                p.Motrecherche = new string[12];
            }
            else
            {
                c = 28;
                p.Motrecherche = new string[28];
            }

            do
            {

                string s = "a";
                if (p.insertion(d,ref s))
                {
                    p.Motrecherche[b] = s;
                    ++b;
                    
                }

            } while (b < c);
            p.CompleteRand();

            DateTime debut = DateTime.Now;
            bool verifTempsPartie = true;
            int e = 1;
            while (verifTempsPartie == true)
            {
                p.MotATrouver();
                p.toString();

                if (e % 2 == 0)
                {
                    Console.WriteLine("\n Au tour du joueur 2");
                }
                else
                {
                    Console.WriteLine("\n Au tour du joueur 1");
                }
                Console.WriteLine("Quel mot avez vous trouvé ?");
                string mot = Console.ReadLine();
                Console.WriteLine("A quelle ligne commence-t-il ?");
                int ligne = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("A quelle colonne commence-t-il ?");
                int col = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Quelle est sa direction ? exemple: N,SE...");
                string direction = Console.ReadLine();
                Console.Clear();
                bool test = p.Test_Plateau(mot, ligne, col, direction, d); 
                TimeSpan tempsEcoule = DateTime.Now - debut;
                if (test == true)
                {
                    if (e % 2 == 0 &&   !(joueur2.deja_Trouve(mot)) && !(joueur1.deja_Trouve(mot)))
                    {
                        Console.WriteLine("Youpi!! Mot valide");
                        joueur2.Add_Mot(mot);
                        joueur2.Add_Score(a,mot);

                        Console.WriteLine(joueur2.getnom.ToUpper() + ", votre score est de " + joueur2.getscore + " points!"); 

                    }
                    if (e % 2 == 1 && !(joueur1.deja_Trouve(mot)) && !(joueur2.deja_Trouve(mot)))
                    {
                        Console.WriteLine("Youpi!! Mot valide");
                        joueur1.Add_Mot(mot);
                        joueur1.Add_Score(a,mot);

                        Console.WriteLine(joueur1.getnom.ToUpper() + ", votre score est de " + joueur1.getscore + " points!");
                    }


                }
                else
                {
                    Console.WriteLine("Dommage, ce mot n'est pas valide");

                }

                if (tempsEcoule.Seconds >= TempsPartie / 6)         
                {
                    Console.WriteLine(" le temps est écoulé, vous passez la main"); 
                }

                if (tempsEcoule.Seconds > TempsPartie)
                {
                    verifTempsPartie = false;
                    Console.WriteLine("Fin de plateau");
                }
            }
        }
        /// <summary>
        /// Crée un plateau a partir d un fichier et joue une partie dessus
        /// </summary>
        public void executionJeuFile()
        {
            Console.WriteLine("Saisissez le nom du fichier");
            string x = Console.ReadLine();
            p.ToRead(x);
            int a = p.Difficulte;
            
            DateTime debut = DateTime.Now;
            bool verifTempsPartie = true;
            int e = 1;
            while (verifTempsPartie == true)
            {
                p.MotATrouver();
                p.toString();

                if (e % 2 == 0)
                {
                    Console.WriteLine("\n"+"Au tour du joueur 2");
                }
                else
                {
                    Console.WriteLine("\n Au tour du joueur 1");
                }
                Console.WriteLine("Quel mot avez vous trouvez ?");
                string mot = Console.ReadLine();
                Console.WriteLine("A quelle ligne commence-t-il ?");
                int ligne = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("A quelle colonne commence-t-il ?");
                int col = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Quelle est sa direction ? exemple: N,SE...");
                string direction = Console.ReadLine();
                Console.Clear();
                bool test = p.Test_Plateau(mot, ligne, col, direction, d); 
                TimeSpan tempsEcoule = DateTime.Now - debut;
                if (test == true)
                {
                    if (e % 2 == 0 && !(joueur2.deja_Trouve(mot)) && !(joueur1.deja_Trouve(mot)))
                    {
                        Console.WriteLine("Youpi!! Mot valide");
                       
                        joueur2.Add_Mot(mot);
                        joueur2.Add_Score(a,mot);

                        Console.WriteLine(joueur2.getnom.ToUpper() + ", votre score est de " + joueur2.getscore + " points!");

                    }
                    if (e % 2 == 1 && !(joueur2.deja_Trouve(mot)) && !(joueur1.deja_Trouve(mot)))
                    {
                        Console.WriteLine("Youpi!! Mot valide");
                        joueur1.Add_Mot(mot);
                        joueur1.Add_Score(a,mot);

                        Console.WriteLine(joueur1.getnom.ToUpper() + ", votre score est de " + joueur1.getscore + " points!");
                    }


                }
                else
                {
                    Console.WriteLine("Dommage, ce mot n'est pas valide");

                }

                if (tempsEcoule.Seconds >= TempsPartie / 10)         //temps défini au début de la partie à ligne de code 29
                {
                    Console.WriteLine(" le temps est écoulé, vous passez la main");
                    ++e;//p est le joueur entrain de jouer
                }
            
                if (tempsEcoule.Seconds > TempsPartie)
                {
                    verifTempsPartie = false;
                    Console.WriteLine("Temps écoulé!Fin de plateau!");
                }
            }
            






        }
        /// <summary>
        /// Donne le gagnant
        /// </summary>
        /// <returns>un string désignat le vainqueur</returns>
        public string victoire()
        {
            string vainqueur = "";
            string perdant = "";
            int max = 0;
            int min = 0;
            if (joueur1.getscore > joueur2.getscore)
            {
                vainqueur = joueur1.getnom;
                perdant = joueur2.getnom;
                max = joueur1.getscore;
                min = joueur2.getscore;
            }
            else
            {
                vainqueur = joueur2.getnom;
                perdant = joueur1.getnom;
                max = joueur2.getscore;
                min = joueur1.getscore;
            }
            return ("Félicitations, " + vainqueur.ToUpper() + " a gagné avec " + max + " points."+ perdant+" avait "+min+ " points.");

        }
    }
}
