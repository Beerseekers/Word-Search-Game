using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    class Joueur
    {
        private string nom;
        private int score;
        private List<string> motsTrouves;



        /// <summary>
        /// Initialise les valeurs de la classse, par exemple le score à0, la liste mots trouves à une valeur assez grande, ect
        /// </summary>
        /// <param name="nom">
        /// Le nom que va entrer l'utilisateur, qui sera le nom du joueur
        /// 
        /// </param>
        /// 
        /// <return>
        /// rien
        /// </return>
        /// 
        public Joueur(string nom)
        {
            if (nom != null)
            {
                this.nom = nom;
                this.score = 0;
                this.motsTrouves = new List<string>(100);
            }
        }
        public int getscore
        {
            get { return score; }
            set { score = value; }
        }
        public string getnom
        {
            get { return nom; }
            set { nom = value; }
        }
       /// <summary>
       /// Ajoute un mot à la liste des mots trouvés par le joueur.
       /// </summary>
       /// <param name="mot">un mot trouvé par le joueur</param>
       /// <returns>rien</returns>
        
        public void Add_Mot(string mot)
        {

            this.motsTrouves.Add(mot);                          

        }
        /// <summary>
        /// Verifie si le joueur a deja trouve le mot passe en paramètre
        /// </summary>
        /// <param name="mot">mot entré par le joueur</param>
        /// <returns>booléen:vrai si le mot a été trouvé false si non.</returns>
        public bool deja_Trouve(string mot)
        {
            bool present = false;
            for (int i = 0; i < motsTrouves.Count; i++)
            {
                if (mot == motsTrouves[i])
                {
                    present = true;
                    break;
                }
            }
            return present;
        }
        /// <summary>
        /// Augmente le score du joueur en fonction de la taille du mot et de la difficulte du plateau
        /// </summary>
        /// <param name="val">difficulte(/5)</param>
        /// <param name="mot">mot trouve</param>
        public void Add_Score(int val, string mot)
        {
            score += (val*mot.Length);

        }
       /// <summary>
       /// Presente le score et les mots trouves par le joueur
       /// </summary>
       /// <returns>le string presentant les éléments cités ci dessus</returns>
        public string toString()
        {
            string description = nom.ToUpper() + " a un score de " + score + " points" + "\nIl a trouvé les mots :";  
            for (int i = 0; i < motsTrouves.Count; i++)
            {
                description += "\n" + motsTrouves[i];
            }
            description = description + "\n";
            return description;

        }

    }
}
