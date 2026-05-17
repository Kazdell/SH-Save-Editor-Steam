using System.Collections.Generic;

namespace SpaceHaven_Save_Editor.References
{
    public static class IdCollection
    {
        public static List<string> AmountModifiers = new()
        {
            "None", "Very Rare", "Rare", "Medium", "Common", "Very Common"
        };

        public static readonly Dictionary<int, string> DefaultResearchIDs = new()
        {
            {2532, "Scanner"},
            {2533, "Shield Generator"},
            {2539, "Autopsy Table"},
            {2559, "Medical Bed"},
            {2561, "CO2 Producer"},
            {2563, "Arcade Machine"},
            {2564, "Jukebox"},
            {2565, "Solar Panel"},
            {2566, "X2 Power Gen"},
            {2567, "X3 Power Gen"},
            {2568, "Power Capacity Node"},
            {2569, "Item Fabricator"},
            {2570, "Micro-Weaver"},
            {2571, "Assembler"},
            {2572, "Energy Refinery"},
            {2573, "Chemical Refinery"},
            {2574, "Water Collector"},
            {2575, "Advanced Assembler"},
            {2576, "Composter"},
            {2577, "Hypersleep Chamber"},
            {2581, "Basic"},
            {2585, "Advanced"},
            {2586, "Optronic"},
            {2587, "Quantum"},
            {2589, "Weapons Console"},
            {2590, "Shields Console"},
            {2591, "Missle Turret"},
            {2592, "Energy Turret"},
            {2594, "X1 Power Generator"},
            {2595, "X1 Hyperdrive"},
            {2601, "Targeting Jammer"},
            {2612, "Metal Refinery"},
            {2618, "Fabrics"},
            {2619, "Fibers"},
            {2623, "Botony"},
            {2626, "Advanced Nutrition"},
            {2628, "Artificial Meat"},
            {2694, "Optronics Fabricator"},
            {2696, "X1 Couch"},
            {2847, "Enslavement Facility"},
            {3024, "Logistics Robot Station"},
            {3025, "Salvage Robot Station"}
        };

        public static readonly Dictionary<int, string> DefaultAttributeIDs = new()
        {
            {210, "Bravery"},
            {212, "Zest"},
            {213, "Intelligence"},
            {214, "Perception"}
        };

        public static readonly Dictionary<int, string> DefaultSkillIDs = new()
        {
            {1, "Piloting"},
            {2, "Mining"},
            {3, "Botany"},
            {4, "Construct"},
            {5, "Industry"},
            {6, "Medical"},
            {7, "Gunner"},
            {8, "Shielding"},
            {9, "Operations"},
            {10, "Weapons"},
            {12, "Logistics"},
            {13, "Maintenance"},
            {14, "Navigation"},
            {16, "Research"},
            {22, "Fighter Piloting"}
        };

        public static readonly Dictionary<int, string> DefaultTraitIDs = new()
        {
            {191, "Hero"},
            {655, "Wimp"},
            {656, "Clumsy"},
            {1034, "Suicidal"},
            {1035, "Smart"},
            {1036, "Bloodlust"},
            {1037, "Antisocial"},
            {1038, "Needy"},
            {1039, "Fast learner"},
            {1040, "Lazy"},
            {1041, "Hard working"},
            {1042, "Psychopath"},
            {1043, "Peace-loving"},
            {1044, "Iron-willed"},
            {1045, "Spacefarer"},
            {1046, "Confident"},
            {1047, "Neurotic"},
            {1048, "Charming"},
            {1533, "Iron stomach"},
            {1534, "Nyctophilia"},
            {1535, "Minimalist"},
            {1560, "Talkative"},
            {1562, "Gourmand"},
            {2082, "Alien lover"}
        };

        public static readonly Dictionary<int, string> DefaultItemIDs = new()
        {
            {15, "Root Vegetables"},
            {16, "Water"},
            {40, "Ice"},
            {71, "Bio Matter"},
            {125, "Item Fabricator"},
            {158, "Energium"},
            {162, "Infrablock"},
            {173, "Electronic Component"},
            {174, "Energy Rod"},
            {175, "Plastics"},
            {176, "Chemicals"},
            {177, "Fabrics"},
            {178, "Hyperfuel"},
            {179, "Processed Food"},
            {184, "Grow Bed 1"},
            {185, "Grow Bed 2"},
            {623, "Kitchen"},
            {706, "Fruits"},
            {707, "Artificial Meat"},
            {712, "Space Food"},
            {725, "Assault Rifle"},
            {728, "SMG"},
            {729, "Shotgun"},
            {760, "Five-Seven Pistol"},
            {921, "Grow Bed 5"},
            {930, "Techblock"},
            {938, "Chemical Refinery"},
            {1447, "Tools Facility"},
            {1759, "Hull Block"},
            {1871, "CO2 Producer"},
            {1873, "Infra Scrap"},
            {1874, "Soft Scrap"},
            {1876, "Hull Scrap"},
            {1880, "Recycler"},
            {1908, "Assembler"},
            {1919, "Energy Block"},
            {1920, "Superblock"},
            {1921, "Soft Block"},
            {1922, "Steel Plates"},
            {1924, "Optronics Component"},
            {1925, "Quantronics Component"},
            {1926, "Energy Cell"},
            {1932, "Fibers"},
            {1946, "Tech Scrap"},
            {1947, "Energy Scrap"},
            {1954, "Human Corpse"},
            {1955, "Monster Corpse"},
            {1956, "Micro Weaver"},
            {1989, "Optronics Fabricator"},
            {2002, "Advanced Assembler"},
            {2010, "Water Purifier"},
            {2053, "Medical Supplies"},
            {2058, "IV Fluid"},
            {2451, "Water Collector"},
            {2458, "Composter"},
            {2475, "Fertilizer"},
            {2646, "Algae Dispenser"},
            {2657, "Nuts and Seeds"},
            {2715, "Explosive Ammo"},
            {4006, "Combat Stimulant"}
        };

        public static readonly Dictionary<int, string> DefaultConditionIDs = new()
        {
            {0, "Normal"},
            {6, "Pain"},
            {186, "Low Oxygen"},
            {1052, "Starving"},
            {1053, "Hungry"},
            {1127, "Lonely"},
            {1550, "Fresh Air"},
            {1570, "Comfortable"},
            {2055, "Medical Treatment"},
            {3309, "Very Happy"},
            {3314, "Well Rested"},
            {3329, "Full Stomach"},
            {3335, "Feeling Unwell"},
            {3338, "Uncomfortable"},
            {3339, "Extremely Unhappy"}
        };

    }
}
