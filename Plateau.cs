using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Projet
{
    class Plateau
    {
        private char[,] board;
        private int difficulte;
        private string[] motrecherche;
        private Random rand = new Random();
        /// <summary>
        /// constructeur initialise les variables
        /// </summary>
        /// <param name="board">une matrice en entrée</param>
        /// <param name="difficulte">la difficulté entrée par l'utilisateur</param>
        public Plateau(char[,] board, int difficulte)
        {
            this.board = board;
            this.difficulte = difficulte;
            this.motrecherche = null;
        }

        public char[,] Board
        {
            get { return board; }

        }
        public string[] Motrecherche
        {
            get { return motrecherche; }
            set { motrecherche = value; }

        }
        public int Difficulte
        {
            get { return difficulte; }
            set { difficulte = value; }

        }
        /// <summary>
        /// Convertit un fichier csv et l'adapte pour modifier le board la difficulte les mots recherches du plateau
        /// </summary>
        /// <param name="nomfile">Le nom du fichier csv</param>
        public void ToRead(string nomfile)
        {
            string[] tab = new string[15];
            int i = 0;

            try
            {

                foreach (string line in File.ReadLines(nomfile))
                {
                    tab[i] = line;
                    ++i;
                   
                }

            }
            catch (FileNotFoundException f)
            {
                Console.WriteLine("Le fichier n'existe pas " + f.Message);

            }


            string diff = null;
            string ligne = null;
            string col = null;
            string nbmots = null;
            int tot = 0;
            for (int j = 0; j < tab[0].Length; ++j)
            {
                

                if (tab[0][j] != ';')
                {
                    switch (tot)
                    {
                        case 0:
                            diff += tab[0][j];
                            break;
                        case 1:
                            ligne += tab[0][j];
                            break;

                        case 2:
                            col += tab[0][j];
                            break;
                        case 3:
                            nbmots += tab[0][j];
                            break;

                    }
                }
                else
                {
                    tot += 1;
                }


            }
            difficulte = Convert.ToInt32(diff);
            board = new char[Convert.ToInt32(ligne), Convert.ToInt32(col)];
            motrecherche = new string[Convert.ToInt32(nbmots)];
            int k =0;
                for (int j = 0; j < tab[1].Length; ++j)
                {

                    if (tab[1][j] != ';')
                    {

                        motrecherche[k] += tab[1][j];
                        
                    }
                    else
                    {
                        ++k;
                       
                    }


                }
            
            for (int j = 2; j < Convert.ToInt32(ligne)+2; ++j)
            {
                int d = 0;
                    for (int l = 0; l < tab[2].Length; ++l)
                    {
                        if (tab[j][l] != ';')
                        {
                            board[j - 2, d] = tab[j][l];
                        }
                        else
                        {
                            ++d;
                           
                        }
                    }
                
            }

        }
        /// <summary>
        /// Retourne un mot random en prenant d'abord une taille random 
        /// </summary>
        /// <param name="d">le dictionnaire dans lequel prendre le mot</param>
        /// <returns>le mot sous forme de string</returns>

        public string RandWord(Dictionnaire d)
        {

            int x= rand.Next(0, 14);
            int a = rand.Next( d.Dicotrie[x].Length-1);
            return d.Dicotrie[x][a];


        }

        /// <summary>
        /// Donne un taille au board selon la difficulté
        /// </summary>
        /// <param name="diff">la difficulté entrée par l'utilisateur</param>
        public void boardset(int diff)
        {
            switch (diff)
            {
                case 1:
                    board = new char[8, 8];
                    break;
                case 2:
                    board = new char[9, 9];
                    break;
                case 3:
                    board = new char[10, 10];
                    break;
                case 4:
                    board = new char[13, 13];
                    break;
                case 5:
                    board = new char[15, 15];
                    break;
            }

       
        }
        /// <summary>
        /// Genere une direction une ligne et une colonne aléatoires
        /// </summary>
        /// <param name="ligne">une ligne du tableau passée par référence</param>
        /// <param name="col">une colonne passée par référence</param>
        /// <param name="DIR">une directon passée par référence</param>
        public void Dirrandom(ref int ligne, ref int col, ref string DIR)
        {
            int a = rand.Next(0, 7); ;
            switch (a)
            {
                case 0:
                    DIR = "N";
                    break;
                case 1:
                    DIR = "S";
                    break;
                case 2:
                    DIR = "E";
                    break;
                case 3:
                    DIR = "O";
                    break;
                case 4:
                    DIR = "NE";
                    break;
                case 5:
                    DIR = "NO";
                    break;
                case 6:
                    DIR = "SE";
                    break;
                case 7:
                    DIR = "SO";
                    break;
            }
            ligne = rand.Next(0, board.GetLength(0));
            col = rand.Next(0, board.GetLength(1));
        }
      
     /// <summary>
     /// Verifie si un mot rentre dans le board du plateau
     /// </summary>
     /// <param name="dir">une direction</param>
     /// <param name="ligne">la ligne de depart du mot</param>
     /// <param name="col">la colonne de depart du mot</param>
     /// <param name="mot">le mot</param>
     /// <returns>un booléen vrai si il peut être placé faux sinon</returns>     
        public bool checkInsertion(string dir, int ligne, int col, string mot)
        {

            int taille = mot.Length;
            bool flag = true;
            switch (dir)
            {
                case "N":
                    if (ligne - taille  < 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne - i, col] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;
                    }
                case "S":
                    if (ligne + taille > board.GetLength(0))
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne + i, col] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;
                    }

                case "E":
                    if (col + taille > board.GetLength(1))
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne, col + i] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;
                    }



                case "O":
                    if (col - taille  < 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne, col - i] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;
                    }
                case "NE":
                    if (col + taille > board.GetLength(1) || ligne - (taille ) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne - i, col + i] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;
                    }

                case "NO":
                    if (col - taille  < 0 || ligne - (taille ) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne - i, col - i] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;

                    }

                case "SE":
                    if (col + taille > board.GetLength(1) || ligne + taille > board.GetLength(0))
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne + i, col + i] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;
                    }
                case "SO":
                    if (col - taille  < 0 || (ligne + taille > board.GetLength(0)))
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < taille && flag == true; i++)
                        {
                            if (board[ligne + i, col - i] != '\0')
                            {
                                flag = false;
                            }
                        }
                        return flag;
                    }
                default:
                    Console.WriteLine("Pas une vraie direction");
                    return false;
            }


        }
        /// <summary>
        /// Teste l'insertion d'un mot aleatoire à des positions aléatoires.
        /// </summary>
        /// <param name="d">Le dictionnaire</param>
        /// <param name="mot">un mot null passe par reference pour pouvoir garder le mot inséré une fois sorti de la fonction </param>
        /// <returns>booléen true si un mot a été inséré</returns>
        public bool insertion(Dictionnaire d, ref string mot)
        {
            int ligne = 0;
            int col = 0;
            string dir = null;
            
             mot = RandWord(d);
            int compt = 0;
            do {
              
                Dirrandom(ref ligne, ref col, ref dir);
                if (checkInsertion(dir, ligne, col, mot))
                {
                    switch (dir)
                    {
                        case "N":



                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne - i, col] = mot[i];

                               
                            }
                            
                            break;
                        case "S":

                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne + i, col] = mot[i];

                              
                            }
                      
                            break;

                        case "E":


                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne, col + i] = mot[i];

                                
                            }
                           
                            break;


                        case "O":
                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne, col - i] = mot[i];

                                
                            }
                            break;
                        case "NE":
                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne - i, col + i] = mot[i];

                               
                            }
                            break;

                        case "NO":
                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne - i, col - i] = mot[i];

                            }
                            break;

                        case "SE":
                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne + i, col + i] = mot[i];

                              
                            }
                            break;
                        case "SO":
                            for (int i = 0; i < mot.Length; i++)
                            {
                                board[ligne + i, col - i] = mot[i];
                            }
                            break;
                    }

                    return true;
                }
                ++compt;
            } while (compt < 50);
