using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using SpaceHaven_Save_Editor.Data;
using SpaceHaven_Save_Editor.References;

namespace SpaceHaven_Save_Editor.FileHandling
{
    public static class FindCharacters
    {
        #region Read

        public static List<Character> ReadCharacters(XmlNode characterRootNode)
        {
            var characterNodes = characterRootNode.SelectNodes(".//c[@cid]");

            return characterNodes == null
                ? new List<Character>()
                : (from XmlNode characterNode in characterNodes select ReadCharacter(characterNode)).ToList();
        }

        private static Character ReadCharacter(XmlNode characterNode)
        {
            Character character = new()
            {
                CharacterXmlNode = characterNode,
                CharacterName = Utilities.GetAttributeValue(characterNode, NodeCollection.CharacterNameAttribute),
                FactionSide = Utilities.GetAttributeValue(characterNode, "side")
            };

            if (int.TryParse(Utilities.GetAttributeValue(characterNode, NodeCollection.CharacterEidAttribute),
                out var result))
                character.CharacterEntityId = result;
            else
                throw new Exception("Error at attempt to parsing character entity id.");

            var statsNode = characterNode.SelectSingleNode(".//props");
            var attributesNode = characterNode.SelectSingleNode(".//attr");
            var traitsNode = characterNode.SelectSingleNode(".//traits");
            var skillsNode = characterNode.SelectSingleNode(".//skills");
            var conditionsNode = characterNode.SelectSingleNode(".//conditions");

            if (statsNode == null || attributesNode == null || traitsNode == null || skillsNode == null || conditionsNode == null)
                throw new Exception("Error at attempt to find all of " + character.CharacterName + "'s nodes.");

            character.CharacterStats = ReadStats(statsNode);
            character.CharacterAttributes = ReadAttributes(attributesNode);
            character.CharacterSkills = ReadSkills(skillsNode);
            character.CharacterTraits = ReadTraits(traitsNode);
            character.CharacterConditions = ReadConditions(conditionsNode);

            return character;
        }

        private static ObservableCollection<DataProp> ReadStats(XmlNode xmlNode)
        {
            ObservableCollection<DataProp> characterStats = new();

            foreach (var characterStat in NodeCollection.CharacterStats)
            {
                var statNode = xmlNode.SelectSingleNode(".//" + characterStat + "[@v]");
                if (statNode == null) continue;
                if (int.TryParse(Utilities.GetAttributeValue(statNode, "v"), out var result))
                {
                    int.TryParse(Utilities.GetAttributeValue(statNode, "ltv"), out var maxN);
                    if (maxN <= 0) maxN = 100;
                    characterStats.Add(new DataProp
                    {
                        Name = characterStat,
                        Value = result,
                        MaxValue = maxN
                    });
                }
            }

            return characterStats;
        }

        private static ObservableCollection<DataProp> ReadAttributes(XmlNode attributesNode)
        {
            ObservableCollection<DataProp> characterAttributes = new();

            foreach (var (key, value) in IdCollection.DefaultAttributeIDs)
            {
                var attributeNode = attributesNode.SelectSingleNode(".//a[@id='" + key + "']");
                if (attributeNode == null) continue;
                if (int.TryParse(Utilities.GetAttributeValue(attributeNode, "points"), out var result))
                    characterAttributes.Add(new DataProp
                    {
                        Id = key,
                        Name = value,
                        Value = result
                    });
            }

            return characterAttributes;
        }

        private static ObservableCollection<DataProp> ReadTraits(XmlNode traitsNode)
        {
            ObservableCollection<DataProp> characterTraits = new();
            var traitNodes = traitsNode.SelectNodes(".//t[@id]");
            if (traitNodes != null)
            {
                foreach (XmlNode traitNode in traitNodes)
                {
                    if (int.TryParse(Utilities.GetAttributeValue(traitNode, "id"), out var id))
                    {
                        if (id == 0) continue;
                        IdCollection.DefaultTraitIDs.TryGetValue(id, out var name);
                        characterTraits.Add(new DataProp
                        {
                            Id = id,
                            Name = name ?? "Unknown (" + id + ")"
                        });
                    }
                }
            }
            return characterTraits;
        }

