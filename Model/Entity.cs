
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Game.Utils;

namespace Game.Model
{
    public abstract class Entity
    {
        public string Name {get;} = "Unknown player";
        public int MaxHealth {get;} = 100;
        public int Health {get; private set;} = 100;
        public int Strength {get;} = 0;
        public int Dexterity {get;} = 0;
        public int Endurance {get;} = 0;
        public int Intelligence {get;} = 0;

        public ReadOnlyCollection<IOffensive> Offensives {get;}
        private readonly List<IOffensive> _offensives;
        public ReadOnlyCollection<Multiplier> Multipliers {get;}
        private readonly List<Multiplier> _multipliers;


        public Entity(string name, int maxHealth)
        {
            if(NameIsValid(name) == false)
                throw new ArgumentException($"{name} n'est pas un nom valide.");

            this.Name = name;
            this.Health = maxHealth;
            this.MaxHealth = maxHealth;
            _multipliers = new List<Multiplier>();
            Multipliers = _multipliers.AsReadOnly();

            _offensives = new List<IOffensive>();
            Offensives = _offensives.AsReadOnly();
        }

        public Entity(string name, int maxHealth, int strength) : this(name, maxHealth)
        {
            this.Strength = strength;
        }

        public Entity(string name, int maxHealth, int strength, int dexterity, int endurance, int intelligence) : this(name, maxHealth, strength)
        {
            Dexterity = dexterity;
            Endurance = endurance;
            Intelligence = intelligence;
        }

        public int Attack(Entity target, IOffensive offense) 
        {
            var calculatedDamages = CalculateDamages(target, offense);
            target.TakeDamages(calculatedDamages);
            return calculatedDamages;
        }

        public void TakeDamages(int calculatedDamages)
        {
            Health -= calculatedDamages;
        }

        public void Die() => Health = 0;

        public bool Defeated => Health <= 0;

        public void Equip(IOffensive offensive)
        {
            _offensives.Add(offensive);
        }

        public void AddMultiplier(Multiplier multiplier)
        {
            _multipliers.Add(multiplier);
        }

        public static bool NameIsValid(string input)
        {
            var regexMatch = new Regex("([a-zA-Z]+\\s?)+");
            return regexMatch.Matches(input).Count == 1;
        }

        private int CalculateDamages(Entity target, IOffensive offensive)
        {
            var damages = offensive.RandomDamages();
            damages += MultiplierDamages(target, offensive, damages);
            damages += CharacteristicDamages(offensive, damages);
            return damages;
        }

        private int CharacteristicDamages(IOffensive offensive, int damages)
        {
            double result = damages;
            switch(offensive.Effect)
            {
                case EffectType.Blunt:
                    result = result + Strength * 1.7 + Dexterity * 1.2;
                    break;
                case EffectType.Sharp:
                    result = result + Strength * 1.3 + Dexterity * 1.3 + Intelligence * 1.1;
                    break;
                case EffectType.Thrust:
                    result = result + Strength * 1.1 + Dexterity * 1.3 + Intelligence * 1.3;
                    break;
                case EffectType.Fire:
                    result = result + Strength * 1.2 + Intelligence * 1.7;
                    break;
                case EffectType.Ice:
                    result = result + Strength * 1.1 + Dexterity * 1.1 + Intelligence * 1.5;
                    break;
                case EffectType.Wind:
                    result = result + Dexterity * 1.2 + Intelligence * 1.7;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown {offensive.Effect}");
            }
            return (int)result;
        }

        private int MultiplierDamages(Entity target, IOffensive offense, int damages)
        {
            var effectMultiplier = target.Multipliers.Where(m => m.EffectType == offense.Effect).Select(m => m.Value).Sum();
            return damages * effectMultiplier / 100;
        }

        public override string ToString() => this.Name;
    }
}