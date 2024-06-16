using System.Text.RegularExpressions;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Client.MirScenes.Dialogs
{
    public sealed class NewCharacterDialog : MirImageControl
    {
        private static readonly Regex Reg = new Regex(@"^[A-Za-z0-9]|[\u4e00-\u9fa5]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");
        private static readonly int[] ClassBodyShapes = new int[3]
        {
            2,
            4,
            5
        };

        private static readonly int[] ClassWeaponShapes = new int[3]
        {
            2,
            4,
            5
        };

        public MirImageControl CharacterDisplay, HeadDisplay, WeaponDisplay;

        public MirTextBox NameTextBox;
        public MirLabel Description;
        public MirButton HairLeft, HairRight, GenderLeft, GenderRight, ClassLeft, ClassRight;

        public MirClass Class;
        public MirGender Gender;
        public byte Hair;

        #region Descriptions
        public const string DarkWarriorDescription =
            "<$Gender> Dark Warrior\r\nDark Warriors are masters of the shadowy arts, excelling in combat through their exceptional physical prowess and demonic abilities." +
            " Unlike other classes, they harness the dark energies to perform devastating attacks. They can unleash powerful Dark strikes that overwhelm their foes." +
            " Dark Warriors must carefully manage their demon force to maximize their destructive potential, striking fear into the hearts of their enemies with each move.";

        public const string LightWarriorDescription =
            "<$Gender> Light Warrior\r\nLight Warriors are masters of agility and precision, with a deep understanding of the balance between light and shadow. They prefer to" +
            " strike with speed and accuracy, using their skills to outmaneuver opponents rather than overpowering them. Light Warriors can harness the energy of light" +
            " to perform swift attacks and moderatly resist magic. This class excels in hit-and-run tactics, making them formidable foes on the battlefield.";

        public const string PyromancerDescription =
            "<$Gender> Pyromancer\r\nPyromancers are masters of fire magic, harnessing the raw energy of flames to incinerate their foes. With a deep understanding of the" +
            " destructive and purifying aspects of fire, they wield spells that can erupt from the ground or engulf their targets in searing heat. While their attacks are" +
            " immensely powerful, Pyromancers must remain vigilant, positioning themselves strategically to unleash their fiery wrath while avoiding retaliation.";

        public const string ElectromancerDescription =
            "<$Gender> Electromancer\r\nElectromancers are masters of the elemental force of lightning, channeling the raw energy of storms to smite their foes. With a" +
            " deep understanding of electrical magic, they can unleash devastating bolts and protective barriers. Electromancers skills allow them to control the" +
            " battlefield with electrifying precision, making them formidable adversaries from any range.";

        public const string WaterSageDescription =
            "<$Gender> Water Sage\r\nWater Sages are masters of hydro-sorcery, drawing on the mystical properties of water to both nurture and devastate. They prefer" +
            " to outmaneuver their opponents, wielding spells that can heal allies or freeze enemies in their tracks. With a deep understanding of the ebb and flow of" +
            " battle, Water Sages can create protective barriers and even resurrect fallen comrades, making them formidable supporters in any skirmish.";

        public const string EarthSageDescription =
            "<$Gender> Earth Sage\r\nEarth Sages are the guardians of terrestrial mysteries, mastering the elements and the land itself. With a deep understanding of" +
            " geomancy and natural magic, they prefer to outwit their adversaries rather than confront them head-on. Earth Sages can manipulate the terrain to their" +
            " advantage, offering a harmonious blend of offensive and defensive capabilities.";

        #endregion

        private SelectScene ParentScene => (SelectScene)Parent;

        public NewCharacterDialog()
        {
            Index = 136;
            Library = Libraries.Prguse;
            Location = new Point(Settings.ScreenWidth - 230, 200);

            NameTextBox = new MirTextBox
            {
                Location = new Point(31, 146),
                Parent = this,
                Size = new Size(100, 16),
                MaxLength = Globals.MaxCharacterNameLength
            };
            NameTextBox.TextBox.KeyPress += TextBox_KeyPress;
            NameTextBox.TextBox.TextChanged += CharacterNameTextBox_TextChanged;
            NameTextBox.SetFocus();

            CharacterDisplay = new MirImageControl
            {
                Library = Libraries.Body,
                Location = new Point(57, 115),
                Parent = this,
                NotControl = true,
                UseOffSet = true,
            };
            HeadDisplay = new MirImageControl
            {
                Library = Libraries.Head,
                Location = new Point(57, 115),
                Parent = this,
                NotControl = true,
                UseOffSet = true,
            };
            WeaponDisplay = new MirImageControl
            {
                Library = Libraries.Weapon,
                Location = new Point(57, 115),
                Parent = this,
                NotControl = true,
                UseOffSet = true,
            };

            Description = new MirLabel
            {
                Border = true,
                Location = new Point(-200, 150),
                Parent = this,
                Size = new Size(178, 170),
                Text = DarkWarriorDescription,
            };

            HairLeft = new MirButton
            {
                HoverIndex = 32,
                Index = 33,
                Library = Libraries.Prguse,
                Location = new Point(42, 46),
                Parent = this,
                PressedIndex = 34
            };
            HairLeft.Click += (o, e) =>
            {
                Hair--;
                UpdateInterface();
            };

            HairRight = new MirButton
            {
                HoverIndex = 35,
                Index = 36,
                Library = Libraries.Prguse,
                Location = new Point(104, 46),
                Parent = this,
                PressedIndex = 37
            };
            HairRight.Click += (o, e) =>
            {
                Hair++;
                UpdateInterface();
            };

            ClassLeft = new MirButton
            {
                HoverIndex = 32,
                Index = 33,
                Library = Libraries.Prguse,
                Location = new Point(42, 76),
                Parent = this,
                PressedIndex = 34
            };
            ClassLeft.Click += (o, e) =>
            {
                Class--;
                UpdateInterface();
            };

            ClassRight = new MirButton
            {
                HoverIndex = 35,
                Index = 36,
                Library = Libraries.Prguse,
                Location = new Point(104, 76),
                Parent = this,
                PressedIndex = 37
            };
            ClassRight.Click += (o, e) =>
            {
                Class++;
                UpdateInterface();
            };

            GenderLeft = new MirButton
            {
                HoverIndex = 32,
                Index = 33,
                Library = Libraries.Prguse,
                Location = new Point(42, 106),
                Parent = this,
                PressedIndex = 34
            };
            GenderLeft.Click += (o, e) =>
            {
                Gender--;
                UpdateInterface();
            };

            GenderRight = new MirButton
            {
                HoverIndex = 35,
                Index = 36,
                Library = Libraries.Prguse,
                Location = new Point(104, 106),
                Parent = this,
                PressedIndex = 37
            };
            GenderRight.Click += (o, e) =>
            {
                Gender++;
                UpdateInterface();
            };
        }

        public override void Show()
        {
            base.Show();

            Class = MirClass.DarkWarrior;
            Gender = MirGender.Male;
            Hair = 1;
            NameTextBox.Text = string.Empty;

            UpdateInterface();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == null) return;
            if (e.KeyChar != (char)Keys.Enter) return;
            e.Handled = true;

            if (ParentScene.CreateCharacterButton.Enabled)
                ParentScene.CreateCharacterButton.InvokeMouseClick(null);
        }
        private void CharacterNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                ParentScene.CreateCharacterButton.Enabled = false;
                NameTextBox.Border = false;
            }
            else if (!Reg.IsMatch(NameTextBox.Text))
            {
                ParentScene.CreateCharacterButton.Enabled = false;
                NameTextBox.Border = true;
                NameTextBox.BorderColour = Color.Red;
            }
            else
            {
                ParentScene.CreateCharacterButton.Enabled = true;
                NameTextBox.Border = true;
                NameTextBox.BorderColour = Color.Green;
            }
        }

        public event EventHandler OnCreateCharacter;
        public void CreateCharacter()
        {
            ParentScene.CreateCharacterButton.Enabled = false;

            if (OnCreateCharacter != null)
                OnCreateCharacter.Invoke(this, EventArgs.Empty);        
        }

        private void UpdateInterface()
        {
            //CharacterDisplay.Index = 74 + (ClassBodyShapes[(int)Class] +1 * 240) + (120 * (int)Gender);
            HeadDisplay.Index = 74 + (Hair * 240) + (120 * (int)Gender);
            //WeaponDisplay.Index = 74 + (ClassWeaponShapes[(int)Class] * 240) + (120 * (int)Gender);

            switch (Class)
            {
                case MirClass.DarkWarrior:
                    CharacterDisplay.Index = 554 + (120 * (int)Gender);
                    WeaponDisplay.Index = 554;
                    Description.Text = DarkWarriorDescription;
                    break;
                case MirClass.LightWarrior:
                    CharacterDisplay.Index = 1274 + (120 * (int)Gender);
                    WeaponDisplay.Index = 914;
                    Description.Text = LightWarriorDescription;
                    break;
                case MirClass.Pyromancer:
                    CharacterDisplay.Index = 1034 + (120 * (int)Gender);
                    WeaponDisplay.Index = 314;
                    Description.Text = PyromancerDescription;
                    break;
                case MirClass.Electromancer:
                    CharacterDisplay.Index = 794 + (120 * (int)Gender);
                    WeaponDisplay.Index = 314;
                    Description.Text = ElectromancerDescription;
                    break;
                case MirClass.WaterSage:
                    CharacterDisplay.Index = 2234 + (120 * (int)Gender);
                    WeaponDisplay.Index = 1154;
                    Description.Text = WaterSageDescription;
                    break;
                case MirClass.EarthSage:
                    CharacterDisplay.Index = 1994 + (120 * (int)Gender);
                    WeaponDisplay.Index = 1154;
                    Description.Text = EarthSageDescription;
                    break;
            }
            Description.Text = Description.Text.Replace("<$Gender>", Gender.ToString());

            HairLeft.Visible = Hair > 1;
            HairRight.Visible = Hair < 21;
            ClassLeft.Visible = Class > MirClass.DarkWarrior;
            ClassRight.Visible = Class < MirClass.EarthSage;
            GenderLeft.Visible = Gender > MirGender.Male;
            GenderRight.Visible = Gender < MirGender.Female;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            OnCreateCharacter = null;
        }
    }
}
