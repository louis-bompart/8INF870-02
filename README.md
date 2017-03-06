#8INF870-02
##Guide d'utilisation:
Cloner/telecharger le projet et ouvrez le avec Unity 5.5.0f3
Ouvrez la scene "scene"
Lancer la simulation de la scene
Pour jouer utiliser sd ou les fleches directionnels
Pour modifier les parametres du niveau, modifier l'objet niveau dans l'inspecteur en modifiant N pour la taille et X pour le nombre de bonus
```csharp
Cette algo est utilise par l'IA pour calculer son chemin
    /// <summary>
    /// Genere la suite de chemin donnant le meilleur score
    /// NextBonus etant initalise avec le meilleur bonus trouvable depuis la case [0,0]
    /// </summary>
    void ComputePath()
    {
        //Tant qu'on trouve un bonus apres le bonus actuel
        while (nextBonus != null)
        {
            //On l'ajoute au chemin
            path.Add(nextBonus);
            //On cherche le meilleur bonus suivant
            nextBonus.ReachableBonus(path.Count);
            //Il devient l'actuel
            nextBonus = nextBonus.bestNext;
        }
        //On met le score de cote
        maxScore = path.Count;
        //On ajoute la sortie afin de finir le niveau
        path.Add(Map.map[Map.xSize-1, Map.ySize-1]);
        //Quelques logs pour le joueur
        Debug.Log("Path complete");
        Debug.Log("AI Score :"+maxScore);
        //On fait voyager l'IA sur le chemin (Algo de voyage non detaille, il s'aligne sur X puis sur Y par rapport au prochain waypoint de path
        TravelPath();
    }
```
Cette algo donne le score que peut atteindre chaque bonus
```csharp
    /// <summary>
    /// Calcul le meilleur score atteignable depuis ce bonus ainsi que le prochain bonus a viser et le meilleur gain de score de ce bonus.
    /// </summary>
    /// <param name="scorePossible">Le score obtenu avant d'atteindre ce bonus</param>
    /// <returns>Le meilleur score que ce bonus peut atteindre</returns>
    public int ReachableBonus(int scorePossible)
    {
        /*  x,y:                     coordonnees en x du bonus actuel
         *  Map.xSize, Map.ySize:    dimensions maximales de la carte
         *  Map.map:                 la carte en elle meme
         */

        //Si on a deja calculer le gain de score, on renvoie la somme de ce dernier et du score obtenu avant d'atteindre ce bonus.
        if (bestNext != null)
        {
            return bestScoreAddition + scorePossible;
        }
        //Sinon on incremente le score de 1, car ce bonus ajoute 1 au score.
        scorePossible++;
        //Au pire, ce bonus rapporte le score possible plus un.
        int currentNextMax = scorePossible;
        //On parcours la section de la carte situer en dessous et a droite du bonus actuel
        for (int i = x; i < Map.xSize; i++)
        {
            for (int j = y; j < Map.ySize; j++)
            {
                //Si la case actuel n'est pas la case la ou se situe le bonus actuel
                if (Map.map[i, j] != this)
                {
                    //Et qu'il s'agit d'un bonus
                    if (Map.map[i, j] is Bonus)
                    {
                        //Alors on calcul son score possible a partir de notre score possible deja atteint.
                        int tmp = (Map.map[i, j] as Bonus).ReachableBonus(scorePossible);
                        //S'il est superieur au score maximal que l'on connait actuellement
                        if (tmp > currentNextMax)
                        {
                            //On remplace le score maximal par le sien, et on le garde en memoire
                            currentNextMax = tmp;
                            bestNext = (Map.map[i, j] as Bonus);
                        }
                    }
                }
            }
        }
        //On stocke le gain de score afin d'eviter l'execution de la boucle a chaque fois
        bestScoreAddition = currentNextMax - scorePossible + 1;
        //On renvoie le score maximal atteint avec cette case, compte tenu du score offert en entree
        return currentNextMax;
    }
```
