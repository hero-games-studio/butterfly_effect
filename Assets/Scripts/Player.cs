using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    #region Variables

    private int level;
    private int butterflyCount;
    private int coins;

    private static Player player;

    #endregion

    #region Functions

    #region Constructors

    private Player()
    {
        coins = 0;
        butterflyCount = 0;
        level = 1;
    }

    #endregion

    #region Get Functions

    public int getLevel()
    {
        return level;
    }

    public int getCoins()
    {
        return coins;
    }

    public int getButterflyCount()
    {
        return butterflyCount;
    }

    public static Player getInstance()
    {
        if (player == null)
        {
            player = new Player();
        }
        return player;
    }
    #endregion

    #region Set Functions

    public void setLevel(int level)
    {
        this.level = level;
    }

    public void setCoiıns(int coins)
    {
        this.coins = coins;
    }

    public void setButterflyCount(int butterflyCount)
    {
        this.butterflyCount = butterflyCount;
    }

    #endregion

    #endregion


}
