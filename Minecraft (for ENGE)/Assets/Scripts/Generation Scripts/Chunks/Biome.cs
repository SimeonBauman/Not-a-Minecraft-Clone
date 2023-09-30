using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome
{
    public string name;
    public int biomeStepth;
    public float treeOdds;
    public Block[] blockLayers;

    public Biome(string name, int biomeStepth, int treeOdds, Block[] blockLayers)
    {
        this.name = name;
        this.biomeStepth = biomeStepth;
        this.treeOdds = (float)treeOdds/100;
        this.blockLayers = blockLayers;
    }

    public static Biome Chunk_10 = new Biome("Chunk_10", 80, 10, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_11 = new Biome("Chunk_11", 79, 11, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_12 = new Biome("Chunk_12", 78, 12, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_13 = new Biome("Chunk_13", 77, 13, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_14 = new Biome("Chunk_14", 76, 14, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_15 = new Biome("Chunk_15", 75, 15, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_16 = new Biome("Chunk_16", 74, 16, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_17 = new Biome("Chunk_17", 73, 17, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_18 = new Biome("Chunk_18", 72, 18, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_19 = new Biome("Chunk_19", 71, 19, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_20 = new Biome("Chunk_20", 70, 20, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_21 = new Biome("Chunk_21", 69, 21, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_22 = new Biome("Chunk_22", 68, 22, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_23 = new Biome("Chunk_23", 67, 23, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_24 = new Biome("Chunk_24", 66, 24, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_25 = new Biome("Chunk_25", 65, 25, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_26 = new Biome("Chunk_26", 64, 26, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_27 = new Biome("Chunk_27", 63, 27, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_28 = new Biome("Chunk_28", 62, 28, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_29 = new Biome("Chunk_29", 61, 29, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_30 = new Biome("Chunk_30", 60, 30, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_31 = new Biome("Chunk_31", 59, 31, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_32 = new Biome("Chunk_32", 58, 32, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_33 = new Biome("Chunk_33", 57, 33, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_34 = new Biome("Chunk_34", 56, 34, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_35 = new Biome("Chunk_35", 55, 35, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_36 = new Biome("Chunk_36", 54, 36, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_37 = new Biome("Chunk_37", 53, 37, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_38 = new Biome("Chunk_38", 52, 38, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_39 = new Biome("Chunk_39", 51, 39, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_40 = new Biome("Chunk_40", 50, 40, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_41 = new Biome("Chunk_41", 49, 41, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_42 = new Biome("Chunk_42", 48, 42, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_43 = new Biome("Chunk_43", 47, 43, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_44 = new Biome("Chunk_44", 46, 44, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_45 = new Biome("Chunk_45", 45, 45, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_46 = new Biome("Chunk_46", 44, 46, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_47 = new Biome("Chunk_47", 43, 47, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_48 = new Biome("Chunk_48", 42, 48, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_49 = new Biome("Chunk_49", 41, 49, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_50 = new Biome("Chunk_50", 40, 50, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_51 = new Biome("Chunk_51", 39, 51, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_52 = new Biome("Chunk_52", 38, 52, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_53 = new Biome("Chunk_53", 37, 53, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_54 = new Biome("Chunk_54", 36, 54, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_55 = new Biome("Chunk_55", 35, 55, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_56 = new Biome("Chunk_56", 34, 56, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_57 = new Biome("Chunk_57", 33, 57, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_58 = new Biome("Chunk_58", 32, 58, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_59 = new Biome("Chunk_59", 31, 59, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_60 = new Biome("Chunk_60", 30, 60, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_61 = new Biome("Chunk_61", 29, 61, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_62 = new Biome("Chunk_62", 28, 62, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_63 = new Biome("Chunk_63", 27, 63, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_64 = new Biome("Chunk_64", 26, 64, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_65 = new Biome("Chunk_65", 25, 65, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_66 = new Biome("Chunk_66", 24, 66, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_67 = new Biome("Chunk_67", 23, 67, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_68 = new Biome("Chunk_68", 22, 68, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_69 = new Biome("Chunk_69", 21, 69, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_70 = new Biome("Chunk_70", 20, 70, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_71 = new Biome("Chunk_71", 19, 71, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_72 = new Biome("Chunk_72", 18, 72, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_73 = new Biome("Chunk_73", 17, 73, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_74 = new Biome("Chunk_74", 16, 74, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_75 = new Biome("Chunk_75", 15, 75, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_76 = new Biome("Chunk_76", 14, 76, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_77 = new Biome("Chunk_77", 13, 77, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_78 = new Biome("Chunk_78", 12, 78, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_79 = new Biome("Chunk_79", 11, 79, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_80 = new Biome("Chunk_80", 10, 80, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_81 = new Biome("Chunk_81", 9, 81, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_82 = new Biome("Chunk_82", 8, 82, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_83 = new Biome("Chunk_83", 7, 83, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_84 = new Biome("Chunk_84", 6, 84, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_85 = new Biome("Chunk_85", 5, 85, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_86 = new Biome("Chunk_86", 4, 86, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_87 = new Biome("Chunk_87", 3, 87, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_88 = new Biome("Chunk_88", 2, 88, new Block[] { new Block(2), new Block(2) });
    public static Biome Chunk_89 = new Biome("Chunk_89", 1, 89, new Block[] { new Block(2), new Block(2) });




    public static Biome[] biomes  = new Biome[] { Chunk_10, Chunk_11, Chunk_12, Chunk_13, Chunk_14, Chunk_15, Chunk_16, Chunk_17, Chunk_18, Chunk_19, Chunk_20, Chunk_21, Chunk_22, Chunk_23, Chunk_24, Chunk_25, Chunk_26, Chunk_27, Chunk_28, Chunk_29, Chunk_30, Chunk_31, Chunk_32, Chunk_33, Chunk_34, Chunk_35, Chunk_36, Chunk_37, Chunk_38, Chunk_39, Chunk_40, Chunk_41, Chunk_42, Chunk_43, Chunk_44, Chunk_45, Chunk_46, Chunk_47, Chunk_48, Chunk_49, Chunk_50, Chunk_51, Chunk_52, Chunk_53, Chunk_54, Chunk_55, Chunk_56, Chunk_57, Chunk_58, Chunk_59, Chunk_60, Chunk_61, Chunk_62, Chunk_63, Chunk_64, Chunk_65, Chunk_66, Chunk_67, Chunk_68, Chunk_69, Chunk_70, Chunk_71, Chunk_72, Chunk_73, Chunk_74, Chunk_75, Chunk_76, Chunk_77, Chunk_78, Chunk_79, Chunk_80, Chunk_81, Chunk_82, Chunk_83, Chunk_84, Chunk_85, Chunk_86, Chunk_87, Chunk_88, Chunk_89 };
}

