using System.Collections.ObjectModel;
using System.Xml;
using ReactiveUI;
using SpaceHaven_Save_Editor.References;

namespace SpaceHaven_Save_Editor.Data
{
    public class Character : ReactiveObject
    {
        private bool _isCrewman;
        private string _factionSide = "";

        public Character()
        {
            CharacterName = "";
            FactionSide = "";
            CharacterStats = new ObservableCollection<DataProp>();
            CharacterAttributes = new ObservableCollection<DataProp>();
            CharacterSkills = new ObservableCollection<DataProp>();
            CharacterTraits = new ObservableCollection<DataProp>();
            CharacterConditions = new ObservableCollection<DataProp>();
        }

        public XmlNode? CharacterXmlNode { get; set; }
        public int CharacterEntityId { get; set; }
        public string CharacterName { get; set; }

        public string FactionSide
        {
            get => _factionSide;
            set
            {
                if (_factionSide != value)
                {
                    this.RaiseAndSetIfChanged(ref _factionSide, value);
                    IsCrewman = (value == "Player");
                    this.RaisePropertyChanged(nameof(CharacterNameToShow));
                }
            }
        }

        public bool IsCrewman
        {
            get => _isCrewman;
            set
            {
                if (_isCrewman != value)
                {
                    this.RaiseAndSetIfChanged(ref _isCrewman, value);
                    if (value)
                    {
                        FactionSide = "Player";
                    }
                    else if (FactionSide == "Player")
                    {
                        FactionSide = "NotSet";
                    }
                    this.RaisePropertyChanged(nameof(CharacterNameToShow));
                }
            }
        }

        public bool IsAClone { get; private init; }
        public ObservableCollection<DataProp> CharacterStats { get; set; }
        public ObservableCollection<DataProp> CharacterAttributes { get; set; }
        public ObservableCollection<DataProp> CharacterSkills { get; set; }
        public ObservableCollection<DataProp> CharacterTraits { get; set; }
        public ObservableCollection<DataProp> CharacterConditions { get; set; }

        public string CharacterNameToShow =>
            CharacterName + " [" + (IsCrewman ? "Crewman" : "Prisoner/Refugee") +
            (IsAClone ? ", Clone]" : "]");

        public Character CloneCharacter(string newName, int entId)
        {
            if (CharacterXmlNode == null) return new Character();
            var newXmlNode = CharacterXmlNode.CloneNode(true);

            newXmlNode.Attributes![NodeCollection.CharacterNameAttribute]!.Value = newName;
            newXmlNode.Attributes![NodeCollection.CharacterEidAttribute]!.Value = entId.ToString();

            return new Character
            {
                CharacterXmlNode = newXmlNode,
                FactionSide = FactionSide,
                IsCrewman = IsCrewman,
                IsAClone = true,
                CharacterEntityId = entId,
                CharacterName = newName,
                CharacterStats = CharacterStats,
                CharacterAttributes = CharacterAttributes,
                CharacterSkills = CharacterSkills,
                CharacterTraits = CharacterTraits,
                CharacterConditions = CharacterConditions
            };
        }
    }
}