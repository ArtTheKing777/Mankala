﻿namespace Mankala;

public class Board
{
    private int[] pits;
    private int[] p1MankalaIndices;
    private int[] p2MankalaIndices;
    private int numberOfPits;
    private int numberOfMankalas;

    public Board(int _numberOfPitsPP, int startingStones, int[] p1MI, int[] p2MI)
    {
        //set vars for use later
        numberOfPits = 2*_numberOfPitsPP;
        numberOfMankalas = p1MI.Length + p2MI.Length;
            
        //make the pits
        pits = new int[numberOfPits + numberOfMankalas];

        //set the mankala's (player pits)
        p1MankalaIndices = p1MI;
        p2MankalaIndices = p2MI;
        
        //fill the pits if it is not a player pit (assumption: player pit start empty)
        for (int i = 0; i < pits.Length; i++)
        {
            if (!p1MI.Contains(i) && !p2MI.Contains(i))
            {
                pits[i] = startingStones;
            }
        }
    }

    //give the pits that the player can make a move with (assumption: pits of player are between mankala's(player pits))
    /// <summary>
    /// Get the pits that belong to the player
    /// </summary>
    /// <param name="p">the player where you want to pits from</param>
    public int[] GetPitsOfPlayer(Player p)
    {
        int[] pitsOfPlayer = new int[(numberOfPits) / 2];//assume: that both player have half
        int j = 0;
        bool flip = Player.P1 == p;
        
        for (int i = 0; i < pits.Length; i++)
        {
            if (p1MankalaIndices.Contains(i) || p2MankalaIndices.Contains(i))
            {
                flip = !flip;
                continue;
            }
            
            if (flip)
            {
                pitsOfPlayer[j] = pits[i];
                j++;
            }
        }
        return pitsOfPlayer;
    }

    /// <summary>
    /// get a player's mankala's
    /// </summary>
    /// <param name="p">the player where you want to pits from</param>
    public int[] GetPlayerMankalas(Player p)
    {
        int[] indices;
        if (Player.P1 == p) {indices = p1MankalaIndices; }
        else{indices = p2MankalaIndices;}
        
        int[] mankalas = new int[indices.Length];
       
        for (int i = 0; i < mankalas.Length; i++) { mankalas[i] = pits[indices[i]]; }

        return mankalas;
    }
    
    /// <summary>
    /// get a player's mankala's indincies
    /// </summary>
    /// <param name="p">the player where you want to pits from</param>
    public int[] GetPlayerMankalasIndencies(Player p)
    {
        int[] indices;
        if (Player.P1 == p) {indices = p1MankalaIndices; }
        else{indices = p2MankalaIndices;}
        return indices;
    }

    /// <summary>
    /// move an amount
    /// </summary>
    public void MoveAmount(int from, int to, int amount)
    {
        pits[from%pits.Length] -= amount;
        pits[to%pits.Length] += amount;
    }
}