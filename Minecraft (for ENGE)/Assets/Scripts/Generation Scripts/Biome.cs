using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome
{
    string name;
    public int biomeStepth;
    public int biomeHeight;
    public Block[] blockLayers;

    public Biome(string name, int biomeStepth, int biomeHeight, Block[] blockLayers)
    {
        this.name = name;
        this.biomeStepth = biomeStepth;
        this.biomeHeight = biomeHeight;
        this.blockLayers = blockLayers;
    }

    public static Biome plains = new Biome("plains", 80, 10, new Block[] { new Block(1), new Block(2) });
    public static Biome hills = new Biome("hills", 60, 30, new Block[] { new Block(1), new Block(2) });
    public static Biome mountains = new Biome("mountains", 30, 60, new Block[] { new Block(1), new Block(2) });

    public static Biome[] biomes = new Biome[]{plains,hills,mountains };
}

