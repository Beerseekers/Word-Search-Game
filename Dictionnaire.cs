using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;




namespace Projet
{
    class Dictionnaire
    {

        
        private string langue;
        private List<string> dico;
        private string[][] dicotrie;

       /// <summary>
       /// Constructeur donne des valeurs de "base" aux différentes variables
       /// </summary>
       /// <param name="langue">langue saisie par le joueur</param>
        public Dictionnaire( string langue)
        {

            
            this.dico = new List<string>(28);
            this.langue = langue;
            this.dicotrie = new string[15][];

        }
        public void Dicoappliquer()
        {

            if (langue == "EN")
            {
                dico = dictionaire2();
            }
            else
            {
                dico = dictionaire();
            }

        }
        public string getlangue
        {
            get { return langue; }
            set { langue = value; }
        }
        public List<string> Dico
        {
            get { return dico; }
            set { dico = value; }
        }
        public string[][] Dicotrie
        {
            get { return dicotrie; }
            set { dicotrie = value; }
        }
       
        /// <summary>
        /// Convertit le dictionnaire txt en anglais en une liste de string représentant tous les mots d'une même longueur
        /// </summary>
        /// <returns>La liste en question</returns>
        public List<string> dictionaire2()
        {

            for (int i = 0; i < 28; i++)
            {
                this.dico.Add(File.ReadAllLines(@"MotsPossiblesEN.txt")[i]);
            }
            return dico;
        }




        /// <summary>
        /// Même chose que dictionnaire2 mais pour le francais
        /// </summary>
        /// <returns>la liste</returns>
        public List<string> dictionaire()
        {

            for (int i = 0; i < 28; i++)
            {
                this.dico.Add(File.ReadAllLines(@"MotsPossiblesFR.txt")[i]);
            }
            return dico;
        }
        /// <summary>
        /// Compte le nombre de mot dans un string de mots séparés par des espaces
        /// </summary>
        /// <param name="chaine">un string de mots séparés des espaces issus de la liste de string</param>
        /// <returns>le nombre de mots</returns>
        public int nombreDeMot(string chaine)
        {
            int compteurMot = 0;
            for (int i = 0; i < chaine.Length; i++)
            {
                if (chaine[i] == ' ')
                {
                    compteurMot++;
                }
            }
            return compteurMot;
        }

      /// <summary>
      /// Crée à partir de la liste de string un tableau de tablrau de string contenant tous les mots du dictionnaire le premier indice+2 donne la longueur du mot le duxieme donne la situation du mot parmi les mots de même longueur
      /// </summary>
        public void dicosetup (){
            Console.Write("Chargement");
            for(int i = 0; i < 14; ++i)
            {



                Console.Write(".");
               dicotrie[i] = convertionFichierEntableau(dico[2*i+1]);
             

            }
            Console.Write("\n");


            }
        /// <summary>
        /// Convertit un string de mots séparés par un espace en tableau de string (une "case" par mots) est utilisé pour dicosetup
        /// </summary>
        /// <param name="chaine">Une chaine de caractères de mots avec des espaces les délimitant issu de la liste de string</param>
        /// <returns>un tableau de mots</returns>
        public string[] convertionFichierEntableau(string chaine)
        {


          int a=  nombreDeMot(chaine);
            string[] tab = new string[a];
            int k = 0;
            string motTableau = "";
            for (int i = 0; i < a; i++)
            {
                while (chaine[k] != ' ')                        // chaque mot commence après un ' '
                {
                    motTableau = motTableau + chaine[k];
                    k++;
                }
                tab[i] = motTableau;
                motTableau = "";                                // rinitialisation de motTableau
                k++;                                            // sortir du cas de la case chaine[k]==' '
            }
            return tab;
        }
        /// <summary>
        /// Vérifie de facon récursive et dichotomique si un mot appartient au dictionnaire en fonction de sa longueur
        /// </summary>
        /// <param name="tab">Le tableau de tous les mots de même longueur que le mot en entrée</param>
        /// <param name="mot">le mot dont on veut vérifier la présence</param>
        /// <param name="gauche">l'indice 0 du tableau (au premier appel)</param>
        /// <param name="droite">le dernier indice du tableau(au premier appel)</param>
        /// <returns>vrai si le mot est présent dans le dictionnnaire faux sinon</returns>
        public bool RechDichoRecursif(string[] tab, string mot, int gauche, int droite)
        {
            int milieu = (droite + gauche) / 2;
            if (tab[milieu].CompareTo(mot) == 0)
            {
                return true;
            }
            if (droite == gauche)
            {
                return false;
            }
            if (tab[milieu].CompareTo(mot) == 1)
            {
                return RechDichoRecursif(tab, mot, gauche, milieu);
            }
            else
            {
                return RechDichoRecursif(tab, mot, milieu + 1, droite);
            }
        }


        /// <summary>
        /// Indique la langue choisie par l'utilisateur et le nombre de mots de chaque taille
        /// </summary>
        /// <returns>un string contenant ces informations</returns>
        public string toString()
        {
            string rep = "Vous jouez en " + this.langue;
            int k = 2;
            for (int i = 0; i < 14; i++)
            {
               
                
                    rep += "\nIl y a " + dicotrie[i].Length + " mots de " + k + " lettres";
                    k++;
                
            }
            return rep;
        }



    }
}