        private static ObservableCollection<DataProp> ReadConditions(XmlNode conditionsNode)
        {
            ObservableCollection<DataProp> characterConditions = new();
            var conditionNodes = conditionsNode.SelectNodes(".//c[@id]");
            if (conditionNodes != null)
            {
                foreach (XmlNode conditionNode in conditionNodes)
                {
                    if (int.TryParse(Utilities.GetAttributeValue(conditionNode, "id"), out var id))
                    {
                        if (id == 0) continue;
                        int.TryParse(Utilities.GetAttributeValue(conditionNode, "level"), out var level);
                        IdCollection.DefaultConditionIDs.TryGetValue(id, out var name);
                        characterConditions.Add(new DataProp
                        {
                            Id = id,
                            Name = name ?? "Unknown (" + id + ")",
                            Value = level
                        });
                    }
                }
            }
            return characterConditions;
        }

        private static ObservableCollection<DataProp> ReadSkills(XmlNode skillsNode)
        {
            ObservableCollection<DataProp> characterSkills = new();

            foreach (var (key, value) in IdCollection.DefaultSkillIDs)
            {
                var attributeNode = skillsNode.SelectSingleNode(".//s[@sk='" + key + "']");
                if (attributeNode == null) continue;
                if (int.TryParse(Utilities.GetAttributeValue(attributeNode, "level"), out var result))
                {
                    int.TryParse(Utilities.GetAttributeValue(attributeNode, "mxn"), out var maxN);
                    characterSkills.Add(new DataProp
                    {
                        Id = key,
                        Name = value,
                        Value = result,
                        MaxValue = maxN
                    });
                }
            }

            return characterSkills;
        }

        #endregion

        #region Write

        public static void WriteCharacters(XmlNode? rootNode, IEnumerable<Character> characters)
        {
            if (rootNode == null) return;

            foreach (var character in characters)
            {
                var characterNode = rootNode.SelectSingleNode(".//c[@name='" + character.CharacterName + "']");
                if (characterNode == null && character.IsAClone)
                {
                    rootNode!.AppendChild(character.CharacterXmlNode);
                    characterNode = rootNode.SelectSingleNode(".//c[@name='" + character.CharacterName + "']");
                }
                else if (characterNode == null)
                {
                    continue;
                }

                if (character.FactionSide == "Player" && character.IsCrewman)
                {
                    characterNode.Attributes.RemoveNamedItem("oside");
                    characterNode.Attributes.RemoveNamedItem("owside");
                    
                    var sideAttr = characterNode.Attributes["side"];
                    if (sideAttr == null)
                    {
                        var attr = characterNode.OwnerDocument!.CreateAttribute("side");
                        attr.Value = "Player";
                        characterNode.Attributes.Append(attr);
                    }
                    else
                    {
                        sideAttr.Value = "Player";
                    }
                }
                else
                {
                    var sideAttr = characterNode.Attributes["side"];
                    if (sideAttr == null)
                    {
                        var attr = characterNode.OwnerDocument!.CreateAttribute("side");
                        attr.Value = character.FactionSide;
                        characterNode.Attributes.Append(attr);
                    }
                    else
                    {
                        sideAttr.Value = character.FactionSide;
                    }

                    if (!character.IsCrewman)
                    {
                        var osideAttr = characterNode.Attributes["oside"];
                        if (osideAttr == null)
                        {
                            var attr = characterNode.OwnerDocument!.CreateAttribute("oside");
                            attr.Value = "NotSet";
                            characterNode.Attributes.Append(attr);
                        }

                        var owsideAttr = characterNode.Attributes["owside"];
                        if (owsideAttr == null)
                        {
                            var attr = characterNode.OwnerDocument!.CreateAttribute("owside");
                            attr.Value = "Player";
                            characterNode.Attributes.Append(attr);
                        }
                    }
                }

                var statsNode = characterNode?.SelectSingleNode(".//props");
                var attributesNodes = characterNode?.SelectNodes(".//a[@points]");
                var traitNodesRoot = characterNode?.SelectSingleNode(".//traits");
                var skillsNodes = characterNode?.SelectNodes(".//s[@sk]");
                var conditionsNodeRoot = characterNode?.SelectSingleNode(".//conditions");

                if (statsNode == null || attributesNodes == null || traitNodesRoot == null || skillsNodes == null || conditionsNodeRoot == null)
                    throw new Exception("Error at attempt to find all of " + character.CharacterName + " nodes.");

                WriteStats(statsNode, character);
                WriteAttributes(attributesNodes, character);
                WriteTraits(traitNodesRoot, character);
                WriteSkills(skillsNodes, character);
                WriteConditions(conditionsNodeRoot, character);
            }
        }

