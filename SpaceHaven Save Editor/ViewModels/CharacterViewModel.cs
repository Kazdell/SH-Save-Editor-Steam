using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using SpaceHaven_Save_Editor.Data;
using SpaceHaven_Save_Editor.References;
using SpaceHaven_Save_Editor.Views;

namespace SpaceHaven_Save_Editor.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        public CharacterViewModel(Character character)
        {
            Character = character;
            SaveAndExit = ReactiveCommand.Create(() => Character);
            MaxStats = ReactiveCommand.Create(() =>
            {
                foreach (var stat in Character.CharacterStats) stat.Value = stat.MaxValue > 0 ? stat.MaxValue : 100;
            });
            MaxSkills = ReactiveCommand.Create(() =>
            {
                foreach (var skill in Character.CharacterSkills) skill.Value = skill.MaxValue > 0 ? skill.MaxValue : 8;
            });
            MaxAttributes = ReactiveCommand.Create(() =>
            {
                foreach (var attr in Character.CharacterAttributes) attr.Value = 6;
            });
            SetToCrewman = ReactiveCommand.Create(() =>
            {
                Character.IsCrewman = !Character.IsCrewman;
            });
            SetFaction = ReactiveCommand.Create(() =>
            {
                Character.FactionSide = Character.FactionSide == "Player" ? "NotSet" : "Player";
            });
            ViewXmlNode = ReactiveCommand.Create(() =>
            {
                var xmlNodeViewer = new NodeViewerWindow(Character.CharacterName, Character.CharacterXmlNode!);
                xmlNodeViewer.Show();
            });
        }

        public CharacterViewModel()
        {
            Character = new Character();
            SaveAndExit = ReactiveCommand.Create(() => Character);
            MaxStats = ReactiveCommand.Create(() =>
            {
                foreach (var stat in Character.CharacterStats) stat.Value = stat.MaxValue > 0 ? stat.MaxValue : 100;
            });
            MaxSkills = ReactiveCommand.Create(() =>
            {
                foreach (var skill in Character.CharacterSkills) skill.Value = skill.MaxValue > 0 ? skill.MaxValue : 8;
            });
            MaxAttributes = ReactiveCommand.Create(() =>
            {
                foreach (var attr in Character.CharacterAttributes) attr.Value = 6;
            });
            SetToCrewman = ReactiveCommand.Create(() =>
            {
                Character.IsCrewman = !Character.IsCrewman;
            });
            SetFaction = ReactiveCommand.Create(() =>
            {
                Character.FactionSide = Character.FactionSide == "Player" ? "NotSet" : "Player";
            });
            ViewXmlNode = ReactiveCommand.Create(() =>
            {
                var xmlNodeViewer = new NodeViewerWindow(Character.CharacterName, Character.CharacterXmlNode!);
                xmlNodeViewer.Show();
            });
        }

        public ReactiveCommand<Unit, Character> SaveAndExit { get; }
        public ReactiveCommand<Unit, Unit> MaxStats { get; }
        public ReactiveCommand<Unit, Unit> MaxSkills { get; }
        public ReactiveCommand<Unit, Unit> MaxAttributes { get; }
        public ReactiveCommand<Unit, Unit> SetToCrewman { get; }
        public ReactiveCommand<Unit, Unit> SetFaction { get; }
        public ReactiveCommand<Unit, Unit> ViewXmlNode { get; }
        public Character Character { get; }

        public List<string> AllTraits { get; } = IdCollection.DefaultTraitIDs.Values.ToList();
        public string? SelectedCharacterTraitFromComboBox { get; set; }
        public int SelectedCharacterTrait { get; set; }
        
        public List<string> AllConditions { get; } = IdCollection.DefaultConditionIDs.Values.ToList();
        public string? SelectedConditionFromComboBox { get; set; }
        public int SelectedConditionLevel { get; set; } = 1;
        public int SelectedConditionIndex { get; set; }






        public void AddSelectedTrait()
        {
            if (SelectedCharacterTraitFromComboBox == null) return;

            var newTrait = IdCollection.DefaultTraitIDs.FirstOrDefault(x
                => x.Value == SelectedCharacterTraitFromComboBox);

            Character.CharacterTraits.Add(new DataProp
            {
                Id = newTrait.Key,
                Name = newTrait.Value
            });
        }

        public void RemoveSelectedTrait()
        {
            if (SelectedCharacterTrait == -1) return;
            Character.CharacterTraits.RemoveAt(SelectedCharacterTrait);
        }

        public void ClearAllTraits()
        {
            Character.CharacterTraits.Clear();
        }

        public void AddCondition()
        {
            if (SelectedConditionFromComboBox == null) return;
            var newCondition = IdCollection.DefaultConditionIDs.FirstOrDefault(x => x.Value == SelectedConditionFromComboBox);
            Character.CharacterConditions.Add(new DataProp
            {
                Id = newCondition.Key,
                Name = newCondition.Value,
                Value = SelectedConditionLevel
            });
        }

        public void RemoveCondition()
        {
            if (SelectedConditionIndex == -1) return;
            Character.CharacterConditions.RemoveAt(SelectedConditionIndex);
        }

        public void ClearAllConditions()
        {
            Character.CharacterConditions.Clear();
        }
    }
}