;           
                return false;
        }
        /// <summary>
        /// Remplit les cases vides du tableau par des lettres aléatoires
        /// </summary>
        public void CompleteRand()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < board.GetLength(0); ++i)
            {
                for (int j = 0; j < board.GetLength(1); ++j)
                {
                    if (board[i, j] == '\0')
                    {
                        int a = rand.Next(0, 25);
                        board[i, j] = chars[a];
                    }

                }
            }

        }


/// <summary>
/// Ecrit les mots a trouver sur la console.
/// </summary>
        public void MotATrouver()
        {

            Console.Write("Les mots a trouver sont: ");
            for (int i = 0; i < motrecherche.Length; ++i)
            {
                Console.Write(motrecherche[i] + " ; ");
            }


        }

















/// <summary>
/// Ecris le board dans la console
/// </summary>
        public void toString()
        {
            Console.Write("\n"+" 0  ");
            for(int i = 0; i < board.GetLength(0); ++i)
            {
                for(int j = 0; j < board.GetLength(1); ++j)
                {
                    Console.Write(" "+Board[i, j]+" ");
                }
                if (i+1 < board.GetLength(0))
                {

                    if (i < 9)
                        Console.Write("\n " + (i + 1) + "  ");
                    else
                        Console.Write("\n " + (i + 1) + " ");
                }
            }
            Console.Write("\n    ");
            for (int j = 0; j < board.GetLength(1); ++j)
            {
                Console.Write(" "+j + " ");
            }
        }
        /// <summary>
        /// Vérifie qu'un mot entré par l'utilisateur rentre dans le plateau avec les paramètres rentrés eux ausssi par l'utilisateur.
        /// </summary>
        /// <param name="mot">le mot entré</param>
        /// <param name="ligne">la ligne rentrée</param>
        /// <param name="col">la colonne rentrée</param>
        /// <param name="dir">la direction rentrée</param>
        /// <param name="d">le dictionnaire</param>
        /// <returns>booléen truesi le mot rentre false sinon</returns>