        private static void WriteStats(XmlNode statNodes, Character character)
        {
            foreach (var characterStat in character.CharacterStats)
            {
                var statNode = statNodes.SelectSingleNode(".//" + characterStat.Name + "[@v]");
                if (statNode == null) continue;
                statNode.Attributes!["v"]!.Value = characterStat.Value.ToString();
                
                var ltvAttr = statNode.Attributes!["ltv"];
                if (ltvAttr == null && characterStat.MaxValue > 0)
                {
                    var attr = statNode.OwnerDocument!.CreateAttribute("ltv");
                    attr.Value = characterStat.MaxValue.ToString();
                    statNode.Attributes.Append(attr);
                }
                else if (ltvAttr != null)
                {
                    ltvAttr.Value = characterStat.MaxValue.ToString();
                }
            }
        }

        private static void WriteAttributes(IEnumerable attributeNodes, Character character)
        {
            foreach (XmlNode attributeNode in attributeNodes)
            {
                int.TryParse(attributeNode.Attributes!["id"]!.Value, out var attributeId);
                var attribute = character.CharacterAttributes.FirstOrDefault(s => s.Id == attributeId);
                if (attribute == null) continue;
                attributeNode.Attributes["points"]!.Value = attribute.Value.ToString();
            }
        }

        private static void WriteTraits(XmlNode traitNodesRoot, Character character)
        {
            traitNodesRoot.RemoveAll();
            foreach (var characterTrait in character.CharacterTraits)
            {
                var itemTemplate = traitNodesRoot.OwnerDocument!.CreateElement("t");
                itemTemplate.SetAttribute("id", characterTrait.Id.ToString());
                traitNodesRoot.AppendChild(itemTemplate);
            }
        }

        private static void WriteSkills(IEnumerable skillNodes, Character character)
        {
            foreach (XmlNode skillNode in skillNodes)
            {
                int.TryParse(skillNode.Attributes!["sk"]!.Value, out var skillId);
                var skill = character.CharacterSkills.FirstOrDefault(s => s.Id == skillId);
                if (skill == null) continue;
                skillNode.Attributes["level"]!.Value = skill.Value.ToString();

                var mxnAttr = skillNode.Attributes!["mxn"];
                if (mxnAttr == null && skill.MaxValue > 0)
                {
                    var attr = skillNode.OwnerDocument!.CreateAttribute("mxn");
                    attr.Value = skill.MaxValue.ToString();
                    skillNode.Attributes.Append(attr);
                }
                else if (mxnAttr != null)
                {
                    mxnAttr.Value = skill.MaxValue.ToString();
                }
            }
        }

        private static void WriteConditions(XmlNode conditionsNodesRoot, Character character)
        {
            var existingNodes = conditionsNodesRoot.SelectNodes(".//c[@id]");
            var nodesToRemove = new List<XmlNode>();
            if (existingNodes != null)
            {
                foreach (XmlNode existingNode in existingNodes)
                {
                    int.TryParse(Utilities.GetAttributeValue(existingNode, "id"), out var id);
                    if (id == 0) continue;
                    var condition = character.CharacterConditions.FirstOrDefault(c => c.Id == id);
                    if (condition == null)
                        nodesToRemove.Add(existingNode);
                    else
                        existingNode.Attributes!["level"]!.Value = condition.Value.ToString();
                }
            }

            foreach (var node in nodesToRemove) conditionsNodesRoot.RemoveChild(node);

            foreach (var characterCondition in character.CharacterConditions)
            {
                var existingNode = conditionsNodesRoot.SelectSingleNode(".//c[@id='" + characterCondition.Id + "']");
                if (existingNode != null) continue;

                var itemTemplate = conditionsNodesRoot.OwnerDocument!.CreateElement("c");
                itemTemplate.SetAttribute("id", characterCondition.Id.ToString());
                itemTemplate.SetAttribute("level", characterCondition.Value.ToString());
                var moodNode = conditionsNodesRoot.OwnerDocument.CreateElement("mood");
                var mNode = conditionsNodesRoot.OwnerDocument.CreateElement("m");
                mNode.SetAttribute("ac", "0");
                moodNode.AppendChild(mNode);
                itemTemplate.AppendChild(moodNode);
                conditionsNodesRoot.AppendChild(itemTemplate);
            }
        }

        #endregion
    }
}