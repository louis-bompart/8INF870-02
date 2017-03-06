    /// <summary>
    /// Calcul le meilleur score atteignable depuis ce noeud ainsi que le prochain bonus a viser et le meilleur gain de score de ce bonus.
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
