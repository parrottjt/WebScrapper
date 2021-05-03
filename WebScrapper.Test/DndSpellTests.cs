using NUnit.Framework;
using DND.Data;

namespace WebScrapper.Test
{
    public class DndSpellTests
    {
        Spell spell;
        [SetUp]
        public void Setup()
        {
            spell = new Spell();
        }

        [Test]
        public void Create_EmptySpell()
        {
            Assert.NotNull(spell);
        }

        [Test]
        public void Add_SpellLevel()
        {
            spell = new Spell();
            Assert.AreEqual(0, spell.Level);

            spell = new Spell(level: "9th");
            Assert.AreEqual(9, spell.Level);
        }

        [Test]
        public void Add_SpellDescription()
        {
            spell = new Spell(level: "1st", description: "this is a spell", damageType: 0);
            Assert.IsNotEmpty(spell.Description);
        }

        [Test]
        public void Add_SpellDamageType()
        {
            spell = new Spell("","1st", "This is a spell", "", DamageType.Piercing);
            Assert.AreEqual(DamageType.Piercing, spell.DamageType);
        }

        [Test]
        public void Check_SpellAccessorName_IsCorrect()
        {
            spell = new Spell("Abi-Dalzim's Horrid Wilting");
            Assert.AreEqual("abi-dalzims-horrid-wilting", spell.AccessorName);
        }

        [Test]
        public void Check_DefaultDamageType_IsBludgeoning()
        {
            Assert.AreEqual(DamageType.Slashing, spell.DamageType);
        }

        [Test]
        public void Check_DefaultSchoolOfMagic_IsAbjuration()
        {
            Assert.AreEqual(SchoolOfMagic.Abjuration, spell.SchoolOfMagic);
        }

        [Test]
        public void Check_OnCreation_ClassListThatSpellIsApartOf_IsNotNull()
        {
            Assert.IsNotNull(spell.ListOfClassesSpellIsIn);
        }
    }
}