public bool Test_Plateau(string mot, int ligne, int col, string dir, Dictionnaire d)
        {
            bool b = true;
            switch (dir)
            {
                case "E":
                    if (!test_plateau_E(mot, ligne, col))
                        b = false; 
                    break;
                case "N":
                    if (!test_plateau_N(mot, ligne, col))
                        b = false; 
                    break;
                case "S":
                    if (!test_plateau_S(mot, ligne, col))
                        b = false; 
                    break;
                case "O":
                    if (!test_plateau_O(mot, ligne, col))
                        b = false; 
                    break;
                case "NE":
                    if (!test_plateau_NE(mot, ligne, col))
                        b = false; 
                    break;
                case "NO":
                    if (!test_plateau_NO(mot, ligne, col))
                        b = false; 
                    break;
                case "SE":
                    if (!test_plateau_SE(mot, ligne, col))
                        b = false; 
                    break;
                case "SO":
                    if (!test_plateau_SO(mot, ligne, col))
                        b = false; 
                    break;
            }
            
            if (!(d.RechDichoRecursif(d.Dicotrie[mot.Length-2], mot, 0, (d.Dicotrie[mot.Length -2].Length-1))))
            {
                Console.WriteLine("b");
              
                b = false;
            }
            return b;
        }
        /// <summary>
        /// Vérifie qu'un mot de direction sud rentre bien dans le plateau et correspond bien à ce qui est écrit sur le plateau les fonctions suivantes sont similaires mais pour d'autres di
        /// </summary>
        /// <param name="mot">le mot entré</param>
        /// <param name="ligne">la ligne entrée</param>
        /// <param name="col">la colonne entrée</param>
        /// <returns>bool true si le mot rentre</returns>
        public bool test_plateau_S(string mot, int ligne, int col)
        {
            int taille = mot.Length;
            bool flag = true;
            if (ligne + taille > board.GetLength(0) || taille == 1)
            {
            
                return false;
                
            }
            else
            {
                for (int i = 0; i < taille && flag == true; i++)
                {
                    if (board[ligne + i, col] != mot[i])
                    {
                        flag = false;
                        Console.WriteLine("a");
                        
                    }
                }
                return flag;
            }
        }

        public bool test_plateau_E(string mot, int ligne, int col)
        {
            int taille = mot.Length;
            bool flag = true;
            if (col + taille > board.GetLength(1) || taille == 1)
            {
               
                return false;
             
            }
            else
            {
                for (int i = 0; i < taille && flag == true; i++)
                {
                    if (board[ligne, col + i] != mot[i])
                    {
                        flag = false;
                        
                    }
                }
                return flag;
            }
        }

        public bool test_plateau_SE(string mot, int ligne, int col)
        {
            int taille = mot.Length;
            bool flag = true;
            if (col + taille > board.GetLength(1) || ligne + taille > board.GetLength(0) || taille == 1)
            {
                
                return false;
             
            }
            else
            {
                for (int i = 0; i < taille && flag == true; i++)
                {
                    if (board[ligne + i, col + i] != mot[i])
                    {
                        flag = false;
                       
                    }
                }
                return flag;
            }
        }


        public bool test_plateau_NE(string mot, int ligne, int col)
        {
            int taille = mot.Length;
            bool flag = true;
            if (col + taille > board.GetLength(1) || ligne - (taille - 1) < 0 || taille == 1)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < taille && flag == true; i++)
                {
                    if (board[ligne - i, col + i] != mot[i])
                    {
                        flag = false;                    }
                }
                return flag;
            }
        }

        static string motInverse(string mot)
        {
            string nouvmot = "";
            for (int i = 0; i < mot.Length; i++)
            {
                nouvmot = mot[i] + nouvmot;
            }
            return nouvmot;
        }

        public bool test_plateau_N(string mot, int ligne, int col)
        {
            int taille = mot.Length;
           
            
                return (test_plateau_S(motInverse(mot), ligne - taille + 1, col));
            
        }


        public bool test_plateau_O(string mot, int ligne, int col)
        {
            int taille = mot.Length;
           
           
            
            
               return (test_plateau_E(motInverse(mot), ligne, col - taille + 1));
            
        }


        public bool test_plateau_NO(string mot, int ligne, int col)
        {
            int taille = mot.Length;
            
           
            
            
                return (test_plateau_SE(motInverse(mot), ligne - taille + 1, col - taille + 1));
            
        }

        public bool test_plateau_SO(string mot, int ligne, int col)
        {
            int taille = mot.Length;
           
            
               
            
            
            
                return (test_plateau_NE(motInverse(mot), ligne + taille - 1, col - taille + 1));
            
        }

    }
}
